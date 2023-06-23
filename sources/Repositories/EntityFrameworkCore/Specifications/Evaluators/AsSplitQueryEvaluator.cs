using LanguageExt.Common;

using Microsoft.EntityFrameworkCore;

using Rafsan.DataAccess.Repositories.Abstractions;
using Rafsan.DataAccess.Repositories.Abstractions.Evaluators;

namespace Rafsan.DataAccess.EntityFrameworkCore.Evaluators;

#if !NETSTANDARD2_0
public class AsSplitQueryEvaluator : IEvaluator
{
	private AsSplitQueryEvaluator() { }
	public static AsSplitQueryEvaluator Instance { get; } = new AsSplitQueryEvaluator();

	public bool IsCriteriaEvaluator { get; } = true;

	public Result<IQueryable<T>> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification) where T : class
	{
		if (specification.AsSplitQuery)
		{
			query = query.AsSplitQuery();
		}

		return query.AsResult();
	}
}
#endif
