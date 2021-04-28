using Model;
using System;
using System.Linq;

namespace WebAPI
{
	public class VelocityNodeDto
	{
		public DateTime Day { get; set; }

		public VelocitySwimlaneDto[] ClosedCards { get; set; }

		public static VelocityNodeDto BuildFrom(VelocityNode node)
		{
			return new VelocityNodeDto {
				Day = node.Day,
				ClosedCards = node.ClosedCards
					.Select(item => VelocitySwimlaneDto.BuildFrom(
						lane: SwimlaneDto.BuildFrom(item.Key),
						cardCount: item.Value))
					.ToArray()
			};
		}
	}
}
