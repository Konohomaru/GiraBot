using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class CardCycleTime
	{
		private IProjectsRepository Repository { get; }

		public CardCycleTime(IProjectsRepository repository)
		{
			Repository = repository;
		}

		public IReadOnlyCollection<CardCycleTimeNode> GetMetric(int projectId)
		{
			var cards = Repository.GetProjectCards(projectId);

			return GetMetricNodes(cards).ToArray();
		}

		private static IEnumerable<CardCycleTimeNode> GetMetricNodes(IReadOnlyCollection<Card> cards)
		{
			var orderedByDuration = cards.OrderBy(GetDuration);

			for (int i = 0; i < orderedByDuration.Count(); ++i) {
				yield return new CardCycleTimeNode(
					duration: GetDuration(orderedByDuration.ElementAt(i)),
					cards: cards
						.Where(card => GetDuration(card) == GetDuration(orderedByDuration.ElementAt(i)))
						.ToArray());

				i += orderedByDuration.Count(card => GetDuration(card) == GetDuration(orderedByDuration.ElementAt(i)));
			}
		}

		private static int? GetDuration(Card card)
		{
			return (card.ClosedAt - card.CreatedAt)?.Days + 1;
		}
	}
}
