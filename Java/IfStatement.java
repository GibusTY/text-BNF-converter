//File name: IfStatement.java
//Created by Joshua Brock on 9/26/2018

public class IfStatement implements Statemnt
{

	private BooleanExpression bin_expr;
	private Block block1;
	private Block block2;
	
	public IfStatement(BooleanExpression bin_expr, Block block1, Block block2)
	{
		//Creates a value to the specified Id.
		if (bin_expr == null)
			throw new IllegalArgumentException ("null binary expression argument");
		if (block1 == null || block2 == null)
			throw new IllegalArgumentException ("null expression argument");
		this.bin_expr = bin_expr;
		this.block1 = block1;
		this.block2 = block2;
	}

	@Override
	public void execute()
	{
		//If bin_expr is true, run block1, else run block 2.
		if(bin_expr.execute())
			block1.execute();
		else
			block2.execute();
	}
}