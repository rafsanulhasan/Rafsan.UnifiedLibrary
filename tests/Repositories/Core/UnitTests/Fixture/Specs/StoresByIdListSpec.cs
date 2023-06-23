using System;
using System.Collections.Generic;
using System.Linq;

using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.Specs
{
	public class StoresByIdListSpec : Specification<Store>
  {
    public StoresByIdListSpec(IEnumerable<int> Ids)
    {
      Query.Where(x => Ids.Contains(x.Id));
    }
  }
}
