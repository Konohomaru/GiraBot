using System.Collections.Generic;

namespace Model
{
	public interface ITasksDataSource
	{
		IReadOnlyCollection<GiraTask> GetProjectTasks(Project project);
	}
}
