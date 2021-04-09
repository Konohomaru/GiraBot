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
	public class ReposController : ControllerBase
	{
		[HttpGet]
		public RepoDto[] Get(long installationId)
		{
			return AppHost.Instance
				.Get<GitHubBridge>()
				.GetRepositories(installationId)
				.Select(repo => RepoDto.BuildFrom(repo))
				.ToArray();
		}

		[HttpGet]
		[Route("settings")]
		public RepoSettingsDto GetRepoSettings(long installationId, long repoId)
		{
			var repoSettings = AppHost.Instance
				.Get<ReposDataSource>()
				.GetRepoSettings(installationId, repoId);

			return RepoSettingsDto.BuildFrom(repoSettings);
		}

		[HttpGet]
		[Route("projects")]
		public ProjectDto[] GetRepoProjects(long installationId, long repoId)
		{
			return AppHost.Instance
				.Get<GitHubBridge>()
				.GetRepositoryProjects(installationId, repoId)
				.Select(project => ProjectDto.BuildFrom(project))
				.ToArray();
		}

		[HttpGet]
		[Route("issues")]
		public IssueDto[] GetRepoIssues(long installationId, long repoId)
		{
			return AppHost.Instance
				.Get<ReposDataSource>()
				.GetRepoIssues(installationId, repoId)
				.Select(issue => IssueDto.BuildFrom(issue))
				.ToArray();
		}

		[HttpGet]
		[Route("issuesCycleTime")]
		public IssueCycleTimeNodeDto[] GetIssuesCycleTime(long installationId, long repoId)
		{
			return AppHost.Instance
				.Get<IssueCycleTime>()
				.GetMetric(installationId, repoId)
				.Select(node => IssueCycleTimeNodeDto.BuildFrom(node))
				.ToArray();
		}
	}
}
