using Rafsan.DataAccess.Repositories.Abstractions;

namespace Rafsan.DataAccess.Repositories.Specifications;

/// <inheritdoc cref="ISingleResultSpecification{T}"/>
public class SingleResultSpecification<T> : Specification<T>, ISingleResultSpecification<T>
{
}

/// <inheritdoc cref="ISingleResultSpecification{T, TResult}"/>
public class SingleResultSpecification<T, TResult> : Specification<T, TResult>, ISingleResultSpecification<T, TResult>
{
}
