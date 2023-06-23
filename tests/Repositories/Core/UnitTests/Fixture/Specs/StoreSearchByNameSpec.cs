using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoreSearchByNameSpec : Specification<Store>
  {
    public StoreSearchByNameSpec(string searchTerm)
    {
      Query.Search(x => x.Name!, "%" + searchTerm + "%");
    }
  }
}
