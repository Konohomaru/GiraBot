using GitHub;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class ReposDataSource
	{
		private GitHubBridge GitHub { get; }

		private ProjectComparerByName Comparer { get; }

		public ReposDataSource(GitHubBridge gitHub, ProjectComparerByName comparer)
		{
			GitHub = gitHub;
			Comparer = comparer;
		}

		public RepoSettings GetRepoSettings(long installationId, long repoId)
		{
			var repo = GitHub.GetRepository(installationId, repoId);

			// todo: Здесь должны считываться настройки репозитория из БД.
			return new RepoSettings(
				repo: repo,
				new DateTime(2021, 02, 8),
				sprintDuration: 7,
				new[] {
					new Lane(0, "Bugs", "bug"),
					new Lane(1, "Tech Debts", "tech debt"),
					new Lane(2, "Road Map", "road map")
				},
				new[] { new Project(1, "Caprica2.0") });
		}

		public IReadOnlyCollection<Issue> GetRepoIssues(long installationId, long repoId)
		{
			var repoSettings = GetRepoSettings(installationId, repoId);

			var issues = GitHub.GetRepositoryIssues(installationId, repoId);

			return issues
				.Where(issue => issue.Projects.ContainsAny(
					repoSettings.AllowedProjects,
					Comparer))
				.ToArray();
		}
	}
}
