using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBNFConverter
{
    public class Iter
    {
        //Iter is comprised of two (2) ArithExpressions acting as bounds for a for loop
        private ArithExpression arith_begin, arith_end;
        public Iter(ArithExpression arith_begin, ArithExpression arith_end)
        {
            if (arith_begin == null || arith_end == null)
                throw new Exception("Null ArithExpression inputted into new Iter");
            this.arith_begin = arith_begin;
            this.arith_end = arith_end;
        }
        //Gets each individual bound
        public ArithExpression getBegin { get => arith_begin; }
        public ArithExpression getEnd { get => arith_end; }
    }
}
