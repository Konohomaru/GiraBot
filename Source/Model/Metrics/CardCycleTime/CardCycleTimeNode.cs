using System;
using System.Collections.Generic;

namespace Model
{
	public class CardCycleTimeNode
	{
		public int? Duration { get; }

		public IReadOnlyCollection<Card> Cards { get; }

		public CardCycleTimeNode(int? duration, IReadOnlyCollection<Card> cards)
		{
			Duration = duration;
			Cards = cards ?? Array.Empty<Card>();
		}
	}
}
