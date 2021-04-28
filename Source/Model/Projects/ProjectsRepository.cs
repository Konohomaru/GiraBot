using System.Collections.Generic;

namespace Model
{
	public class ProjectsRepository : IProjectsRepository
	{
		private ICalendar Calendar { get; }

		private ICardsDataSource DataSource { get; }

		public ProjectsRepository(ICalendar calendar, ICardsDataSource dataSource)
		{
			Calendar = calendar;
			DataSource = dataSource;
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
					swimlanes: new[] {
						new Swimlane(0, "Bugs", "bug"),
						new Swimlane(1, "Tech Debts", "tech debt"),
						new Swimlane(2, "Road Map", "road map")
					},
					allowedProjectIds: new[] { 4241787 }));
		}

		public IEnumerable<Sprint> GetProjectSprints(int projectId)
		{
			var project = GetProject(projectId);
			var iterationSprntBeginingDay = project.BeginSprintsAt;
			var today = Calendar.GetCurrentUtcDateTime();

			while (iterationSprntBeginingDay <= today)
			{
				yield return new Sprint(
					id: iterationSprntBeginingDay.DayOfYear,
					beginAt: iterationSprntBeginingDay,
					length: project.SprintLength);

				iterationSprntBeginingDay = iterationSprntBeginingDay.AddDays(project.SprintLength);
			}
		}

		public IReadOnlyCollection<Card> GetProjectCards(int projectId)
		{
			return DataSource.GetProjectCards(GetProject(projectId));
		}
	}
}
