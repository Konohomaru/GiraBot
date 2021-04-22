using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class Burndown
	{
		private ICalendar Calendar { get; }

		private IProjectsRepository Directory { get; }

		public Burndown(ICalendar calendar, IProjectsRepository directory)
		{
			Calendar = calendar;
			Directory = directory;
		}

		public IReadOnlyCollection<BurndownNode> GetMetric(int projectId)
		{
			var sprint = Directory
				.GetProjectSprints(projectId)
				.Last();

			var sprintTasks = Directory
				.GetProjectTasks(projectId)
				.GetSprintTasks(sprint)
				.ToArray();

			return GetMetricNodes(sprint, sprintTasks).ToArray();
		}

		private IEnumerable<BurndownNode> GetMetricNodes(Sprint sprint, IReadOnlyCollection<GiraTask> sprintTasks)
		{
			var today = Calendar.GetCurrentUtcDateTime();
			var currentDay = sprint.BeginsAt;

			while (sprint.ContainsDate(currentDay) && currentDay <= today) {
				yield return new BurndownNode(
					currentDay,
					sprintTasks.Count(issue =>
						issue.CreatedAt <= currentDay &&
						(!issue.ClosedAt.HasValue || issue.ClosedAt > currentDay)));
				currentDay = currentDay.AddDays(1);
			}
		}
	}
}
