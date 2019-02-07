//File name: Program.java
//Created by Joshua Brock on 9/26/2018

public class Program {

    private Block block;

    public Program(Block block)
    {
        //Creates a new program containing 1 block.
        this.block = block;
    }

    public void execute()
    {
        //Postcondition: Executes the block.
        block.execute();
    }
}
