using System.Linq;

using LanguageExt.Common;

using Rafsan.DataAccess.Repositories.Exceptions;
using Rafsan.DataAccess.Repositories.Expressions;

using static LanguageExt.Prelude;

namespace Rafsan.DataAccess.Repositories.Abstractions.Evaluators;

public class OrderEvaluator : IEvaluator, IInMemoryEvaluator
{
	private OrderEvaluator() { }
	public static OrderEvaluator Instance { get; } = new OrderEvaluator();

	public bool IsCriteriaEvaluator { get; } = false;

	public Result<IQueryable<T>> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification) where T : class
	{
		return specification.OrderExpressions.AsConditional().Match(
			exp => exp is null,
			_ => query.AsResult(),
			exp =>
			{
				int count = exp.Count(x
					=> x.OrderType == OrderTypeEnum.OrderBy
					|| x.OrderType == OrderTypeEnum.OrderByDescending);
				return count.AsConditional().Match(
					c => c > 1,
					_ => new DuplicateOrderChainException().AsResult<IQueryable<T>>(),
					_ => EvaluateOrderBy(query, specification).AsResult());
			});
	}

	public Result<IEnumerable<T>> Evaluate<T>(IQueryable<T> query, ISpecification<T> specification)
	{
		return specification.OrderExpressions.AsConditional().Match(
			exp => exp is null,
			_ => query.AsEnumerable().AsResult(),
			exp =>
			{
				int count = exp.Count(x
					=> x.OrderType == OrderTypeEnum.OrderBy
					|| x.OrderType == OrderTypeEnum.OrderByDescending);
				return count.AsConditional().Match(
					c => c > 1,
					_ => new DuplicateOrderChainException().AsResult<IEnumerable<T>>(),
					_ => EvaluateOrderBy(query, specification).AsEnumerable().AsResult());
			});
	}

	private IQueryable<T> EvaluateOrderBy<T>(IQueryable<T> query, ISpecification<T> specification)
	{
		IOrderedEnumerable<T> orderedQuery = null!;
		foreach (var orderExpression in specification.OrderExpressions)
		{
			if (orderExpression.OrderType == OrderTypeEnum.OrderBy)
			{
				orderedQuery = query.OrderBy(orderExpression.KeySelectorFunc);
			}
			else if (orderExpression.OrderType == OrderTypeEnum.OrderByDescending)
			{
				orderedQuery = query.OrderByDescending(orderExpression.KeySelectorFunc);
			}
			else if (orderExpression.OrderType == OrderTypeEnum.ThenBy)
			{
				orderedQuery = orderedQuery.ThenBy(orderExpression.KeySelectorFunc);
			}
			else if (orderExpression.OrderType == OrderTypeEnum.ThenByDescending)
			{
				orderedQuery = orderedQuery.ThenByDescending(orderExpression.KeySelectorFunc);
			}
		}

		if (orderedQuery != null)
		{
			query = orderedQuery.AsQueryable();
		}

		return query;
	}
}
