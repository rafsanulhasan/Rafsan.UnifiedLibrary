using System.Linq.Expressions;

using Rafsan.DataAccess.Repositories.Expressions;

namespace Rafsan.DataAccess.Repositories.Abstractions.Builders;

public static class IncludableBuilderExtensions
{
	public static IIncludableSpecificationBuilder<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
	    this IIncludableSpecificationBuilder<TEntity, TPreviousProperty> previousBuilder,
	    Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression)
	    where TEntity : class
	    => previousBuilder.ThenInclude(thenIncludeExpression, true);

	public static IIncludableSpecificationBuilder<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
	    this IIncludableSpecificationBuilder<TEntity, TPreviousProperty> previousBuilder,
	    Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression,
	    bool condition)
	    where TEntity : class
	{
		if (condition && !previousBuilder.IsChainDiscarded)
		{
			var info = new IncludeExpressionInfo(thenIncludeExpression, typeof(TEntity), typeof(TProperty), typeof(TPreviousProperty));

			((List<IncludeExpressionInfo>)previousBuilder.Specification.IncludeExpressions).Add(info);
		}

		var includeBuilder = new IncludableSpecificationBuilder<TEntity, TProperty>(previousBuilder.Specification, !condition || previousBuilder.IsChainDiscarded);

		return includeBuilder;
	}

	public static IIncludableSpecificationBuilder<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
	    this IIncludableSpecificationBuilder<TEntity, IEnumerable<TPreviousProperty>> previousBuilder,
	    Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression)
	    where TEntity : class
	    => previousBuilder.ThenInclude(thenIncludeExpression, true);

	public static IIncludableSpecificationBuilder<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
	    this IIncludableSpecificationBuilder<TEntity, IEnumerable<TPreviousProperty>> previousBuilder,
	    Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression,
	    bool condition)
	    where TEntity : class
	{
		if (condition && !previousBuilder.IsChainDiscarded)
		{
			var info = new IncludeExpressionInfo(thenIncludeExpression, typeof(TEntity), typeof(TProperty), typeof(IEnumerable<TPreviousProperty>));

			((List<IncludeExpressionInfo>)previousBuilder.Specification.IncludeExpressions).Add(info);
		}

		var includeBuilder = new IncludableSpecificationBuilder<TEntity, TProperty>(previousBuilder.Specification, !condition || previousBuilder.IsChainDiscarded);

		return includeBuilder;
	}
}
