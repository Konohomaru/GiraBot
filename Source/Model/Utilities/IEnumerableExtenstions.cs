using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public static class IEnumerableExtenstions
	{
		public static bool ContainsAnyOf<T>(
			this IEnumerable<T> source,
			IEnumerable<T> collection)
			where T: IComparable<T>
		{
			if (source is null) throw new ArgumentNullException(nameof(source));
			if (collection is null) throw new ArgumentNullException(nameof(source));

			foreach (var i in source)
				foreach (var j in collection)
					if (i.CompareTo(j) == 0) return true;

			return false;
		}

		public static IEnumerable<GrTask> GetSprintTasks(
			this IEnumerable<GrTask> tasks,
			Sprint sprint)
		{
			return tasks.Where(task =>
				!task.ClosedAt.HasValue ||
				task.ClosedAt >= sprint.BeginsAt);
		}
	}
}
