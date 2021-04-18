using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class IssueCycleTime
	{
		private GiraProjectsDirectory Directory { get; }

		public IssueCycleTime(GiraProjectsDirectory directory)
		{
			Directory = directory;
		}

		public IReadOnlyCollection<IssueCycleTimeNode> GetMetric(int projectId)
		{
			return Directory
				.GetGiraProjectTasks(projectId)
				.Select(task => new IssueCycleTimeNode(
					task.Title,
					task.CreatedAt,
					task.ClosedAt,
					task.Labels.ToArray()))
				.ToArray();
		}
	}
}
