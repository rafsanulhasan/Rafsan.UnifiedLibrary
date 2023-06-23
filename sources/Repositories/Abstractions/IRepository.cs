namespace Rafsan.DataAccess.Repositories;

/// <summary>
/// <para>
/// A <see cref="IWriteRepository{T}" /> can be used to query and save instances of <typeparamref name="T" />.
/// </para>
/// </summary>
/// <typeparam name="T">The type of entity being operated on by this repository.</typeparam>
public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
	where T : class
{
	/// <summary>
	/// Persists changes to the database.
	/// </summary>
	/// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
