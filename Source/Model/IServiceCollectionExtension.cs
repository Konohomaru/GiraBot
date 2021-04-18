using Microsoft.Extensions.DependencyInjection;

namespace Model
{
	public static class IServiceCollectionExtension
	{
		public static void AddGiraModel(this IServiceCollection services, string gitHubPem)
		{
			services.AddTransient<IGitHubWrapper, GiraGitHubWrapper>(_ => new GiraGitHubWrapper(gitHubPem));
			services.AddTransient<IGitHubFacade, GiraGitHubFacade>();
			services.AddSingleton<ICalendar, GiraCalendar>();
			services.AddSingleton<Burndown>();
			services.AddSingleton<Velocity>();
			services.AddSingleton<IssueCycleTime>();
			services.AddSingleton<ReposDataSource>();
			services.AddSingleton<SprintsDataSource>();
			services.AddSingleton<GiraProjectsDirectory>();
		}
	}
}
