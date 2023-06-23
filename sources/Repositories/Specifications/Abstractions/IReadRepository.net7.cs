using System.Diagnostics.CodeAnalysis;

using LanguageExt.Common;

namespace Rafsan.DataAccess.Repositories.Abstractions;

/// <summary>
/// <para>
/// A <see cref="IRepository{T}" /> can be used to query instances of <typeparamref name="TEntity" />.
/// An <see cref="ISpecification{T}"/> (or derived) is used to encapsulate the LINQ queries against the database.
/// </para>
/// </summary>
/// <typeparam name="TEntity">The type of entity being operated on by this repository.</typeparam>
public partial interface IReadRepository<TEntity>
{
	Result<IQueryable<TEntity>> FromSql(
		[NotNullWhen(false)] FormattableString sql,
		[NotNullWhen(false)] ISpecification<TEntity> specification);
}
