using System;

namespace Model
{
	public class RepositoryProject : IComparable
	{
		public int Id { get; private set; }

		public string Name { get; private set; }

		public RepositoryProject(int id, string name)
		{
			Id = id;
			Name = name;
		}

		public int CompareTo(object obj)
		{
			return Id.CompareTo((obj as RepositoryProject).Id);
		}
	}
}
