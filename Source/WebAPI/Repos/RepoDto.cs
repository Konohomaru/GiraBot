using GitHub;
using System;

namespace WebAPI
{
	public class RepoDto
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public DateTime CreatedAt { get; set; }

		public static RepoDto BuildFrom(Repository repository)
		{
			return new RepoDto {
				Id = repository.Id,
				Name = repository.Name,
				CreatedAt = repository.CreatedAt
			};
		}
	}
}
