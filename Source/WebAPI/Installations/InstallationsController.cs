using GitHub;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebAPI
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class InstallationsController : ControllerBase
	{
		private GitHubBridge GitHub { get; }

		public InstallationsController(GitHubBridge gitHub)
		{
			GitHub = gitHub;
		}

		[HttpGet]
		public InstallationDto[] Get()
		{
			return GitHub
				.GetInstallations()
				.Select(installation => InstallationDto.BuildFrom(installation))
				.ToArray();
		}
	}
}
