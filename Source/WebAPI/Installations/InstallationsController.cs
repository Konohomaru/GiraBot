using GitHub;
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
		[HttpGet]
		public InstallationDto[] Get()
		{
			return AppHost.Instance
				.Get<GitHubBridge>()
				.GetInstallations()
				.Select(installation => InstallationDto.BuildFrom(installation))
				.ToArray();
		}
	}
}
