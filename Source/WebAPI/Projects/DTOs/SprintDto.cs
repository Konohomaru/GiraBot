using Model;
using System;

namespace WebAPI
{
	public class SprintDto
	{
		public int Id { get; set; }

		public DateTime BeginAt { get; set; }

		public int Length { get; set; }

		public static SprintDto BuildFrom(Sprint sprint)
		{
			return new SprintDto {
				Id = sprint.Id,
				BeginAt = sprint.BeginsAt,
				Length = sprint.Length
			};
		}
	}
}
