using System.Diagnostics.CodeAnalysis;

namespace LanguageExt.Common;

public static partial class ResultHelper
{
	public static Result<T> AsResult<T>(
		this T? result,
		[NotNullWhen(false)] string paramName)
	{
		return result.AsConditional().Match(
			r => r is null,
			_ =>
			{
				paramName.ThrowIfNullOrWhiteSpace(nameof(paramName));
				return new ArgumentNullException(paramName).AsResult<T>();
			},
			r => r!.AsResult());
	}
}
