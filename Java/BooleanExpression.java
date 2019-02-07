public class  BooleanExpression
{
	private ArithmeticExpression expr1, expr2;
	
	private BooleanOperator op;
	
	public BooleanExpression(BooleanOperator op, ArithmeticExpression expr1, ArithmeticExpression expr2)
	{
		//Creates a new boolean expression.
		if (expr1 == null || expr2 == null)
			throw new IllegalArgumentException ("null expression argument");
		this.expr1 = expr1;
		this.expr2 = expr2;
		this.op = op;
	}

	public boolean execute()
	{
		boolean value;
		if (op == BooleanOperator.LSQU_OP)
			return expr1.evaluate() <= expr2.evaluate();
		else if (op == BooleanOperator.LSTN_OP)
			return expr1.evaluate() < expr2.evaluate();
		else if (op == BooleanOperator.GREQ_OP)
			return expr1.evaluate() >= expr2.evaluate();
		else if (op == BooleanOperator.GRTN_OP)
			return expr1.evaluate() > expr2.evaluate();
		else if (op == BooleanOperator.EQL_OP)
			return expr1.evaluate() == expr2.evaluate();
		else
			return expr1.evaluate() != expr2.evaluate();
	}
}