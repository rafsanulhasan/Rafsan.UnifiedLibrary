namespace Rafsan.DataAccess.Repositories;

using System.Collections.Generic;
using System.Linq.Expressions;

using Abstractions;
using Abstractions.Builders;
using Abstractions.Evaluators;
using Abstractions.Validators;

using Expressions;

using LanguageExt.Common;

using Specifications.Builders;

/// <inheritdoc cref="ISpecification{T, TResult}"/>
public partial class Specification<T, TResult> : Specification<T>, ISpecification<T, TResult>
{
	public new virtual ISpecificationBuilder<T, TResult> Query { get; }
	/// <inheritdoc/>
	public Option<Expression<Func<T, TResult>>> Selector { get; internal set; } = Option<Expression<Func<T, TResult>>>.None;

	/// <inheritdoc/>
	public Option<Expression<Func<T, IEnumerable<TResult>>>> SelectorMany { get; internal set; } = Option<Expression<Func<T, IEnumerable<TResult>>>>.None;

	/// <inheritdoc/>
	public new Option<Func<IEnumerable<TResult>, IEnumerable<TResult>>> PostProcessingAction { get; internal set; } = Option<Func<IEnumerable<TResult>, IEnumerable<TResult>>>.None;

	protected Specification()
	    : this(InMemorySpecificationEvaluator.Default)
	{
	}

	protected Specification(IInMemorySpecificationEvaluator inMemorySpecificationEvaluator)
	    : base(inMemorySpecificationEvaluator)
	{
		Query = new SpecificationBuilder<T, TResult>(this);
	}

	public new virtual Result<IEnumerable<TResult>> Evaluate(IQueryable<T> entities)
	{
		return Evaluator.Evaluate(entities, this);
	}
}

/// <inheritdoc cref="ISpecification{T}"/>
public partial class Specification<T> : ISpecification<T>
{
	protected Specification()
	    : this(InMemorySpecificationEvaluator.Default, SpecificationValidator.Default)
	{
	}

	protected Specification(IInMemorySpecificationEvaluator inMemorySpecificationEvaluator)
	    : this(inMemorySpecificationEvaluator, SpecificationValidator.Default)
	{
	}

	protected Specification(ISpecificationValidator specificationValidator)
	    : this(InMemorySpecificationEvaluator.Default, specificationValidator)
	{
	}

	protected Specification(
		IInMemorySpecificationEvaluator inMemorySpecificationEvaluator,
		ISpecificationValidator specificationValidator)
	{
		Evaluator = inMemorySpecificationEvaluator;
		Validator = specificationValidator;
		Query = new SpecificationBuilder<T>(this);
	}

	/// <inheritdoc/>
	public IDictionary<string, object> Items { get; set; } = new Dictionary<string, object>();

	/// <inheritdoc/>
	public IEnumerable<WhereExpressionInfo<T>> WhereExpressions { get; } = Enumerable.Empty<WhereExpressionInfo<T>>();

	public IEnumerable<OrderExpressionInfo<T>> OrderExpressions { get; } = Enumerable.Empty<OrderExpressionInfo<T>>();

	/// <inheritdoc/>
	public IEnumerable<IncludeExpressionInfo> IncludeExpressions { get; } = Enumerable.Empty<IncludeExpressionInfo>();

	/// <inheritdoc/>
	public IEnumerable<string> IncludeStrings { get; } = Enumerable.Empty<string>();

	/// <inheritdoc/>
	public IEnumerable<SearchExpressionInfo<T>> SearchCriterias { get; } = Enumerable.Empty<SearchExpressionInfo<T>>();

	/// <inheritdoc/>
	public Option<int> Take { get; internal set; } = Option<int>.None;

	/// <inheritdoc/>
	public Option<int> Skip { get; internal set; } = Option<int>.None;

	/// <summary>
	/// The transform function to apply to the result of the query encapsulated by the <see cref="ISpecification{T}"/>.
	/// </summary>
	public Option<Func<IEnumerable<T>, IEnumerable<T>>> PostProcessingAction { get; internal set; } = Option<Func<IEnumerable<T>, IEnumerable<T>>>.None;

	/// <inheritdoc/>
	public bool CacheEnabled { get; internal set; }

	/// <inheritdoc/>
	public bool AsNoTracking { get; internal set; } = false;

	/// <inheritdoc/>
	public bool AsSplitQuery { get; internal set; } = false;

	/// <inheritdoc/>
	public bool AsNoTrackingWithIdentityResolution { get; internal set; } = false;

	/// <inheritdoc/>
	public bool IgnoreQueryFilters { get; internal set; } = false;

	protected IInMemorySpecificationEvaluator Evaluator { get; }

	protected ISpecificationValidator Validator { get; }

	public virtual ISpecificationBuilder<T> Query { get; }

	/// <inheritdoc/>
	public Option<string> CacheKey { get; internal set; } = Option<string>.None;

	/// <inheritdoc/>
	public virtual Result<IEnumerable<T>> Evaluate(IQueryable<T> entities)
	{
		return Evaluator.Evaluate(entities, this);
	}

	/// <inheritdoc/>
	public virtual bool IsSatisfiedBy(T entity)
	{
		return Validator.IsValid(entity, this);
	}
}
