using System;

using LanguageExt.Common;

using Rafsan.DataAccess.Repositories.Exceptions;

namespace Rafsan.DataAccess.Repositories.Errors;

public record RecordNotFoundError<TId, TEntity> : Error
{
	private readonly TId id;
	private readonly NullReferenceException exception;

	public RecordNotFoundError(TId id)
	{
		this.id = id;
		exception = new NullReferenceException($"'{typeof(TEntity).Name}' with '{id}' not found.");
		Message = exception.Message;
	}

	/// <inheritdoc />
	public override string Message { get; }

	/// <inheritdoc />
	public override bool IsExceptional => true;

	/// <inheritdoc />
	public override bool IsExpected => true;

	/// <inheritdoc />
	public override bool Is<E>()
	{
		return base.Is(this) && IsExceptional;
	}

	/// <inheritdoc />
	public override ErrorException ToErrorException()
	{
		return new RecordNotFoundException<TId, TEntity>(id);
	}
}

public record RecordNotFoundError : Error
{
	private readonly NullReferenceException exception;

	public RecordNotFoundError(NullReferenceException ex)
	{
		exception = ex;
		Message = exception.Message;
	}

	/// <inheritdoc />
	public override string Message { get; }

	/// <inheritdoc />
	public override bool IsExceptional => true;

	/// <inheritdoc />
	public override bool IsExpected => true;

	/// <inheritdoc />
	public override bool Is<E>()
	{
		return base.Is(this) && IsExceptional;
	}

	/// <inheritdoc />
	public override ErrorException ToErrorException()
	{
		return new RecordNotFoundException(exception);
	}
}
