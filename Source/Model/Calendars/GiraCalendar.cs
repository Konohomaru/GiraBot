using System;

namespace Model
{
	public class GiraCalendar : ICalendar
	{
		public DateTime GetCurrentUtcDateTime()
		{
			return DateTime.UtcNow;
		}
	}
}
