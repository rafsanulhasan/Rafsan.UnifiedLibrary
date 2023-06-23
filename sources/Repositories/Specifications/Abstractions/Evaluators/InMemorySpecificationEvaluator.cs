using LanguageExt.Common;

using Rafsan.DataAccess.Repositories.Exceptions;

namespace Rafsan.DataAccess.Repositories.Abstractions.Evaluators;

public class InMemorySpecificationEvaluator : IInMemorySpecificationEvaluator
{
	// Will use singleton for default configuration. Yet, it can be instantiated if necessary, with default or provided evaluators.
	public static InMemorySpecificationEvaluator Default { get; } = new InMemorySpecificationEvaluator();

	protected List<IInMemoryEvaluator> Evaluators { get; } = new List<IInMemoryEvaluator>();

	public InMemorySpecificationEvaluator()
	{
		Evaluators.AddRange(new IInMemoryEvaluator[]
		{
			 WhereEvaluator.Instance,
			 SearchEvaluator.Instance,
			 OrderEvaluator.Instance,
			 PaginationEvaluator.Instance
		});
	}
	public InMemorySpecificationEvaluator(IEnumerable<IInMemoryEvaluator> evaluators)
	{
		Evaluators.AddRange(evaluators);
	}

	public virtual Result<IEnumerable<TResult>> Evaluate<TEntity, TResult>(
		IQueryable<TEntity> source,
		ISpecification<TEntity, TResult> specification)
	{
		if (!specification.Selector.IsNone && !specification.SelectorMany.IsNone)
		{
			return new Result<IEnumerable<TResult>>(new ConcurrentSelectorsException());
		}

		var baseQuery = Evaluate(source, (ISpecification<TEntity>)specification);

		Result<IEnumerable<TResult>> resultQuery = specification.Selector
			.Match(
				selector => baseQuery.Match(
					q => q.Select(selector.Compile()).AsResult(),
					ex => new Result<IEnumerable<TResult>>(ex)),
				() => specification.SelectorMany.Match(
					manySelector => baseQuery.Match(
						q => q.SelectMany(manySelector.Compile()).AsResult(),
						ex => new Result<IEnumerable<TResult>>(ex)),
					() => new Result<IEnumerable<TResult>>(new SelectorNotFoundException()))
			);

		return specification.PostProcessingAction.Match(
			postProcessingAction => resultQuery.Match(
				q => postProcessingAction(q).AsResult(),
				ex => new Result<IEnumerable<TResult>>(ex)),
			() => resultQuery.Match(
				q => q.AsResult(),
				ex => new Result<IEnumerable<TResult>>(ex)));
	}

	public virtual Result<IEnumerable<TEntity>> Evaluate<TEntity>(
		IQueryable<TEntity> source,
		ISpecification<TEntity> specification)
	{
		Result<IEnumerable<TEntity>> resultQuery = new Result<IEnumerable<TEntity>>();
		foreach (var evaluator in Evaluators)
		{
			resultQuery = evaluator.Evaluate(source, specification).Match(
				q => q.AsResult(),
				ex => ex.AsResult<IEnumerable<TEntity>>());
		}

		return specification.PostProcessingAction.Match(
				postProcessingAction => resultQuery.Match(
					q => postProcessingAction(q).AsResult(),
					ex => ex.AsResult<IEnumerable<TEntity>>()),
				() => resultQuery.Match(
					q => q.AsResult(),
					ex => ex.AsResult<IEnumerable<TEntity>>()));
	}
}
