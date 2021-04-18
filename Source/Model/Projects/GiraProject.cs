using System.Collections.Generic;

namespace Model
{
	public class GiraProject
	{
		public int Id { get; private set; }

		public string Name { get; private set; }

		public int SprintDefaultDaysCount { get; private set; } = 7;

		public IReadOnlyCollection<GiraTask> Tasks { get; private set; }

		public GitHubSettings GitHubSettings { get; private set; }

		public GiraProject(int id, string name, GiraTask[] tasks, GitHubSettings gitHubSettings)
		{
			Id = id;
			Name = name;
			Tasks = tasks;
			GitHubSettings = gitHubSettings;
		}
	}
}
