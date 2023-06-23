using System;

using LanguageExt;
using LanguageExt.Common;

using Rafsan.DataAccess.Repositories.Errors;

namespace Rafsan.DataAccess.Repositories.Exceptions;

public class NullInputException : ErrorException
{
	private readonly ArgumentNullException exception;
	private Option<ErrorException> inner = Option<ErrorException>.None;

	public NullInputException(string paramName)
		: base(100)
	{
		exception = new(paramName);
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
		return new ArgumentNullError(exception.ParamName!);
	}
}