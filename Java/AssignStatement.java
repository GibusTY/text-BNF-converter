
public class AssignStatement implements Statemnt
{

	private ArithmeticExpression expr;
	
	private Id var;
	
	public AssignStatement(Id var, ArithmeticExpression expr)
	{
		//Creates a value to the specified Id.
		if (var == null)
			throw new IllegalArgumentException ("null Id argument");
		if (expr == null)
			throw new IllegalArgumentException ("null expression argument");
		this.expr = expr;
		this.var = var;		
	}

	@Override
	public void execute()
	{
		Memory.store(var.getChar(), expr.evaluate());
	}
}