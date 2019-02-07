
public class Constant implements ArithmeticExpression
{

	private int value;
	
	public Constant(int value)
	{
		//Creates a new constant value.
		this.value = value;
	}

	@Override
	public int evaluate()
	{
		return value;
	}

}