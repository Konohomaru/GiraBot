using Model;
using System;
using System.Linq;

namespace WebAPI
{
	public class VelocityNodeDto
	{
		public DateTime SprintDay { get; set; }

		public VelocityLaneDto[] IssuesByLane { get; set; }

		public static VelocityNodeDto BuildFrom(VelocityNode node)
		{
			return new VelocityNodeDto {
				SprintDay = node.SprintDay,
				IssuesByLane = node.IssuesByLane
					.Select(item => VelocityLaneDto.BuildFrom(LaneDto.BuildFrom(item.Key), item.Value))
					.ToArray()
			};
		}
	}
}
