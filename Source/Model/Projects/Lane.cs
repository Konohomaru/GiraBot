namespace Model
{
	public class Lane
	{
		public int Id { get; private set; }

		public string Name { get; private set; }

		public string MappedName { get; private set; }

		public Lane(int id, string name, string mappedName)
		{
			Id = id;
			Name = name;
			MappedName = mappedName;
		}
	}
}
