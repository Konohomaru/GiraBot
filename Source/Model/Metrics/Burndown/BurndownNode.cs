using System;

namespace Model
{
	public class BurndownNode
	{
		public DateTime Day { get; private set; }

		public int RemainingTasks { get; private set; }

		public BurndownNode(DateTime dateStamp, int remainingIssues)
		{
			Day = dateStamp;
			RemainingTasks = remainingIssues;
		}
	}
}
