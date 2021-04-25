using Model;
using Moq;
using System;
using System.Linq;
using Xunit;

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
	}
}
