using System.Collections.Generic;
using System.Linq;

namespace GitHub
{
	public partial class GitHubBridge
	{
		public IReadOnlyCollection<Project> GetRepositoryProjects(long installationId, long repositoryId)
		{
			return GetClient(installationId).Repository.Project
				.GetAllForRepository(repositoryId).Result
				.Select(project => Project.BuildFrom(project))
				.ToArray();
		}
	}
}
