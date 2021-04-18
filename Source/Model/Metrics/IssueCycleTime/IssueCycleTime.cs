using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class IssueCycleTime
	{
		private ReposDataSource Repos { get; }

		public IssueCycleTime(ReposDataSource repos)
		{
			Repos = repos;
		}

		public IReadOnlyCollection<IssueCycleTimeNode> GetMetric(long installationId, long repoId)
		{
			var repoIssues = Repos.GetRepoIssues(installationId, repoId);

			return GetMetricNodes(repoIssues)
				.ToArray();
		}

		private IEnumerable<IssueCycleTimeNode> GetMetricNodes(IReadOnlyCollection<Issue> repoIssues)
		{
			foreach (var issue in repoIssues) {
				yield return new IssueCycleTimeNode(
					issue.Title,
					issue.CreateAt,
					issue.ClosedAt,
					issue.Labels.ToArray());
			}
		}
	}
}
