namespace Rafsan.DataAccess.Repositories.Abstractions.Validators;

public interface ISpecificationValidator
{
	bool IsValid<T>(T entity, ISpecification<T> specification);
}
