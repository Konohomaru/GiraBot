namespace WebAPI
{
	public class GiraProjectDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int SprintDefaultDaysCount { get; set; }

		public long InstallationId { get; set; }

		public long RepositoryId { get; set; }

		public LaneDto[] Lanes { get; set; }

		public RepositoryProjectDto[] AllowedProjects { get; set; }
	}
}
