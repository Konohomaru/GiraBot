using Model;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace ModelTests
{
	public class BurndownTests
	{
		private Mock<ICalendar> CalendarMock { get; }

		private Mock<IProjectsRepository> DirectoryMock { get; }

		private Burndown Burndown { get; }

		public BurndownTests()
		{
			CalendarMock = new();
			DirectoryMock = new();
			Burndown = new(CalendarMock.Object, DirectoryMock.Object);
		}

		[Fact]
		public void GetMetric_OneOpenedTask_OneRemainedTask()
		{
			CalendarMock
				.Setup(calendar => calendar.GetCurrentUtcDateTime())
				.Returns(new DateTime(2021, 01, 01));

			DirectoryMock
				.Setup(directory => directory.GetProjectSprints(It.IsAny<int>()))
				.Returns(new[] { new Sprint(0, new(2021, 01, 01), 7) });

			DirectoryMock
				.Setup(directory => directory.GetProjectTasks(It.IsAny<int>()))
				.Returns(new[] { new GrTask(0, new(2021, 01, 01), null, "task", null) });

			var actualMetric = Burndown.GetMetric(0).Single();

			Assert.Equal(1, actualMetric.RemainingTasks);
		}

		[Fact]
		public void GetMetric_TwoOpenedTasks_TwoRemainingTasks()
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
					new GrTask(0, new(2021, 01, 01), null, "task", null),
					new GrTask(0, new(2021, 01, 01), null, "task 2", null),
				});

			var actualMetric = Burndown.GetMetric(0).Single();

			Assert.Equal(2, actualMetric.RemainingTasks);
		}

		[Fact]
		public void GetMetric_OpenedAndClosedTasks_OneRemainingTask()
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
					new GrTask(0, new(2021, 01, 01), null, "task", null),
					new GrTask(0, new(2021, 01, 01), new(2021, 01, 01), "task 2", null),
				});

			var actualMetric = Burndown.GetMetric(0).Single();

			Assert.Equal(1, actualMetric.RemainingTasks);
		}

		[Fact]
		public void GetMetric_OpenedAndClosedTasksInDefferentDays_OneRemainingTaskInEachDay()
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
					new GrTask(0, new(2021, 01, 01), new(2021, 01, 02), "task", null),
					new GrTask(0, new(2021, 01, 02), new(2021, 01, 03), "task 2", null),
				});

			var actualMetric = Burndown.GetMetric(0);

			Assert.Equal(2, actualMetric.Count);
			Assert.Equal(new(2021, 01, 01), actualMetric.ElementAt(0).Day);
			Assert.Equal(1, actualMetric.ElementAt(0).RemainingTasks);
			Assert.Equal(new(2021, 01, 02), actualMetric.ElementAt(1).Day);
			Assert.Equal(1, actualMetric.ElementAt(1).RemainingTasks);
		}

		[Fact]
		public void GetMetric_TwoOpenedTasks_OneAndTwoRemainingTasks()
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
					new GrTask(0, new(2021, 01, 01), new(2021, 01, 03), "task", null),
					new GrTask(0, new(2021, 01, 02), new(2021, 01, 03), "task 2", null),
				});

			var actualMetric = Burndown.GetMetric(0);

			Assert.Equal(2, actualMetric.Count);
			Assert.Equal(new(2021, 01, 01), actualMetric.ElementAt(0).Day);
			Assert.Equal(1, actualMetric.ElementAt(0).RemainingTasks);
			Assert.Equal(new(2021, 01, 02), actualMetric.ElementAt(1).Day);
			Assert.Equal(2, actualMetric.ElementAt(1).RemainingTasks);
		}

		[Fact]
		public void GetMetrix_NoTasks_NoRemainingTasks()
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

			var actualMetric = Burndown.GetMetric(0).Single();

			Assert.Equal(new(2021, 01, 01), actualMetric.Day);
			Assert.Equal(0, actualMetric.RemainingTasks);
		}
	}
}
