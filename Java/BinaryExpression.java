public class  BinaryExpression implements ArithmeticExpression
{
	private ArithmeticExpression expr1, expr2;
	
	private ArithmeticOperator op;
	
	public BinaryExpression(ArithmeticOperator op, ArithmeticExpression expr1, ArithmeticExpression expr2)
	{
		//Creates a new arithmetic expression.
		if (expr1 == null || expr2 == null)
			throw new IllegalArgumentException ("null expression argument");
		this.expr1 = expr1;
		this.expr2 = expr2;
		this.op = op;
	}

	@Override
	public int evaluate()
	{
		int value;
		if (op == ArithmeticOperator.ADD_OP)
			return expr1.evaluate() + expr2.evaluate();
		else if (op == ArithmeticOperator.SUB_OP)
			return expr1.evaluate() - expr2.evaluate();
		else if (op == ArithmeticOperator.MUL_OP)
			return expr1.evaluate() * expr2.evaluate();
		else if (op == ArithmeticOperator.DIV_OP)
			return expr1.evaluate() / expr2.evaluate();
		else if (op == ArithmeticOperator.REMNDR_OP)
			return expr1.evaluate() % expr2.evaluate();
		else if (op == ArithmeticOperator.VID_OP)
			return expr2.evaluate() / expr1.evaluate();	//Todo: check if right
		else
			return (int) Math.pow(expr1.evaluate(), expr2.evaluate());
	}
}