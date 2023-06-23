using System;
using System.Collections.Generic;
using System.Text;

using FluentAssertions;

using Rafsan.DataAccess.Repositories.Exceptions;

using Xunit;

namespace Ardalis.Specification.UnitTests.ExceptionTests
{
	public class DuplicateOrderChainExceptionTests
	{
		private const string defaultMessage = "The specification contains more than one Order chain!";

		[Fact]
		public void ThrowWithDefaultConstructor()
		{
			Action action = () => throw new DuplicateOrderChainException();

			action.Should().Throw<DuplicateOrderChainException>().WithMessage(defaultMessage);
		}

		[Fact]
		public void ThrowWithInnerException()
		{
			var inner = new Exception("test");
			Action action = () => throw new DuplicateOrderChainException(inner);

			action.Should().Throw<DuplicateOrderChainException>().WithMessage(defaultMessage).WithInnerException<Exception>().WithMessage("test");
		}
	}
}
