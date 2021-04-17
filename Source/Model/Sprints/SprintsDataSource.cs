using GitHub;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class SprintsDataSource
	{
		private ICalendar Calendar { get; }

		private ReposDataSource Repos { get; }

		public SprintsDataSource(ICalendar calendar, ReposDataSource repos)
		{
			Calendar = calendar;
			Repos = repos;
		}

		private IEnumerable<Sprint> BuildSprintsFor(long installationId, long repoId)
		{
			var repoSettings = Repos.GetRepoSettings(installationId, repoId);

			var calculatingDate = repoSettings.BeginAnalysisFrom;
			var today = Calendar.GetCurrentUtcDateTime();

			while (calculatingDate <= today) {
				yield return new Sprint(
					id: calculatingDate.DayOfYear,
					beginAt: calculatingDate,
					repoSettings.SprintDuration);

				calculatingDate = calculatingDate.AddDays(repoSettings.SprintDuration);
			}
		}

		public Sprint GetRepoSprint(long installationId, long repoId, int sprintId)
		{
			return BuildSprintsFor(installationId, repoId)
				.Single(sprint => sprint.Id == sprintId);
		}

		public Sprint GetRepoLatestSprint(long installationId, long repoId)
		{
			return BuildSprintsFor(installationId, repoId).Last();
		}

		public IReadOnlyCollection<Sprint> GetRepoSprints(long installationId, long repoId)
		{
			return BuildSprintsFor(installationId, repoId).ToArray();
		}

		public IReadOnlyCollection<Issue> GetSprintIssues(long installationId, long repoId, int sprintId)
		{
			var repoIssues = Repos.GetRepoIssues(installationId, repoId);

			var sprint = GetRepoSprint(installationId, repoId, sprintId);

			return repoIssues
				.Where(issue => sprint.ContainsIssue(issue))
				.ToArray();
		}
	}
}
