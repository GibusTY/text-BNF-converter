using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBNFConverter
{
    public interface Statement
    {
        /*
		    An interface to be expanded upon by the following classes:
		    IfStatement
		    PrintStatement
		    ForStatement
		    WhileStatement
		    AssignStatement
	    */
        void execute();
    }

    public class IfStatement : Statement
    {
        //IfStatement takes two (2) Block objects and a BoolExpression. Only one (1) block will be ran at any time based on the result of the BoolExpression
        private Block block1, block2;
        private BoolExpression bool_expr;
        public IfStatement(Block block1, Block block2, BoolExpression bool_expr)
        {
            //Postcondition: Creates new IfStatement using two (2) Block objects and one (1) BoolExpression
            if (block1 == null || block2 == null)
                throw new Exception("Null Block inputted into new IfStatement");
            this.block1 = block1;
            this.block2 = block2;
            this.bool_expr = bool_expr ?? throw new Exception("Null BoolExpression inputted into new IfStatement");
        }
        public void execute()
        {
            //Postcondition: If bool_expr is true, execute block1, otherwise execute block2
            if (bool_expr.execute())
                block1.execute();
            else
                block2.execute();
        }
    }
    public class PrintStatement : Statement
    {
        //PrintStatement merely prints an ArithExpression's value to the console
        private ArithExpression arith;
        public PrintStatement(ArithExpression arith)
        {
            this.arith = arith ?? throw new Exception("Null ArithExpression inputted to new PrintStatement");
        }
        public void execute()
        {
            //Postcondition: Prints the ArithExpression's value to the console
            Console.WriteLine($"{arith.evaluate()}");
        }
    }
    public class ForStatement : Statement
    {
        //ForStatement takes a Block, Iter, and Id. Iter is the Id's start and ending values. Executes Block every time Id's value modifies
        private Block block;
        private Iter iter;
        private Id id;
        public ForStatement(Block block, Iter iter, Id id)
        {
            this.block = block ?? throw new Exception("Null Block inputted into new ForStatement");
            this.iter = iter ?? throw new Exception("Null Iter inputted into new ForStatement");
            this.id = id ?? throw new Exception("Null Id inputted into new ForStatement");
        }
        public void execute()
        {
            //Postcondition: Executes Block continuously as Id modifies from its start to its end value.
            new AssignStatement(id, iter.getBegin).execute();

            //Decides whether our Id needs to increment or decrement to reach the end value
            BinaryExpression changeidval;
            if (id.evaluate() < iter.getEnd.evaluate())
                changeidval = new BinaryExpression(id, new Constant(1), ArithOperator.ADD_OP);
            else
                changeidval = new BinaryExpression(id, new Constant(1), ArithOperator.SUB_OP);

            //Execution conditions
            while (id.evaluate() != iter.getEnd.evaluate())
            {
                block.execute();
                new AssignStatement(id, changeidval).execute();
            }
        }
    }
    public class WhileStatement : Statement
    {
        //WhileStatement takes a BoolExpression and a Block. It repeatedly executes Block until BoolExpression returns false
        private BoolExpression bool_expr;
        private Block block;
        public WhileStatement(BoolExpression bool_expr, Block block)
        {
            //Postcondition: Creates a new WhileStatement using BoolExpression and Block
            this.bool_expr = bool_expr ?? throw new Exception("Null BoolExpression inputted into new WhileStatement");
            this.block = block ?? throw new Exception("Null Block inputted into new WhileStatement");
        }
        public void execute()
        {
            //Postcondition: Executes Block continuously until BoolExpression returns false
            while (bool_expr.execute())
                block.execute();
        }
    }
    public class AssignStatement : Statement
    {
        //AssignStatement takes an Id and an ArithExpression. It sets the Id equal to the ArithExpression's value (i.e. [Id] = [ArithExpression]) 
        private Id id;
        private ArithExpression arith;
        public AssignStatement(Id id, ArithExpression arith)
        {
            this.id = id ?? throw new Exception("Null Id inputted into new AssignStatement");
            this.arith = arith ?? throw new Exception("Null ArithExpression inputted into new AssignStatement");
        }
        public void execute()
        {
            //Postcondition: Makes the Id's current value equal to the ArithExpression's value
            Memory.store(id.Character, arith.evaluate());
        }
    }
}
