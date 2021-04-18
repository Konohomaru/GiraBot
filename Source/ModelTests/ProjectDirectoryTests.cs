using Model;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace ModelTests
{
	public class ProjectDirectoryTests
	{
		private Mock<ICalendar> CalendarMock { get; }

		private Mock<IGitHubFacade> FacadeMock { get; }

		private ProjectsDirectory Directory { get; }

		public ProjectDirectoryTests()
		{
			CalendarMock = new Mock<ICalendar>();
			FacadeMock = new Mock<IGitHubFacade>();
			Directory = new(CalendarMock.Object, FacadeMock.Object);
		}

		[Fact]
		public void GetProjectSprints_DayAheadOfSprintBegining_OneSprint()
		{
			CalendarMock
				.Setup(calendar => calendar.GetCurrentUtcDateTime())
				.Returns(new DateTime(2020, 01, 02));

			FacadeMock
				.Setup(facade => facade.GetRepository(It.IsAny<long>(), It.IsAny<long>()))
				.Returns(new Repository(
					id: 0,
					name: "whipping-boy",
					fullName: "user/whipping-boy",
					owner: "user",
					createdAt: new DateTime(2020, 01, 01)));

			var actualSprint = Directory
				.GetProjectSprints(0)
				.Single();

			Assert.Equal(new DateTime(2020, 01, 01), actualSprint.BeginsAt);
		}

		[Fact]
		public void GetProjectSprints_SprintAndDayAheadOfSprintBegining_TwoSprints()
		{
			CalendarMock
				.Setup(calendar => calendar.GetCurrentUtcDateTime())
				.Returns(new DateTime(2020, 01, 09));

			FacadeMock
				.Setup(facade => facade.GetRepository(It.IsAny<long>(), It.IsAny<long>()))
				.Returns(new Repository(
					id: 0,
					name: "whipping-boy",
					fullName: "user/whipping-boy",
					owner: "user",
					createdAt: new DateTime(2020, 01, 01)));

			var actualSprints = Directory.GetProjectSprints(0);

			Assert.Equal(2, actualSprints.Count());
			Assert.Equal(new DateTime(2020, 01, 01), actualSprints.ElementAt(0).BeginsAt);
			Assert.Equal(new DateTime(2020, 01, 08), actualSprints.ElementAt(1).BeginsAt);
		}

		[Fact]
		public void GetProjectSprints_35DaysAheadOfSprintBegining_7Sprint()
		{
			CalendarMock
				.Setup(calendar => calendar.GetCurrentUtcDateTime())
				.Returns(new DateTime(2021, 02, 04));

			FacadeMock
				.Setup(facade => facade.GetRepository(It.IsAny<long>(), It.IsAny<long>()))
				.Returns(new Repository(
					id: 0,
					name: "whipping-boy",
					fullName: "user/whipping-boy",
					owner: "user",
					createdAt: new DateTime(2021, 01, 01)));

			var actualSprints = Directory.GetProjectSprints(0);

			Assert.Equal(5, actualSprints.Count());
			Assert.Equal(new DateTime(2021, 01, 01), actualSprints.ElementAt(0).BeginsAt);
			Assert.Equal(new DateTime(2021, 01, 08), actualSprints.ElementAt(1).BeginsAt);
			Assert.Equal(new DateTime(2021, 01, 15), actualSprints.ElementAt(2).BeginsAt);
			Assert.Equal(new DateTime(2021, 01, 22), actualSprints.ElementAt(3).BeginsAt);
			Assert.Equal(new DateTime(2021, 01, 29), actualSprints.ElementAt(4).BeginsAt);
		}
	}
}
