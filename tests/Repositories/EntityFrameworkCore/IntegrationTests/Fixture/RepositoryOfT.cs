using Rafsan.DataAccess.EntityFrameworkCore;
using Rafsan.DataAccess.EntityFrameworkCore.Evaluators;
using Rafsan.DataAccess.Repositories.Evaluators;

namespace Ardalis.Specification.EntityFrameworkCore.IntegrationTests.Fixture
{
	/// <inheritdoc/>
	public class Repository<T> : RepositoryBase<T> where T : class
  {
    protected readonly TestDbContext dbContext;

    public Repository(TestDbContext dbContext) : this(dbContext, SpecificationEvaluator.Default)
    {
    }

    public Repository(TestDbContext dbContext, ISpecificationEvaluator specificationEvaluator) : base(dbContext, specificationEvaluator)
    {
      this.dbContext = dbContext;
    }
  }
}
