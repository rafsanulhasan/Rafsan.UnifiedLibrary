using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoresByCompanyPaginatedSpec : Specification<Store>
  {
    public StoresByCompanyPaginatedSpec(int companyId, int skip, int take)
    {
      Query.Where(x => x.CompanyId == companyId)
          .OrderBy(x => x.CompanyId)
           .Skip(skip)
           .Take(take);
    }
  }
}
