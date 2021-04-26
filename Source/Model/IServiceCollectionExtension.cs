using Microsoft.Extensions.DependencyInjection;

namespace Model
{
	public static class IServiceCollectionExtension
	{
		public static void AddGiraModel(this IServiceCollection services, ICardsDataSource dataSource)
		{
			services.AddSingleton(dataSource);
			services.AddSingleton<ICalendar, Calendar>();
			services.AddSingleton<Burndown>();
			services.AddSingleton<Velocity>();
			services.AddSingleton<CardCycleTime>();
			services.AddSingleton<IProjectsRepository, ProjectsRepository>();
		}
	}
}
