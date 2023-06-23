namespace Rafsan.DataAccess.Repositories;

using System.Diagnostics.CodeAnalysis;
using System.Linq;

using LanguageExt.Common;

using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;
using Rafsan.DataAccess.Repositories.Abstractions;
using LanguageExt;

/// <inheritdoc/>
public abstract partial class RepositoryBase<TEntity> : IRepository<TEntity>
	where TEntity : class
{
	public virtual Result<IQueryable<TEntity>> FromSql(
		FormattableString sql)
	{
		if (string.IsNullOrWhiteSpace(sql.Format))
		{
			return new Result<IQueryable<TEntity>>(new ArgumentNullException(nameof(sql)));
		}

		return dbContext.Set<TEntity>().FromSql(sql).AsResult();
	}

	public virtual Result<IQueryable<TEntity>> FromSql(
		[NotNullWhen(false)] FormattableString sql,
		[NotNullWhen(false)] ISpecification<TEntity> specification)
	{
		if (string.IsNullOrWhiteSpace(sql.Format))
		{
			return new Result<IQueryable<TEntity>>(new ArgumentNullException(nameof(sql)));
		}

		if (specification is null)
		{
			return new Result<IQueryable<TEntity>>(new ArgumentNullException(nameof(specification)));
		}

		var query = dbContext.Set<TEntity>().FromSql(sql);
		query = ApplySpecification(specification, query);
		return query.AsResult();
	}
}
