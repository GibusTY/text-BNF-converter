import java.util.ArrayList;

public class Block {
    private ArrayList<Statemnt> statements;

    public Block(ArrayList<Statemnt> statements)
    {
        //Creates a new program containing 1 block.
        this.statements = statements;
    }

    public void execute()
    {
        //Postcondition: Executes all statements in the block.
        for(int i = 0; i < statements.size(); i++)
            statements.get(i).execute();
    }
}
