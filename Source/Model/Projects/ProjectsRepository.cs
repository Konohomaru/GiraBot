using System.Collections.Generic;

namespace Model
{
	public class ProjectsRepository : IProjectsRepository
	{
		private ICalendar Calendar { get; }

		private ITasksDataSource TasksDataSource { get; }

		public ProjectsRepository(ICalendar calendar, ITasksDataSource tasksDataSource)
		{
			Calendar = calendar;
			TasksDataSource = tasksDataSource;
		}

		public Project GetProject(int projectId)
		{
			return new Project(
				id: 0,
				name: "mindbox-moscow/issues-devx",
				beginSprintsAt: new(2021, 01, 01),
				gitHubSettings: new GitHubSettings(
					installationId: 15161810,
					repositoryId: 229932826,
					lanes: new[] {
						new Lane(0, "Bugs", "bug"),
						new Lane(1, "Tech Debts", "tech debt"),
						new Lane(2, "Road Map", "road map")
					},
					allowedProjectIds: new[] { 3720514 }));
		}

		public IEnumerable<Sprint> GetProjectSprints(int projectId)
		{
			var giraProject = GetProject(projectId);
			var iterationSprntBeginingDay = giraProject.BeginSprintsAt;
			var today = Calendar.GetCurrentUtcDateTime();

			while (iterationSprntBeginingDay <= today)
			{
				yield return new Sprint(
					id: iterationSprntBeginingDay.DayOfYear,
					beginAt: iterationSprntBeginingDay,
					length: giraProject.SprintLength);

				iterationSprntBeginingDay = iterationSprntBeginingDay.AddDays(giraProject.SprintLength);
			}
		}

		public IReadOnlyCollection<GrTask> GetProjectTasks(int projectId)
		{
			return TasksDataSource.GetProjectTasks(GetProject(projectId));
		}
	}
}
