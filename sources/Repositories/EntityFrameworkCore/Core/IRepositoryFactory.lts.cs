using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Rafsan.DataAccess.Repositories;

namespace Rafsan.DataAccess.EntityFrameworkCore;

/// <summary>
/// Generates new instances to encapsulate the 'Repository' pattern
/// in scenarios where injected types may be long-lived (e.g. Blazor)
/// </summary>
public partial interface IRepositoryFactory
{
	/// <summary>
	/// Generates a new <typeparamref name="TRepository"/> instance.
	/// </summary>
	/// <typeparam name="TRepository">
	/// The Interface of the Repository to be generated.
	/// </typeparam>
	/// <returns>The generated repository instance</returns>
	public ValueTask<IRepository<TEntity>> ResolveRepository<TEntity, TContext>(ServiceLifetime lifetime = ServiceLifetime.Scoped)
		where TEntity : class
		where TContext : DbContext;

	/// <summary>
	/// Generates a new <typeparamref name="TRepository"/> instance.
	/// </summary>
	/// <typeparam name="TRepository">
	/// The Interface of the Repository to be generated.
	/// </typeparam>
	/// <returns>The generated repository instance</returns>
	public ValueTask<IRepository<TEntity>> ResolveRepositoryFromPool<TEntity, TContext>(
		bool fromFactory = true,
		CancellationToken cancellationToken = default
	)
		where TEntity : class
		where TContext : DbContext;
}
