using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

using LanguageExt;
using LanguageExt.Common;

namespace Rafsan.DataAccess.Repositories;

/// <summary>
/// <para>
/// A <see cref="IRepository{T}" /> can be used to query instances of <typeparamref name="TEntity" />.
/// </para>
/// </summary>
/// <typeparam name="TEntity">The type of entity being operated on by this repository.</typeparam>
public partial interface IReadRepository<TEntity> where TEntity : notnull
{
	/// <summary>
	/// Finds an entity with the given primary key value.
	/// </summary>
	/// <typeparam name="TId">The type of primary key.</typeparam>
	/// <param name="id">The value of the primary key for the entity to be found.</param>
	/// <param name="cancellationToken"></param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The <see cref="ValueTask{TResult}"/> contains the <see cref="Result{T}"/>, or an Error <see cref="Error"/>.
	/// </returns>
	Aff<Option<TEntity>> ReadByIdAsync<TId>(
		[NotNullWhen(false)] TId id,
		CancellationToken cancellationToken = default)
		where TId : notnull;

	Result<IQueryable<TEntity>> FromSql(
		[NotNullWhen(false)] FormattableString sql);

	Result<IQueryable<TEntity>> FromSqlInterpolated(
		[NotNullWhen(false)] FormattableString sql);

	Result<IQueryable<TEntity>> FromSqlRaw(
		[NotNullWhen(false)] string sql,
		params object[] parameters);
}
