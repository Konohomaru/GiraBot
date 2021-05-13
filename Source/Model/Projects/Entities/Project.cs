using System;

namespace Model
{
	public class Project
	{
		public int Id { get; }

		public string Name { get; }

		public int SprintLength { get; } = 28;

		public DateTime BeginSprintsAt { get; }

		public GitHubSettings GitHubSettings { get; }

		public Project(
			int id,
			string name,
			DateTime beginSprintsAt,
			GitHubSettings gitHubSettings)
		{
			Id = id;
			Name = name;
			BeginSprintsAt = beginSprintsAt;
			GitHubSettings = gitHubSettings;
		}
	}
}
