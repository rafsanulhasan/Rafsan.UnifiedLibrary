using System;

using LanguageExt;
using LanguageExt.Common;

using Rafsan.DataAccess.Repositories.Errors;

namespace Rafsan.DataAccess.Repositories.Exceptions;

public class RecordNotFoundException<TId, TEntity> : ErrorException
{
	private readonly TId id;
	private readonly NullReferenceException exception;
	private Option<ErrorException> inner = Option<ErrorException>.None;

	public RecordNotFoundException(TId id)
		: base(101)
	{
		this.id = id;
		exception = new NullReferenceException($"'{typeof(TEntity).Name}' with '{id}' not found.");
	}

	/// <inheritdoc/>
	public override int Code { get; }

	/// <inheritdoc/>
	public override Option<ErrorException> Inner => inner;

	/// <inheritdoc/>
	public override bool IsExceptional => true;

	/// <inheritdoc />
	public override bool IsExpected => true;

	/// <inheritdoc/>
	public override ErrorException Append(ErrorException error)
	{
		inner = error;
		return this;
	}

	/// <inheritdoc/>
	public override Error ToError()
	{
		return new RecordNotFoundError<TId, TEntity>(id);
	}
}

public class RecordNotFoundException : ErrorException
{
	private readonly NullReferenceException exception;
	private Option<ErrorException> inner = Option<ErrorException>.None;

	public RecordNotFoundException(NullReferenceException exception)
		: base(101)
	{
		this.exception = exception;
	}

	/// <inheritdoc/>
	public override int Code { get; }

	/// <inheritdoc/>
	public override Option<ErrorException> Inner => inner;

	/// <inheritdoc/>
	public override bool IsExceptional => true;

	/// <inheritdoc />
	public override bool IsExpected => true;

	/// <inheritdoc/>
	public override ErrorException Append(ErrorException error)
	{
		inner = error;
		return this;
	}

	/// <inheritdoc/>
	public override Error ToError()
	{
		return new RecordNotFoundError(exception);
	}
}
