using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoreWithPostProcessingActionSpec : Specification<Store>
  {
    public StoreWithPostProcessingActionSpec()
    {
      Query.PostProcessingAction(x => x);
    }
  }
}
