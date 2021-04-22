using System.Collections.Generic;

namespace Model
{
	public interface IProjectsRepository
	{
		GitHubSettings GetProjectGitHubSettings(int projectId);

		Project GetProject(int projectId);

		IEnumerable<Sprint> GetProjectSprints(int projectId);

		IReadOnlyCollection<GiraTask> GetProjectTasks(int projectId);
	}
}
