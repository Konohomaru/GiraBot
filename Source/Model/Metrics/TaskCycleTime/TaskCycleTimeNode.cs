using System;
using System.Collections.Generic;

namespace Model
{
	public class TaskCycleTimeNode
	{
		public string Title { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime? ClosedAt { get; private set; }

		public IReadOnlyCollection<string> Labels { get; private set; }

		public TaskCycleTimeNode(
			string title,
			DateTime createdAt,
			DateTime? closedAt,
			string[] labels)
		{
			Title = title;
			CreatedAt = createdAt;
			ClosedAt = closedAt;
			Labels = labels;
		}
	}
}
