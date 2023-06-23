namespace LanguageExt;

public partial record struct Conditional<T>
{
	public Unit If(Func<T, bool> condition, Action<T> succ)
	{
		condition.ThrowIfNull(nameof(condition));
		if (condition(value))
		{
			succ.ThrowIfNull(nameof(succ));
			succ(value);
		}

		return Unit.Default;
	}

	public async ValueTask<Unit> IfAsync(Func<T, bool> condition, Func<T, Task> succ)
	{
		condition.ThrowIfNull(nameof(condition));
		if (condition(value))
		{
			succ.ThrowIfNull(nameof(succ));
			await succ(value).ConfigureAwait(false);
		}

		return Unit.Default;
	}

	public async ValueTask<Unit> IfAsync(Func<T, bool> condition, Func<T, ValueTask> succ)
	{
		condition.ThrowIfNull(nameof(condition));
		if (condition(value))
		{
			succ.ThrowIfNull(nameof(succ));
			await succ(value).ConfigureAwait(false);
		}

		return Unit.Default;
	}

	public async ValueTask<Option<T>> IfAsync(Func<T, ValueTask<bool>> condition, Action<T> succ)
	{
		condition.ThrowIfNull(nameof(condition));

		if (await condition(value).ConfigureAwait(false))
		{
			succ.ThrowIfNull(nameof(succ));
			succ(value);
		}

		return value.AsOption();
	}

	public async ValueTask<Option<T>> IfAsync(Func<T, Task<bool>> condition, Action<T> succ)
	{
		condition.ThrowIfNull(nameof(condition));

		if (await condition(value).ConfigureAwait(false))
		{
			succ.ThrowIfNull(nameof(succ));
			succ(value);
		}

		return value.AsOption();
	}

	public async ValueTask<Option<T>> IfAsync(Func<T, Task<bool>> condition, Func<T, Task> succ)
	{
		condition.ThrowIfNull(nameof(condition));

		if (await condition(value))
		{
			succ.ThrowIfNull(nameof(succ));
			await succ(value);
		}

		return value.AsOption();
	}

	public async ValueTask<Option<T>> IfAsync(Func<T, ValueTask<bool>> condition, Func<T, Task> succ)
	{
		condition.ThrowIfNull(nameof(condition));

		if (await condition(value).ConfigureAwait(false))
		{
			succ.ThrowIfNull(nameof(succ));
			await succ(value).ConfigureAwait(false);
		}

		return value.AsOption();
	}

	public async ValueTask<Option<T>> IfAsync(Func<T, Task<bool>> condition, Func<T, ValueTask> succ)
	{
		condition.ThrowIfNull(nameof(condition));

		if (await condition(value).ConfigureAwait(false))
		{
			succ.ThrowIfNull(nameof(succ));
			await succ(value).ConfigureAwait(false);
		}

		return value.AsOption();
	}

	public async ValueTask<Option<T>> IfAsync(Func<T, ValueTask<bool>> condition, Func<T, ValueTask> succ)
	{
		condition.ThrowIfNull(nameof(condition));

		if (await condition(value).ConfigureAwait(false))
		{
			succ.ThrowIfNull(nameof(succ));
			await succ(value).ConfigureAwait(false);
		}

		return value.AsOption();
	}

	public Unit If(Func<T, bool> condition, Action<T> succ, Action<T> fail)
	{
		condition.ThrowIfNull(nameof(condition));
		if (condition(value))
		{
			succ.ThrowIfNull(nameof(succ));
			succ(value);
		}
		else
		{
			fail.ThrowIfNull(nameof(fail));
			fail.Invoke(value);
		}

		return Unit.Default;
	}

	public async Task<Option<T>> IfAsync(Func<T, ValueTask<bool>> condition, Func<T, ValueTask> succ, Func<ValueTask> fail = null!)
	{
		condition.ThrowIfNull(nameof(condition));

		if (await condition(value))
		{
			succ.ThrowIfNull(nameof(succ));
			await succ(value);
		}
		else
		{
			fail.ThrowIfNull(nameof(fail));
			await fail();
		}

		return value.AsOption();
	}

	public TResult Match<TResult>(Func<T, bool> condition, Func<T, TResult> succ, Func<T, TResult> fail)
	{
		condition.ThrowIfNull(nameof(condition));
		if (condition(value))
		{
			succ.ThrowIfNull(nameof(succ));
			return succ.Invoke(value);
		}
		else
		{
			fail.ThrowIfNull(nameof(fail));
			return fail.Invoke(value);
		}
	}

	public async Task<TResult> MatchAsync<TResult>(Func<T, bool> condition, Func<T, Task<TResult>> succ, Func<T, Task<TResult>> fail)
	{
		condition.ThrowIfNull(nameof(condition));
		if (condition(value))
		{
			succ.ThrowIfNull(nameof(succ));
			return await succ(value);
		}
		else
		{
			fail.ThrowIfNull(nameof(fail));
			return await fail(value);
		}
	}

	public async ValueTask<TResult> MatchAsync<TResult>(Func<T, bool> condition, Func<T, Task<TResult>> succ, Func<T, ValueTask<TResult>> fail)
	{
		condition.ThrowIfNull(nameof(condition));
		if (condition(value))
		{
			succ.ThrowIfNull(nameof(succ));
			return await succ(value);
		}
		else
		{
			fail.ThrowIfNull(nameof(fail));
			return await fail(value);
		}
	}

	public async ValueTask<TResult> MatchAsync<TResult>(Func<T, bool> condition, Func<T, ValueTask<TResult>> succ, Func<T, Task<TResult>> fail)
	{
		condition.ThrowIfNull(nameof(condition));
		if (condition(value))
		{
			succ.ThrowIfNull(nameof(succ));
			return await succ(value);
		}
		else
		{
			fail.ThrowIfNull(nameof(fail));
			return await fail(value);
		}
	}

	public async ValueTask<TResult> MatchAsync<TResult>(Func<T, bool> condition, Func<T, ValueTask<TResult>> succ, Func<T, ValueTask<TResult>> fail)
	{
		condition.ThrowIfNull(nameof(condition));
		if (condition?.Invoke(value) ?? false)
		{
			succ.ThrowIfNull(nameof(succ));
			return await succ(value);
		}
		else
		{
			fail.ThrowIfNull(nameof(fail));
			return await fail(value);
		}
	}
}
