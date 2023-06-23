using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoreDuplicateSkipSpec : Specification<Store>
  {
    public StoreDuplicateSkipSpec()
    {
      Query.Skip(1)
           .Skip(2);
    }
  }
}
