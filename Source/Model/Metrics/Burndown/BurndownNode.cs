using System;

namespace Model
{
	public class BurndownNode
	{
		public DateTime Day { get; private set; }

		public int RemainingTasks { get; private set; }

		public BurndownNode(DateTime day, int remainingTasks)
		{
			Day = day;
			RemainingTasks = remainingTasks;
		}
	}
}
