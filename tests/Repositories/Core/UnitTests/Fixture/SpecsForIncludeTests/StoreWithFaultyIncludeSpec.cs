using System;
using System.Collections.Generic;
using System.Text;

using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.SpecsForIncludeTests
{
	public class StoreWithFaultyIncludeSpec : Specification<Store>
	{
		public StoreWithFaultyIncludeSpec()
		{
			Query.Include(x => x.Id == 1 && x.Name == "Something");
		}
	}
}
