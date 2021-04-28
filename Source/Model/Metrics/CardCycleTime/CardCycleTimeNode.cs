using System;
using System.Collections.Generic;

namespace Model
{
	public class CardCycleTimeNode
	{
		public string Title { get; }

		public DateTime CreatedAt { get; }

		public DateTime? ClosedAt { get; }

		public IReadOnlyCollection<string> Labels { get; }

		public CardCycleTimeNode(
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
