using System.Collections.Generic;
using System.Linq;

namespace GitHub
{
	public partial class GitHubBridge
	{
		public Installation GetInstallation(long installationId)
		{
			var installation = GetAppClient().GitHubApps.GetInstallationForCurrent(installationId).Result;

			return Installation.BuildFrom(installation, Owner.BuildFrom(installation.Account));
		}

		public IReadOnlyCollection<Installation> GetInstallations()
		{
			return GetAppClient().GitHubApps
				.GetAllInstallationsForCurrent().Result
				.Select(installation => Installation.BuildFrom(installation, Owner.BuildFrom(installation.Account)))
				.ToArray();
		}
	}
}
