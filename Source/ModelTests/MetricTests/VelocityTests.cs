using Model;
using Moq;
using System;
using System.Linq;
using Xunit;

using static Moq.It;
using static Xunit.Assert;


namespace ModelTests
{
	public class VelocityTests
	{
		private Mock<ICalendar> CalendarMock { get; }

		private Mock<IProjectsRepository> RepositoryMock { get; }

		private Velocity Velocity { get; }

		private Lane[] Lanes { get; }

		public VelocityTests()
		{
			CalendarMock = new();
			RepositoryMock = new();
			Velocity = new(CalendarMock.Object, RepositoryMock.Object);

			Lanes = new[] { 
				new Lane(0, "Documents", "docs"),
				new Lane(1, "Tech Debts", "tech debts"),
				new Lane(2, "Bug Fixes", "bugs")
			};

			RepositoryMock
				.Setup(repository => repository.GetProject(1))
				.Returns(new Project(
					id: 0,
					name: "whipping-boy",
					beginSprintsAt: new(2021, 01, 01),
					gitHubSettings: new(
						installationId: 123456, 
						repositoryId: 123457,
						lanes: Lanes,
						allowedProjectIds: null)));

			RepositoryMock
				.Setup(repository => repository.GetProjectSprints(1))
				.Returns(new[] { new Sprint(0, new(), 7) });
		}

		[Fact]
		public void ThreeLanesForOneDayIfSprintHasOneDay()
		{
			RepositoryMock
				.Setup(repository => repository.GetProjectTasks(IsAny<int>()))
				.Returns(Array.Empty<GrTask>());

			var metric = Velocity.GetMetric(1).Single();

			Equal(new(), metric.Day);
			Equal(3, metric.ClosedTasksByLane.Count);
			Equal(0, metric.ClosedTasksByLane[Lanes[0]]);
			Equal(0, metric.ClosedTasksByLane[Lanes[1]]);
			Equal(0, metric.ClosedTasksByLane[Lanes[2]]);
		}

		[Fact]
		public void NoClosedTasksIfTaskIsOpen()
		{
			RepositoryMock
				.Setup(repository => repository.GetProjectTasks(IsAny<int>()))
				.Returns(new[] {
					new GrTaskBuilder()
						.ClosedAt(new DateTime().AddDays(1))
						.Build()
				});

			var metric = Velocity.GetMetric(1).Single();

			Equal(new(), metric.Day);
			Equal(3, metric.ClosedTasksByLane.Count);
			Equal(0, metric.ClosedTasksByLane[Lanes[0]]);
			Equal(0, metric.ClosedTasksByLane[Lanes[1]]);
			Equal(0, metric.ClosedTasksByLane[Lanes[2]]);
		}

		[Fact]
		public void OneClosedTaskByLaneIfTaskIsClosed()
		{
			RepositoryMock
				.Setup(repository => repository.GetProjectTasks(IsAny<int>()))
				.Returns(new[] {
					new GrTaskBuilder()
						.ClosedAt(new DateTime())
						.WithLabel(Lanes[0].MappedAlias)
						.Build()
				});

			var metric = Velocity.GetMetric(1).Single();

			Equal(new(), metric.Day);
			Equal(3, metric.ClosedTasksByLane.Count);
			Equal(1, metric.ClosedTasksByLane[Lanes[0]]);
			Equal(0, metric.ClosedTasksByLane[Lanes[1]]);
			Equal(0, metric.ClosedTasksByLane[Lanes[2]]);
		}
	}
}
