using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoreByIdIncludeProductsUsingStringSpec : Specification<Store>, ISingleResultSpecification
  {
    public StoreByIdIncludeProductsUsingStringSpec(int id)
    {
      Query.Where(x => x.Id == id)
          .Include(nameof(Store.Products));
    }
  }
}
