using System.Collections.Generic;

namespace Model
{
	public interface ICardsDataSource
	{
		IReadOnlyCollection<Card> GetProjectCards(Project project);
	}
}
