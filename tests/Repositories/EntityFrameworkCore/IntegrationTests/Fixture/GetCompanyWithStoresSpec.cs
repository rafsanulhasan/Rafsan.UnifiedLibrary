using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.EntityFrameworkCore.IntegrationTests.Fixture
{
	public class GetCompanyWithStoresSpec : Specification<Company>, ISingleResultSpecification<Company>
  {
    public GetCompanyWithStoresSpec(int companyId)
    {
      this.Query.Where(x => x.Id == companyId).Include(x => x.Stores);
    }
  }
}
