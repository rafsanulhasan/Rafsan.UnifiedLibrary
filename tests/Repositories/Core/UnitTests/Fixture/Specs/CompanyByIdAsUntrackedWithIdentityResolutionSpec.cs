using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class CompanyByIdAsUntrackedWithIdentityResolutionSpec : Specification<Company>, ISingleResultSpecification
  {
    public CompanyByIdAsUntrackedWithIdentityResolutionSpec(int id)
    {
      Query.Where(company => company.Id == id).AsNoTrackingWithIdentityResolution();
    }
  }
}
