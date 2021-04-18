namespace WebAPI
{
	public class VelocityLaneDto
	{
		public LaneDto Lane { get; set; }

		public int IssuesCount { get; set; }

		public static VelocityLaneDto BuildFrom(LaneDto lane, int issuesCount)
		{
			return new VelocityLaneDto {
				Lane = lane,
				IssuesCount = issuesCount
			};
		}
	}
}
