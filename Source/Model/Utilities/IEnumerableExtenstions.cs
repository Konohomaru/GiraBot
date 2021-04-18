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
		public static bool ContainsAny(this IEnumerable<RepositoryProject> source, IEnumerable<RepositoryProject> collection)
		{
			if (source is null) throw new ArgumentNullException(nameof(source));
			if (collection is null) throw new ArgumentNullException(nameof(source));

			foreach (var i in source)
				foreach (var j in collection)
					if (i.Id.CompareTo(j.Id) == 0)
						return true;

			return false;
		}
	}
}
