namespace Rafsan.DataAccess.Repositories;

using System.Diagnostics.CodeAnalysis;

using LanguageExt;
using LanguageExt.Common;

using Microsoft.EntityFrameworkCore;

using static LanguageExt.Prelude;

public abstract partial class RepositoryBase<TEntity> : IRepository<TEntity>
	where TEntity : class
{
	private readonly DbContext dbContext;

	/// <inheritdoc />
	protected RepositoryBase(DbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	/// <inheritdoc />
	protected RepositoryBase(IDbContextFactory<DbContext> dbContextFactory)
		: this(dbContextFactory.CreateDbContext())
	{
	}

	/// <inheritdoc/>
	public virtual TEntity Add(TEntity entity)
	{
		dbContext.Set<TEntity>().Add(entity);
		return entity;
	}

	/// <inheritdoc/>
	public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		await dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
		return entity;
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
	public virtual async ValueTask<OptionalResult<TEntity>> ReadByIdAsync<TId>(
		[NotNullWhen(false)] TId id,
		CancellationToken cancellationToken = default
	)
		where TId : notnull
	{
		return await id.AsConditional().MatchAsync(
			k => k is null,
			_ => new ArgumentNullException(nameof(id)).AsOptionalResult<TEntity>().AsValueTask(),
			_ => dbContext.Set<TEntity>()
				.FindAsync(new object[] { id }, cancellationToken: cancellationToken)
				.TryOptionalResultAsync());
	}

	public Aff<TEntity> FirstAsync(CancellationToken cancellationToken = default) =>
		dbContext.Set<TEntity>().FirstAsync(cancellationToken).ToAff();

	public Aff<Option<TEntity>> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
	{
		return dbContext.Set<TEntity>()
			.FirstOrDefaultAsync(cancellationToken)
			.ToAff()
			.MatchAff(
				e => SuccessAff(e.AsOption()),
				FailAff<Option<TEntity>>);
	}

	public Task<Result<TEntity>> SingleAsync(CancellationToken cancellationToken = default)
	{
		return dbContext.Set<TEntity>()
			.SingleAsync(cancellationToken)
			.TryResultAsync();
	}

	public ValueTask<OptionalResult<TEntity>> SingleOrDefaultAsync(CancellationToken cancellationToken = default)
	{
		return dbContext.Set<TEntity>()
			.SingleOrDefaultAsync(cancellationToken)
			.TryOptionalResultAsync();
	}

	/// <inheritdoc/>
	public virtual ValueTask<Result<Lst<TEntity>>> ListAsync(CancellationToken cancellationToken = default)
	{
		return dbContext.Set<TEntity>()
			.ToListAsync(cancellationToken)
			.TryListResultAsync();
	}

	/// <inheritdoc/>
	public virtual Task<Result<bool>> AnyAsync(CancellationToken cancellationToken = default)
	{
		return dbContext.Set<TEntity>()
			.AnyAsync(cancellationToken)
			.TryResultAsync();
	}

	/// <inheritdoc/>
	public virtual Task<Result<int>> CountAsync(CancellationToken cancellationToken = default)
	{
		return dbContext.Set<TEntity>().CountAsync(cancellationToken).TryResultAsync();
	}

	/// <inheritdoc/>
	public virtual Result<IAsyncEnumerable<TEntity>> StreamAsync()
	{
		Func<IAsyncEnumerable<TEntity>> func = dbContext.Set<TEntity>().AsAsyncEnumerable;
		return func.TryResult();
	}

	public virtual Result<IQueryable<TEntity>> FromSqlRaw(
		[NotNullWhen(false)] string sql,
		params object[] parameters)
	{
		return sql.AsConditional().Match(
			s => s is null,
			_ => new ArgumentNullException(nameof(sql)).AsResult<IQueryable<TEntity>>(),
			s => Try(() => dbContext.Set<TEntity>().FromSqlRaw(sql, parameters)).Match(
				q => q.AsResult(),
				ex => ex.AsResult<IQueryable<TEntity>>())
		);
	}
}

internal class EfRepository<T> : RepositoryBase<T>
	where T : class
{
	public EfRepository(DbContext dbContext)
		: base(dbContext)
	{
	}

	public EfRepository(IDbContextFactory<DbContext> dbContextFactory)
		: base(dbContextFactory)
	{
	}
}
