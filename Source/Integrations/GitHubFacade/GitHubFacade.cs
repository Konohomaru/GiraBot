using GitHubJwt;
using Octokit;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System.Collections.Generic;
using System.Linq;

using static Octokit.GraphQL.Variable;

using GqlConnection = Octokit.GraphQL.Connection;
using GqlProductHeader = Octokit.GraphQL.ProductHeaderValue;
using OctoProductHeader = Octokit.ProductHeaderValue;

namespace Integrations
{
	public class GitHubFacade : IGitHubFacade
	{
		private static ICompiledQuery<IEnumerable<Issue>> _repositoryIssuesQuery;

		private string Pem { get; }

		static GitHubFacade()
		{
			CompileRepositoryIssuesQuery();
		}

		public GitHubFacade(string pem)
		{
			Pem = pem;
		}

		private static void CompileRepositoryIssuesQuery()
		{
			_repositoryIssuesQuery = new Query()
				.Repository(Var("name"), Var("login"))
				.Issues(orderBy: new IssueOrder { Field = IssueOrderField.UpdatedAt })
				.AllPages()
				.Select(issue => new Issue(
					issue.DatabaseId.Value,
					issue.CreatedAt.UtcDateTime,
					issue.ClosedAt != null ?
						issue.ClosedAt.Value.UtcDateTime :
						null,
					issue.Title,
					issue
						.Labels(null, null, null, null, null)
						.AllPages()
						.Select(label => label.Name)
						.ToList()
						.ToArray(),
					issue
						.ProjectCards(null, null, null, null, null)
						.AllPages()
						.Select(projectCard => projectCard.Project.DatabaseId.Value)
						.ToList()
						.ToArray()))
				.Compile();
		}

		public IReadOnlyCollection<Issue> GetRepositoryIssues(long installationId, long repositoryId)
		{
			var repository = GetClient(installationId).Repository
				.Get(repositoryId).Result;

			// Look for variables names in CompileIssuesQuery.
			var queryVariables = new Dictionary<string, object> {
				{ "name", repository.Name },
				{ "login", repository.Owner.Login }
			};

			return GetConnection(installationId)
				.Run(_repositoryIssuesQuery, queryVariables).Result
				.ToArray();
		}

		private GqlConnection GetConnection(long installationId)
		{
			return new GqlConnection(
				new GqlProductHeader($"GiraBotInstallation-{installationId}-GraphQL"),
				token: GetClient(installationId).Credentials.GetToken());
		}

		private GitHubClient GetClient(long installationId)
		{
			var token = GetClient().GitHubApps.CreateInstallationToken(installationId).Result.Token;

			return new GitHubClient(new OctoProductHeader($"GiraBotInstallation-{installationId}")) {
				Credentials = new Credentials(token)
			};
		}

		private GitHubClient GetClient()
		{
			var generator = new GitHubJwtFactory(
				new StringPrivateKeySource(Pem),
				new GitHubJwtFactoryOptions {
					AppIntegrationId = 97880,
					ExpirationSeconds = 530
				});

			var jwt = generator.CreateEncodedJwtToken();

			return new GitHubClient(new OctoProductHeader("GiraBot")) {
				Credentials = new Credentials(jwt, AuthenticationType.Bearer)
			};
		}
	}
}
