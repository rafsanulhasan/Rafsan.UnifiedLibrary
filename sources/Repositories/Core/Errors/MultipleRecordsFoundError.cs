using System;

using LanguageExt.Common;

using Rafsan.DataAccess.Repositories.Exceptions;

namespace Rafsan.DataAccess.Repositories.Errors;

public record MultipleRecordsFoundError<TId, TEntity> : Error
{
	private readonly TId id;
	private readonly InvalidOperationException exception;

	public MultipleRecordsFoundError(TId id)
	{
		this.id = id;
		exception = new InvalidOperationException($"Found multiple entries of '{typeof(TEntity).Name}' entity with '{id}'.");
		Message = exception.Message;
	}

	public MultipleRecordsFoundError(InvalidOperationException exception)
	{
		this.exception = exception;
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
		return new MultipleRecordsFoundException<TId, TEntity>(id);
	}
}

public record MultipleRecordsFoundError : Error
{
	private readonly InvalidOperationException exception;

	public MultipleRecordsFoundError(InvalidOperationException exception)
	{
		this.exception = exception;
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
		return new MultipleRecordsFoundException(exception);
	}
}
