using System;
using System.Collections.Generic;
using System.Text;

using FluentAssertions;

using Rafsan.DataAccess.Repositories.Exceptions;

using Xunit;

namespace Ardalis.Specification.UnitTests.ExceptionTests
{
	public class DuplicateSkipExceptionTests
	{
		private const string defaultMessage = "Duplicate use of Skip(). Ensure you don't use Skip() more than once in the same specification!";

		[Fact]
		public void ThrowWithDefaultConstructor()
		{
			Action action = () => throw new DuplicateSkipException();

			action.Should().Throw<DuplicateSkipException>().WithMessage(defaultMessage);
		}

		[Fact]
		public void ThrowWithInnerException()
		{
			var inner = new Exception("test");
			Action action = () => throw new DuplicateSkipException(inner);

			action.Should().Throw<DuplicateSkipException>().WithMessage(defaultMessage).WithInnerException<Exception>().WithMessage("test");
		}
	}
}
