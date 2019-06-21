using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBNFConverter
{
    class Memory
    {
        //Memory is a static object that stores two (2) arrays. One will contain the unique character of every Id in the Program. The other will have all of the values of said Ids
        private static List<char> chars = new List<char>();
        private static List<int> values = new List<int>();
        public static int fetch(char ch)
        {
            //Precondition: Character ch exists in our memory
            //Postcondition: Returns the value associated with our character
            if (!chars.Contains(ch))
                throw new Exception("Memory attempted to access non-existant variable");
            return values[chars.IndexOf(ch)];
        }
        public static void store (char ch, int value)
        {
            //Postcondition: If ch exists, override current value with 'value' otherwise store a new character with a new value of 'value'
            if (chars.Contains(ch))
                values[chars.IndexOf(ch)] = value;
            else
            {
                chars.Add(ch);
                values.Add(value);
            }
        }
        public static void displayMem()
        {
            //Postcondition: Prints out all of the stored variable values.
            for(var i = 0; i < chars.Count; i++)
                Console.WriteLine($"{chars[i]} - {values[i]}");
        }
    }
}
