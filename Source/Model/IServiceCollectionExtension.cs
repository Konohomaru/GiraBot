using Microsoft.Extensions.DependencyInjection;

namespace Model
{
	public static class IServiceCollectionExtension
	{
		public static void AddGiraModel(this IServiceCollection services, string gitHubPem)
		{
			services.AddTransient<IGitHubFacade, GiraGitHubFacade>(_ => new GiraGitHubFacade(gitHubPem));
			services.AddSingleton<ICalendar, GiraCalendar>();
			services.AddSingleton<Burndown>();
			services.AddSingleton<Velocity>();
			services.AddSingleton<IssueCycleTime>();
			services.AddSingleton<GiraProjectsDirectory>();
		}
	}
}
