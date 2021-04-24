using System;
using System.Collections.Generic;

namespace Model
{
	public class GrTask
	{
		public int Id { get; }

		public DateTime CreatedAt { get; }

		public DateTime? ClosedAt { get; }

		public string Title { get; }

		public IReadOnlyCollection<string> Labels { get; }

		public GrTask(
			int id,
			DateTime createdAt,
			DateTime? closedAt,
			string title,
			IReadOnlyCollection<string> labels)
		{
			Id = id;
			CreatedAt = createdAt;
			ClosedAt = closedAt;
			Title = title;
			Labels = labels ?? Array.Empty<string>();
		}
	}
}
