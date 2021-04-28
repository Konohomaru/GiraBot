using System;

namespace Model
{
	public class Sprint
	{
		public int Id { get; }

		public DateTime BeginsAt { get; }

		public int Length { get; }

		public Sprint(int id, DateTime beginAt, int length)
		{
			Id = id;
			BeginsAt = beginAt;
			Length = length;
		}

		public bool ContainsDate(DateTime date)
		{
			return date >= BeginsAt && date < BeginsAt.AddDays(Length);
		}
	}
}
