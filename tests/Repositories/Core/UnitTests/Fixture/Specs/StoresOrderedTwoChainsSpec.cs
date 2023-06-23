using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoresOrderedTwoChainsSpec : Specification<Store>
  {
    public StoresOrderedTwoChainsSpec()
    {
      Query.OrderBy(x => x.Name)
          .OrderBy(x => x.Id);
    }
  }
}
