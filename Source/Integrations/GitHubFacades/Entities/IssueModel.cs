using System;
using System.Collections.Generic;

namespace Model
{
	internal class IssueModel
	{
		public int Id { get; set; }

		public DateTimeOffset CreateAt { get; set; }

		public DateTimeOffset UpdateAt { get; set; }

		public DateTimeOffset? ClsoedAt { get; set; }

		public string Title { get; set; }

		public List<string> Labels { get; set; }

		public List<RepositoryProjectModel> Projects { get; set; }

		public IssueState State { get; set; }
	}
}
