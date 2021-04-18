using System;

namespace WebAPI
{
	public class IssueDto
	{
		public int Id { get; set; }

		public DateTime UpdateAt { get; set; }

		public DateTime? ClosedAt { get; set; }

		public string Title { get; set; }

		public string[] Labels { get; set; }

		public string State { get; set; }
	}
}
