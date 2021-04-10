namespace Model
{
	public class Lane
	{
		public int Id { get; private set; }

		public string Name { get; private set; }

		public string MappedLabelName { get; private set; }

		public Lane(int id, string name, string mappedLabelName)
		{
			Id = id;
			Name = name;
			MappedLabelName = mappedLabelName;
		}
	}
}
