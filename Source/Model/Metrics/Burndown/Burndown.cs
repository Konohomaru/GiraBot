using GitHub;
using System.Collections.Generic;
using System.Linq;

using static GitHub.IssueState;

namespace Model
{
	public class Burndown
	{
		public IReadOnlyCollection<BurndownNode> GetMetric(long installationId, long repoId)
		{
			var sprint = AppHost.Instance
				.Get<SprintsDataSource>()
				.GetRepoLatestSprint(installationId, repoId);

			var sprintIssues = AppHost.Instance
				.Get<SprintsDataSource>()
				.GetSprintIssues(installationId, repoId, sprint.Id);

			return GetMetricNodes(sprint, sprintIssues).ToArray();
		}

		private IEnumerable<BurndownNode> GetMetricNodes(Sprint sprint, IReadOnlyCollection<Issue> sprintIssues)
		{
			var today = AppHost.Instance.GetTodayUtc();
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
