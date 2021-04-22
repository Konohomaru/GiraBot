using System;
using System.Collections.Generic;

namespace Model
{
	public class GiraTask
	{
		public int Id { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime? ClosedAt { get; private set; }

		public string Title { get; private set; }

		public IReadOnlyCollection<string> Labels { get; private set; }

		public GiraTask(
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
