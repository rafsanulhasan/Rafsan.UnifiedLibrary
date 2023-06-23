using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoreSearchByNameOrCitySpec : Specification<Store>
  {
    public StoreSearchByNameOrCitySpec(string searchTerm)
    {
      Query.Search(x => x.Name!, "%" + searchTerm + "%")
          .Search(x => x.City!, "%" + searchTerm + "%");
    }
  }
}
