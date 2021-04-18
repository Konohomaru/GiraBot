using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Linq;

namespace WebAPI
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class GiraProjectsController : Controller
	{
		private IGitHubFacade Facade { get; }

		public GiraProjectsController(IGitHubFacade facade)
		{
			Facade = facade;
		}

		[HttpGet]
		public GiraProjectDto GetGiraProject(int projectId)
		{
			var giraProject = Facade.GetGiraProject(projectId);

			return new GiraProjectDto {
				Id = giraProject.Id,
				Name = giraProject.Name,
				InstallationId = giraProject.GitHubSettings.InstallationId,
				RepositoryId = giraProject.GitHubSettings.RepositoryId,
				SprintDefaultDaysCount = giraProject.SprintDefaultDaysCount,
				Lanes = giraProject.GitHubSettings.Lanes
					.Select(lane =>
						new LaneDto {
							Name = lane.Name,
							MappedLabelName = lane.MappedName
						})
					.ToArray(),
				AllowedProjects = giraProject.GitHubSettings.AllowedProjects
					.Select(allowedProject =>
						new RepositoryProjectDto {
							Id = allowedProject.Id,
							Name = allowedProject.Name
						})
					.ToArray(),
			};
		}

		[HttpGet]
		[Route("tasks")]
		public GiraTaskDto[] GetGiraProjectTasks(int projectId)
		{
			var giraProject = Facade.GetGiraProject(projectId);

			return giraProject.Tasks
				.Select(giraTask => new GiraTaskDto {
					Id = giraTask.Id,
					ClosedAt = giraTask.ClosedAt,
					CreatedAt = giraTask.CreatedAt,
					Title = giraTask.Title,
					Labels = giraTask.Labels.ToArray()
				})
				.ToArray();
		}
	}
}
