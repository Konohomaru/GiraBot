using Model;
using System.Linq;

namespace WebAPI
{
	public class ProjectDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int SprintDurationDays { get; set; }

		public long InstallationId { get; set; }

		public long RepositoryId { get; set; }

		public LaneDto[] Lanes { get; set; }

		public int[] AllowedGitHubProjectIds { get; set; }

		public static ProjectDto BuildFrom(Project project)
		{
			return new ProjectDto {
				Id = project.Id,
				Name = project.Name,
				InstallationId = project.GitHubSettings.InstallationId,
				RepositoryId = project.GitHubSettings.RepositoryId,
				SprintDurationDays = project.SprtinDurationDays,
				Lanes = project.GitHubSettings.Lanes
					.Select(lane =>
						new LaneDto {
							Name = lane.Name,
							MappedLabelName = lane.MappedAlias
						})
					.ToArray(),
				AllowedGitHubProjectIds = project.GitHubSettings.AllowedProjectIds.ToArray(),
			};
		}
	}
}
