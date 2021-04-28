using Model;
using System;
using System.Collections.Generic;

namespace WebAPI
{
	public class CardCycleTimeNodeDto
	{
		public string Title { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? ClosedAt { get; set; }

		public IReadOnlyCollection<string> Labels { get; set; }

		public static CardCycleTimeNodeDto BuildFrom(CardCycleTimeNode node)
		{
			return new CardCycleTimeNodeDto {
				Title = node.Title,
				CreatedAt = node.CreatedAt,
				ClosedAt = node.ClosedAt,
				Labels = node.Labels
			};
		}
	}
}
