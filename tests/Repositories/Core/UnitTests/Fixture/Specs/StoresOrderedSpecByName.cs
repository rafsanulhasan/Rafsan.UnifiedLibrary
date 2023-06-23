using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoresOrderedSpecByName : Specification<Store>
  {
    public StoresOrderedSpecByName()
    {
      Query.OrderBy(x => x.Name);
    }
  }
}
