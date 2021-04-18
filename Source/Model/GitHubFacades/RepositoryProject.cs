namespace Model
{
	public class RepositoryProject
	{
		public int Id { get; private set; }

		public string Name { get; private set; }

		public RepositoryProject(int id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}
