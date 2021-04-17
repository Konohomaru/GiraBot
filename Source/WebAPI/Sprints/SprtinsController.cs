using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Linq;

namespace WebAPI
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class SprintsController : ControllerBase
	{
		private SprintsDataSource Sprints { get; }

		private Burndown Burndown { get; }

		private Velocity Velocity { get; }

		public SprintsController(SprintsDataSource sprints, Burndown burndown, Velocity velocity)
		{
			Sprints = sprints;
			Burndown = burndown;
			Velocity = velocity;
		}

		[HttpGet]
		public SprintDto[] Get(long installationId, long repoId)
		{
			return Sprints
				.GetRepoSprints(installationId, repoId)
				.Select(sprint => SprintDto.BuildFrom(sprint))
				.ToArray();
		}

		[HttpGet]
		[Route("latest")]
		public SprintDto GetLatest(long installationId, long repoId)
		{
			var sprint = Sprints.GetRepoLatestSprint(installationId, repoId);

			return SprintDto.BuildFrom(sprint);
		}

		[HttpGet]
		[Route("burndown")]
		public BurndownNodeDto[] GetBurndown(long installationId, long repoId)
		{
			return Burndown
				.GetMetric(installationId, repoId)
				.Select(node => BurndownNodeDto.BuildFrom(node))
				.ToArray();
		}

		[HttpGet]
		[Route("velocity")]
		public VelocityNodeDto[] GetVelocity(long installationId, long repoId, int sprintId)
		{
			return Velocity
				.GetMetric(installationId, repoId, sprintId)
				.Select(node => VelocityNodeDto.BuildFrom(node))
				.ToArray();
		}
	}
}
