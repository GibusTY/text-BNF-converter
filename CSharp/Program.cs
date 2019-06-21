using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBNFConverter
{
    public class Program
    {
        private Block block;
        public Program(Block block)
        {
            //Postcondition: Creates new Program using Block
            this.block = block ?? throw new Exception("New Program contains null Block | No code to execute");
        }
        public void execute()
        {
            //Postcondition: Executes the Block
            block.execute();
        }
    }
}
