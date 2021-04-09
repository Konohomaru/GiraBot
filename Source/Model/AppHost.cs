using GitHub;
using SimpleInjector;
using System;

namespace Model
{
	public class AppHost
	{
		public static AppHost Instance { get; } = new AppHost();

		public string Pem { get; set; }

		private Container Dependencies { get; } = new Container();

		private AppHost()
		{
			SetDependencies(Dependencies);

			Dependencies.Verify();
		}

		public TService Get<TService>() where TService : class => Dependencies.GetInstance<TService>();

		public DateTime GetTodayUtc() => DateTime.UtcNow;

		private void SetDependencies(Container dependencies)
		{
			// Clients & bridges.
			dependencies.Register(() => new GitHubBridge(Pem));

			// Comparers.
			dependencies.Register<ProjectComparerByName>();

			// Metrics.
			dependencies.Register<Burndown>();
			dependencies.Register<Velocity>();
			dependencies.Register<IssueCycleTime>();

			// Data sources.
			dependencies.Register<ReposDataSource>();
			dependencies.Register<SprintsDataSource>();
		}
	}
}
