using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class Velocity
	{
		private ICalendar Calendar { get; }

		private IProjectsRepository Repository { get; }

		public Velocity(ICalendar calendar, IProjectsRepository repository)
		{
			Calendar = calendar;
			Repository = repository;
		}

		public IReadOnlyCollection<VelocityNode> GetMetric(int projectId)
		{
			var projectLines = Repository.GetProject(projectId).GitHubSettings.Lanes;

			var latestSprint = Repository
				.GetProjectSprints(projectId)
				.Last();

			var sprintTasks = Repository
				.GetProjectTasks(projectId)
				.GetSprintTasks(latestSprint)
				.ToArray();

			return GetMetricNodes(latestSprint, projectLines, sprintTasks).ToArray();
		}

		private IEnumerable<VelocityNode> GetMetricNodes(
			Sprint latestSprint,
			IReadOnlyCollection<Line> lines,
			IReadOnlyCollection<GiraTask> sprintTasks)
		{
			var today = Calendar.GetCurrentUtcDateTime();
			var iterationDay = latestSprint.BeginsAt;

			while (latestSprint.ContainsDate(iterationDay) && iterationDay <= today) {
				yield return new VelocityNode(
					iterationDay,
					lines.ToDictionary(
						keySelector: line => line,
						elementSelector: line => sprintTasks
							.Where(task => task.ClosedAt <= iterationDay)
							.Count(task => task.Labels.Contains(line.MappedAlias))));
				iterationDay = iterationDay.AddDays(1);
			}
		}
	}
}
