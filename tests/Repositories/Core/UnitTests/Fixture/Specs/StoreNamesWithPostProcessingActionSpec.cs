using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoreNamesWithPostProcessingActionSpec : Specification<Store, string?>
  {
    public StoreNamesWithPostProcessingActionSpec()
    {
      Query.Select(x => x.Name)
           .PostProcessingAction(x => x);
    }
  }
}
