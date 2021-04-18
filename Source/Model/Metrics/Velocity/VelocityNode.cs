using System;
using System.Collections.Generic;

namespace Model
{
	public class VelocityNode
	{
		public DateTime Day { get; private set; }

		public IDictionary<Lane, int> CompletedTasksByLane { get; private set; }

		public VelocityNode(DateTime day, IDictionary<Lane, int> completedTasksByLane)
		{
			Day = day;
			CompletedTasksByLane = completedTasksByLane;
		}
	}
}
