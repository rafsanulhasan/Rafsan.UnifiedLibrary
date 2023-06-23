using LanguageExt;

namespace Rafsan.DataAccess.Repositories;

/// <summary>
/// <para>
/// A <see cref="IWriteRepository{T}" /> can be used to query and save instances of <typeparamref name="T" />.
/// An <see cref="ISpecification{T}"/> (or derived) is used to encapsulate the LINQ queries against the database.
/// </para>
/// </summary>
/// <typeparam name="T">The type of entity being operated on by this repository.</typeparam>
public interface IWriteRepository<T> where T : class
{
	/// <summary>
	/// Adds an entity in the database.
	/// </summary>
	/// <param name="entity">The entity to add.</param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains the <typeparamref name="T" />.
	/// </returns>
	Eff<T> Add(T entity);

	/// <summary>
	/// Adds an entity in the database.
	/// </summary>
	/// <param name="entity">The entity to add.</param>
	/// <param name="cancellationToken"></param>
	/// <returns>
	/// A task that represents the asynchronous operation.
	/// The task result contains the <typeparamref name="T" />.
	/// </returns>
	Aff<T> AddAsync(T entity, CancellationToken cancellationToken = default);

	/// <summary>
	/// Adds the given entities in the database
	/// </summary>
	/// <param name="entities"></param>
	/// <returns>
	/// The task result contains the <see cref="IEnumerable{T}" />.
	/// </returns>
	IEnumerable<T> AddRange(IEnumerable<T> entities);

	/// <summary>
	/// Updates an entity in the database
	/// </summary>
	/// <param name="entity">The entity to update.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	void Update(T entity);

	/// <summary>
	/// Updates the given entities in the database
	/// </summary>
	/// <param name="entities">The entities to update.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	void UpdateRange(IEnumerable<T> entities);

	/// <summary>
	/// Removes an entity in the database
	/// </summary>
	/// <param name="entity">The entity to delete.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	void Delete(T entity);

	/// <summary>
	/// Removes the given entities in the database
	/// </summary>
	/// <param name="entities">The entities to remove.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	void DeleteRange(IEnumerable<T> entities);
}
