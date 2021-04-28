using GitHubJwt;
using Octokit;
using System.Linq;

using static System.Console;

namespace GitHubCLI
{
	class Program
	{
		static readonly string PathToPem = "girabot.pem";

		static void Main(string[] args)
		{
			var commandName = args.First();
			var commandParams = args.Skip(1).ToArray();

			switch (commandName) {
				default:
					WriteLine("Command not recognized.");
					break;

				case "installations":
					PrintInstallations();
					break;

				case "repositories":
					PrintRepositories(long.Parse(commandParams[0]));
					break;

				case "projects":
					PrintProjects(long.Parse(commandParams[0]), long.Parse(commandParams[1]));
					break;

				case "issues":
					PrintIssues(
						installationId: long.Parse(commandParams[0]),
						repositoryId: long.Parse(commandParams[1]),
						projectId: int.Parse(commandParams[2])); 
					break;
			}
		}

		private static void PrintInstallations()
		{
			var installations = GetClient().GitHubApps
				.GetAllInstallationsForCurrent().Result;

			foreach (var installation in installations) {
				WriteLine($"{installation.Account.Login}: {installation.Id}");
			}
		}

		private static void PrintRepositories(long installationId)
		{
			var repositories = GetClient(installationId).GitHubApps.Installation
				.GetAllRepositoriesForCurrent().Result.Repositories;

			foreach (var repository in repositories) {
				WriteLine($"{repository.FullName}: {repository.Id}, {repository.Description}");
			}
		}

		private static void PrintProjects(long installationId, long repositoryId)
		{
			var projects = GetClient(installationId).Repository.Project
				.GetAllForRepository(repositoryId).Result;

			foreach (var project in projects) {
				WriteLine($"{project.Name}: {project.Id}");
			}
		}

		private static void PrintIssues(long installationId, long repositoryId, int projectId)
		{
			var projectColumns = GetClient(installationId)
				.Repository.Project.Column
				.GetAll(projectId).Result;

			foreach (var column in projectColumns) {
				var columnsCards = GetClient(installationId)
					.Repository.Project.Card
					.GetAll(column.Id).Result;

				foreach (var card in columnsCards) {
					WriteLine($"{column.Name}: {card.Note}: {card.Id}");
				}
			}
		}

		private static GitHubClient GetClient(long installationId)
		{
			var token = GetClient().GitHubApps.CreateInstallationToken(installationId).Result.Token;

			return new GitHubClient(new ProductHeaderValue($"GitHubCLIInstallation-{installationId}")) {
				Credentials = new Credentials(token)
			};
		}

		private static GitHubClient GetClient()
		{
			var generator = new GitHubJwtFactory(
				new FilePrivateKeySource(PathToPem),
				new GitHubJwtFactoryOptions {
					AppIntegrationId = 97880,
					ExpirationSeconds = 530
				});

			var jwt = generator.CreateEncodedJwtToken();

			return new GitHubClient(new ProductHeaderValue("GitHubCLI")) {
				Credentials = new Credentials(jwt, AuthenticationType.Bearer)
			};
		}
	}
}
