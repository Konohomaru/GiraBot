using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ModelTests
{
	public abstract class EntityBuilder<TEntity, TBuilder> where TBuilder : EntityBuilder<TEntity, TBuilder>
	{
		private Dictionary<string, object> Values { get; } = new();

		protected TBuilder SetValue<TValue>(
			Expression<Func<TEntity, TValue>> accessor,
			TValue value)
		{
			Values[accessor.Body.ToString()] = value;
			return (TBuilder)this;
		}

		protected TValue GetValue<TValue>(
			Expression<Func<TEntity, TValue>> accessor,
			TValue defaultValue = default)
		{
			if (Values.TryGetValue(accessor.Body.ToString(), out object value)) {
				return (TValue)value;
			}

			return defaultValue;
		}

		public abstract TEntity Build();
	}
}
