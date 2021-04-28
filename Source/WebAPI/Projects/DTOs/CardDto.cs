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

		public static CardDto BuildFrom(Card giraTask)
		{
			return new CardDto {
				Id = giraTask.Id,
				CreatedAt = giraTask.CreatedAt,
				ClosedAt = giraTask.ClosedAt,
				Title = giraTask.Title,
				Labels = giraTask.Labels.ToArray()
			};
		}
	}
}
