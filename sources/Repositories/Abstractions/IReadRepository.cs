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
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	Aff<TEntity> FirstAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	Aff<Option<TEntity>> FirstOrDefaultAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	Aff<TEntity> SingleAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<OptionalResult<TEntity>> SingleOrDefaultAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<Result<Lst<TEntity>>> ListAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the total number of records.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation. The task result contains the
	/// number of elements in the input sequence.
	/// </returns>
	Task<Result<int>> CountAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a boolean whether any entity exists or not.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation. The task result contains true if the 
	/// source sequence contains any elements; otherwise, false.
	/// </returns>
	Task<Result<bool>> AnyAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" />.
	/// </summary>
	/// <returns>
	/// Returns an <see cref="IAsyncEnumerable{T}"/> which can be enumerated asynchronously.
	/// </returns>
	Result<IAsyncEnumerable<TEntity>> StreamAsync();
}
