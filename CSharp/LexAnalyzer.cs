using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBNFConverter
{
    public class LexAnalyzer
    {
        //The lexical analyzer's job involves creating a list of tokens, assigning values and qualities to each.
        //This will be a dependent to the parser which will need its gathered token list to check for proper code grammar.
        private List<Token> tokenList = new List<Token>();
        public LexAnalyzer(string file)
        {
            //Precondition: file refers to an existing file/file directory
            //Postcondition: Creates a new Lexical Analyzer, automatically splitting the file into an array of words.
            if (file == null)
                throw new Exception("Lexical Analyzer - Invalid file inputted");
            //Split our files into lines, then split those lines into individual words
            var lines = System.IO.File.ReadAllLines(file);
            var words = new List<string>();
            foreach (string line in lines)
            {
                words.AddRange(line.Split(' '));
            }
            //Process each word to create our new tokens
            foreach (string word in words){
                //Helps remove blank tokens caused by multiple spaces
                if(!(word.Length == 0))
                    tokenList.Add(new Token(word, getType(word)));
            }

            //Create an EOS token at the end to tell our parser the program has ended
            tokenList.Add(new Token("EOS", TokenType.EOS_TOK));
        }
        private TokenType getType(string word)
        {
            //Precondition: word is not null and is valid
            //Postcondition: Returns the best fitting type for the word inputted.
            if (word == null)
                throw new Exception("LexAnalyzer - Null word inputted to getType");
            if (int.TryParse(word, out var i))
            {
                return TokenType.CONST_TOK;
            }
            else
            {
                if(word.Length == 1)
                {
                    switch (word)
                    {
                        case "=":
                            return TokenType.ASSIGN_TOK;
                        case "<":
                            return TokenType.LSTN_TOK;
                        case ">":
                            return TokenType.GRTN_TOK;
                        case "+":
                            return TokenType.ADD_TOK;
                        case "-":
                            return TokenType.SUB_TOK;
                        case "*":
                            return TokenType.MULT_TOK;
                        case "/":
                            return TokenType.DIV_TOK;
                        case "%":
                            return TokenType.REMNDR_TOK;
                        case @"\":
                            return TokenType.VID_TOK;
                        case "^":
                            return TokenType.PWR_TOK;
                        case "(":
                            return TokenType.INPUTSTRT_TOK;
                        case ")":
                            return TokenType.INPUTEND_TOK;
                        case ":":
                            return TokenType.COLON_TOK;
                        default:
                            return TokenType.ID_TOK;
                    }
                }
                else
                {
                    switch (word)
                    {
                        case "function":
                            return TokenType.METHOD_TOK;
                        case "if":
                            return TokenType.IF_TOK;
                        case "else":
                            return TokenType.ELSE_TOK;
                        case "while":
                            return TokenType.WHILE_TOK;
                        case "for":
                            return TokenType.FOR_TOK;
                        case "print":
                            return TokenType.PRINT_TOK;
                        case "end":
                            return TokenType.END_TOK;
                        case "<=":
                            return TokenType.LSEQ_TOK;
                        case ">=":
                            return TokenType.GREQ_TOK;
                        case "==":
                            return TokenType.EQL_TOK;
                        case "!=":
                            return TokenType.NTEQL_TOK;
                        default:
                            throw new Exception($"LexAnalyzer - Inputted word ({word}) doesn't match any given type in getType");
                    }
                }
            }
        }
        public Token getNextToken()
        {
            //Precondition: At least 1 token remains in the list
            //Postcondition: Return the first token in the list, then remove it from the list
            if (tokenList.Count == 0)
                throw new Exception("LexAnalyzer - getNextTok has reached the end of token list");
            var token = tokenList[0];
            tokenList.RemoveAt(0);
            return token;
        }

        public Token getToken()
        {
            //Precondition: At least 1 token remains in the list
            //Postcondition: Retrieve the first token in the list
            if (tokenList.Count == 0)
                throw new Exception("LexAnalyzer - getToken has no token to return");
            return tokenList[0];
        }
    }
}
