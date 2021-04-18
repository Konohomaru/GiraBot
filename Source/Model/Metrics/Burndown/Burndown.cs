using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class Burndown
	{
		private ICalendar Calendar { get; }

		private SprintsDataSource Sprints { get; }

		public Burndown(ICalendar calendar, SprintsDataSource sprints)
		{
			Calendar = calendar;
			Sprints = sprints;
		}

		public IReadOnlyCollection<BurndownNode> GetMetric(long installationId, long repoId)
		{
			var sprint = Sprints.GetRepoLatestSprint(installationId, repoId);

			var sprintIssues = Sprints.GetSprintIssues(installationId, repoId, sprint.Id);

			return GetMetricNodes(sprint, sprintIssues).ToArray();
		}

		private IEnumerable<BurndownNode> GetMetricNodes(Sprint sprint, IReadOnlyCollection<Issue> sprintIssues)
		{
			var today = Calendar.GetCurrentUtcDateTime();
			var currentDay = sprint.BeginAt;

			while (sprint.ContainesDate(currentDay) && currentDay <= today) {
				yield return new BurndownNode(
					currentDay,
					sprintIssues.Count(issue => issue.ClosedAt is null || issue.ClosedAt <= currentDay));
				currentDay = currentDay.AddDays(1);
			}
		}
	}
}
