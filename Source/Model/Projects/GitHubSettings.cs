using System.Collections.Generic;

namespace Model
{
	public class GitHubSettings
	{
		public long InstallationId { get; private set; }

		public long RepositoryId { get; private set; }

		public IReadOnlyCollection<Lane> Lanes { get; private set; }

		public IReadOnlyCollection<RepositoryProject> AllowedProjects { get; private set; }

		public GitHubSettings(
			long installationId,
			long repositoryId,
			Lane[] lanes,
			RepositoryProject[] allowedProjects)
		{
			InstallationId = installationId;
			RepositoryId = repositoryId;
			Lanes = lanes;
			AllowedProjects = allowedProjects;
		}
	}
}
