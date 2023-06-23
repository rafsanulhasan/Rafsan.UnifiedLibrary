using LanguageExt.Common;

namespace Rafsan.DataAccess.Repositories.Abstractions.Evaluators;

public interface IEvaluator
{
	bool IsCriteriaEvaluator { get; }

	Result<IQueryable<TEntity>> GetQuery<TEntity>(
		IQueryable<TEntity> query,
		ISpecification<TEntity> specification) 
	where TEntity : class;
}
