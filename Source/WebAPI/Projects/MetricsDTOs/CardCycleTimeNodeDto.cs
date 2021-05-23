using Model;
using System.Linq;

namespace WebAPI
{
	public class CardCycleTimeNodeDto
	{
		public int? Duration { get; set; }

		public CardDto[] Cards { get; set; }

		public static CardCycleTimeNodeDto BuildFrom(CardCycleTimeNode node)
		{
			return new CardCycleTimeNodeDto {
				Duration = node.Duration,
				Cards = node.Cards
					.Select(card => CardDto.BuildFrom(card))
					.ToArray()
			};
		}
	}
}
