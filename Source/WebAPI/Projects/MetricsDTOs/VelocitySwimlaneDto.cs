namespace WebAPI
{
	public class VelocitySwimlaneDto
	{
		public SwimlaneDto Lane { get; set; }

		public int CardCount { get; set; }

		public static VelocitySwimlaneDto BuildFrom(SwimlaneDto lane, int cardCount)
		{
			return new VelocitySwimlaneDto {
				Lane = lane,
				CardCount = cardCount
			};
		}
	}
}
