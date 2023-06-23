using System;

using LanguageExt;
using LanguageExt.Common;

using Rafsan.DataAccess.Repositories.Errors;

namespace Rafsan.DataAccess.Repositories.Exceptions;

public class MultipleRecordsFoundException<TId, TEntity> : MultipleRecordsFoundException
{
	private readonly TId id;
	private readonly InvalidOperationException exception;
	private Option<ErrorException> inner = Option<ErrorException>.None;

	public MultipleRecordsFoundException(TId id)
		: base(new InvalidOperationException($"Found multiple entries of '{typeof(TEntity).Name}' entity with '{id}'."))
	{
		this.id = id;
		exception = new InvalidOperationException($"'{typeof(TEntity).Name}' with '{id}' not found.");
	}

	public MultipleRecordsFoundException(InvalidOperationException exception, TId id)
		: base(exception)
	{
		this.id = id;
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
		return new MultipleRecordsFoundError<TId, TEntity>(id);
	}
}

public class MultipleRecordsFoundException : ErrorException
{
	private readonly InvalidOperationException exception;
	private Option<ErrorException> inner = Option<ErrorException>.None;

	public MultipleRecordsFoundException(InvalidOperationException exception)
		: base(102)
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
		return new MultipleRecordsFoundError(exception);
	}
}
