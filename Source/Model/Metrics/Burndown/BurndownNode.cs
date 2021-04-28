using System;

namespace Model
{
	public class BurndownNode
	{
		public DateTime Day { get; }

		public int RemainingCards { get; }

		public BurndownNode(DateTime day, int remainingCards)
		{
			Day = day;
			RemainingCards = remainingCards;
		}
	}
}
