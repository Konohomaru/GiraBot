using System.Collections.Generic;

namespace Model
{
	public class ProjectComparerByName : IComparer<Project>
	{
		public int Compare(Project x, Project y)
		{
			return string.Compare(x.Name, y.Name);
		}
	}
}
