using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class Velocity
	{
		private ICalendar Calendar { get; }

		private ReposDataSource Repos { get; }

		private SprintsDataSource Sprints { get; }

		public Velocity(ICalendar calendar, ReposDataSource repos, SprintsDataSource sprints)
		{
			Calendar = calendar;
			Repos = repos;
			Sprints = sprints;
		}

		public IReadOnlyCollection<VelocityNode> GetMetric(long installationId, long repoId, int sprintId)
		{
			var repoSettings = Repos.GetRepoSettings(installationId, repoId);

			var sprint = Sprints.GetRepoSprint(installationId, repoId, sprintId);

			var sprintIssues = Sprints.GetSprintIssues(installationId, repoId, sprintId);

			return GetMtricNodes(repoSettings, sprint, sprintIssues).ToArray();
		}

		private IEnumerable<VelocityNode> GetMtricNodes(
			RepoSettings repoSettings,
			Sprint sprint,
			IReadOnlyCollection<Issue> sprintIssues)
		{
			var today = Calendar.GetCurrentUtcDateTime();
			var currentDay = sprint.BeginAt;

			while (sprint.ContainesDate(currentDay) && currentDay <= today) {
				yield return new VelocityNode(
					currentDay,
					repoSettings.Lanes.ToDictionary(
						keySelector: lane => lane,
						elementSelector: lane => sprintIssues
							.Where(issue => issue.State == IssueState.Closed)
							.Count(issue => issue.Labels.Contains(lane.MappedLabelName))));
				currentDay = currentDay.AddDays(1);
			}
		}
	}
}
