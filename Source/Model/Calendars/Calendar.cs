using System;

namespace Model
{
	public class Calendar : ICalendar
	{
		public DateTime GetCurrentUtcDateTime()
		{
			return DateTime.UtcNow;
		}
	}
}
