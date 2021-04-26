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
			where T : IComparable<T>
		{
			foreach (var i in source)
				foreach (var j in collection)
					if (i.CompareTo(j) == 0) return true;

			return false;
		}

		public static IEnumerable<Card> GetSprintTasks(
			this IEnumerable<Card> cards,
			Sprint sprint)
		{
			return cards.Where(card => !card.ClosedAt.HasValue || card.ClosedAt >= sprint.BeginsAt);
		}
	}
}
