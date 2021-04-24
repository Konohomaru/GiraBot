using System;
using System.Collections.Generic;

namespace Model
{
	public class VelocityNode
	{
		public DateTime Day { get; set; }

		public IDictionary<Lane, int> ClosedTasksByLane { get; set; }

		public VelocityNode(DateTime day, IDictionary<Lane, int> closedTasksByLane)
		{
			Day = day;
			ClosedTasksByLane = closedTasksByLane;
		}
	}
}
