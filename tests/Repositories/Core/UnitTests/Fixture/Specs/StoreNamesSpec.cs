using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoreNamesSpec : Specification<Store, string?>
  {
    public StoreNamesSpec()
    {
      Query.Select(x => x.Name);
    }
  }
}
