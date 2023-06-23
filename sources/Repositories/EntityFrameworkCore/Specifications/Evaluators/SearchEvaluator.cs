using LanguageExt.Common;

using Rafsan.DataAccess.EntityFrameworkCore.Extensions;
using Rafsan.DataAccess.Repositories.Abstractions;
using Rafsan.DataAccess.Repositories.Abstractions.Evaluators;

namespace Rafsan.DataAccess.EntityFrameworkCore.Evaluators;

public class SearchEvaluator : IEvaluator
{
	private SearchEvaluator() { }
	public static SearchEvaluator Instance { get; } = new SearchEvaluator();

	public bool IsCriteriaEvaluator { get; } = true;

	public Result<IQueryable<T>> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification) where T : class
	{
		foreach (var searchCriteria in specification.SearchCriterias.GroupBy(x => x.SearchGroup))
		{
			query = query.Search(searchCriteria);
		}

		return query.AsResult();
	}
}
