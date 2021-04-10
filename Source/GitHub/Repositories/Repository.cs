using System;

namespace GitHub
{
	public class Repository
	{
		public long Id { get; private set; }

		public string Name { get; private set; }

		public string FullName { get; private set; }

		public Owner Owner { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public Repository(long id, string name,string fullName, Owner owner, DateTime createdAt)
		{
			Id = id;
			Name = name;
			FullName = fullName;
			Owner = owner;
			CreatedAt = createdAt;
		}

		internal static Repository BuildFrom(Octokit.Repository repository, Owner owner)
		{
			return new Repository(
				repository.Id,
				repository.Name,
				repository.FullName,
				owner,
				repository.CreatedAt.UtcDateTime);
		}
	}
}
