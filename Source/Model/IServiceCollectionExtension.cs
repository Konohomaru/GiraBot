using Microsoft.Extensions.DependencyInjection;

namespace Model
{
	public static class IServiceCollectionExtension
	{
		public static void AddGiraModel(this IServiceCollection services, ITasksDataSource tasksDataSource)
		{
			services.AddSingleton(tasksDataSource);
			services.AddSingleton<ICalendar, GiraCalendar>();
			services.AddSingleton<Burndown>();
			services.AddSingleton<Velocity>();
			services.AddSingleton<TaskCycleTime>();
			services.AddSingleton<IProjectsRepository, ProjectsRepository>();
		}
	}
}
