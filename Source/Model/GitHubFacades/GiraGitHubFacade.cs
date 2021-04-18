using System.Linq;

namespace Model
{
	public class GiraGitHubFacade : IGitHubFacade
	{
		private IGitHubWrapper GitHubClient { get; }

		private GiraProjectsDirectory Directory { get; }

		public GiraGitHubFacade(IGitHubWrapper gitHubWrapper, GiraProjectsDirectory directory)
		{
			GitHubClient = gitHubWrapper;
			Directory = directory;
		}

		public GiraProject GetGiraProject(int id)
		{
			var gitHubSettings = Directory.GetGitHubSettings(id);

			var gitHubRepository = GitHubClient.GetRepository(
				installationId: gitHubSettings.InstallationId,
				repositoryId: gitHubSettings.RepositoryId);

			var projectTasks = GitHubClient
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

			return new GiraProject(
				id: 0,
				name: gitHubRepository.FullName,
				tasks: projectTasks,
				gitHubSettings: gitHubSettings);
		}
	}
}
