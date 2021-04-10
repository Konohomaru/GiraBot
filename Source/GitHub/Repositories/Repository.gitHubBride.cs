using System.Collections.Generic;
using System.Linq;

namespace GitHub
{
	public partial class GitHubBridge
	{
		public Repository GetRepository(long installationId, long repositoryId)
		{
			var repository = GetClient(installationId).Repository.Get(repositoryId).Result;

			return Repository.BuildFrom(repository, Owner.BuildFrom(repository.Owner));
		}

		public IReadOnlyCollection<Repository> GetRepositories(long installationId)
		{
			return GetClient(installationId).GitHubApps.Installation
				.GetAllRepositoriesForCurrent().Result.Repositories
				.Select(repository => Repository.BuildFrom(repository, Owner.BuildFrom(repository.Owner)))
				.ToArray();
		}
	}
}
