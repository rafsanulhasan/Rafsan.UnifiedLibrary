using System.Text;

namespace LanguageExt;

public static partial class Guard
{
	/// <inheritdoc />
	public class ArgumentEmtyException : ArgumentNullException
	{
		/// <inheritdoc />
		public ArgumentEmtyException(string paramName)
			: base(paramName)
		{
			StringBuilder messageBuilder = new();
			messageBuilder.Append("Parameter ");
			paramName.AsConditional().If(
				p => p.Equals(string.Empty),
				_ => { },
				p => messageBuilder.Append($"with name {p} "));

			messageBuilder.Append("is empty.");

			Message = messageBuilder.ToString();
		}

		private Option<string> messsage;

		/// <inheritdoc />
		public new Option<string> Message
		{
			get => messsage;
			private set => messsage = value;
		}
	}

	/// <inheritdoc />
	public class ArgumentWhiteSpaceException : ArgumentNullException
	{
		/// <inheritdoc />
		public ArgumentWhiteSpaceException(string paramName)
			: base(paramName)
		{
			StringBuilder messageBuilder = new();
			messageBuilder.Append("Parameter ");
			paramName.AsConditional().If(
				p => p.Equals(string.Empty),
				_ => { },
				p => messageBuilder.Append($"with name {p} "));

			messageBuilder.Append("is whitespcce.");

			Message = messageBuilder.ToString();
		}

		private Option<string> messsage;

		/// <inheritdoc />
		public new Option<string> Message
		{
			get => messsage;
			private set => messsage = value;
		}
	}

	private static void Throw(string paramName)
	{
		throw new ArgumentNullException(paramName);
	}
}
