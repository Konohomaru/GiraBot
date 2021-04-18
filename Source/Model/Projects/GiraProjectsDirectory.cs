namespace Model
{
	public class GiraProjectsDirectory
	{
		public GitHubSettings GetGitHubSettings(int id)
		{
			return new GitHubSettings(
				installationId: 15161810,
				repositoryId: 229932826,
				lanes: new[] {
					new Lane(0, "Bugs", "bug"),
					new Lane(1, "Tech Debts", "tech debt"),
					new Lane(2, "Road Map", "road map")
				},
				allowedProjects: new[] { new RepositoryProject(3720514, "Caprica2.0") });
		}
	}
}
