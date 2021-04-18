using Model;

namespace WebAPI
{
	public class RepositoryProjectDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public static RepositoryProjectDto BuildFrom(RepositoryProject project)
		{
			if (project is null)
				return default;

			return new RepositoryProjectDto {
				Id = project.Id,
				Name = project.Name
			};
		}
	}
}
