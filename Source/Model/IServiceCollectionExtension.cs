using Microsoft.Extensions.DependencyInjection;

namespace Model
{
	public static class IServiceCollectionExtension
	{
		public static void AddGiraModel(this IServiceCollection services, string gitHubPem)
		{
			services.AddTransient<IGitHubWrapper, GiraGitHubWrapper>(_ => new GiraGitHubWrapper(gitHubPem));
			services.AddSingleton<ICalendar, GiraCalendar>();
			services.AddSingleton<ProjectComparerByName>();
			services.AddSingleton<Burndown>();
			services.AddSingleton<Velocity>();
			services.AddSingleton<IssueCycleTime>();
			services.AddSingleton<ReposDataSource>();
			services.AddSingleton<SprintsDataSource>();
		}
	}
}
