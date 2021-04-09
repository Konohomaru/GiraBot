using GitHubJwt;
using Octokit;

using GraphQL = Octokit.GraphQL;

namespace GitHub
{
	public partial class GitHubBridge
	{
		public string Pem { get; private set; }

		static GitHubBridge()
		{
			CompileIssuesQuery();
		}

		public GitHubBridge(string pem)
		{
			Pem = pem;
		}

		/// <summary>
		/// Creates Octokit.GraphQL connection based on Installation GitHub client.
		/// </summary>
		private GraphQL.Connection GetConnection(long installationId)
		{
			return new GraphQL.Connection(
				new GraphQL.ProductHeaderValue($"GiraBotInstallation-{installationId}-GraphQL"),
				GetClient(installationId).Credentials.GetToken());
		}

		/// <summary>
		/// Creates installation GitHub client with time-limited JWT key.
		/// </summary>
		private GitHubClient GetClient(long installationId)
		{
			var appClient = GetAppClient();

			return new GitHubClient(new ProductHeaderValue($"GiraBotInstallation-{installationId}")) {
				Credentials = new Credentials(appClient.GitHubApps.CreateInstallationToken(installationId).Result.Token)
			};
		}

		/// <summary>
		/// Creates GitHub App client with 530 seconds JWT key.
		/// </summary>
		private GitHubClient GetAppClient()
		{
			var generator = new GitHubJwtFactory(
				new StringPrivateKeySource(Pem),
				new GitHubJwtFactoryOptions { AppIntegrationId = 97880, ExpirationSeconds = 530 });

			var jwt = generator.CreateEncodedJwtToken();

			return new GitHubClient(new ProductHeaderValue("GiraBot")) {
				Credentials = new Credentials(jwt, AuthenticationType.Bearer)
			};
		}
	}
}
