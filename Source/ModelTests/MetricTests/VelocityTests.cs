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
	}
}
