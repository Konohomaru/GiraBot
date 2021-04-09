using GitHub;
using System.Collections.Generic;
using System.Linq;

using static GitHub.IssueState;

namespace Model
{
	public class Velocity
	{
		public IReadOnlyCollection<VelocityNode> GetMetric(long installationId, long repoId, int sprintId)
		{
			var repoSettings = AppHost.Instance
				.Get<ReposDataSource>()
				.GetRepoSettings(installationId, repoId);

			var sprint = AppHost.Instance
				.Get<SprintsDataSource>()
				.GetRepoSprint(installationId, repoId, sprintId);

			var sprintIssues = AppHost.Instance
				.Get<SprintsDataSource>()
				.GetSprintIssues(installationId, repoId, sprintId);

			return GetMtricNodes(repoSettings, sprint, sprintIssues).ToArray();
		}

		private IEnumerable<VelocityNode> GetMtricNodes(
			RepoSettings repoSettings,
			Sprint sprint,
			IReadOnlyCollection<Issue> sprintIssues)
		{
			var today = AppHost.Instance.GetTodayUtc();
			var currentDay = sprint.BeginAt;

			while (sprint.ContainesDate(currentDay) && currentDay <= today) {
				yield return new VelocityNode(
					currentDay,
					repoSettings.Lanes.ToDictionary(
						keySelector: lane => lane,
						elementSelector: lane => sprintIssues
							.Where(issue => issue.State == Closed)
							.Count(issue => issue.Labels.Contains(lane.MappedLabelName))));
				currentDay = currentDay.AddDays(1);
			}
		}
	}
}
