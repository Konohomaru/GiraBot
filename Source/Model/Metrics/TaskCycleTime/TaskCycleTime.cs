using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class TaskCycleTime
	{
		private IProjectsRepository Repository { get; }

		public TaskCycleTime(IProjectsRepository repository)
		{
			Repository = repository;
		}

		public IReadOnlyCollection<TaskCycleTimeNode> GetMetric(int projectId)
		{
			return Repository
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
