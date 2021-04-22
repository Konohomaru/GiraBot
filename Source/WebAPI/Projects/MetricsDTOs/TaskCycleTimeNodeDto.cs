using Model;
using System;
using System.Collections.Generic;

namespace WebAPI
{
	public class TaskCycleTimeNodeDto
	{
		public string Title { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? ClosedAt { get; set; }

		public TimeSpan Duration { get; set; }

		public IReadOnlyCollection<string> Labels { get; set; }

		public static TaskCycleTimeNodeDto BuildFrom(TaskCycleTimeNode node)
		{
			return new TaskCycleTimeNodeDto {
				Title = node.Title,
				CreatedAt = node.CreatedAt,
				ClosedAt = node.ClosedAt,
				Labels = node.Labels
			};
		}
	}
}
