namespace Model
{
	public class Swimlane
	{
		public int Id { get; }

		public string Name { get; }

		public string MappedAlias { get; }

		public Swimlane(int id, string name, string mappedAlias)
		{
			Id = id;
			Name = name;
			MappedAlias = mappedAlias;
		}
	}
}
