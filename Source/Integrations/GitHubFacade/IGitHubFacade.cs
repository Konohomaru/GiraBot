using System.Collections.Generic;

namespace Integrations
{
	public interface IGitHubFacade
	{
		IReadOnlyCollection<Issue> GetRepositoryIssues(long installationId, long repositoryId);
	}
}
