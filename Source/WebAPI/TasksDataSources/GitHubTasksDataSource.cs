using Integrations;
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

		public IReadOnlyCollection<GrTask> GetProjectTasks(Project project)
		{
			return Facade
				.GetRepositoryIssues(
					installationId: project.GitHubSettings.InstallationId,
					repositoryId: project.GitHubSettings.RepositoryId)
				.Where(issue => issue.ProjectIds
					.ContainsAnyOf(project.GitHubSettings.AllowedProjectIds))
				.Select(issue => new GrTask(
					id: issue.Id,
					createdAt: issue.CreatedAt,
					closedAt: issue.ClosedAt,
					title: issue.Title,
					labels: issue.Labels))
				.ToArray();
		}
	}
}
