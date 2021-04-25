using Model;
using System;

namespace ModelTests
{
	public class GrTaskBuilder : EntityBuilder<GrTask, GrTaskBuilder>
	{
		public GrTaskBuilder ClosedAt(DateTime closedAt)
		{
			return SetValue(task => task.ClosedAt, closedAt);
		}

		public override GrTask Build()
		{
			return new GrTask(
				id: default,
				createdAt: default,
				closedAt: GetValue(task => task.ClosedAt),
				title: default,
				labels: default);
		}
	}
}
