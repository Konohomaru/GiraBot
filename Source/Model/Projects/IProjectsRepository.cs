﻿using System.Collections.Generic;

namespace Model
{
	public interface IProjectsRepository
	{
		Project GetProject(int projectId);

		IEnumerable<Sprint> GetProjectSprints(int projectId);

		IReadOnlyCollection<GiraTask> GetProjectTasks(int projectId);
	}
}
