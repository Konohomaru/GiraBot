using Model;
using System;
using System.Collections.Generic;

namespace WebAPI
{
	public class IssueCycleTimeNodeDto
	{
		public string Title { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? ClosedAt { get; set; }

		public TimeSpan Duration { get; set; }

		public IReadOnlyCollection<string> Labels { get; set; }

		public static IssueCycleTimeNodeDto BuildFrom(IssueCycleTimeNode node)
		{
			return new IssueCycleTimeNodeDto {
				Title = node.Title,
				CreatedAt = node.CreatedAt,
				ClosedAt = node.ClosedAt,
				Duration = node.Duration,
				Labels = node.Labels
			};
		}
	}
}
