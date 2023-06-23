using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoreByIdIncludeProductsSpec : Specification<Store>, ISingleResultSpecification
  {
    public StoreByIdIncludeProductsSpec(int id)
    {
      Query.Where(x => x.Id == id)
          .Include(x => x.Products);
    }
  }
}
