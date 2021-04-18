using Model;
using System;

namespace WebAPI
{
	public class BurndownNodeDto
	{
		public DateTime SprintDay { get; set; }

		public int RemainingIssues { get; set; }

		public static BurndownNodeDto BuildFrom(BurndownNode node)
		{
			return new BurndownNodeDto {
				SprintDay = node.Day,
				RemainingIssues = node.RemainingTasks
			};
		}
	}
}
