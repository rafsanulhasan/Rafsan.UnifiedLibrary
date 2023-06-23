using LanguageExt.Common;

namespace Rafsan.DataAccess.Repositories.Abstractions.Evaluators;

public class SearchEvaluator : IInMemoryEvaluator
{
	private SearchEvaluator() { }
	public static SearchEvaluator Instance { get; } = new SearchEvaluator();

	public Result<IEnumerable<T>> Evaluate<T>(IQueryable<T> query, ISpecification<T> specification)
	{
		foreach (var searchGroup in specification.SearchCriterias.GroupBy(x => x.SearchGroup))
		{
			query = query.Where(x => searchGroup.Any(c => c.SelectorFunc(x).Like(c.SearchTerm)));
		}

		return query.AsEnumerable().AsResult();
	}
}
