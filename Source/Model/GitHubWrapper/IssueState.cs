using System;
using GraphQL = Octokit.GraphQL.Model;

namespace Model
{
	public enum IssueState
	{
		Closed,
		Open
	}

	public class IssueStateConverter
	{
		public static IssueState BuildFrom(GraphQL.IssueState state)
		{
			return state switch {
				GraphQL.IssueState.Closed => IssueState.Closed,
				GraphQL.IssueState.Open => IssueState.Open,
				_ => throw new ArgumentException($"The value \"{nameof(state)}\" is invalid.")
			};
		}
	}
}
