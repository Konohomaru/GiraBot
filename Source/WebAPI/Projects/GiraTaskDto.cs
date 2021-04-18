using System;
using System.Collections.Generic;

namespace WebAPI
{
	public class GiraTaskDto
	{
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? ClosedAt { get; set; }

		public string Title { get; set; }

		public string[] Labels { get; set; }
	}
}
