using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class ProjectsDirectory
	{
		private ICalendar Calendar { get; }

		private IGitHubFacade GitHubClient { get; }

		public ProjectsDirectory(ICalendar calendar, IGitHubFacade facade)
		{
			Calendar = calendar;
			GitHubClient = facade;
		}

		public GitHubSettings GetProjectGitHubSettings(int projectId)
		{
			return new GitHubSettings(
				installationId: 15161810,
				repositoryId: 229932826,
				lanes: new[] {
					new Lane(0, "Bugs", "bug"),
					new Lane(1, "Tech Debts", "tech debt"),
					new Lane(2, "Road Map", "road map")
				},
				allowedProjects: new[] { new RepositoryProject(3720514, "Caprica2.0") });
		}

		public Project GetProject(int projectId)
		{
			var gitHubSettings = GetProjectGitHubSettings(projectId);

			var gitHubRepository = GitHubClient.GetRepository(
				installationId: gitHubSettings.InstallationId,
				repositoryId: gitHubSettings.RepositoryId);

			return new Project(
				id: 0,
				name: gitHubRepository.FullName,
				startSprintsAt: gitHubRepository.CreatedAt,
				gitHubSettings: gitHubSettings);
		}

		public IEnumerable<Sprint> GetProjectSprints(int projectId)
		{
			var giraProject = GetProject(projectId);
			var currentSpritnStartDay = giraProject.StartSprintsAt;
			var today = Calendar.GetCurrentUtcDateTime();

			while (currentSpritnStartDay <= today) {
				yield return new Sprint(
					id: currentSpritnStartDay.DayOfYear,
					beginAt: currentSpritnStartDay,
					duration: giraProject.SprintDefaultDaysCount);

				currentSpritnStartDay = currentSpritnStartDay.AddDays(giraProject.SprintDefaultDaysCount);
			}
		}

		public IReadOnlyCollection<GiraTask> GetProjectTasks(int projectId)
		{
			var gitHubSettings = GetProjectGitHubSettings(projectId);

			return GitHubClient
				.GetRepositoryIssues(
					installationId: gitHubSettings.InstallationId,
					repositoryId: gitHubSettings.RepositoryId)
				.Where(issue => issue.Projects.ContainsAny(gitHubSettings.AllowedProjects))
				.Select(issue => new GiraTask(
					id: issue.Id,
					createdAt: issue.CreateAt,
					closedAt: issue.ClosedAt,
					title: issue.Title,
					labels: issue.Labels))
				.ToArray();
		}
	}
}
