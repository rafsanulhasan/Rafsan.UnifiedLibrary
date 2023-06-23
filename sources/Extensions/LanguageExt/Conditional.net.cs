using System.Diagnostics.CodeAnalysis;

namespace LanguageExt;

public partial record struct Conditional<T>
{
	public readonly Unit If(
		[NotNullWhen(false)] Func<T, bool> condition,
		[NotNullWhen(false)] Action<T> succ)
	{
		condition.ThrowIfNull();
		if (condition(value))
		{
			succ.ThrowIfNull();
			succ(value);
		}

		return Unit.Default;
	}

	public readonly async ValueTask<Unit> If(
		[NotNullWhen(false)] Func<T, bool> condition,
		[NotNullWhen(false)] Func<T, Task> succ)
	{
		condition.ThrowIfNull();
		if (condition(value))
		{
			succ.ThrowIfNull();
			await succ(value).ConfigureAwait(false);
		}

		return Unit.Default;
	}

	public readonly async ValueTask<Unit> If(
		[NotNullWhen(false)] Func<T, bool> condition,
		[NotNullWhen(false)] Func<T, ValueTask> succ)
	{
		condition.ThrowIfNull();
		if (condition(value))
		{
			succ.ThrowIfNull();
			await succ(value).ConfigureAwait(false);
		}

		return Unit.Default;
	}

	public readonly async ValueTask<Option<T>> IfAsync(
		[NotNullWhen(false)] Func<T, ValueTask<bool>> condition,
		[NotNullWhen(false)] Action<T> succ)
	{
		condition.ThrowIfNull();

		if (await condition(value).ConfigureAwait(false))
		{
			succ.ThrowIfNull();
			succ(value);
		}

		return value.AsOption();
	}

	public readonly async ValueTask<Option<T>> IfAsync(
		[NotNullWhen(false)] Func<T, Task<bool>> condition,
		[NotNullWhen(false)] Action<T> succ)
	{
		condition.ThrowIfNull();

		if (await condition(value).ConfigureAwait(false))
		{
			succ.ThrowIfNull();
			succ(value);
		}

		return value.AsOption();
	}

	public readonly async ValueTask<Option<T>> IfAsync(
		[NotNullWhen(false)] Func<T, Task<bool>> condition,
		[NotNullWhen(false)] Func<T, Task> succ)
	{
		condition.ThrowIfNull();

		if (await condition(value))
		{
			succ.ThrowIfNull();
			await succ(value);
		}

		return value.AsOption();
	}

	public readonly async ValueTask<Option<T>> IfAsync(
		[NotNullWhen(false)] Func<T, ValueTask<bool>> condition,
		[NotNullWhen(false)] Func<T, Task> succ)
	{
		condition.ThrowIfNull();

		if (await condition(value).ConfigureAwait(false))
		{
			succ.ThrowIfNull();
			await succ(value).ConfigureAwait(false);
		}

		return value.AsOption();
	}

	public readonly async ValueTask<Option<T>> IfAsync(
		[NotNullWhen(false)] Func<T, Task<bool>> condition,
		[NotNullWhen(false)] Func<T, ValueTask> succ)
	{
		condition.ThrowIfNull();

		if (await condition(value).ConfigureAwait(false))
		{
			succ.ThrowIfNull();
			await succ(value).ConfigureAwait(false);
		}

		return value.AsOption();
	}

	public readonly async ValueTask<Option<T>> IfAsync(
		[NotNullWhen(false)] Func<T, ValueTask<bool>> condition,
		[NotNullWhen(false)] Func<T, ValueTask> succ)
	{
		condition.ThrowIfNull();

		if (await condition(value).ConfigureAwait(false))
		{
			succ.ThrowIfNull();
			await succ(value).ConfigureAwait(false);
		}

		return value.AsOption();
	}

	public readonly Unit If(
		[NotNullWhen(false)] Func<T, bool> condition,
		[NotNullWhen(false)] Action<T> succ,
		[NotNullWhen(false)] Action<T> fail)
	{
		condition.ThrowIfNull();
		if (condition(value))
		{
			succ.ThrowIfNull();
			succ(value);
		}
		else
		{
			fail.ThrowIfNull();
			fail.Invoke(value);
		}

		return Unit.Default;
	}

	public readonly async Task<Option<T>> IfAsync(
		[NotNullWhen(false)] Func<T, ValueTask<bool>> condition,
		[NotNullWhen(false)] Func<T, ValueTask> succ,
		[NotNullWhen(false)] Func<ValueTask> fail)
	{
		condition.ThrowIfNull();

		if (await condition(value))
		{
			succ.ThrowIfNull();
			await succ(value);
		}
		else
		{
			fail.ThrowIfNull();
			await fail();
		}

		return value.AsOption();
	}

	public readonly TResult Match<TResult>(
		[NotNullWhen(false)] Func<T, bool> condition,
		[NotNullWhen(false)] Func<T, TResult> succ,
		[NotNullWhen(false)] Func<T, TResult> fail)
	{
		condition.ThrowIfNull();
		if (condition(value))
		{
			succ.ThrowIfNull();
			return succ.Invoke(value);
		}
		else
		{
			fail.ThrowIfNull();
			return fail.Invoke(value);
		}
	}

	public readonly async Task<TResult> MatchAsync<TResult>(
		[NotNullWhen(false)] Func<T, bool> condition,
		[NotNullWhen(false)] Func<T, Task<TResult>> succ,
		[NotNullWhen(false)] Func<T, Task<TResult>> fail)
	{
		condition.ThrowIfNull();
		if (condition(value))
		{
			succ.ThrowIfNull();
			return await succ(value);
		}
		else
		{
			fail.ThrowIfNull();
			return await fail(value);
		}
	}

	public readonly async ValueTask<TResult> MatchAsync<TResult>(
		[NotNullWhen(false)] Func<T, bool> condition,
		[NotNullWhen(false)] Func<T, Task<TResult>> succ,
		[NotNullWhen(false)] Func<T, ValueTask<TResult>> fail)
	{
		condition.ThrowIfNull();
		if (condition(value))
		{
			succ.ThrowIfNull();
			return await succ(value);
		}
		else
		{
			fail.ThrowIfNull();
			return await fail(value);
		}
	}

	public readonly async ValueTask<TResult> MatchAsync<TResult>(
		[NotNullWhen(false)] Func<T, bool> condition,
		[NotNullWhen(false)] Func<T, ValueTask<TResult>> succ,
		[NotNullWhen(false)] Func<T, Task<TResult>> fail)
	{
		condition.ThrowIfNull();
		if (condition(value))
		{
			succ.ThrowIfNull();
			return await succ(value);
		}
		else
		{
			fail.ThrowIfNull();
			return await fail(value);
		}
	}

	public readonly async ValueTask<TResult> MatchAsync<TResult>(
		[NotNullWhen(false)] Func<T, bool> condition,
		[NotNullWhen(false)] Func<T, ValueTask<TResult>> succ,
		[NotNullWhen(false)] Func<T, ValueTask<TResult>> fail)
	{
		condition.ThrowIfNull();
		if (condition?.Invoke(value) ?? false)
		{
			succ.ThrowIfNull();
			return await succ(value);
		}
		else
		{
			fail.ThrowIfNull();
			return await fail(value);
		}
	}
}
