using System.Collections.Generic;

namespace Model
{
	public class GitHubSettings
	{
		public long InstallationId { get; private set; }

		public long RepositoryId { get; private set; }

		public IReadOnlyCollection<Line> Lanes { get; private set; }

		public IReadOnlyCollection<RepositoryProject> AllowedProjects { get; private set; }

		public GitHubSettings(
			long installationId,
			long repositoryId,
			Line[] lines,
			RepositoryProject[] allowedProjects)
		{
			InstallationId = installationId;
			RepositoryId = repositoryId;
			Lanes = lines;
			AllowedProjects = allowedProjects;
		}
	}
}
