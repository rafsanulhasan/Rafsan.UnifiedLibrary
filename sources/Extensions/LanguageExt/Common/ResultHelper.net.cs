using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace LanguageExt.Common;

public static partial class ResultHelper
{
	public static Result<T> AsResult<T>(
		this T? result,
		[CallerArgumentExpression(nameof(result))][NotNullWhen(false)] string paramName = null!)
	{
		return result.AsConditional().Match(
			r => r is null,
			_ => new ArgumentNullException(paramName).AsResult<T>(),
			r => r!.AsResult());
	}
}
