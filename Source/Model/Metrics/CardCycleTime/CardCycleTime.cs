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
			return Repository
				.GetProjectCards(projectId)
				.Select(card => new CardCycleTimeNode(
					card.Title,
					card.CreatedAt,
					card.ClosedAt,
					card.Labels.ToArray()))
				.ToArray();
		}
	}
}
