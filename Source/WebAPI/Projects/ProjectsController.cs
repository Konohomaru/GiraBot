﻿using Microsoft.AspNetCore.Authorization;
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
		private IProjectsRepository Repository { get; }

		private Burndown Burndown { get; }

		private Velocity Velocity { get; }

		private CardCycleTime TaskCycleTime { get; }

		public ProjectsController(
			IProjectsRepository directory,
			Burndown burndown,
			Velocity velocity,
			CardCycleTime taskCycleTime)
		{
			Repository = directory;
			Burndown = burndown;
			Velocity = velocity;
			TaskCycleTime = taskCycleTime;
		}

		[HttpGet]
		[Route("{projectId}")]
		public ProjectDto GetProject(int projectId)
		{
			return ProjectDto.BuildFrom(Repository.GetProject(projectId));
		}

		[HttpGet]
		[Route("{projectId}/tasks")]
		public CardDto[] GetProjectTasks(int projectId)
		{
			return Repository
				.GetProjectCards(projectId)
				.Select(giraTask => CardDto.BuildFrom(giraTask))
				.ToArray();
		}

		[HttpGet]
		[Route("{projectId}/sprints")]
		public SprintDto[] GetProjectSprints(int projectId)
		{
			return Repository
				.GetProjectSprints(projectId)
				.Select(sprint => SprintDto.BuildFrom(sprint))
				.ToArray();
		}

		[HttpGet]
		[Route("{projectId}/burndown")]
		public BurndownNodeDto[] GetBurndownMetric(int projectId)
		{
			return Burndown
				.GetMetric(projectId)
				.Select(node => BurndownNodeDto.BuildFrom(node))
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
		public CardCycleTimeNodeDto[] GetTaskCycleTimeMetric(int projectId)
		{
			return TaskCycleTime
				.GetMetric(projectId)
				.Select(node => CardCycleTimeNodeDto.BuildFrom(node))
				.ToArray();
		}
	}
}
