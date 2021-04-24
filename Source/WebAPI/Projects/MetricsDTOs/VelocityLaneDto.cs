namespace WebAPI
{
	public class VelocityLaneDto
	{
		public LaneDto Lane { get; set; }

		public int TaskCount { get; set; }

		public static VelocityLaneDto BuildFrom(LaneDto lane, int taskCount)
		{
			return new VelocityLaneDto {
				Lane = lane,
				TaskCount = taskCount
			};
		}
	}
}
