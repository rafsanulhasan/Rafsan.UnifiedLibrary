using LanguageExt.Common;

namespace Rafsan.DataAccess.Repositories.Abstractions.Evaluators;

public class PaginationEvaluator : IEvaluator, IInMemoryEvaluator
{
	private PaginationEvaluator() { }
	public static PaginationEvaluator Instance { get; } = new PaginationEvaluator();

	public bool IsCriteriaEvaluator { get; } = false;

	public Result<IQueryable<T>> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification)
		where T : class
	{
		// If skip is 0, avoid adding to the IQueryable. It will generate more optimized SQL that way.
		query = specification.Skip.Match(
			skip => skip.AsConditional().Match(s => s > 0, _ => query.Skip(skip), _ => query),
			() => query);

		query = specification.Take.Match(
			take => query.Take(take),
			() => query);

		return query.AsResult();
	}

	public Result<IEnumerable<T>> Evaluate<T>(IQueryable<T> query, ISpecification<T> specification)
	{
		// If skip is 0, avoid adding to the IQueryable. It will generate more optimized SQL that way.
		query = specification.Skip.Match(
			skip => skip.AsConditional().Match(s => s > 0, _ => query.Skip(skip), _ => query),
			() => query);

		query = specification.Take.Match(
			take => query.Take(take),
			() => query);

		return query.AsEnumerable().AsResult();
	}
}
