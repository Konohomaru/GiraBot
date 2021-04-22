using Model;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace ModelTests
{
	public class TaskCycleTimeTests
	{
		private Mock<IProjectsRepository> DirectoryMock { get; }

		private TaskCycleTime IssueCycleTime { get; }

		public TaskCycleTimeTests()
		{
			DirectoryMock = new();
			IssueCycleTime = new(DirectoryMock.Object);
		}

		[Fact]
		public void GetMetric_OneClosedTask_OneMetricNode()
		{
			var taskCreatedTime = new DateTime(2021, 01, 01);
			var taskClosedTime = new DateTime(2021, 01, 02);

			DirectoryMock
				.Setup(directory => directory.GetProjectTasks(It.IsAny<int>()))
				.Returns(new[] {
					new GiraTask(0, taskCreatedTime, taskClosedTime, "task", null)
				});

			var actualMetric = IssueCycleTime
				.GetMetric(0)
				.Single();

			Assert.Equal(taskCreatedTime, actualMetric.CreatedAt);
			Assert.Equal(taskClosedTime, actualMetric.ClosedAt);
		}

		[Fact]
		public void GetMetric_OneOpenTask_OnemetricNode()
		{
			var taskCreatedTime = new DateTime(2021, 01, 01);

			DirectoryMock
				.Setup(directory => directory.GetProjectTasks(It.IsAny<int>()))
				.Returns(new[] {
					new GiraTask(0, taskCreatedTime, null, "task", null)
				});

			var actualMetric = IssueCycleTime
				.GetMetric(0)
				.Single();

			Assert.Equal(taskCreatedTime, actualMetric.CreatedAt);
		}
	}
}
