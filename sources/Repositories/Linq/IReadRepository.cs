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
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<Result<TEntity>> FirstAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	Task<Result<TResult>> FirstAsync<TResult>(
		Func<TEntity, TResult> selector,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<Result<TResult>> FirstAsync<TResult>(
		Expression<Func<TEntity, bool>> predicate,
		Func<TEntity, TResult> selector,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<Option<TEntity>> FirstOrDefaultAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<Option<TEntity>> FirstOrDefaultAsync<TResult>(
		Func<TEntity, TResult> selector,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<Option<TResult>> FirstOrDefaultAsync<TResult>(
		Expression<Func<TEntity, bool>> predicate,
		Func<TEntity, TResult> selector,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<Result<TEntity>> SingleAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<Result<TResult>> SingleAsync<TResult>(
		Func<TEntity, TResult> selector,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<Result<TResult>> SingleAsync<TResult>(
		Expression<Func<TEntity, bool>> predicate,
		Func<TEntity, TResult> selector,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<Option<TEntity>> SingleOrDefaultAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<Option<TResult>> SingleOrDefaultAsync<TResult>(
		Func<TEntity, TResult> selector,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<Option<TResult>> SingleOrDefaultAsync<TResult>(
		Expression<Func<TEntity, bool>> predicate,
		Func<TEntity, TResult> selector,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<List<TEntity>> ListAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<List<TResult>> ListAsync<TResult>(
		Func<TEntity, TResult> selector,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" /> from the database.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<List<TResult>> ListAsync<TResult>(
		Expression<Func<TEntity, bool>> predicate,
		Func<TEntity, TResult> selector,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the total number of records.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation. The task result contains the
	/// number of elements in the input sequence.
	/// </returns>
	ValueTask<int> CountAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a boolean whether any entity exists or not.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation. The task result contains true if the 
	/// source sequence contains any elements; otherwise, false.
	/// </returns>
	ValueTask<bool> AnyAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" />.
	/// </summary>
	/// <returns>
	/// Returns an <see cref="IAsyncEnumerable{T}"/> which can be enumerated asynchronously.
	/// </returns>
	Result<IAsyncEnumerable<TEntity>> StreamAsync(Expression<Func<TEntity, bool>> predicate);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" />.
	/// </summary>
	/// <returns>
	/// Returns an <see cref="IAsyncEnumerable{T}"/> which can be enumerated asynchronously.
	/// </returns>
	Result<IAsyncEnumerable<TResult>> StreamAsync<TResult>(
		Func<TEntity, TResult> selector,
		Expression<Func<TEntity, bool>> predicate);
}
