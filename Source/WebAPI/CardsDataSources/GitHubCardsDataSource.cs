using GitHubJwt;
using Model;
using System.Collections.Generic;
using System.Linq;
using static Octokit.CredentialsExtensions;
using static Octokit.GraphQL.ConnectionExtensions;
using static Octokit.GraphQL.PagingConnectionExtensions;
using static Octokit.GraphQL.QueryableListExtensions;
using static Octokit.GraphQL.Variable;
using Gql = Octokit.GraphQL;
using GqlModel = Octokit.GraphQL.Model;
using Octo = Octokit;

namespace WebAPI
{
	public class GitHubCardsDataSource : ICardsDataSource
	{
		private static Gql.ICompiledQuery<IEnumerable<Issue>> _repositoryIssuesQuery;

		private string Pem { get; }

		static GitHubCardsDataSource()
		{
			CompileRepositoryIssuesQuery();
		}

		public GitHubCardsDataSource(string pem)
		{
			Pem = pem;
		}

		public IReadOnlyCollection<Card> GetProjectCards(Project project)
		{
			return GetRepositoryIssues(
					installationId: project.GitHubSettings.InstallationId,
					repositoryId: project.GitHubSettings.RepositoryId)
				.Where(issue => issue.Locations
					.Select(location => location.ProjectId)
					.ContainsAnyOf(project.GitHubSettings.AllowedProjectIds))
				.Select(issue => new Card(
					id: issue.Id,
					createdAt: issue.CreatedAt,
					closedAt: issue.ClosedAt,
					title: issue.Title,
					labels: issue.Labels))
				.ToArray();
		}

		private static void CompileRepositoryIssuesQuery()
		{
			_repositoryIssuesQuery = new Gql.Query()
				.Repository(Var("name"), Var("login"))
				.Issues(orderBy: new GqlModel.IssueOrder {
					Field = GqlModel.IssueOrderField.CreatedAt,
					Direction = GqlModel.OrderDirection.Desc
				})
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
						.Select(projectCard => new IssueLocation(
							projectCard.Project.DatabaseId.Value,
							projectCard.Project.Name,
							projectCard.Column.Name))
						.ToList()
						.ToArray()))
				.Compile();
		}

		private IEnumerable<Issue> GetRepositoryIssues(long installationId, long repositoryId)
		{
			var repository = GetClient(installationId).Repository
				.Get(repositoryId).Result;

			// Look for variable names in query compilation code.
			var queryVariables = new Dictionary<string, object> {
				{ "name", repository.Name },
				{ "login", repository.Owner.Login }
			};

			return GetConnection(installationId)
				.Run(_repositoryIssuesQuery, queryVariables).Result
				.ToArray();
		}

		private Gql.Connection GetConnection(long installationId)
		{
			return new Gql.Connection(
				new Gql.ProductHeaderValue($"GiraBotInstallation-{installationId}-GraphQL"),
				token: GetClient(installationId).Credentials.GetToken());
		}

		private Octo.GitHubClient GetClient(long installationId)
		{
			var token = GetClient().GitHubApps.CreateInstallationToken(installationId).Result.Token;

			return new Octo.GitHubClient(new Octo.ProductHeaderValue($"GiraBotInstallation-{installationId}")) {
				Credentials = new Octo.Credentials(token)
			};
		}

		private Octo.GitHubClient GetClient()
		{
			var generator = new GitHubJwtFactory(
				new StringPrivateKeySource(Pem),
				new GitHubJwtFactoryOptions {
					AppIntegrationId = 97880,
					ExpirationSeconds = 530
				});

			var jwt = generator.CreateEncodedJwtToken();

			return new Octo.GitHubClient(new Octo.ProductHeaderValue("GiraBot")) {
				Credentials = new Octo.Credentials(jwt, Octo.AuthenticationType.Bearer)
			};
		}
	}
}
