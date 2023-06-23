using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoreByIdSearchByNameOrCitySpec : Specification<Store>
  {
    public StoreByIdSearchByNameOrCitySpec(int id, string searchTerm)
    {
      Query.Where(x => x.Id == id)
          .Search(x => x.Name!, "%" + searchTerm + "%")
          .Search(x => x.City!, "%" + searchTerm + "%");
    }
  }
}
