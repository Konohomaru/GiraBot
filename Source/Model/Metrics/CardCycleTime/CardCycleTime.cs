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

			return cards
				.GroupBy(GetDuration)
				.Select(group => new CardCycleTimeNode(group.Key, group.ToArray()))
				.OrderBy(node => node.Duration)
				.ToArray();

			static int? GetDuration(Card card)
			{
				return (card.ClosedAt - card.CreatedAt)?.Days + 1;
			}
		}
	}
}
