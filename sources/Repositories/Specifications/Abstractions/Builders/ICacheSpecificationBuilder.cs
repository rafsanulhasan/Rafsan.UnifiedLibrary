namespace Rafsan.DataAccess.Repositories.Abstractions.Builders;

public interface ICacheSpecificationBuilder<T> : ISpecificationBuilder<T> where T : class
{
	bool IsChainDiscarded { get; set; }
}
