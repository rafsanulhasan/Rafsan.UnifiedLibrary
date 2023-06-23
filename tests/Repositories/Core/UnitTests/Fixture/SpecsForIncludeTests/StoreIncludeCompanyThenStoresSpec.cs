using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.SpecsForIncludeTests
{
	public class StoreIncludeCompanyThenStoresSpec : Specification<Store>
	{
		public StoreIncludeCompanyThenStoresSpec()
		{
			Query.Include(x => x.Company)
			    .ThenInclude(x => x!.Stores)
			    .ThenInclude(x => x.Products);
		}
	}
}
