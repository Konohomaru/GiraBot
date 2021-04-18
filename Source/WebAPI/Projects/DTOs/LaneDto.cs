﻿using Model;

namespace WebAPI
{
	public class LaneDto
	{
		public string Name { get; set; }

		public string MappedLabelName { get; set; }

		public static LaneDto BuildFrom(Lane lane)
		{
			return new LaneDto {
				Name = lane.Name,
				MappedLabelName = lane.MappedName
			};
		}
	}
}