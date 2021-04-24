using Model;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI
{
	public class GitHubTasksDataSource : ITasksDataSource
	{
		private IGitHubFacade Facade { get; }

		public GitHubTasksDataSource(IGitHubFacade facade)
		{
			Facade = facade;
		}

		public IReadOnlyCollection<GiraTask> GetProjectTasks(Project project)
		{
			return Facade
				.GetRepositoryIssues(
					installationId: project.GitHubSettings.InstallationId,
					repositoryId: project.GitHubSettings.RepositoryId)
				.Where(issue => issue.Projects
					.Select(project => project.Id)
					.ContainsAnyOf(project.GitHubSettings.AllowedProjectIds))
				.Select(issue => new GiraTask(
					id: issue.Id,
					createdAt: issue.CreateAt,
					closedAt: issue.ClosedAt,
					title: issue.Title,
					labels: issue.Labels))
				.ToArray();
		}
	}
}
