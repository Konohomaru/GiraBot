using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class TaskCycleTime
	{
		private IProjectsDirectory Directory { get; }

		public TaskCycleTime(IProjectsDirectory directory)
		{
			Directory = directory;
		}

		public IReadOnlyCollection<TaskCycleTimeNode> GetMetric(int projectId)
		{
			return Directory
				.GetProjectTasks(projectId)
				.Select(task => new TaskCycleTimeNode(
					task.Title,
					task.CreatedAt,
					task.ClosedAt,
					task.Labels.ToArray()))
				.ToArray();
		}
	}
}
