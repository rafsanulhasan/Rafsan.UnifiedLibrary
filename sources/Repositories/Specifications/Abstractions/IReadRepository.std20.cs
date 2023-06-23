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
	/// <summary>
	/// Returns the first element of a sequence, or a default value if the sequence contains no elements.
	/// </summary>
	/// <param name="specification">The encapsulated query logic.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The <see cref="ValueTask{TResult}"/> contains the <see cref="Result{T}"/>, or an Error <see cref="Error"/>.
	/// </returns>
	ValueTask<Result<TEntity>> FirstAsync(
		ISpecification<TEntity> specification,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the first element of a sequence, or a default value if the sequence contains no elements.
	/// </summary>
	/// <param name="specification">The encapsulated query logic.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The <see cref="ValueTask{TResult}"/> contains the <see cref="Result{T}"/>, or an Error <see cref="Error"/>.
	/// </returns>
	ValueTask<Result<TResult>> FirstAsync<TResult>(
		ISpecification<TEntity, TResult> specification,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the first element of a sequence, or a default value if the sequence contains no elements.
	/// </summary>
	/// <param name="specification">The encapsulated query logic.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The <see cref="ValueTask{TResult}"/> contains the <see cref="Result{T}"/>, or an Error <see cref="Error"/>.
	/// </returns>
	ValueTask<Result<Option<TEntity>>> FirstOrDefaultAsync(
		ISpecification<TEntity> specification,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the first element of a sequence, or a default value if the sequence contains no elements.
	/// </summary>
	/// <param name="specification">The encapsulated query logic.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The <see cref="ValueTask{TResult}"/> contains the <see cref="Result{T}"/>, or an Error <see cref="Error"/>.
	/// </returns>
	ValueTask<Result<Option<TResult>>> FirstOrDefaultAsync<TResult>(
		ISpecification<TEntity, TResult> specification,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.
	/// </summary>
	/// <param name="specification">The encapsulated query logic.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains the <typeparamref name="TEntity" />, or <see langword="null"/>.
	/// </returns>
	ValueTask<Result<TEntity>> SingleAsync(
		ISingleResultSpecification<TEntity> specification,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.
	/// </summary>
	/// <param name="specification">The encapsulated query logic.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains the <typeparamref name="TEntity" />, or <see langword="null"/>.
	/// </returns>
	ValueTask<Result<TResult>> SingleAsync<TResult>(
		ISingleResultSpecification<TEntity, TResult> specification,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.
	/// </summary>
	/// <param name="specification">The encapsulated query logic.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains the <typeparamref name="TEntity" />, or <see langword="null"/>.
	/// </returns>
	ValueTask<Result<Option<TEntity>>> SingleOrDefaultAsync(
		ISingleResultSpecification<TEntity> specification,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.
	/// </summary>
	/// <param name="specification">The encapsulated query logic.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains the <typeparamref name="TResult" />, or <see langword="null"/>.
	/// </returns>
	ValueTask<Result<Option<TResult>>> SingleOrDefaultAsync<TResult>(
		ISingleResultSpecification<TEntity, TResult> specification,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" />, that matches the encapsulated query logic of the
	/// <paramref name="specification"/>, from the database.
	/// </summary>
	/// <param name="specification">The encapsulated query logic.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<Result<Option<List<TEntity>>>> ListAsync(
		ISpecification<TEntity> specification,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" />, that matches the encapsulated query logic of the
	/// <paramref name="specification"/>, from the database.
	/// <para>
	/// Projects each entity into a new form, being <typeparamref name="TResult" />.
	/// </para>
	/// </summary>
	/// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
	/// <param name="specification">The encapsulated query logic.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains a <see cref="List{TResult}" /> that contains elements from the input sequence.
	/// </returns>
	ValueTask<Result<Option<List<TResult>>>> ListAsync<TResult>(
		ISpecification<TEntity, TResult> specification,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a number that represents how many entities satisfy the encapsulated query logic
	/// of the <paramref name="specification"/>.
	/// </summary>
	/// <param name="specification">The encapsulated query logic.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation. The task result contains the
	/// number of elements in the input sequence.
	/// </returns>
	ValueTask<Result<int>> CountAsync(
		ISpecification<TEntity> specification,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a boolean that represents whether any entity satisfy the encapsulated query logic
	/// of the <paramref name="specification"/> or not.
	/// </summary>
	/// <param name="specification">The encapsulated query logic.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation. The task result contains true if the 
	/// source sequence contains any elements; otherwise, false.
	/// </returns>
	ValueTask<Result<bool>> AnyAsync(
		ISpecification<TEntity> specification,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" />, that matches the encapsulated query logic of the
	/// <paramref name="specification"/>, from the database.
	/// </summary>
	/// <param name="specification">The encapsulated query logic.</param>
	/// <returns>
	/// Returns an <see cref="IAsyncEnumerable{T}"/> which can be enumerated asynchronously.
	/// </returns>
	Result<IAsyncEnumerable<TEntity>> StreamAsync(ISpecification<TEntity> specification);

	/// <summary>
	/// Finds all entities of <typeparamref name="TEntity" />, that matches the encapsulated query logic of the
	/// <paramref name="specification"/>, from the database.
	/// <para>
	/// Projects each entity into a new form, being <typeparamref name="TResult" />.
	/// </para>
	/// </summary>
	/// <param name="specification">The encapsulated query logic.</param>
	/// <returns>
	/// Returns an <see cref="IAsyncEnumerable{T}"/> which can be enumerated asynchronously.
	/// </returns>
	Result<IAsyncEnumerable<TResult>> StreamAsync<TResult>(ISpecification<TEntity, TResult> specification);

	Result<IQueryable<TEntity>> FromSqlInterpolated(
		FormattableString sql,
		ISpecification<TEntity> specification);

	Result<IQueryable<TEntity>> FromSqlRaw(
		string sql,
		ISpecification<TEntity> specification,
		params object[] parameters);
}
