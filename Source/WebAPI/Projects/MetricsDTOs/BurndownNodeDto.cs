using Model;
using System;

namespace WebAPI
{
	public class BurndownNodeDto
	{
		public DateTime Day { get; set; }

		public int RemainingCards { get; set; }

		public static BurndownNodeDto BuildFrom(BurndownNode node)
		{
			return new BurndownNodeDto {
				Day = node.Day,
				RemainingCards = node.RemainingCards
			};
		}
	}
}
