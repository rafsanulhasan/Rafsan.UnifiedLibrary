using System.Diagnostics.CodeAnalysis;

namespace LanguageExt;

public static partial class Guard
{
	public static void ThrowIfNull(
		this object obj,
		[NotNullWhen(false)] string paramName)
	{
		if (string.IsNullOrWhiteSpace(paramName))
		{
			throw new ArgumentNullException(nameof(paramName));
		}

		obj.ThrowIfNull(nameof(obj));
	}

	public static void ThrowIfNullOrEmpty(
		this string obj,
		[NotNullWhen(false)] string paramName)
	{
		var conditional = obj.AsConditional();
		conditional.If(
			o => o is null,
			o =>
			{
				paramName.ThrowIfNull(nameof(paramName));
				throw new ArgumentNullException(paramName);
			},
			o =>
			{
				paramName.ThrowIfNull(nameof(paramName));
				conditional.If(
					o => o.Equals(string.Empty),
					_ => throw new ArgumentEmtyException(paramName));
			});
	}

	public static void ThrowIfNullOrWhiteSpace(
		this string obj,
		[NotNullWhen(false)] string paramName)
	{
		var conditional = obj.AsConditional();
		conditional.If(
			o => string.IsNullOrEmpty(o),
			o => o.ThrowIfNullOrEmpty(paramName),
			o =>
			{
				paramName.ThrowIfNull(nameof(paramName));
				conditional.If(
					o => o.Trim().Equals(string.Empty),
					_ => throw new ArgumentWhiteSpaceException(paramName));
			});
	}
}
