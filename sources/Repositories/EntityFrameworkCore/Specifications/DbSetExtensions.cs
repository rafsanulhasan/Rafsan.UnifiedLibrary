using LanguageExt;
using LanguageExt.Common;

using Microsoft.EntityFrameworkCore;

using Rafsan.DataAccess.EntityFrameworkCore.Evaluators;
using Rafsan.DataAccess.EntityFrameworkCore.Extensions;
using Rafsan.DataAccess.Repositories.Abstractions;
using Rafsan.DataAccess.Repositories.Abstractions.Evaluators;

namespace Rafsan.DataAccess.EntityFrameworkCore.Extensions;

public static class DbSetExtensions
{
	public static async Task<List<TSource>> ToListAsync<TSource>(
	  this DbSet<TSource> source,
	  ISpecification<TSource> specification,
	  CancellationToken cancellationToken = default
	)
	  where TSource : class
	{
		List<TSource> result = await SpecificationEvaluator.Default.GetQuery(source, specification).Match(
			q => q.ToListAsync(cancellationToken),
			_ => Task.FromResult(new List<TSource>()));

		return specification.PostProcessingAction
			.Match(
				action => action(result),
				() => result)
			.ToList();
	}

	public static async Task<IEnumerable<TSource>> AsEnumerableAsync<TSource>(
	  this DbSet<TSource> source,
	  ISpecification<TSource> specification,
	  CancellationToken cancellationToken = default)
	  where TSource : class
	{
		IEnumerable<TSource> result = await SpecificationEvaluator.Default.GetQuery(source, specification).Match(
			q => q.AsEnumerable().AsTask(),
			ex => Task.FromException<IEnumerable<TSource>>(ex));

		return specification.PostProcessingAction.Match(
			action => action(result),
			() => result);
	}

	public static Result<IQueryable<TSource>> WithSpecification<TSource>(
	  this IQueryable<TSource> source,
	  ISpecification<TSource> specification,
	  ISpecificationEvaluator? evaluator = null)
	  where TSource : class
	{
		evaluator ??= SpecificationEvaluator.Default;
		return evaluator.GetQuery(source, specification);
	}

	public static Result<IQueryable<TResult>> WithSpecification<TSource, TResult>(
	  this IQueryable<TSource> source,
	  ISpecification<TSource, TResult> specification,
	  ISpecificationEvaluator? evaluator = null)
	  where TSource : class
	{
		evaluator ??= SpecificationEvaluator.Default;
		return evaluator.GetQuery(source, specification);
	}
}
