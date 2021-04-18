using System.Collections.Generic;

namespace Model
{
	public interface IGitHubFacade
	{
		IReadOnlyCollection<Installation> GetInstallations();

		Installation GetInstallation(long installationId);

		IReadOnlyCollection<Repository> GetRepositories(long installationId);

		Repository GetRepository(long installationId, long repositoryId);

		IReadOnlyCollection<Issue> GetRepositoryIssues(long installationId, long repositoryId);

		IReadOnlyCollection<RepositoryProject> GetRepositoryProjects(long installationId, long repositoryId);
	}
}
