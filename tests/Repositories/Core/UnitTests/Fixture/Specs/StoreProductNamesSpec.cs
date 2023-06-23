using System.Linq;

using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoreProductNamesSpec : Specification<Store, string?>
  {
    public StoreProductNamesSpec()
    {
      Query.SelectMany(s => s.Products.Select(p => p.Name));
    }
  }
}
