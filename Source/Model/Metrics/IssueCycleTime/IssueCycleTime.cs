using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class IssueCycleTime
	{
		private IProjectsDirectory Directory { get; }

		public IssueCycleTime(IProjectsDirectory directory)
		{
			Directory = directory;
		}

		public IReadOnlyCollection<IssueCycleTimeNode> GetMetric(int projectId)
		{
			return Directory
				.GetProjectTasks(projectId)
				.Select(task => new IssueCycleTimeNode(
					task.Title,
					task.CreatedAt,
					task.ClosedAt,
					task.Labels.ToArray()))
				.ToArray();
		}
	}
}
