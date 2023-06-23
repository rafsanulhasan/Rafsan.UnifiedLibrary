using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoreDuplicateTakeSpec : Specification<Store>
  {
    public StoreDuplicateTakeSpec()
    {
      Query.Take(1)
           .Take(2);
    }
  }
}
