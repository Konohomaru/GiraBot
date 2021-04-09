using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System.Collections.Generic;
using System.Linq;

using static Octokit.GraphQL.Variable;

namespace GitHub
{
	public partial class GitHubBridge
	{
		private static ICompiledQuery<IEnumerable<IssueModel>> issuesQuery;

		private static void CompileIssuesQuery()
		{
			issuesQuery = new Query()
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

		public IReadOnlyCollection<Issue> GetRepositoryIssues(long installationId, long repositoryId)
		{
			var repository = GetRepository(installationId, repositoryId);
			var queryVariables = new Dictionary<string, object> {
				{ "name", repository.Name },
				{ "login", repository.Owner.Login }
			};

			return GetConnection(installationId)
				.Run(issuesQuery, queryVariables).Result
				.Select(model => Issue.BuildFrom(model))
				.ToArray();
		}
	}
}
