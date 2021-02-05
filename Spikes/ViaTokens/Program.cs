using Octokit;
using System.Linq;
using System.Threading.Tasks;

using static System.Console;

namespace ViaTokens
{
	class Program
	{
		static async Task Main(string[] args)
		{
			// Токен, сгенерированный на https://github.com в настройках пользователя или организации.
			var token = args[0];

			// В область видимости токена попадают все репозитории, к которым есть доступ
			// у пользователя или организации. Поэтому необходимо получать название репозитория отдельно.
			var repoName = args[1];

			// Авторизация программы в GitHub API.
			var client = new GitHubClient(new ProductHeaderValue("GiraBot")) {
				Credentials = new Credentials(token)
			};

			var repoIssues = await client.Issue.GetAllForRepository(
				(await client.User.Current()).Login,
				repoName,
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
