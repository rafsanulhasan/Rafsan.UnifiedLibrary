using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoreByIdSpec : Specification<Store>
  {
    public StoreByIdSpec(int Id)
    {
      Query.Where(x => x.Id == Id);
    }
  }
}
