using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Rafsan.DataAccess.Repositories;

namespace Rafsan.DataAccess.EntityFrameworkCore;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TRepository">The Interface of the repository created by this Factory</typeparam>
/// <typeparam name="TConcreteRepository">
/// The Concrete implementation of the repository interface to create
/// </typeparam>
/// <typeparam name="TContext">The DbContext derived class to support the concrete repository</typeparam>
public partial class EFRepositoryFactory : IRepositoryFactory
{
	private readonly IServiceProvider serviceProvider;

	/// <summary>
	/// Initialises a new instance of the EFRepositoryFactory
	/// </summary>
	/// <param name="serviceProvider">The <see cref="IServiceProvider"/> to resolve dependencies from IoC container.</param>
	public EFRepositoryFactory(IServiceProvider serviceProvider)
	{
		this.serviceProvider = serviceProvider;
	}

	/// <inheritdoc />
	public IRepository<TEntity> ResolveRepository<TEntity, TContext>(ServiceLifetime lifetime)
		where TEntity : class
		where TContext : DbContext
	{
		if (lifetime != ServiceLifetime.Scoped)
		{
			return serviceProvider.GetRequiredService<IRepository<TEntity>>();
		}

		using var scope = serviceProvider.CreateScope();
		return scope.ServiceProvider.GetRequiredService<IRepository<TEntity>>();
	}

	public IRepository<TEntity> ResolveRepositoryFromPool<TEntity, TContext>(
		bool fromFactory = true,
		CancellationToken cancellationToken = default
	)
		where TEntity : class
		where TContext : DbContext
	{
		if (!fromFactory)
		{
			return serviceProvider.GetRequiredService<IRepository<TEntity>>();
		}

		var factory = serviceProvider.GetRequiredService<IDbContextFactory<TContext>>();
		var dbContext = factory.CreateDbContext();
		return new EfRepository<TEntity>(dbContext);
	}
}
