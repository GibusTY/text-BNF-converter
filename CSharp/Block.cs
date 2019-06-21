using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBNFConverter
{
    public class Block
    {
        //Block takes an array of individual statements to execute. Blocks can contain any number of statements
        private List<Statement> statements;
        public Block(List<Statement> statements)
        {
            //Postcondition: Creates new Block using list of Statements
            if (statements == null || statements.Count == 0)
                throw new Exception("New Block contains empty list of statements | No code to execute");
            this.statements = statements.ToList();
        }
        public void execute()
        {
            //Postcondition: Execute each statement in the array once
            foreach (Statement statement in statements)
                statement.execute();
        }
    }
}
