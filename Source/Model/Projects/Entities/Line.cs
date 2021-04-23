namespace Model
{
	public class Line
	{
		public int Id { get; private set; }

		public string Name { get; private set; }

		public string MappedAlias { get; private set; }

		public Line(int id, string name, string mappedName)
		{
			Id = id;
			Name = name;
			MappedAlias = mappedName;
		}
	}
}
