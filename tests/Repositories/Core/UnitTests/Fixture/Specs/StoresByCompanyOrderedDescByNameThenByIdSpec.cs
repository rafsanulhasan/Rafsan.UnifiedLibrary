using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoresByCompanyOrderedDescByNameThenByIdSpec : Specification<Store>
  {
    public StoresByCompanyOrderedDescByNameThenByIdSpec(int companyId)
    {
      Query.Where(x => x.CompanyId == companyId)
           .OrderByDescending(x => x.Name)
           .ThenBy(x => x.Id);
    }
  }
}
