using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class CompanyByIdIgnoreQueryFilters : Specification<Company>, ISingleResultSpecification
  {
    public CompanyByIdIgnoreQueryFilters(int id)
    {
      Query.Where(company => company.Id == id).IgnoreQueryFilters();
    }
  }
}
