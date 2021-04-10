using GitHub;

namespace WebAPI
{
	public class InstallationDto
	{
		public long Id { get; set; }

		public string Owner { get; set; }

		public static InstallationDto BuildFrom(Installation installation)
		{
			return new InstallationDto {
				Id = installation.Id,
				Owner = installation.Owner.Login
			};
		}
	}
}
