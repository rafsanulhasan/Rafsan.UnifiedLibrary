using LanguageExt.Common;

using Microsoft.EntityFrameworkCore;

using Rafsan.DataAccess.Repositories.Abstractions;
using Rafsan.DataAccess.Repositories.Abstractions.Evaluators;

namespace Rafsan.DataAccess.EntityFrameworkCore.Evaluators;

#if !NETSTANDARD2_0
public class AsNoTrackingWithIdentityResolutionEvaluator : IEvaluator
{
	private AsNoTrackingWithIdentityResolutionEvaluator() { }
	public static AsNoTrackingWithIdentityResolutionEvaluator Instance { get; } = new AsNoTrackingWithIdentityResolutionEvaluator();

	public bool IsCriteriaEvaluator { get; } = true;

	public Result<IQueryable<T>> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification) where T : class
	{
		if (specification.AsNoTrackingWithIdentityResolution)
		{
			query = query.AsNoTrackingWithIdentityResolution();
		}

		return query.AsResult();
	}
}
#endif
