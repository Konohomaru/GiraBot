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
		public void EmptyMetricIfNoCards()
		{
			RepositoryMock
				.Setup(mock => mock.GetProjectCards(IsAny<int>()))
				.Returns(Array.Empty<Card>());

			var actual = CardCycleTime.GetMetric(0);

			Equal(0, actual.Count);
		}

		[Fact]
		public void NullGroupIfNoClosedCards()
		{
			RepositoryMock
				.Setup(mock => mock.GetProjectCards(IsAny<int>()))
				.Returns(new[] {
					new CardBuilder()
						.CreatedAt(new(2021, 01, 01))
						.Build()
				});

			var actual = CardCycleTime.GetMetric(0).Single();

			Null(actual.Duration);
			Equal(1, actual.Cards.Count);
		}

		[Fact]
		public void OneDayDurationIfOneDayDurationCardExists()
		{
			RepositoryMock
				.Setup(mock => mock.GetProjectCards(IsAny<int>()))
				.Returns(new[] {
					new CardBuilder()
						.CreatedAt(new(2021, 01, 01))
						.ClosedAt(new(2021, 01, 01))
						.Build()
				});

			var actual = CardCycleTime.GetMetric(0).Single();

			Equal(1, actual.Duration);
			Equal(1, actual.Cards.Count);
		}

		[Fact]
		public void DifferentDurationsIfExistCardsWithDifferentDurations()
		{
			RepositoryMock
				.Setup(mock => mock.GetProjectCards(IsAny<int>()))
				.Returns(new[] {
					new CardBuilder()
						.CreatedAt(new(2021, 01, 01))
						.ClosedAt(new(2021, 01, 01))
						.Build(),
					new CardBuilder()
						.CreatedAt(new(2021, 01, 01))
						.ClosedAt(new(2021, 01, 02))
						.Build()
				});

			var actual = CardCycleTime.GetMetric(0);

			Equal(2, actual.Count);
			Equal(1, actual.ElementAt(0).Duration);
			Equal(1, actual.ElementAt(0).Cards.Count);
			Equal(2, actual.ElementAt(1).Duration);
			Equal(1, actual.ElementAt(1).Cards.Count);
		}
	}
}
