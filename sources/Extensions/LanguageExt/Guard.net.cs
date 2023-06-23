using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace LanguageExt;

public static partial class Guard
{
	public static void ThrowIfNull(
		this object obj,
		[CallerArgumentExpression(nameof(obj))][NotNullWhen(false)] string paramName = null!)
	{
		if (obj is null)
		{
			Throw(paramName!);
		}
	}

	public static void ThrowIfNullOrEmpty(
		this string obj,
		[CallerArgumentExpression(nameof(obj))][NotNullWhen(false)] string paramName = null!)
	{
		if (string.IsNullOrEmpty(obj))
		{
			Throw(paramName!);
		}
	}

	public static void ThrowIfNullOrWhiteSpace(
		this string obj,
		[CallerArgumentExpression(nameof(obj))][NotNullWhen(false)] string paramName = null!)
	{
		if (string.IsNullOrWhiteSpace(obj))
		{
			Throw(paramName!);
		}
	}
}
