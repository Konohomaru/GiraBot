using System;

namespace Model
{
	public class Project
	{
		public int Id { get; private set; }

		public string Name { get; private set; }

		public int SprtinDurationDays { get; private set; } = 7;

		public DateTime BeginSprintsAt { get; private set; }

		public GitHubSettings GitHubSettings { get; private set; }

		public Project(int id, string name, DateTime startSprintsAt, GitHubSettings gitHubSettings)
		{
			Id = id;
			Name = name;
			BeginSprintsAt = startSprintsAt;
			GitHubSettings = gitHubSettings;
		}
	}
}
