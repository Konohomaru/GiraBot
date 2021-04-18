using System;

namespace Model
{
	public class Sprint
	{
		public int Id { get; private set; }

		public DateTime BeginsAt { get; private set; }

		public int DaysCount { get; private set; }

		public DateTime EndsAt => BeginsAt.AddDays(DaysCount);

		public Sprint(int id, DateTime beginAt, int duration)
		{
			Id = id;
			BeginsAt = beginAt;
			DaysCount = duration;
		}

		public bool ContainsDate(DateTime date)
		{
			return date >= BeginsAt && date < EndsAt;
		}
	}
}
