namespace Rafsan.DataAccess.Repositories;

using System.Diagnostics.CodeAnalysis;
using System.Linq;

using LanguageExt;
using LanguageExt.Common;

using Microsoft.EntityFrameworkCore;

using static LanguageExt.Guard;
using static LanguageExt.Prelude;

/// <inheritdoc/>
public abstract partial class RepositoryBase<TEntity> : IRepository<TEntity>
	where TEntity : class
{
	public virtual Result<IQueryable<TEntity>> FromSql(
		[NotNullWhen(false)] FormattableString sql)
	{
		return sql.AsConditional().Match(
			s => s is null,
			_ => new ArgumentNullException(nameof(sql)).AsResult<IQueryable<TEntity>>(),
			_ => sql.Format.AsConditional().Match(
				f => string.IsNullOrWhiteSpace(f),
				_ => new ArgumentWhiteSpaceException(nameof(sql)).AsResult<IQueryable<TEntity>>(),
				_ => Try(() => dbContext.Set<TEntity>().FromSql(sql)).Match(
					q => q.AsResult(),
					ex => ex.AsResult<IQueryable<TEntity>>()))
			);
	}
}
