using Model;
using Moq;
using System;
using System.Linq;
using Xunit;

using static Moq.It;
using static Xunit.Assert;

namespace ModelTests
{
	public class CardCycleTimeTests
	{
		private Mock<IProjectsRepository> RepositoryMock { get; }

		private CardCycleTime CardCycleTime { get; }

		public CardCycleTimeTests()
		{
			RepositoryMock = new();
			CardCycleTime = new(RepositoryMock.Object);
		}

		[Fact]
		public void OneOpenTaskIfOneOpenTaskExists()
		{
			RepositoryMock
				.Setup(repository => repository.GetProjectCards(IsAny<int>()))
				.Returns(new[] {
					new CardBuilder()
						.Build()
				});

			var metric = CardCycleTime.GetMetric(0).Single();

			Equal(new(), metric.CreatedAt);
		}
	}
}
