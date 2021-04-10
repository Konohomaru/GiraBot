using System;
using System.Collections.Generic;

namespace Model
{
	public static class IEnumerableExtenstions
	{
		/// <summary>
		/// Determines whether a sequence contains any element of second.
		/// </summary>
		/// <returns>True if contains, else False.</returns>
		public static bool ContainsAny<T>(this IEnumerable<T> source, IEnumerable<T> collection, IComparer<T> comparer)
		{
			if (source is null) throw new ArgumentNullException(nameof(source));
			if (collection is null) throw new ArgumentNullException(nameof(source));

			foreach (var i in source)
				foreach (var j in collection)
					if (comparer.Compare(i, j) == 0)
						return true;

			return false;
		}
	}
}
