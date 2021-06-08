using System;
using System.Collections.Generic;

namespace Model
{
	public class VelocityNode
	{
		public DateTime Day { get; }

		public IDictionary<Swimlane, int> ClosedCards { get; }

		public VelocityNode(DateTime day, IDictionary<Swimlane, int> closedCards)
		{
			Day = day;
			ClosedCards = closedCards;
		}
	}
}
