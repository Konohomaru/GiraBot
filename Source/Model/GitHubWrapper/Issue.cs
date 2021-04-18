using System;
using System.Collections.Generic;

namespace Model
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
	}
}
