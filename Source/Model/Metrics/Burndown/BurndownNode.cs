using System;

namespace Model
{
	public class BurndownNode
	{
		public DateTime Day { get; }

		public int RemainingTasks { get; }

		public BurndownNode(DateTime day, int remainingTasks)
		{
			Day = day;
			RemainingTasks = remainingTasks;
		}
	}
}
