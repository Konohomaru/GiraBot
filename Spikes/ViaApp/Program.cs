using GitHubJwt;
using Octokit;
using System.Linq;
using System.Threading.Tasks;

using static System.Console;

namespace ViaApp
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var pathToPemFile = args[0];

			var generator = new GitHubJwtFactory(
				new FilePrivateKeySource(pathToPemFile),
				new GitHubJwtFactoryOptions { AppIntegrationId = 97880, ExpirationSeconds = 600 });

			var jwt = generator.CreateEncodedJwtToken();

			// Авторизация в GitHub API как GitHub App.
			var client = new GitHubClient(new ProductHeaderValue("GiraBot")) {
				Credentials = new Credentials(jwt, AuthenticationType.Bearer)
			};

			// Чтобы найти установку именно для тестирования.
			var installationId = int.Parse(args[1]);

			// Авторизация в GitHub API как конкретный GitHub App Installation.
			var installationClient = new GitHubClient(new ProductHeaderValue($"GiraBotInstallation-{installationId}")) {
				Credentials = new Credentials((await client.GitHubApps.CreateInstallationToken(installationId)).Token)
			};

			var repo = (await installationClient.GitHubApps.Installation.GetAllRepositoriesForCurrent()).Repositories.Single();

			var repoIssues = await installationClient.Issue.GetAllForRepository(
				repo.Id, 
				new RepositoryIssueRequest { State = ItemStateFilter.All });

			WriteLine("Processed issues:");
			foreach (var issue in repoIssues)
				WriteLine($"\t{issue.Number}. {issue.Title} (state: {issue.State}).");

			// Считаем число закрытых issues, которые будут закрытыми задачами.
			var closedIssuesCount = repoIssues.Count(issue => issue.State == ItemState.Closed);

			WriteLine($"Sprint Burndown is {closedIssuesCount}/{repoIssues.Count}.");

			// Чтобы консольное окно не закрывалось сразу после вывода.
			ReadKey();
		}
	}
}
