using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBNFConverter
{
    public class BoolExpression
    {
        //BoolExpression takes two (2) ArithExpressions and a single BoolOperator. Compares both expressions using the given operator
        private BoolOperator op;
        private ArithExpression arith1, arith2;
        public BoolExpression(BoolOperator op, ArithExpression arith1, ArithExpression arith2)
        {
            //Postcondition: Creates a new BoolExpression using op, arith1, and arith2
            if (arith1 == null || arith2 == null)
                throw new Exception("Null ArithExpression inputted into new BoolExpression");
            this.arith1 = arith1;
            this.arith2 = arith2;
            this.op = op;
        }
        public bool execute()
        {
            //Postcondition: Returns the boolean result of [arith1] [op] [arith2] (i.e. 1 < 2)
            switch (op)
            {
                case BoolOperator.EQL_OP:
                    return (arith1.evaluate() == arith2.evaluate());
                case BoolOperator.GREQ_OP:
                    return (arith1.evaluate() >= arith2.evaluate());
                case BoolOperator.GRTN_OP:
                    return (arith1.evaluate() > arith2.evaluate());
                case BoolOperator.LSQU_OP:
                    return (arith1.evaluate() <= arith2.evaluate());
                case BoolOperator.LSTN_OP:
                    return (arith1.evaluate() < arith2.evaluate());
                case BoolOperator.NTEQL_OP:
                    return (arith1.evaluate() != arith2.evaluate());
                default:
                    throw new Exception("Invalid BoolOperator used in BoolExpression evaluation");
            }
        }
    }
}
