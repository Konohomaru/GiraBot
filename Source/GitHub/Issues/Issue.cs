using System;
using System.Collections.Generic;
using System.Linq;

namespace GitHub
{
	public class Issue
	{
		public int Id { get; private set; }

		public DateTime CreateAt { get; private set; }

		public DateTime UpdateAt { get; private set; }

		public DateTime? ClosedAt { get; private set; }

		public string Title { get; private set; }

		public IReadOnlyCollection<string> Labels { get; private set; }

		public IReadOnlyCollection<Project> Projects { get; private set; }

		public IssueState State { get; private set; }

		public Issue(
			int id,
			DateTime createAt,
			DateTime updateAt,
			DateTime? closedAt,
			string title,
			string[] labels,
			Project[] projects,
			IssueState state)
		{
			Id = id;
			CreateAt = createAt;
			UpdateAt = updateAt;
			ClosedAt = closedAt;
			Title = title;
			Labels = labels;
			Projects = projects;
			State = state;
		}

		internal static Issue BuildFrom(IssueModel issue)
		{
			return new Issue(
				issue.Id,
				issue.CreateAt.UtcDateTime,
				issue.UpdateAt.UtcDateTime,
				issue.ClsoedAt?.UtcDateTime,
				issue.Title,
				issue.Labels.ToArray(),
				issue.Projects
					.Select(project => Project.BuildFrom(project))
					.ToArray(),
				issue.State);
		}
	}
}
