using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoresByCompanyOrderedDescByNameThenByDescIdSpec : Specification<Store>
  {
    public StoresByCompanyOrderedDescByNameThenByDescIdSpec(int companyId)
    {
      Query.Where(x => x.CompanyId == companyId)
           .OrderByDescending(x => x.Name)
           .ThenByDescending(x => x.Id);
    }
  }
}
