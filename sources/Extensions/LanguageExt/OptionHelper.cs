namespace LanguageExt;

public static partial class OptionHelper
{
	public static Option<T> AsOption<T>(this T? result)
	{
		return result.AsConditional().Match(
			r => r is null,
			_ => Option<T>.None,
			r => r.AsOption());
	}

	public static Option<TCollection> AsOption<T, TCollection>(this TCollection result)
		where TCollection : IEnumerable<T>
	{
		return result.AsConditional().Match(
			r => r is null || !r.Any(),
			_ => default(TCollection).AsOption(),
			r => r.AsOption());
	}
}
