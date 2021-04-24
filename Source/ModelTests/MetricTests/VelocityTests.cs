using Model;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace ModelTests
{
	public class VelocityTests
	{
		private Mock<ICalendar> CalendarMock { get; }

		private Mock<IProjectsRepository> DirectoryMock { get; }

		private Velocity Velocity { get; }

		private Lane[] Lanes { get; }

		public VelocityTests()
		{
			CalendarMock = new();
			DirectoryMock = new();
			Velocity = new(CalendarMock.Object, DirectoryMock.Object);

			Lanes = new[] { 
				new Lane(0, "Documents", "docs"),
				new Lane(1, "Tech Debts", "tech debts"),
				new Lane(2, "Bug Fixes", "bugs")
			};

			DirectoryMock
				.Setup(directory => directory.GetProject(0))
				.Returns(new Project(
					id: 0,
					name: "whipping-boy",
					beginSprintsAt: new(2021, 01, 01),
					gitHubSettings: new(
						installationId: 123456, 
						repositoryId: 123457,
						lanes: Lanes,
						allowedProjectIds: null)));
		}

		[Fact]
		public void GetMetric_OneClosedTaskInEachLane_LanesWithOneClosedTask()
		{
			CalendarMock
				.Setup(calendar => calendar.GetCurrentUtcDateTime())
				.Returns(new DateTime(2021, 01, 01));

			DirectoryMock
				.Setup(directory => directory.GetProjectSprints(It.IsAny<int>()))
				.Returns(new[] { new Sprint(0, new(2021, 01, 01), 7) });

			DirectoryMock
				.Setup(directory => directory.GetProjectTasks(It.IsAny<int>()))
				.Returns(new[] {
					new GrTask(0, new(2021, 01, 01), new(2021, 01, 01), "task", new[] { "docs" }),
					new GrTask(0, new(2021, 01, 01), new(2021, 01, 01), "task 2", new[] { "tech debts" }),
					new GrTask(0, new(2021, 01, 01), new(2021, 01, 01), "task 3", new[] { "bugs" }),
				});

			var actualMetric = Velocity.GetMetric(0).Single();

			Assert.Equal(3, actualMetric.ClosedTasksByLane.Count);
			Assert.Equal(1, actualMetric.ClosedTasksByLane[Lanes[0]]);
			Assert.Equal(1, actualMetric.ClosedTasksByLane[Lanes[1]]);
			Assert.Equal(1, actualMetric.ClosedTasksByLane[Lanes[2]]);
		}

		[Fact]
		public void GetMetric_OneClosedTaskByOneLane_OneClosedTaskByOneLaneAndZeroByOthers()
		{
			CalendarMock
				.Setup(calendar => calendar.GetCurrentUtcDateTime())
				.Returns(new DateTime(2021, 01, 01));

			DirectoryMock
				.Setup(directory => directory.GetProjectSprints(It.IsAny<int>()))
				.Returns(new[] { new Sprint(0, new(2021, 01, 01), 7) });

			DirectoryMock
				.Setup(directory => directory.GetProjectTasks(It.IsAny<int>()))
				.Returns(new[] {
					new GrTask(0, new(2021, 01, 01), new(2021, 01, 01), "task", new[] { "docs" })
				});

			var actualMetric = Velocity.GetMetric(0).Single();

			Assert.Equal(3, actualMetric.ClosedTasksByLane.Count);
			Assert.Equal(1, actualMetric.ClosedTasksByLane[Lanes[0]]);
			Assert.Equal(0, actualMetric.ClosedTasksByLane[Lanes[1]]);
			Assert.Equal(0, actualMetric.ClosedTasksByLane[Lanes[2]]);
		}

		[Fact]
		public void GetMetric_OneOpenedTaskByOneLane_ZeroClosedTasksInAllLanes()
		{
			CalendarMock
				.Setup(calendar => calendar.GetCurrentUtcDateTime())
				.Returns(new DateTime(2021, 01, 01));

			DirectoryMock
				.Setup(directory => directory.GetProjectSprints(It.IsAny<int>()))
				.Returns(new[] { new Sprint(0, new(2021, 01, 01), 7) });

			DirectoryMock
				.Setup(directory => directory.GetProjectTasks(It.IsAny<int>()))
				.Returns(Array.Empty<GrTask>());

			var actualMetric = Velocity.GetMetric(0).Single();

			Assert.Equal(3, actualMetric.ClosedTasksByLane.Count);
			Assert.Equal(0, actualMetric.ClosedTasksByLane[Lanes[0]]);
			Assert.Equal(0, actualMetric.ClosedTasksByLane[Lanes[1]]);
			Assert.Equal(0, actualMetric.ClosedTasksByLane[Lanes[2]]);
		}

		[Fact]
		public void GetMetric_TwoClosedTasksByEachLane_OneClosedTasksEachDayByEachLane()
		{
			CalendarMock
				.Setup(calendar => calendar.GetCurrentUtcDateTime())
				.Returns(new DateTime(2021, 01, 02));

			DirectoryMock
				.Setup(directory => directory.GetProjectSprints(It.IsAny<int>()))
				.Returns(new[] { new Sprint(0, new(2021, 01, 01), 7) });

			DirectoryMock
				.Setup(directory => directory.GetProjectTasks(It.IsAny<int>()))
				.Returns(new[] {
					new GrTask(0, new(2021, 01, 01), new(2021, 01, 01), "task", new[] { "docs" }),
					new GrTask(0, new(2021, 01, 01), new(2021, 01, 02), "task 2", new[] { "docs" }),
					new GrTask(0, new(2021, 01, 01), new(2021, 01, 01), "task 3", new[] { "tech debts" }),
					new GrTask(0, new(2021, 01, 01), new(2021, 01, 02), "task 4", new[] { "tech debts" }),
					new GrTask(0, new(2021, 01, 01), new(2021, 01, 01), "task 5", new[] { "bugs" }),
					new GrTask(0, new(2021, 01, 01), new(2021, 01, 02), "task 6", new[] { "bugs" }),
				});

			var actualMetric = Velocity.GetMetric(0);

			Assert.Equal(3, actualMetric.ElementAt(0).ClosedTasksByLane.Count);
			Assert.Equal(1, actualMetric.ElementAt(0).ClosedTasksByLane[Lanes[0]]);
			Assert.Equal(1, actualMetric.ElementAt(0).ClosedTasksByLane[Lanes[1]]);
			Assert.Equal(1, actualMetric.ElementAt(0).ClosedTasksByLane[Lanes[2]]);
			Assert.Equal(2, actualMetric.ElementAt(1).ClosedTasksByLane[Lanes[0]]);
			Assert.Equal(2, actualMetric.ElementAt(1).ClosedTasksByLane[Lanes[1]]);
			Assert.Equal(2, actualMetric.ElementAt(1).ClosedTasksByLane[Lanes[2]]);
		}
	}
}
