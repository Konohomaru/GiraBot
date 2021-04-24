using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class Burndown
	{
		private ICalendar Calendar { get; }

		private IProjectsRepository Repository { get; }

		public Burndown(ICalendar calendar, IProjectsRepository repository)
		{
			Calendar = calendar;
			Repository = repository;
		}

		public IReadOnlyCollection<BurndownNode> GetMetric(int projectId)
		{
			var latestSprint = Repository
				.GetProjectSprints(projectId)
				.Last();

			var sprintTasks = Repository
				.GetProjectTasks(projectId)
				.GetSprintTasks(latestSprint)
				.ToArray();

			return GetMetricNodes(latestSprint, sprintTasks).ToArray();
		}

		private IEnumerable<BurndownNode> GetMetricNodes(Sprint latestSprint, IReadOnlyCollection<GrTask> sprintTasks)
		{
			var today = Calendar.GetCurrentUtcDateTime();
			var iterationDay = latestSprint.BeginsAt;

			while (latestSprint.ContainsDate(iterationDay) && iterationDay <= today) {
				yield return new BurndownNode(
					iterationDay,
					sprintTasks.Count(task => !task.ClosedAt.HasValue || task.ClosedAt > iterationDay));

				iterationDay = iterationDay.AddDays(1);
			}
		}
	}
}
