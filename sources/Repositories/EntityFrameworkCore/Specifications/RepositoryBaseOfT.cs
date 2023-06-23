using System.Diagnostics.CodeAnalysis;

using LanguageExt;
using LanguageExt.Common;

using Microsoft.EntityFrameworkCore;

using Rafsan.DataAccess.EntityFrameworkCore.Evaluators;
using Rafsan.DataAccess.Repositories.Abstractions;
using Rafsan.DataAccess.Repositories.Abstractions.Evaluators;
using Rafsan.DataAccess.Repositories.Errors;
using Rafsan.DataAccess.Repositories.Exceptions;

using static LanguageExt.Prelude;

namespace Rafsan.DataAccess.Repositories;

/// <inheritdoc/>
public abstract partial class RepositoryBase<TEntity> : IRepository<TEntity>
	where TEntity : class
{
	private readonly DbContext dbContext;
	private readonly ISpecificationEvaluator specificationEvaluator;

	/// <inheritdoc />
	protected RepositoryBase(DbContext dbContext)
	    : this(dbContext, SpecificationEvaluator.Default)
	{
	}

	/// <inheritdoc/>
	protected RepositoryBase(DbContext dbContext, ISpecificationEvaluator specificationEvaluator)
	{
		this.dbContext = dbContext;
		this.specificationEvaluator = specificationEvaluator;
	}

	/// <inheritdoc/>
	public virtual Eff<TEntity> Add(TEntity entity)
	{
		var state = dbContext.Set<TEntity>().Add(entity);
		return SuccessEff(state.Entity);
	}

	/// <inheritdoc/>
	public virtual Aff<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		return dbContext
			.Set<TEntity>()
			.AddAsync(entity, cancellationToken)
			.ToAff()
			.Map(e => e.Entity);
	}

	/// <inheritdoc/>
	public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
	{
		dbContext.Set<TEntity>().AddRange(entities);
		return entities;
	}

	/// <inheritdoc/>
	public virtual void Update(TEntity entity)
	{
		dbContext.Set<TEntity>().Update(entity);
	}

	/// <inheritdoc/>
	public virtual void UpdateRange(IEnumerable<TEntity> entities)
	{
		dbContext.Set<TEntity>().UpdateRange(entities);
	}

	/// <inheritdoc/>
	public virtual void Delete(TEntity entity)
	{
		dbContext.Set<TEntity>().Remove(entity);
	}

	/// <inheritdoc/>
	public virtual void DeleteRange(IEnumerable<TEntity> entities)
	{
		dbContext.Set<TEntity>().RemoveRange(entities);
	}

	/// <inheritdoc/>
	public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return await dbContext.SaveChangesAsync(cancellationToken);
	}

	/// <inheritdoc/>
	public virtual Aff<Option<TEntity>> ReadByIdAsync<TId>(
		[NotNullWhen(false)] TId id,
		CancellationToken cancellationToken = default
	)
		where TId : notnull
	{
		return id.AsConditional().Match(
			k => k is null,
			_ => FailAff<Option<TEntity>>(new ArgumentNullError(nameof(id))),
			k => dbContext.Set<TEntity>()
					.FindAsync(new object[] { id }, cancellationToken: cancellationToken)
					.ToAff()
					.Map(e => e is null ? Option<TEntity>.None : Option<TEntity>.Some(e)));
	}

	public Aff<TEntity> FirstAsync(CancellationToken cancellationToken = default)
	{
		return dbContext.Set<TEntity>()
			.FirstAsync(cancellationToken)
			.ToAff();
	}

	public Aff<Option<TEntity>> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
	{
		return dbContext.Set<TEntity>()
			.FirstOrDefaultAsync(cancellationToken)
			.ToAff()
			.Map(e => e is null ? Option<TEntity>.None : Option<TEntity>.Some(e));
	}

	/// <inheritdoc/>
	public virtual ValueTask<OptionalResult<TEntity>> FirstOrDefaultAsync(
		ISpecification<TEntity> specification,
		CancellationToken cancellationToken = default)
	{
		Func<Task<TEntity?>> func = () =>
		{
			var queryResult = ApplySpecification(specification, dbContext.Set<TEntity>());
			return queryResult.FirstOrDefaultAsync(cancellationToken);
		};
		return func.TryOptionalResultAsync();
	}

	/// <inheritdoc/>
	public virtual ValueTask<OptionalResult<TResult>> FirstOrDefaultAsync<TResult>(
		ISpecification<TEntity, TResult> specification,
		CancellationToken cancellationToken = default)
		where TResult : class
	{
		Func<Task<TResult?>> func = () =>
		{
			var queryResult = ApplySpecification(specification);
			return queryResult.Match(
				q => q.FirstOrDefaultAsync(cancellationToken),
				ex => Task.FromException<TResult?>(ex));
		};
		return func.TryOptionalResultAsync();
	}

	public Aff<TEntity> SingleAsync(CancellationToken cancellationToken = default)
	{
		return dbContext.Set<TEntity>()
			.SingleAsync(cancellationToken)
			.ToAff();
	}

	public ValueTask<OptionalResult<TEntity>> SingleOrDefaultAsync(CancellationToken cancellationToken = default)
	{
		return dbContext.Set<TEntity>()
			.SingleOrDefaultAsync(cancellationToken)
			.TryOptionalResultAsync();
	}

	/// <inheritdoc/>
	public virtual ValueTask<OptionalResult<TEntity>> SingleOrDefaultAsync(
		ISingleResultSpecification<TEntity> specification,
		CancellationToken cancellationToken = default)
	{
		Func<Task<TEntity?>> func = () =>
		{
			var queryResult = ApplySpecification(specification, dbContext.Set<TEntity>());

			return queryResult.SingleOrDefaultAsync(cancellationToken);
		};
		return func.TryOptionalResultAsync();
	}

	/// <inheritdoc/>
	public virtual ValueTask<OptionalResult<TResult>> SingleOrDefaultAsync<TResult>(
		ISingleResultSpecification<TEntity, TResult> specification,
		CancellationToken cancellationToken = default)
		where TResult : class
	{

		Func<Task<TResult?>> func = () =>
		{
			var queryResult = ApplySpecification(specification);
			return queryResult.Match(
				q => q.SingleOrDefaultAsync(cancellationToken),
				ex => Task.FromException<TResult?>(ex));
		};
		return func.TryOptionalResultAsync();
	}

	/// <inheritdoc/>
	public virtual ValueTask<Result<Lst<TEntity>>> ListAsync(CancellationToken cancellationToken = default)
	{
		return dbContext.Set<TEntity>()
			.ToListAsync(cancellationToken)
			.TryListResultAsync();
	}

	/// <inheritdoc/>
	public virtual ValueTask<Result<Lst<TEntity>>> ListAsync(
		ISpecification<TEntity> specification,
		CancellationToken cancellationToken = default)
	{
		Func<Task<List<TEntity>>> func = () =>
		{
			var queryResult = ApplySpecification(specification, dbContext.Set<TEntity>());
			return queryResult.ToListAsync(cancellationToken);
		};

		return func.TryListResultAsync();
	}

	/// <inheritdoc/>
	public virtual ValueTask<Result<Lst<TResult>>> ListAsync<TResult>(
		ISpecification<TEntity, TResult> specification,
		CancellationToken cancellationToken = default)
	{
		Func<Task<List<TResult>>> func = () =>
		{
			var queryResult = ApplySpecification(specification);
			return queryResult.Match(
				q => q.ToListAsync(cancellationToken),
				ex => Task.FromException<List<TResult>>(ex));
		};
		return func.TryListResultAsync();
	}

	/// <inheritdoc/>
	public virtual ValueTask<Result<int>> CountAsync(
		ISpecification<TEntity> specification,
		CancellationToken cancellationToken = default)
	{
		Func<Task<int>> func = () =>
		{
			var queryResult = ApplySpecification(specification, dbContext.Set<TEntity>());
			return queryResult.CountAsync(cancellationToken);
		};
		return func.TryResultAsync();
	}

	/// <inheritdoc/>
	public virtual Task<Result<int>> CountAsync(CancellationToken cancellationToken = default)
	{
		return dbContext.Set<TEntity>().CountAsync(cancellationToken).TryResultAsync();
	}

	/// <inheritdoc/>
	public virtual ValueTask<Result<bool>> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
	{
		Func<Task<bool>> func = () =>
		{
			var queryResult = ApplySpecification(specification, dbContext.Set<TEntity>());
			return queryResult.AnyAsync(cancellationToken);
		};
		return func.TryResultAsync();
	}

	/// <inheritdoc/>
	public virtual Task<Result<bool>> AnyAsync(CancellationToken cancellationToken = default)
	{
		return dbContext.Set<TEntity>()
			.AnyAsync(cancellationToken)
			.TryResultAsync();
	}

	/// <inheritdoc/>
	public virtual Result<IAsyncEnumerable<TEntity>> StreamAsync()
	{
		var @try = Try(dbContext.Set<TEntity>().AsAsyncEnumerable());
		return @try.Match(
			data => data.AsResult(),
			ex => new Result<IAsyncEnumerable<TEntity>>(ex));
	}

	/// <inheritdoc/>
	public virtual Result<IAsyncEnumerable<TEntity>> StreamAsync(ISpecification<TEntity> specification)
	{
		var @try = Try(() =>
		{
			var queryResult = ApplySpecification(specification, dbContext.Set<TEntity>());
			return queryResult.AsAsyncEnumerable();
		});
		return @try.Match(
			data => data.AsResult(),
			ex => new Result<IAsyncEnumerable<TEntity>>(ex));
	}

	/// <inheritdoc/>
	public virtual Result<IAsyncEnumerable<TResult>> StreamAsync<TResult>(ISpecification<TEntity, TResult> specification)
		where TResult : class
	{
		var @try = Try(() =>
		{
			var queryResult = ApplySpecification(specification);
			return queryResult.Match(
				q => q.AsAsyncEnumerable(),
				ex => throw ex);
		});
		return @try.Match(
			data => data.AsResult(),
			ex => ex.AsResult<IAsyncEnumerable<TResult>>());
	}

	public virtual Result<IQueryable<TEntity>> FromSqlInterpolated(
		[NotNullWhen(false)] FormattableString sql)
	{
		return dbContext.Set<TEntity>().FromSqlInterpolated(sql).AsResult();
	}

	public virtual Result<IQueryable<TEntity>> FromSqlRaw(string sql, params object[] parameters)
	{
		return dbContext.Set<TEntity>().FromSqlRaw(sql, parameters).AsResult();
	}

	public virtual Result<IQueryable<TEntity>> FromSqlInterpolated(
		[NotNullWhen(false)] FormattableString sql,
		[NotNullWhen(false)] ISpecification<TEntity> specification)
	{
		return sql.AsConditional().Match(
			s => s is null,
			_ => new ArgumentNullException(nameof(sql)).AsResult<IQueryable<TEntity>>(),
			_ =>
			{
				var query = dbContext.Set<TEntity>().FromSqlInterpolated(sql);
				query = ApplySpecification(specification, query);
				return query.AsResult();
			});
	}

	public virtual IQueryable<TEntity> FromSqlRaw(
		string sql,
		ISpecification<TEntity> specification,
		params object[] parameters)
	{
		var query = dbContext.Set<TEntity>().FromSqlRaw(sql, parameters);
		query = ApplySpecification(specification, query);
		return query;
	}

	/// <summary>
	/// Filters the entities  of <typeparamref name="TEntity"/>, to those that match the encapsulated query logic of the
	/// <paramref name="specification"/>.
	/// </summary>
	/// <param name="specification">The encapsulated query logic.</param>
	/// <returns>The filtered entities as >an <see cref="IQueryable{T}"/>.</returns>
	protected virtual IQueryable<TEntity> ApplySpecification(
		ISpecification<TEntity> specification,
		IQueryable<TEntity> query,
		bool evaluateCriteriaOnly = false)
	{
		if (specification is null)
		{
			throw new NullInputException(nameof(specification));
		}

		return specificationEvaluator
			.GetQuery(
				query,
				specification,
				evaluateCriteriaOnly)
			.Match(q => q, _ => query);
	}

	/// <summary>
	/// Filters all entities of <typeparamref name="TEntity" />, that matches the encapsulated query logic of the
	/// <paramref name="specification"/>, from the database.
	/// <para>
	/// Projects each entity into a new form, being <typeparamref name="TResult" />.
	/// </para>
	/// </summary>
	/// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
	/// <param name="specification">The encapsulated query logic.</param>
	/// <returns>The filtered projected entities as an <see cref="IQueryable{T}"/>.</returns>
	protected virtual Result<IQueryable<TResult>> ApplySpecification<TResult>(ISpecification<TEntity, TResult> specification)
	{
		if (specification is null)
		{
			throw new NullInputException(nameof(specification));
		}

		var query = dbContext.Set<TEntity>().AsQueryable();

		return specificationEvaluator.GetQuery(query, specification);
	}
}


internal class EfRepository<T> : RepositoryBase<T>
	where T : class
{
	public EfRepository(DbContext dbContext)
		: base(dbContext)
	{
	}
}
