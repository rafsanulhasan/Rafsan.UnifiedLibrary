﻿using Rafsan.DataAccess.Repositories.Abstractions.Builders;

namespace Rafsan.DataAccess.Repositories.Specifications.Builders;

public class CacheSpecificationBuilder<T> : ICacheSpecificationBuilder<T> where T : class
{
	public Specification<T> Specification { get; }
	public bool IsChainDiscarded { get; set; }

	public CacheSpecificationBuilder(Specification<T> specification)
	    : this(specification, false)
	{
	}

	public CacheSpecificationBuilder(Specification<T> specification, bool isChainDiscarded)
	{
		Specification = specification;
		IsChainDiscarded = isChainDiscarded;
	}
}
