namespace GitHub
{
	public class Project
	{
		public int Id { get; private set; }

		public string Name { get; private set; }

		public Project(int id, string name)
		{
			Id = id;
			Name = name;
		}

		internal static Project BuildFrom(Octokit.Project project)
		{
			return new Project(project.Id, project.Name);
		}

		internal static Project BuildFrom(ProjectModel project)
		{
			return new Project(project.Id, project.Name);
		}
	}
}
