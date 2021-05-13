using System;
using System.Collections.Generic;

namespace WebAPI
{
	public class Issue
	{
		public int Id { get; }

		public DateTime CreatedAt { get; }

		public DateTime? ClosedAt { get; }

		public string Title { get; }

		public IReadOnlyCollection<string> Labels { get; }

		public IReadOnlyCollection<IssueLocation> Locations { get; }

		public Issue(
			int id,
			DateTime createdAt,
			DateTime? closedAt,
			string title,
			string[] labels,
			IssueLocation[] locations)
		{
			Id = id;
			CreatedAt = createdAt;
			ClosedAt = closedAt;
			Title = title;
			Labels = labels;
			Locations = locations;
		}
	}
}
