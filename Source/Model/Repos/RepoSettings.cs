using System;
using System.Collections.Generic;

namespace Model
{
	public class RepoSettings
	{
		public Repository Repo { get; private set; }
		public DateTime BeginAnalysisFrom { get; private set; }

		/// <summary>
		/// Count of days.
		/// </summary>
		public int SprintDuration { get; private set; }

		public IReadOnlyCollection<Lane> Lanes { get; private set; }

		public IReadOnlyCollection<Project> AllowedProjects { get; private set; }

		/// <summary>
		/// Represents Gira settings over GitHub repository.
		/// </summary>
		/// <param name="sprintDuration">Count of days.</param>
		public RepoSettings(
			Repository repo,
			DateTime beginAt,
			int sprintDuration,
			Lane[] lanes,
			Project[] allowedProjects)
		{
			Repo = repo;
			BeginAnalysisFrom = beginAt;
			SprintDuration = sprintDuration;
			Lanes = lanes;
			AllowedProjects = allowedProjects;
		}
	}
}