using System;

namespace Model
{
	public class Repository
	{
		public long Id { get; private set; }

		public string Name { get; private set; }

		public string FullName { get; private set; }

		public string Owner { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public Repository(long id, string name,string fullName, string owner, DateTime createdAt)
		{
			Id = id;
			Name = name;
			FullName = fullName;
			Owner = owner;
			CreatedAt = createdAt;
		}
	}
}
