using GitHubJwt;
using Octokit;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System.Collections.Generic;
using System.Linq;

using static Octokit.GraphQL.Variable;

namespace Model
{
	public class GiraGitHubWrapper : IGitHubWrapper
	{
		private static ICompiledQuery<IEnumerable<IssueModel>> _repositoryIssuesQuery;

		private string Pem { get; }

		static GiraGitHubWrapper()
		{
			CompileIssuesQuery();
		}

		public GiraGitHubWrapper(string pem)
		{
			Pem = pem;
		}

		private static void CompileIssuesQuery()
		{
			_repositoryIssuesQuery = new Query()
				.Repository(Var("name"), Var("login"))
				.Issues(orderBy: new IssueOrder { Field = IssueOrderField.UpdatedAt })
				.AllPages()
				.Select(issue => new IssueModel {
					Id = issue.DatabaseId.Value,
					CreateAt = issue.CreatedAt,
					UpdateAt = issue.UpdatedAt,
					ClsoedAt = issue.ClosedAt,
					Title = issue.Title,
					Labels = issue
						.Labels(100, null, null, null, null).Nodes
						.Select(label => label.Name)
						.ToList(),
					Projects = issue
						.ProjectCards(100, null, null, null, null).Nodes
						.Select(projectCard => new ProjectModel {
							Id = projectCard.Project.DatabaseId.Value,
							Name = projectCard.Project.Name
						})
						.ToList(),
					State = IssueStateConverter.BuildFrom(issue.State)
				}).Compile();
		}

		private Octokit.GraphQL.Connection GetConnection(long installationId)
		{
			return new Octokit.GraphQL.Connection(
				new Octokit.GraphQL.ProductHeaderValue($"GiraBotInstallation-{installationId}-GraphQL"),
				GetClient(installationId).Credentials.GetToken());
		}

		private GitHubClient GetClient(long installationId)
		{
			var appClient = GetAppClient();

			return new GitHubClient(new Octokit.ProductHeaderValue($"GiraBotInstallation-{installationId}")) {
				Credentials = new Credentials(appClient.GitHubApps.CreateInstallationToken(installationId).Result.Token)
			};
		}

		private GitHubClient GetAppClient()
		{
			var generator = new GitHubJwtFactory(
				new StringPrivateKeySource(Pem),
				new GitHubJwtFactoryOptions { AppIntegrationId = 97880, ExpirationSeconds = 530 });

			var jwt = generator.CreateEncodedJwtToken();

			return new GitHubClient(new Octokit.ProductHeaderValue("GiraBot")) {
				Credentials = new Credentials(jwt, AuthenticationType.Bearer)
			};
		}

		public Installation GetInstallation(long installationId)
		{
			var installation = GetAppClient().GitHubApps
				.GetInstallationForCurrent(installationId).Result;

			return new Installation(
				installation.Id,
				installation.Account.Login);
		}

		public IReadOnlyCollection<Installation> GetInstallations()
		{
			return GetAppClient().GitHubApps
				.GetAllInstallationsForCurrent().Result
				.Select(installation => new Installation(installation.Id, installation.Account.Login))
				.ToArray();
		}

		public IReadOnlyCollection<Repository> GetRepositories(long installationId)
		{
			return GetClient(installationId).GitHubApps.Installation
				.GetAllRepositoriesForCurrent().Result.Repositories
				.Select(repository => 
					new Repository(
						repository.Id,
						repository.Name,
						repository.FullName,
						repository.Owner.Login,
						repository.CreatedAt.UtcDateTime))
				.ToArray();
		}

		public Repository GetRepository(long installationId, long repositoryId)
		{
			var repository = GetClient(installationId).Repository
				.Get(repositoryId).Result;

			return new Repository(
				repositoryId,
				repository.Name,
				repository.FullName,
				repository.Owner.Login,
				repository.CreatedAt.UtcDateTime);
		}

		public IReadOnlyCollection<Issue> GetRepositoryIssues(long installationId, long repositoryId)
		{
			var repository = GetRepository(installationId, repositoryId);

			// Look for variables names in CompileIssuesQuery.
			var queryVariables = new Dictionary<string, object> {
				{ "name", repository.Name },
				{ "login", repository.OwnerLogin }
			};

			return GetConnection(installationId)
				.Run(_repositoryIssuesQuery, queryVariables).Result
				.Select(issueModel =>
					new Issue(
						issueModel.Id,
						issueModel.CreateAt.UtcDateTime,
						issueModel.UpdateAt.UtcDateTime,
						issueModel.ClsoedAt?.UtcDateTime,
						issueModel.Title,
						issueModel.Labels.ToArray(),
						issueModel.Projects
							.Select(project => new Project( project.Id, project.Name))
							.ToArray(),
						issueModel.State))
				.ToArray();
		}

		public IReadOnlyCollection<Project> GetRepositoryProjects(long installationId, long repositoryId)
		{
			return GetClient(installationId).Repository.Project
				.GetAllForRepository(repositoryId).Result
				.Select(project => new Project(project.Id, project.Name))
				.ToArray();
		}
	}
}
