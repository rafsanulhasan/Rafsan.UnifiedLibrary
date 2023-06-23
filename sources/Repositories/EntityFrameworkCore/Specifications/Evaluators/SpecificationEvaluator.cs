using LanguageExt;
using LanguageExt.Common;

using Rafsan.DataAccess.Repositories.Abstractions;
using Rafsan.DataAccess.Repositories.Abstractions.Evaluators;
using Rafsan.DataAccess.Repositories.Exceptions;

namespace Rafsan.DataAccess.EntityFrameworkCore.Evaluators;

/// <inheritdoc/>
public class SpecificationEvaluator : ISpecificationEvaluator
{
	// Will use singleton for default configuration. Yet, it can be instantiated if necessary, with default or provided evaluators.
	/// <summary>
	/// <see cref="SpecificationEvaluator" /> instance with default evaluators and without any additional features enabled.
	/// </summary>
	public static SpecificationEvaluator Default { get; } = new SpecificationEvaluator();

	/// <summary>
	/// <see cref="SpecificationEvaluator" /> instance with default evaluators and enabled caching.
	/// </summary>
	public static SpecificationEvaluator Cached { get; } = new SpecificationEvaluator(true);

	protected List<IEvaluator> Evaluators { get; } = new List<IEvaluator>();

	public SpecificationEvaluator(bool cacheEnabled = false)
	{
		Evaluators.AddRange(new IEvaluator[]
		{
		 WhereEvaluator.Instance,
		 SearchEvaluator.Instance,
		 cacheEnabled ? IncludeEvaluator.Cached : IncludeEvaluator.Default,
		 OrderEvaluator.Instance,
		 PaginationEvaluator.Instance,
		 AsNoTrackingEvaluator.Instance,
		 IgnoreQueryFiltersEvaluator.Instance,
#if !NETSTANDARD2_0
                AsSplitQueryEvaluator.Instance,
		 AsNoTrackingWithIdentityResolutionEvaluator.Instance
#endif
		});
	}

	public SpecificationEvaluator(IEnumerable<IEvaluator> evaluators)
	{
		Evaluators.AddRange(evaluators);
	}

	/// <inheritdoc/>
	public virtual Result<IQueryable<TResult>> GetQuery<T, TResult>(
		IQueryable<T> query,
		ISpecification<T, TResult> specification
	)
		where T : class
	{
		specification.ThrowIfNull(nameof(specification));

		return specification.Selector.IsSome.AsConditional().Match(
			s => s && specification.SelectorMany.IsSome,
			_ => new ConcurrentSelectorsException().AsResult<IQueryable<TResult>>(),
			_ =>
			{
				query = GetQuery(query, (ISpecification<T>)specification).Match(q => q, _ => query);

				return specification.Selector.Match(
					s => query.Select(s).AsResult(),
					() => specification.SelectorMany.Match(
						sm => query.SelectMany(sm).AsResult(),
						() => new SelectorNotFoundException().AsResult<IQueryable<TResult>>()));
			});
	}

	/// <inheritdoc/>
	public virtual Result<IQueryable<T>> GetQuery<T>(
		IQueryable<T> query,
		ISpecification<T> specification,
		bool evaluateCriteriaOnly = false
	)
		where T : class
	{
		if (specification is null) throw new ArgumentNullException("Specification is required");

		var evaluators = evaluateCriteriaOnly ? Evaluators.Where(x => x.IsCriteriaEvaluator) : Evaluators;

		foreach (var evaluator in evaluators)
		{
			query = evaluator.GetQuery(query, specification).Match(q => q, _ => query);
		}

		return query.AsResult();
	}
}
