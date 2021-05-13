using Model;
using System;
using System.Linq;

namespace WebAPI
{
	public class CardDto
	{
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? ClosedAt { get; set; }

		public string Title { get; set; }

		public string[] Labels { get; set; }

		public static CardDto BuildFrom(Card card)
		{
			return new CardDto {
				Id = card.Id,
				CreatedAt = card.CreatedAt,
				ClosedAt = card.ClosedAt,
				Title = card.Title,
				Labels = card.Labels.ToArray()
			};
		}
	}
}
