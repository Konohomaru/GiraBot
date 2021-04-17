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
		private GitHubBridge GitHub { get; }

		private ReposDataSource Repos { get; }

		private IssueCycleTime IssueCycleTime { get; }

		public ReposController(GitHubBridge gitHub, ReposDataSource repos, IssueCycleTime issueCycleTime)
		{
			GitHub = gitHub;
			Repos = repos;
			IssueCycleTime = issueCycleTime;
		}

		[HttpGet]
		public RepoDto[] Get(long installationId)
		{
			return GitHub
				.GetRepositories(installationId)
				.Select(repo => RepoDto.BuildFrom(repo))
				.ToArray();
		}

		[HttpGet]
		[Route("settings")]
		public RepoSettingsDto GetRepoSettings(long installationId, long repoId)
		{
			var repoSettings = Repos.GetRepoSettings(installationId, repoId);

			return RepoSettingsDto.BuildFrom(repoSettings);
		}

		[HttpGet]
		[Route("projects")]
		public ProjectDto[] GetRepoProjects(long installationId, long repoId)
		{
			return GitHub
				.GetRepositoryProjects(installationId, repoId)
				.Select(project => ProjectDto.BuildFrom(project))
				.ToArray();
		}

		[HttpGet]
		[Route("issues")]
		public IssueDto[] GetRepoIssues(long installationId, long repoId)
		{
			return Repos
				.GetRepoIssues(installationId, repoId)
				.Select(issue => IssueDto.BuildFrom(issue))
				.ToArray();
		}

		[HttpGet]
		[Route("issuesCycleTime")]
		public IssueCycleTimeNodeDto[] GetIssuesCycleTime(long installationId, long repoId)
		{
			return IssueCycleTime
				.GetMetric(installationId, repoId)
				.Select(node => IssueCycleTimeNodeDto.BuildFrom(node))
				.ToArray();
		}
	}
}
