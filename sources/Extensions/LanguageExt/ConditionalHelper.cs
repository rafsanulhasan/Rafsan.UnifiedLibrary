namespace LanguageExt;

public static class ConditionalHelper
{
	public static Conditional<T> AsConditional<T>(this T self)
	{
		return new Conditional<T>(self);
	}
}
