using Model;
using System;

namespace WebAPI
{
	public class BurndownNodeDto
	{
		public DateTime Day { get; set; }

		public int RemainingTasks { get; set; }

		public static BurndownNodeDto BuildFrom(BurndownNode node)
		{
			return new BurndownNodeDto {
				Day = node.Day,
				RemainingTasks = node.RemainingTasks
			};
		}
	}
}
