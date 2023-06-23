using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace LanguageExt.Common;

public static partial class ResultHelper
{
	public static Result<T> AsResult<T>(this T result)
	{
		return new Result<T>(result);
	}

	public static Result<T> AsResult<T>(this Exception exception)
	{
		return new Result<T>(exception);
	}

	public static Result<T> AsResult<T>(this Error error)
	{
		return new Result<T>(error.ToErrorException());
	}
}
