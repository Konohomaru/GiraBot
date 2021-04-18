using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Linq;

namespace WebAPI
{
	[Authorize]
	[ApiController]
	[Route("projects")]
	public class GiraProjectsController : Controller
	{
		private GiraProjectsDirectory Directory { get; }

		public GiraProjectsController(GiraProjectsDirectory directory)
		{
			Directory = directory;
		}

		[HttpGet]
		[Route("{projectId}")]
		public GiraProjectDto GetGiraProject(int projectId)
		{
			var giraProject = Directory.GetGiraProject(projectId);

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
		[Route("{projectId}/tasks")]
		public GiraTaskDto[] GetGiraProjectTasks(int projectId)
		{
			return Directory
				.GetGiraProjectTasks(projectId)
				.Select(giraTask => new GiraTaskDto {
					Id = giraTask.Id,
					ClosedAt = giraTask.ClosedAt,
					CreatedAt = giraTask.CreatedAt,
					Title = giraTask.Title,
					Labels = giraTask.Labels.ToArray()
				})
				.ToArray();
		}

		[HttpGet]
		[Route("{projectId}/sprints")]
		public SprintDto[] GetProjectSprints(int projectId)
		{
			return Directory
				.GetGiraProjectSprints(projectId)
				.Select(sprint => new SprintDto {
					Id = sprint.Id,
					BeginAt = sprint.BeginAt,
					Duration = sprint.Duration
				})
				.ToArray();
		}
	}
}
