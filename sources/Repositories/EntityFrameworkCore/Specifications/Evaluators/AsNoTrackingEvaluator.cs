using LanguageExt.Common;

using Microsoft.EntityFrameworkCore;

using Rafsan.DataAccess.Repositories.Abstractions;
using Rafsan.DataAccess.Repositories.Abstractions.Evaluators;

namespace Rafsan.DataAccess.EntityFrameworkCore.Evaluators;

public class AsNoTrackingEvaluator : IEvaluator
{
	private AsNoTrackingEvaluator() { }
	public static AsNoTrackingEvaluator Instance { get; } = new AsNoTrackingEvaluator();

	public bool IsCriteriaEvaluator { get; } = true;

	public Result<IQueryable<T>> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification)
		where T : class
	{
		if (specification.AsNoTracking)
		{
			query = query.AsNoTracking();
		}

		return query.AsResult();
	}
}
