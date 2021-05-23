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

		public CardBuilder CreatedAt(DateTime createdAt)
		{
			return SetValue(card => card.CreatedAt, createdAt);
		}

		/// <summary>
		/// Can be called multiple times to add more labels.
		/// </summary>
		/// <param name="label">Label as a string.</param>
		/// <returns>Card builder with added label.</returns>
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
				createdAt: GetValue(card => card.CreatedAt),
				closedAt: GetValue(card => card.ClosedAt),
				title: default,
				labels: GetValue(card => card.Labels, Array.Empty<string>()));
		}
	}
}
