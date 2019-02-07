public class ForStatement implements Statemnt {

    private Id id;
    private Iter iter;
    private Block block;

    public ForStatement(Id id, Iter iter, Block block)
    {
        if (block == null)
            throw new IllegalArgumentException ("null block argument");

        this.id = id;
        this.iter = iter;
        this.block = block;
    }
	
	@Override
    public void execute()
    {
        new AssignStatement(id, iter.getBegin()).execute();

        BinaryExpression changeidval;

        if(id.evaluate() < iter.getEnd().evaluate())
            changeidval = new BinaryExpression(ArithmeticOperator.ADD_OP, id, new Constant(1));
        else
            changeidval = new BinaryExpression(ArithmeticOperator.SUB_OP, id, new Constant(1));;


        while(id.evaluate() != iter.getEnd().evaluate())
        {
            block.execute();
            new AssignStatement(id, changeidval).execute();
        }
    }
}
