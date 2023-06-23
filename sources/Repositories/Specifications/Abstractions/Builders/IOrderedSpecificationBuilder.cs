namespace Rafsan.DataAccess.Repositories.Abstractions.Builders;

public interface IOrderedSpecificationBuilder<T> : ISpecificationBuilder<T>
{
	bool IsChainDiscarded { get; set; }
}
