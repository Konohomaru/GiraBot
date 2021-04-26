using System.Collections.Generic;

namespace Model
{
	public class GitHubSettings
	{
		public long InstallationId { get; }

		public long RepositoryId { get; }

		public IReadOnlyCollection<Swimlane> Swimlanes { get; }

		public IReadOnlyCollection<int> AllowedProjectIds { get; }

		public GitHubSettings(
			long installationId,
			long repositoryId,
			Swimlane[] swimlanes,
			int[] allowedProjectIds)
		{
			InstallationId = installationId;
			RepositoryId = repositoryId;
			Swimlanes = swimlanes;
			AllowedProjectIds = allowedProjectIds;
		}
	}
}
