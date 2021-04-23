using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Linq;

namespace WebAPI
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class ProjectsController : Controller
	{
		private IProjectsRepository Directory { get; }

		private Burndown Burndown { get; }

		private Velocity Velocity { get; }

		private TaskCycleTime TaskCycleTime { get; }

		public ProjectsController(
			IProjectsRepository directory,
			Burndown burndown,
			Velocity velocity,
			TaskCycleTime taskCycleTime)
		{
			Directory = directory;
			Burndown = burndown;
			Velocity = velocity;
			TaskCycleTime = taskCycleTime;
		}

		[HttpGet]
		[Route("{projectId}")]
		public ProjectDto GetGiraProject(int projectId)
		{
			var giraProject = Directory.GetProject(projectId);

			return new ProjectDto {
				Id = giraProject.Id,
				Name = giraProject.Name,
				InstallationId = giraProject.GitHubSettings.InstallationId,
				RepositoryId = giraProject.GitHubSettings.RepositoryId,
				SprintDefaultDaysCount = giraProject.SprtinDurationDays,
				Lanes = giraProject.GitHubSettings.Lanes
					.Select(lane =>
						new LaneDto {
							Name = lane.Name,
							MappedLabelName = lane.MappedAlias
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
				.GetProjectTasks(projectId)
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
				.GetProjectSprints(projectId)
				.Select(sprint => new SprintDto {
					Id = sprint.Id,
					BeginAt = sprint.BeginsAt,
					Duration = sprint.DaysCount
				})
				.ToArray();
		}

		[HttpGet]
		[Route("{projectId}/burndown")]
		public BurndownNodeDto[] GetBurndownMetric(int projectId)
		{
			return Burndown
				.GetMetric(projectId)
				.Select(node => new BurndownNodeDto {
					Day = node.Day,
					RemainingTasks = node.RemainingTasks
				})
				.ToArray();
		}

		[HttpGet]
		[Route("{projectId}/velocity")]
		public VelocityNodeDto[] GetVelocityMetric(int projectId)
		{
			return Velocity
				.GetMetric(projectId)
				.Select(node => VelocityNodeDto.BuildFrom(node))
				.ToArray();
		}

		[HttpGet]
		[Route("{projectId}/taskcycletime")]
		public TaskCycleTimeNodeDto[] GetTaskCycleTimeMetric(int projectId)
		{
			return TaskCycleTime
				.GetMetric(projectId)
				.Select(node => TaskCycleTimeNodeDto.BuildFrom(node))
				.ToArray();
		}
	}
}
