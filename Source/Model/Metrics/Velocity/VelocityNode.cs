using System;
using System.Collections.Generic;

namespace Model
{
	public class VelocityNode
	{
		public DateTime Day { get; private set; }

		public IDictionary<Line, int> ClosetTasksByLane { get; private set; }

		public VelocityNode(DateTime day, IDictionary<Line, int> closedTasksByLane)
		{
			Day = day;
			ClosetTasksByLane = closedTasksByLane;
		}
	}
}
