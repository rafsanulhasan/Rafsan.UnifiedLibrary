using System;

using LanguageExt.Common;

using Rafsan.DataAccess.Repositories.Exceptions;

namespace Rafsan.DataAccess.Repositories.Errors;

public record ArgumentNullError : Error
{
	private readonly ArgumentNullException exception;

	public ArgumentNullError(string paramName)
	{
		exception = new ArgumentNullException(paramName);
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
		return new NullInputException(exception.ParamName);
	}
}
