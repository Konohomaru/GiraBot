using System;

namespace Model
{
	public class BurndownNode
	{
		public DateTime SprintDay { get; private set; }

		public int RemainingIssues { get; private set; }

		public BurndownNode(DateTime dateStamp, int remainingIssues)
		{
			SprintDay = dateStamp;
			RemainingIssues = remainingIssues;
		}
	}
}
