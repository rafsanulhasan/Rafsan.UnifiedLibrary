using LanguageExt.Common;

using System.Linq;

namespace Rafsan.DataAccess.Repositories.Abstractions.Evaluators;

public class WhereEvaluator : IEvaluator, IInMemoryEvaluator
{
	private WhereEvaluator() { }
	public static WhereEvaluator Instance { get; } = new WhereEvaluator();

	public bool IsCriteriaEvaluator { get; } = true;

	public Result<IQueryable<T>> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification) where T : class
	{
		foreach (var info in specification.WhereExpressions)
		{
			query = query.Where(info.Filter);
		}

		return query.AsResult();
	}

	public Result<IEnumerable<T>> Evaluate<T>(IQueryable<T> query, ISpecification<T> specification)
	{
		foreach (var info in specification.WhereExpressions)
		{
			query = query.Where(q => info.FilterFunc(q));
		}

		return query.AsEnumerable().AsResult();
	}
}
