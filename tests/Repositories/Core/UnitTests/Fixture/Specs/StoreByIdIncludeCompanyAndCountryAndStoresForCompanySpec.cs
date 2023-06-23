using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoreByIdIncludeCompanyAndCountryAndStoresForCompanySpec : Specification<Store>, ISingleResultSpecification
  {
    public StoreByIdIncludeCompanyAndCountryAndStoresForCompanySpec(int id)
    {
      Query.Where(x => x.Id == id);
      Query.Include(x => x.Company).ThenInclude(x => x!.Country);
      Query.Include(x => x.Company).ThenInclude(x => x!.Stores).ThenInclude(x => x.Products);
    }
  }
}
