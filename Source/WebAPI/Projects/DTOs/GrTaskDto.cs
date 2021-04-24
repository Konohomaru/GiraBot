using Model;
using System;
using System.Linq;

namespace WebAPI
{
	public class GrTaskDto
	{
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? ClosedAt { get; set; }

		public string Title { get; set; }

		public string[] Labels { get; set; }

		public static GrTaskDto BuildFrom(GrTask giraTask)
		{
			return new GrTaskDto {
				Id = giraTask.Id,
				CreatedAt = giraTask.CreatedAt,
				ClosedAt = giraTask.ClosedAt,
				Title = giraTask.Title,
				Labels = giraTask.Labels.ToArray()
			};
		}
	}
}
