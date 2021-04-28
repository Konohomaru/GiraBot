using Model;

namespace WebAPI
{
	public class SwimlaneDto
	{
		public string Name { get; set; }

		public string MappedAlias { get; set; }

		public static SwimlaneDto BuildFrom(Swimlane swimlane)
		{
			return new SwimlaneDto {
				Name = swimlane.Name,
				MappedAlias = swimlane.MappedAlias
			};
		}
	}
}
