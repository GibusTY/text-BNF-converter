//File name: WhileStatement.java
//Created by Joshua Brock on 9/26/2018

public class WhileStatement implements Statemnt {

    private BooleanExpression booleanExpression;

    private Block block;

    public WhileStatement(BooleanExpression booleanExpression, Block block)
    {
		//Precondition: BooleanExpression & Block exist.
		//Postcondition: Default constructor of a WhileStatement object.
        if (booleanExpression == null)
            throw new IllegalArgumentException ("null boolean argument");
        if (block == null)
            throw new IllegalArgumentException ("null block argument");
        this.booleanExpression = booleanExpression;
        this.block = block;
    }

	@Override
    public void execute()
    {
		//Postcondition: While the BooleanExpression is true, execute the Block. Continue executing the Block until BooleanExpression becomes false.
        while(booleanExpression.execute())
            block.execute();
    }
}
