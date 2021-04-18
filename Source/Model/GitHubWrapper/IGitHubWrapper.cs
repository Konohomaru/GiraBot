using System.Collections.Generic;

namespace Model
{
	public interface IGitHubWrapper
	{
		IReadOnlyCollection<Installation> GetInstallations();

		Installation GetInstallation(long installationId);

		IReadOnlyCollection<Repository> GetRepositories(long installationId);

		Repository GetRepository(long installationId, long repositoryId);

		IReadOnlyCollection<Issue> GetRepositoryIssues(long installationId, long repositoryId);

		IReadOnlyCollection<Project> GetRepositoryProjects(long installationId, long repositoryId);
	}
}
