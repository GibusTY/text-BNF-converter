using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBNFConverter
{
    public interface ArithExpression
    {
        //This will be used as an empty class that will be expanded in other classes.
        /*
            Id
            Constant
            BinaryExpression
        */
        int evaluate();
    }

    public class Id : ArithExpression
    {
        //Ids are single characters meant to act as variables, if referenced, their values must be acquired/modified through Memory
        //The character associated with the Id's value, includes get method
        private char ch;
        public char Character { get => ch; }
        public Id(char ch)
        {
            //Postcondition: Creates a new Id with character 'ch'
            if (ch == null)
                throw new Exception("Null character inputted into new Id");
            this.ch = ch;
        }
        public int evaluate()
        {
            //Postcondition: Returns the value associated with this Id
            return Memory.fetch(ch);
        }
    }

    public class Constant : ArithExpression
    {
        //Constants are static numbers. Once created, their values cannot be modified
        private int value;
        public Constant(int value)
        {
            //Postcondition: Returns a new Constant with value of 'value'
            if (value == null)
                throw new Exception("Null value inputted into new Constant");
            this.value = value;
        }
        public int evaluate()
        {
            //Postcondition: Returns our value, similar to a get but for our overriding method
            return value;
        }
    }

    public class BinaryExpression : ArithExpression
    {
        //BinaryExpression is the phrase [number] [mathematical operator] [number] (i.e. 1 + 2)
        private ArithExpression arith1, arith2;
        private ArithOperator op;
        public BinaryExpression(ArithExpression arith1, ArithExpression arith2, ArithOperator op)
        {
            if (arith1 == null || arith2 == null)
                throw new Exception("Null ArithExpression inputted into new BinaryExpression");
            this.arith1 = arith1;
            this.arith2 = arith2;
            this.op = op;
        }
        public int evaluate()
        {
            switch (op)
            {
                case ArithOperator.ADD_OP:
                    return (arith1.evaluate() + arith2.evaluate());
                case ArithOperator.SUB_OP:
                    return (arith1.evaluate() - arith2.evaluate());
                case ArithOperator.MUL_OP:
                    return (arith1.evaluate() * arith2.evaluate());
                case ArithOperator.DIV_OP:
                    return (arith1.evaluate() / arith2.evaluate());
                case ArithOperator.REMNDR_OP:
                    return (arith1.evaluate() % arith2.evaluate());
                case ArithOperator.VID_OP:
                    return (arith2.evaluate() / arith1.evaluate());
                case ArithOperator.PWR_OP:
                    return (int)Math.Pow(arith1.evaluate(), arith2.evaluate());
                default:
                    throw new Exception("Invalid ArithOperator used in BinaryExpression evaluation");
            }
        }
    }
}
