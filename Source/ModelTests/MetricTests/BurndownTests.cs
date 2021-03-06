﻿using Model;
using Moq;
using System;
using System.Linq;
using Xunit;

using static Moq.It;
using static Xunit.Assert;

namespace ModelTests
{
	public class BurndownTests
	{
		private Mock<ICalendar> CalendarMock { get; }

		private Mock<IProjectsRepository> RepositoryMock { get; }

		private Burndown Burndown { get; }

		public BurndownTests()
		{
			CalendarMock = new();
			RepositoryMock = new();
			Burndown = new(CalendarMock.Object, RepositoryMock.Object);

			RepositoryMock
				.Setup(repository => repository.GetProjectSprints(1))
				.Returns(new[] { new Sprint(0, new(), 7) });
		}

		[Fact]
		public void ArgumentNullExceptionIfSprtinsIsNull()
		{
			RepositoryMock
				.Setup(repository => repository.GetProjectSprints(IsAny<int>()))
				.Returns(value: null);

			Throws<ArgumentNullException>(() => Burndown.GetMetric(0));
		}

		[Fact]
		public void ArgumentNullExceptionIfCardsIsNull()
		{
			RepositoryMock
				.Setup(repository => repository.GetProjectCards(IsAny<int>()))
				.Returns(value: null);

			Throws<ArgumentNullException>(() => Burndown.GetMetric(1));
		}
		
		[Fact]
		public void InvalidOperationExceptionIfNoSprints()
		{
			RepositoryMock
				.Setup(repository => repository.GetProjectSprints(IsAny<int>()))
				.Returns(Array.Empty<Sprint>());

			Throws<InvalidOperationException>(() => Burndown.GetMetric(0));
		}

		[Fact]
		public void NoRemainingCardsIfNoCards()
		{
			RepositoryMock
				.Setup(repository => repository.GetProjectCards(IsAny<int>()))
				.Returns(Array.Empty<Card>());

			var metric = Burndown.GetMetric(1).Single();

			Equal(new(), metric.Day);
			Equal(0, metric.RemainingCards);
		}

		[Fact]
		public void OneRemainingCardIfOneOpenedCard()
		{
			RepositoryMock
				.Setup(repository => repository.GetProjectCards(IsAny<int>()))
				.Returns(new[] { new CardBuilder().Build() });

			var metric = Burndown.GetMetric(1).Single();

			Equal(new(), metric.Day);
			Equal(1, metric.RemainingCards);
		}

		[Fact]
		public void NoRemainingCardsIfOneClosedCard()
		{
			RepositoryMock
				.Setup(repository => repository.GetProjectCards(IsAny<int>()))
				.Returns(new[] {
					new CardBuilder()
						.ClosedAt(new())
						.Build()
				});

			var metric = Burndown.GetMetric(1).Single();

			Equal(new(), metric.Day);
			Equal(0, metric.RemainingCards);
		}

		[Fact]
		public void OneRemainingCardForTwoDaysIfItWasClosedOnThirdDay()
		{
			CalendarMock
				.Setup(calendar => calendar.GetCurrentUtcDateTime())
				.Returns(new DateTime().AddDays(2));

			RepositoryMock
				.Setup(repository => repository.GetProjectCards(IsAny<int>()))
				.Returns(new[] {
					new CardBuilder()
						.ClosedAt(new DateTime().AddDays(2))
						.Build()
				});

			var metric = Burndown.GetMetric(1);

			Equal(3, metric.Count);
			Equal(1, metric.ElementAt(0).RemainingCards);
			Equal(1, metric.ElementAt(1).RemainingCards);
			Equal(0, metric.ElementAt(2).RemainingCards);
		}
	}
}
