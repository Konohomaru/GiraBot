using System;
using System.Collections.Generic;

namespace Model
{
	public class VelocityNode
	{
		public DateTime SprintDay { get; private set; }

		public IDictionary<Lane, int> IssuesByLane { get; private set; }

		public VelocityNode(DateTime sprintDay, IDictionary<Lane, int> issuesByLane)
		{
			SprintDay = sprintDay;
			IssuesByLane = issuesByLane;
		}
	}
}
