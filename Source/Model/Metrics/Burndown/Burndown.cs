using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class Burndown
	{
		private ICalendar Calendar { get; }

		private ProjectsDirectory Directory { get; }

		public Burndown(ICalendar calendar, ProjectsDirectory directory)
		{
			Calendar = calendar;
			Directory = directory;
		}

		public IReadOnlyCollection<BurndownNode> GetMetric(int projectId)
		{
			var sprint = Directory
				.GetGiraProjectSprints(projectId)
				.Last();

			var sprintTasks = Directory
				.GetGiraProjectTasks(projectId)
				.GetSprintTasks(sprint)
				.ToArray();

			return GetMetricNodes(sprint, sprintTasks).ToArray();
		}

		private IEnumerable<BurndownNode> GetMetricNodes(Sprint sprint, IReadOnlyCollection<GiraTask> sprintTasks)
		{
			var today = Calendar.GetCurrentUtcDateTime();
			var currentDay = sprint.BeginAt;

			while (sprint.ContainesDate(currentDay) && currentDay <= today) {
				yield return new BurndownNode(
					currentDay,
					sprintTasks.Count(issue => issue.ClosedAt is null || issue.ClosedAt <= currentDay));
				currentDay = currentDay.AddDays(1);
			}
		}
	}
}
