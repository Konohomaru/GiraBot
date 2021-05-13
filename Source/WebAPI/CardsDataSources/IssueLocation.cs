namespace WebAPI
{
	public class IssueLocation
	{
		public int ProjectId { get; }

		public string ProjectName { get; }

		public string ProjectColumn { get; }

		public IssueLocation(int projectId, string projectName, string projectColumn)
		{
			ProjectId = projectId;
			ProjectName = projectName;
			ProjectColumn = projectColumn;
		}

		public override string ToString()
		{
			return $"{ProjectName}/{ProjectColumn}";
		}
	}
}
