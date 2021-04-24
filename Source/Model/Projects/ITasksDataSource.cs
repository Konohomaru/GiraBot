using System.Collections.Generic;

namespace Model
{
	public interface ITasksDataSource
	{
		IReadOnlyCollection<GrTask> GetProjectTasks(Project project);
	}
}
