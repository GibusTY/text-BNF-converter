using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBNFConverter
{
    class Interpreter
    {
        static void Main()
        {
            //Essentially our main method to perform everything else already coded.
            try
            {
                //Take a file and attempt to parse then run it, then display variable memory
                var parser = new Parser(@"[file/directory here]");
                var program = parser.parse();
                program.execute();
                Memory.displayMem();
            }
            catch (Exception e)
            {
                //If something failed, let us know what did
                Console.WriteLine($"ERROR - {e}");
            }
        }
    }
}
