using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Linq;

namespace WebAPI
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class InstallationsController : ControllerBase
	{
		private IGitHubWrapper GitHub { get; }

		public InstallationsController(IGitHubWrapper gitHub)
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
