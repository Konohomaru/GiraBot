using GitHub;
using System;
using System.Linq;

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

		public static IssueDto BuildFrom(Issue issue)
		{
			return new IssueDto {
				Id = issue.Id,
				UpdateAt = issue.UpdateAt,
				ClosedAt = issue.ClosedAt,
				Title = issue.Title,
				Labels = issue.Labels.ToArray(),
				State = issue.State.ToString()
			};
		}
	}
}
