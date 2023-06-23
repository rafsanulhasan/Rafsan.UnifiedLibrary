using LanguageExt.Common;

namespace Rafsan.DataAccess.Repositories.Abstractions.Evaluators;

public interface IInMemorySpecificationEvaluator
{
	Result<IEnumerable<TResult>> Evaluate<T, TResult>(IQueryable<T> source, ISpecification<T, TResult> specification);
	Result<IEnumerable<T>> Evaluate<T>(IQueryable<T> source, ISpecification<T> specification);
}
