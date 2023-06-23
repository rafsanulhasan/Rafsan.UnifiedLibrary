using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class CompanyByIdAsSplitQuery : Specification<Company>, ISingleResultSpecification
  {
    public CompanyByIdAsSplitQuery(int id)
    {
      Query.Where(company => company.Id == id)
          .Include(x => x.Stores)
          .ThenInclude(x => x.Products)
          .AsSplitQuery();
    }
  }
}
