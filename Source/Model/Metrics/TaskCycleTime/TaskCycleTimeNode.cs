﻿using System;
using System.Collections.Generic;

namespace Model
{
	public class TaskCycleTimeNode
	{
		public string Title { get; }

		public DateTime CreatedAt { get; }

		public DateTime? ClosedAt { get; }

		public IReadOnlyCollection<string> Labels { get; }

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
