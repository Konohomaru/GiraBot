using Model;

namespace WebAPI
{
	public class InstallationDto
	{
		public long Id { get; set; }

		public string OwnerLogin { get; set; }

		public static InstallationDto BuildFrom(Installation installation)
		{
			return new InstallationDto {
				Id = installation.Id,
				OwnerLogin = installation.OwnerLogin
			};
		}
	}
}
