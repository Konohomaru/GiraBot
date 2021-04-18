using Model;
using System;

namespace WebAPI
{
	public class SprintDto
	{
		public int Id { get; set; }

		public DateTime BeginAt { get; set; }

		/// <summary>
		/// In count of days.
		/// </summary>
		public int Duration { get; set; }

		public static SprintDto BuildFrom(Sprint sprint)
		{
			if (sprint is null)
				return default;

			return new SprintDto {
				Id = sprint.Id,
				BeginAt = sprint.BeginAt,
				Duration = sprint.Duration
			};
		}
	}
}
