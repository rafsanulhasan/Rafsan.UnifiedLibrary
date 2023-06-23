using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

/* Unmerged change from project 'Rafsan.Extensions.LanguageExt (net5.0)'
Before:
using System.Threading.Tasks;

using LanguageExt.Common;
After:
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt;
using LanguageExt.Common;
*/
using System.Threading.Tasks;
using LanguageExt.Common;
using LanguageExt.Pretty;

namespace LanguageExt;
public static class TryHelper
{
	//
	// Summary:
	//     The Try monad captures exceptions and uses them to cancel the computation. Primarily
	//     useful for expression based processing of errors.
	//
	// Returns:
	//     A value that represents the outcome of the computation, either Success or Failure
	//
	// Remarks:
	//     To invoke directly, call x.Try()
	public delegate ValueTask<OptionalResult<A>> TryOptionResultAsync<A>(CancellationToken cancellationToken = default);

	//
	// Summary:
	//     TryAsync constructor function
	//
	// Parameters:
	//   f:
	//     Function to run asynchronously
	//
	// Type parameters:
	//   A:
	//     Bound value type
	//
	// Returns:
	//     A lifted operation that returns a value of A
	[Pure]
	public static TryOptionResultAsync<A> TryOptionalResultAsync<A>(this Func<CancellationToken, Task<A?>> f, CancellationToken cancellationToken = default)
		where A : class
	{
		return Memo(async () =>
		{
			var response = await f(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return response is null
				? new OptionalResult<A>(Option<A>.None)
				: new OptionalResult<A>(response);
		});
	}

	//
	// Summary:
	//     Memoize the computation so that it's only run once
	public static TryOptionResultAsync<A> Memo<A>(this TryOptionAsync<A> ma)
	{
		var run = false;
		OptionalResult<A> result = new(Option<A>.None);
		return async delegate (CancellationToken ct)
		{
			if (run)
			{
				return result;
			}

			var optionalResult = await ma.Try().ConfigureAwait(continueOnCapturedContext: false);
			if (optionalResult.IsSome || optionalResult.IsNone)
			{
				run = true;
				result = optionalResult;
			}

			return optionalResult;
		};
	}
}
