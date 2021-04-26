using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class Velocity
	{
		private ICalendar Calendar { get; }

		private IProjectsRepository Repository { get; }

		public Velocity(ICalendar calendar, IProjectsRepository repository)
		{
			Calendar = calendar;
			Repository = repository;
		}

		public IReadOnlyCollection<VelocityNode> GetMetric(int projectId)
		{
			var projectSwimlanes = Repository.GetProject(projectId).GitHubSettings.Swimlanes;

			var latestSprint = Repository
				.GetProjectSprints(projectId)
				.Last();

			var sprintCards = Repository
				.GetProjectCards(projectId)
				.GetSprintCards(latestSprint)
				.ToArray();

			return GetMetricNodes(latestSprint, projectSwimlanes, sprintCards).ToArray();
		}

		private IEnumerable<VelocityNode> GetMetricNodes(
			Sprint latestSprint,
			IReadOnlyCollection<Swimlane> swimlanes,
			IReadOnlyCollection<Card> sprintCards)
		{
			var today = Calendar.GetCurrentUtcDateTime();
			var iterationDay = latestSprint.BeginsAt;

			while (latestSprint.ContainsDate(iterationDay) && iterationDay <= today) {
				yield return new VelocityNode(
					iterationDay,
					swimlanes.ToDictionary(
						keySelector: swimlane => swimlane,
						elementSelector: swimlane => sprintCards
							.Where(card => card.ClosedAt <= iterationDay)
							.Count(card => card.Labels.Contains(swimlane.MappedAlias))));
				iterationDay = iterationDay.AddDays(1);
			}
		}
	}
}
