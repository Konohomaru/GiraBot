using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class Velocity
	{
		private ICalendar Calendar { get; }

		private ProjectsDirectory Directory { get; }

		public Velocity(ICalendar calendar, ProjectsDirectory directory)
		{
			Calendar = calendar;
			Directory = directory;
		}

		public IReadOnlyCollection<VelocityNode> GetMetric(int projectId)
		{
			var lanes = Directory.GetGiraProject(projectId).GitHubSettings.Lanes;

			var sprint = Directory
				.GetGiraProjectSprints(projectId)
				.Last();

			var sprintTasks = Directory
				.GetGiraProjectTasks(projectId)
				.GetSprintTasks(sprint)
				.ToArray();

			return GetMtricNodes(sprint, lanes, sprintTasks).ToArray();
		}

		private IEnumerable<VelocityNode> GetMtricNodes(
			Sprint sprint,
			IReadOnlyCollection<Lane> lanes,
			IReadOnlyCollection<GiraTask> sprintTasks)
		{
			var today = Calendar.GetCurrentUtcDateTime();
			var currentDay = sprint.BeginAt;

			while (sprint.ContainesDate(currentDay) && currentDay <= today) {
				yield return new VelocityNode(
					currentDay,
					lanes.ToDictionary(
						keySelector: lane => lane,
						elementSelector: lane => sprintTasks
							.Where(task => task.ClosedAt.HasValue)
							.Count(task => task.Labels.Contains(lane.MappedName))));
				currentDay = currentDay.AddDays(1);
			}
		}
	}
}
