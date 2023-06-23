using System;
using System.Collections.Generic;
using System.Text;

using FluentAssertions;

using Rafsan.DataAccess.Repositories.Exceptions;

using Xunit;

namespace Ardalis.Specification.UnitTests.ExceptionTests
{
	public class InvalidSearchPatternExceptionTests
	{
		private const string defaultMessage = "Invalid search pattern: " + pattern;
		private const string pattern = "x";

		[Fact]
		public void ThrowWithDefaultConstructor()
		{
			Action action = () => throw new InvalidSearchPatternException(pattern);

			action.Should().Throw<InvalidSearchPatternException>(pattern).WithMessage(defaultMessage);
		}

		[Fact]
		public void ThrowWithInnerException()
		{
			var inner = new Exception("test");
			Action action = () => throw new InvalidSearchPatternException(pattern, inner);

			action.Should().Throw<InvalidSearchPatternException>(pattern).WithMessage(defaultMessage).WithInnerException<Exception>().WithMessage("test");
		}
	}
}
