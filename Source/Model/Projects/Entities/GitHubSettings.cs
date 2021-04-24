using System.Collections.Generic;

namespace Model
{
	public class GitHubSettings
	{
		public long InstallationId { get; }

		public long RepositoryId { get; }

		public IReadOnlyCollection<Lane> Lanes { get; }

		public IReadOnlyCollection<int> AllowedProjectIds { get; }

		public GitHubSettings(
			long installationId,
			long repositoryId,
			Lane[] lanes,
			int[] allowedProjectIds)
		{
			InstallationId = installationId;
			RepositoryId = repositoryId;
			Lanes = lanes;
			AllowedProjectIds = allowedProjectIds;
		}
	}
}
