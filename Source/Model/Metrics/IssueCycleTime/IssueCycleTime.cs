using GitHub;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class IssueCycleTime
	{
		public IReadOnlyCollection<IssueCycleTimeNode> GetMetric(long installationId, long repoId)
		{
			var repoIssues = AppHost.Instance
				.Get<ReposDataSource>()
				.GetRepoIssues(installationId, repoId);

			return GetMetricNodes(repoIssues).ToArray();
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
