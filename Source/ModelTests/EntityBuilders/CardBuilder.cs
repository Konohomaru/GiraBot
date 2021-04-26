using Model;
using System;
using System.Linq;

namespace ModelTests
{
	public class CardBuilder : EntityBuilder<Card, CardBuilder>
	{
		public CardBuilder WithId(int id)
		{
			return SetValue(card => card.Id, id);
		}

		public CardBuilder ClosedAt(DateTime closedAt)
		{
			return SetValue(card => card.ClosedAt, closedAt);
		}

		public CardBuilder WithLabel(string label)
		{
			var labels = GetValue(
				accessor: card => card.Labels,
				defaultValue: Array.Empty<string>())
			.ToList();
			
			labels.Add(label);

			return SetValue(card => card.Labels, labels.ToArray());
		}

		public override Card Build()
		{
			return new Card(
				id: GetValue(card => card.Id),
				createdAt: default,
				closedAt: GetValue(card => card.ClosedAt),
				title: default,
				labels: GetValue(card => card.Labels, Array.Empty<string>()));
		}
	}
}
