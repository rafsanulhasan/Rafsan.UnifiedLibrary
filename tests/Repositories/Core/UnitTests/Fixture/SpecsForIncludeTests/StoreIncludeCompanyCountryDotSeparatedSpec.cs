using System;
using System.Collections.Generic;
using System.Text;

using Ardalis.Specification.UnitTests.Fixture.Entities;

using Rafsan.DataAccess.Repositories;
using Rafsan.DataAccess.Repositories.Builder;

namespace Ardalis.Specification.UnitTests.Fixture.SpecsForIncludeTests
{
	public class StoreIncludeCompanyCountryDotSeparatedSpec : Specification<Store>
	{
		public StoreIncludeCompanyCountryDotSeparatedSpec()
		{
			Query.Include(x => x.Company!.Country);
		}
	}
}
