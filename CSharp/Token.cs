using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBNFConverter
{
    public class Token
    {
        //A Token represents each character/word in the text file. It must contain the string itself alongside its TokenType
        private string lexeme;
        private TokenType tok_type;
        public string Lexeme { get => lexeme; }
        public TokenType Token_Type { get => tok_type; }
        public Token(string lexeme, TokenType tok_type)
        {
            //Postcondition: Creates new Token using Lexeme string and TokenType
            if (lexeme == null || lexeme.Length == 0)
                throw new Exception("Empty Lexeme inputted into new Token");
            this.lexeme = lexeme;
            this.tok_type = tok_type;
        }
    }
}
