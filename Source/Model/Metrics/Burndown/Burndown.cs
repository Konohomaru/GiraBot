using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class Burndown
	{
		private ICalendar Calendar { get; }

		private IProjectsRepository Repository { get; }

		public Burndown(ICalendar calendar, IProjectsRepository repository)
		{
			Calendar = calendar;
			Repository = repository;
		}

		public IReadOnlyCollection<BurndownNode> GetMetric(int projectId)
		{
			var latestSprint = Repository
				.GetProjectSprints(projectId)
				.Last();

			var sprintCards = Repository
				.GetProjectCards(projectId)
				.GetSprintCards(latestSprint)
				.ToArray();

			return GetNodes(latestSprint, sprintCards).ToArray();
		}

		private IEnumerable<BurndownNode> GetNodes(Sprint latestSprint, IReadOnlyCollection<Card> sprintCards)
		{
			var today = Calendar.GetCurrentUtcDateTime();
			var iterationDay = latestSprint.BeginsAt;

			while (latestSprint.ContainsDate(iterationDay) && iterationDay <= today) {
				yield return new BurndownNode(
					iterationDay,
					sprintCards.Count(card => !card.ClosedAt.HasValue || card.ClosedAt > iterationDay));

				iterationDay = iterationDay.AddDays(1);
			}
		}
	}
}
