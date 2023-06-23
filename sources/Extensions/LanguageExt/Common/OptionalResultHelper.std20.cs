namespace LanguageExt.Common;

public static partial class OptionalResultHelper
{
	public static OptionalResult<T> AsOptionalResult<T>(
		this T? self,
		bool returnExceptionIfNull = false,
		string paramName = null!)
		where T : notnull
	{
		Option<T> selfOpt = self.AsOption();
		return returnExceptionIfNull.AsConditional().Match(
			r => r,
			_ =>
			{
				paramName.ThrowIfNullOrWhiteSpace(nameof(paramName));
				return selfOpt.Match(
					r => r.AsOptionalResult(),
					() => new ArgumentNullException(paramName).AsOptionalResult<T>());
			},
			_ => selfOpt.Match(
				r => r.AsOptionalResult(),
				() => new OptionalResult<T>(Option<T>.None)));
	}
}
