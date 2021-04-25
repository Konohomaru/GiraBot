using Model;
using System;
using System.Linq;

namespace ModelTests
{
	public class GrTaskBuilder : EntityBuilder<GrTask, GrTaskBuilder>
	{
		public GrTaskBuilder ClosedAt(DateTime closedAt)
		{
			return SetValue(task => task.ClosedAt, closedAt);
		}

		public GrTaskBuilder WithLabel(string label)
		{
			var labels = GetValue(
				accessor: task => task.Labels,
				defaultValue: Array.Empty<string>())
			.ToList();
			
			labels.Add(label);

			return SetValue(task => task.Labels, labels.ToArray());
		}

		public override GrTask Build()
		{
			return new GrTask(
				id: default,
				createdAt: default,
				closedAt: GetValue(task => task.ClosedAt),
				title: default,
				labels: GetValue(task => task.Labels, Array.Empty<string>()));
		}
	}
}
