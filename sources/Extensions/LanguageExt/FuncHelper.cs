using LanguageExt.Common;

using static LanguageExt.Prelude;

namespace LanguageExt;

public static class FuncHelper
{
	public static Result<T> TryResult<T>(
		this Func<T> func)
	{
		var @try = Try(() => func());
		return @try.Match(
			data => data.AsResult(),
			ex => ex.AsResult<T>());
	}

	public static async ValueTask<Result<T>> TryResultAsync<T>(
		this Func<Task<T>> func)
	{
		var @try = await TryAsync(() => func());
		return @try.Match(
			data => data.AsResult(),
			ex => ex.AsResult<T>());
	}

	public static async Task<Result<T>> TryResultAsync<T>(this Task<T> task)
	{
		var @try = await TryAsync(task);
		return @try.Match(
			data => data.AsResult(),
			ex => ex.AsResult<T>());
	}

	public static async Task<Result<T>> TryResultAsync<T>(
		this Func<ValueTask<T>> func)
	{
		var @try = await TryAsync(await func());
		return @try.Match(
			data => data.AsResult(),
			ex => ex.AsResult<T>());
	}

	public static async Task<Result<T>> TryResultAsync<T>(this ValueTask<T> task)
	{
		var @try = await TryAsync(await task);
		return @try.Match(
			data => data.AsResult(),
			ex => ex.AsResult<T>());
	}

	public static async ValueTask<Result<Lst<T>>> TryListResultAsync<T>(this Task<List<T>> task)
	{
		var @try = await TryAsync(task);
		return @try.Match(
			data => data is null || data.Count == 0 ? Lst<T>.Empty : new Lst<T>(data).AsResult(),
			ex => ex.AsResult<Lst<T>>());
	}

	public static async ValueTask<Result<Lst<T>>> TryListResultAsync<T>(this Func<Task<List<T>>> task)
	{
		var @try = await TryAsync(task());
		return @try.Match(
			data => data is null || data.Count == 0 ? Lst<T>.Empty : new Lst<T>(data).AsResult(),
			ex => ex.AsResult<Lst<T>>());
	}

	public static async ValueTask<OptionalResult<T>> TryOptionalResultAsync<T>(this Func<Task<T?>> task)
	{
		var @try = await TryOptionAsync(task());
		return @try.Match(
			data => data.AsOptionalResult(),
			() => new OptionalResult<T>(Option<T>.None),
			ex => ex.AsOptionalResult<T>());
	}

	public static async ValueTask<OptionalResult<T>> TryOptionalResultAsync<T>(this Task<T?> task)
	{
		var @try = await TryOptionAsync(task);
		return @try.Match(
			data => data.AsOptionalResult(),
			() => new OptionalResult<T>(Option<T>.None),
			ex => ex.AsOptionalResult<T>());
	}

	public static async ValueTask<OptionalResult<T>> TryOptionalResultAsync<T>(this ValueTask<T?> task)
	{
		var @try = await TryOptionAsync(async () => await task);
		return @try.Match(
			data => data.AsOptionalResult(),
			() => new OptionalResult<T>(Option<T>.None),
			ex => ex.AsOptionalResult<T>());
	}
}
