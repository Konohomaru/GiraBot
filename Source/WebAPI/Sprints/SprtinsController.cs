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
		[HttpGet]
		public SprintDto[] Get(long installationId, long repoId)
		{
			return AppHost.Instance
				.Get<SprintsDataSource>()
				.GetRepoSprints(installationId, repoId)
				.Select(sprint => SprintDto.BuildFrom(sprint))
				.ToArray();
		}

		[HttpGet]
		[Route("latest")]
		public SprintDto GetLatest(long installationId, long repoId)
		{
			var sprint = AppHost.Instance
				.Get<SprintsDataSource>()
				.GetRepoLatestSprint(installationId, repoId);

			return SprintDto.BuildFrom(sprint);
		}

		[HttpGet]
		[Route("burndown")]
		public BurndownNodeDto[] GetBurndown(long installationId, long repoId)
		{
			return AppHost.Instance
				.Get<Burndown>()
				.GetMetric(installationId, repoId)
				.Select(node => BurndownNodeDto.BuildFrom(node))
				.ToArray();
		}

		[HttpGet]
		[Route("velocity")]
		public VelocityNodeDto[] GetVelocity(long installationId, long repoId, int sprintId)
		{
			return AppHost.Instance
				.Get<Velocity>()
				.GetMetric(installationId, repoId, sprintId)
				.Select(node => VelocityNodeDto.BuildFrom(node))
				.ToArray();
		}
	}
}
