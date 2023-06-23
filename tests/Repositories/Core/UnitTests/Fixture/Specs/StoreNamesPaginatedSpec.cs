using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoreNamesPaginatedSpec : Specification<Store, string?>
  {
    public StoreNamesPaginatedSpec(int skip, int take)
    {
      Query.OrderBy(x => x.Id)
          .Skip(skip)
          .Take(take);

      Query.Select(x => x.Name);
    }
  }
}
