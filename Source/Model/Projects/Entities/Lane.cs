namespace Model
{
	public class Lane
	{
		public int Id { get; }

		public string Name { get; }

		public string MappedAlias { get; }

		public Lane(int id, string name, string mappedAlias)
		{
			Id = id;
			Name = name;
			MappedAlias = mappedAlias;
		}
	}
}
