using LanguageExt.Common;

namespace Rafsan.DataAccess.Repositories.Abstractions.Evaluators;

public interface IInMemoryEvaluator
{
	Result<IEnumerable<T>> Evaluate<T>(IQueryable<T> query, ISpecification<T> specification);
}
