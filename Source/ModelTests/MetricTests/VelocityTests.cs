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

		private Swimlane[] Swimlanes { get; }

		public VelocityTests()
		{
			CalendarMock = new();
			RepositoryMock = new();
			Velocity = new(CalendarMock.Object, RepositoryMock.Object);

			Swimlanes = new[] { 
				new Swimlane(0, "Documents", "docs"),
				new Swimlane(1, "Tech Debts", "tech debts"),
				new Swimlane(2, "Bug Fixes", "bugs")
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
						swimlanes: Swimlanes,
						allowedProjectIds: null)));

			RepositoryMock
				.Setup(repository => repository.GetProjectSprints(1))
				.Returns(new[] { new Sprint(0, new(), 7) });
		}

		[Fact]
		public void ThreeSwimlanesForOneDayIfSprintHasOneDay()
		{
			RepositoryMock
				.Setup(repository => repository.GetProjectCards(IsAny<int>()))
				.Returns(Array.Empty<Card>());

			var metric = Velocity.GetMetric(1).Single();

			Equal(new(), metric.Day);
			Equal(3, metric.ClosedCards.Count);
			Equal(0, metric.ClosedCards[Swimlanes[0]]);
			Equal(0, metric.ClosedCards[Swimlanes[1]]);
			Equal(0, metric.ClosedCards[Swimlanes[2]]);
		}

		[Fact]
		public void NoClosedCardsIfCardIsOpen()
		{
			RepositoryMock
				.Setup(repository => repository.GetProjectCards(IsAny<int>()))
				.Returns(new[] {
					new CardBuilder()
						.ClosedAt(new DateTime().AddDays(1))
						.Build()
				});

			var metric = Velocity.GetMetric(1).Single();

			Equal(new(), metric.Day);
			Equal(3, metric.ClosedCards.Count);
			Equal(0, metric.ClosedCards[Swimlanes[0]]);
			Equal(0, metric.ClosedCards[Swimlanes[1]]);
			Equal(0, metric.ClosedCards[Swimlanes[2]]);
		}

		[Fact]
		public void OneClosedCardBySwimlaneIfCardIsClosed()
		{
			RepositoryMock
				.Setup(repository => repository.GetProjectCards(IsAny<int>()))
				.Returns(new[] {
					new CardBuilder()
						.ClosedAt(new DateTime())
						.WithLabel(Swimlanes[0].MappedAlias)
						.Build()
				});

			var metric = Velocity.GetMetric(1).Single();

			Equal(new(), metric.Day);
			Equal(3, metric.ClosedCards.Count);
			Equal(1, metric.ClosedCards[Swimlanes[0]]);
			Equal(0, metric.ClosedCards[Swimlanes[1]]);
			Equal(0, metric.ClosedCards[Swimlanes[2]]);
		}
	}
}
