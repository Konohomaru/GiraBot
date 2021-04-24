using Model;
using System;
using System.Linq;

namespace WebAPI
{
	public class VelocityNodeDto
	{
		public DateTime Day { get; set; }

		public VelocityLaneDto[] ClosedTasksByLane { get; set; }

		public static VelocityNodeDto BuildFrom(VelocityNode node)
		{
			return new VelocityNodeDto {
				Day = node.Day,
				ClosedTasksByLane = node.ClosedTasksByLane
					.Select(item => VelocityLaneDto.BuildFrom(
						lane: LaneDto.BuildFrom(item.Key),
						taskCount: item.Value))
					.ToArray()
			};
		}
	}
}
