namespace LanguageExt;

public partial record struct Conditional<T>
{
	private readonly T value;

	internal Conditional(T value)
	{
		this.value = value;
	}
}
