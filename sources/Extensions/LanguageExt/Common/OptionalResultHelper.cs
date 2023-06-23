namespace LanguageExt.Common;

public static partial class OptionalResultHelper
{
	public static OptionalResult<T> AsOptionalResult<T>(this T self)
	{
		return new OptionalResult<T>(self);
	}

	public static OptionalResult<T> AsOptionalResult<T>(this Exception self)
	{
		return new OptionalResult<T>(self);
	}

	public static OptionalResult<T> AsOptionalResult<T>(this Error error)
	{
		return new OptionalResult<T>(error.ToErrorException());
	}
}
