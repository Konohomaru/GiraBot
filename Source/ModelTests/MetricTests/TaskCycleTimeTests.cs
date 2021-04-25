using Model;
using Moq;
using System;
using System.Linq;
using Xunit;

using static Moq.It;
using static Xunit.Assert;

namespace ModelTests
{
	public class TaskCycleTimeTests
	{
		private Mock<IProjectsRepository> RepositoryMock { get; }

		private TaskCycleTime TaskCycleTime { get; }

		public TaskCycleTimeTests()
		{
			RepositoryMock = new();
			TaskCycleTime = new(RepositoryMock.Object);
		}

		[Fact]
		public void OneOpenTaskIfOneOpenTaskExists()
		{
			RepositoryMock
				.Setup(repository => repository.GetProjectTasks(IsAny<int>()))
				.Returns(new[] {
					new GrTaskBuilder()
						.Build()
				});

			var metric = TaskCycleTime.GetMetric(0).Single();

			Equal(new(), metric.CreatedAt);
		}
	}
}
