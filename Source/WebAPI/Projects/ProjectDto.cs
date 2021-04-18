using Model;

namespace WebAPI
{
	public class ProjectDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public static ProjectDto BuildFrom(Project project)
		{
			if (project is null)
				return default;

			return new ProjectDto {
				Id = project.Id,
				Name = project.Name
			};
		}
	}
}
