using Model;
using System;
using System.Linq;

namespace WebAPI
{
	public class RepoSettingsDto
	{
		public string RepoFullName { get; set; }

		public DateTime BeginAnalysisFrom { get; set; }

		/// <summary>
		/// Count of days.
		/// </summary>
		public int SprintDuration { get; set; }

		public LaneDto[] Lanes { get; set; }

		public RepositoryProjectDto[] AllowedProjects { get; set; }

		public static RepoSettingsDto BuildFrom(RepoSettings repoSettings)
		{
			return new RepoSettingsDto {
				RepoFullName = repoSettings.Repo.FullName,
				BeginAnalysisFrom = repoSettings.BeginAnalysisFrom,
				SprintDuration = repoSettings.SprintDuration,
				Lanes = repoSettings.Lanes
					.Select(lane => LaneDto.BuildFrom(lane))
					.ToArray(),
				AllowedProjects = repoSettings.AllowedProjects
					.Select(project => RepositoryProjectDto.BuildFrom(project))
					.ToArray()
			};
		}
	}
}