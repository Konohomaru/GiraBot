using System;

namespace Model
{
	public class Sprint
	{
		public int Id { get; private set; }

		public DateTime BeginAt { get; private set; }

		/// <summary>
		/// In days.
		/// </summary>
		public int Duration { get; private set; }

		public DateTime EndAt => BeginAt.AddDays(Duration);

		/// <summary>
		/// The sprint of Agile metodology.
		/// </summary>
		/// <param name="beginAt">Sprint begining date.</param>
		/// <param name="duration">The duration of sprint in days count.</param>
		/// <param name="issues">The issues related to sprint.</param>
		public Sprint(int id, DateTime beginAt, int duration)
		{
			Id = id;
			BeginAt = beginAt;
			Duration = duration;
		}

		public bool ContainsIssue(Issue issue)
		{
			return issue.ClosedAt is null || issue.ClosedAt >= BeginAt;
		}

		public bool ContainesDate(DateTime date)
		{
			return date >= BeginAt && date < EndAt;
		}
	}
}
