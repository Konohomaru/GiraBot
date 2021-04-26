using Model;
using System;
using System.Linq;

namespace ModelTests
{
	public class CardBuilder : EntityBuilder<Card, CardBuilder>
	{
		public CardBuilder WithId(int id)
		{
			return SetValue(task => task.Id, id);
		}

		public CardBuilder ClosedAt(DateTime closedAt)
		{
			return SetValue(task => task.ClosedAt, closedAt);
		}

		public CardBuilder WithLabel(string label)
		{
			var labels = GetValue(
				accessor: task => task.Labels,
				defaultValue: Array.Empty<string>())
			.ToList();
			
			labels.Add(label);

			return SetValue(task => task.Labels, labels.ToArray());
		}

		public override Card Build()
		{
			return new Card(
				id: GetValue(task => task.Id),
				createdAt: default,
				closedAt: GetValue(task => task.ClosedAt),
				title: default,
				labels: GetValue(task => task.Labels, Array.Empty<string>()));
		}
	}
}
