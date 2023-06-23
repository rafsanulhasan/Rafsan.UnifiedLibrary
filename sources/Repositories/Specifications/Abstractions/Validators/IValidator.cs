namespace Rafsan.DataAccess.Repositories.Abstractions.Validators;

public interface IValidator
{
	bool IsValid<T>(T entity, ISpecification<T> specification);
}
