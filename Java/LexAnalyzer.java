//File Name: LexAnalyzer.java
//Created by Joshua Brock on 9/15/2018

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class LexAnalyzer
{
    //The lexical analyzer's job involves creating a list of tokens, assigning values and qualities to each.
    //This will be a dependent to the parser which will need its gathered token list to check for proper code grammar.
    /*
        id -> letter

        literal_integer -> digit literal_integer | digit

        assignment_operator -> =

        Bool operators
            le_operator -> <=
            lt_operator -> <
            ge_operator -> >=
            gt_operator -> >
            eq_operator -> = =
            ne_operator -> !=
        Math operators
            add_operator -> +
            sub_operator -> -
            mul_operator -> *
            div_operator -> /
            mod_operator -> %
            rev_div_operator -> \
            exp_operator -> ^
    */

    private List<Token> tokenList; //Should contain a list of all things needing to be defined

    public LexAnalyzer(String file) throws FileNotFoundException
    {
        //Precondition: Inputted file exists.
        //Postcondition: Start and run a new lexical analyzer for the given file.
        assert(file != null);
        tokenList = new ArrayList<Token>();
        Scanner scanCode = new Scanner(new File (file));
        int linePos = 0;

        //Processes the whole file by processing each line individually.
        while(scanCode.hasNext())
        {
            String line = scanCode.nextLine();
            workLine(line, linePos);
            linePos++;
        }

        //Concludes our file scan by making an End of State token and closing the scanner.
        tokenList.add(new Token(linePos, 1, "EOS", TokenType.EOS_TOK));
        scanCode.close();
    }

    private void workLine(String line, int linePos)
    {
        //Precondition: The inputted string and line position are not invalid
        //Postcondition: We add the line to our list of tokens.
        assert(line != null && linePos >= 1);
        int index = 0;

        //Adds elements of the line to our token list. Spaces between elements are skipped.
        index = skipSpaces(line, index);
        while(index < line.length())
        {
            String lex = getLex(line, linePos, index);
            TokenType tokType = getType(lex, linePos, index);
            tokenList.add(new Token(linePos + 1, index + 1, lex, tokType));
            index += lex.length();
            index = skipSpaces(line, index);
        }
    }

    private TokenType getType(String lex, int lineNum, int columnNum)
    {
        //Precondition: Lexeme isn't empty and line & columns are valid.
        //Postcondition: Determines the token's type by analyzing traits.
        assert(lex != null && lineNum >= 1 && columnNum >= 1);

        //Assert a default value for our output variable.
        TokenType type = null;

        if(Character.isLetter(lex.charAt(0)))
        {
            if(lex.length() == 1)
            {
                //If our lexeme is of length 1 and is a letter, it must be an id token.
                type = TokenType.ID_TOK;
            }
            else
            {
                //If it is longer than 1 length, it could be a different token.
                if(lex.equals("function"))
                    type = TokenType.METHOD_TOK;
                else if(lex.equals("if"))
                    type = TokenType.IF_TOK;
                else if(lex.equals("else"))
                    type = TokenType.ELSE_TOK;
                else if(lex.equals("while"))
                    type = TokenType.WHILE_TOK;
                else if(lex.equals("for"))
                    type = TokenType.FOR_TOK;
                else if(lex.equals("print"))
                    type = TokenType.PRINT_TOK;
                else if(lex.equals("end"))
                    type = TokenType.END_TOK;
                else
                    throw new IllegalArgumentException("LexicalException: invalid lexeme at row " + (lineNum + 1) + " and column " + (columnNum + 1));
            }
        }
        else if(Character.isDigit(lex.charAt(0)))
        {
            //If we find only a digit (no letters or symbols included) it is a constant value.
            if(onlyDigits(lex))
                type = TokenType.CONST_TOK;
            else
                throw new IllegalArgumentException("LexicalException: invalid lexeme at row " + (lineNum + 1) + " and column " + (columnNum + 1));
        }

        //Remaining else if statements to isolate symbols
        else if(lex.equals("="))
            type = TokenType.ASSIGN_TOK;

        //Boolean token types
        else if(lex.equals("<="))
            type = TokenType.LSEQ_TOK;
        else if(lex.equals("<"))
            type = TokenType.LSTN_TOK;
        else if(lex.equals(">="))
            type = TokenType.GREQ_TOK;
        else if(lex.equals(">"))
            type = TokenType.GRTN_TOK;
        else if(lex.equals("=="))
            type = TokenType.EQL_TOK;
        else if(lex.equals("!="))
            type = TokenType.NTEQL_TOK;

        //Mathematical token types
        else if(lex.equals("+"))
            type = TokenType.ADD_TOK;
        else if(lex.equals("-"))
            type = TokenType.SUB_TOK;
        else if(lex.equals("*"))
            type = TokenType.MULT_TOK;
        else if(lex.equals("/"))
            type = TokenType.DIV_TOK;
        else if(lex.equals("%"))
            type = TokenType.REMNDR_TOK;
        else if(lex.equals("\\"))
            type = TokenType.VID_TOK;
        else if(lex.equals("^"))
            type = TokenType.PWR_TOK;
        else if(lex.equals("("))
            type = TokenType.INPUTSTRT_TOK;
        else if(lex.equals(")"))
            type = TokenType.INPUTEND_TOK;
        else if(lex.equals(":"))
            type = TokenType.COLON_TOK;

        //If it still isn't ANYTHING, it has to be invalid
        else
            throw new IllegalArgumentException("LexicalException: invalid lexeme at row " + (lineNum + 1) + " and column " + (columnNum + 1));

        return type;
    }

    private boolean onlyDigits(String line)
    {
        //Precondition: line isn't empty
        //Postcondition: Returns true if line contains only digits, otherwise false.
        assert(line != null);

        int x = 0;
        while(x < line.length() && Character.isDigit(line.charAt(x)))
            x++;
        return x == line.length();
    }

    private String getLex(String line, int lineNum, int index)
    {
        //Precondition: line isn't empty and lineNum and index is valid.
        //Postcondition: Returns the word found at the index.
        assert(line != null && lineNum >= 1 && index >= 0);
        int x = index;

        //Get the phrase after position index. End if reached the end of the line or hit a space
        while(x < line.length() && !Character.isWhitespace(line.charAt(x)))
            x++;

        return line.substring(index, x);
    }

    private int skipSpaces(String line, int index)
    {
        //Preconditions: line isn't empty and index is valid.
        //Postcondition: Return position of index after all spaces are skipped.
        assert(line != null && index >= 0);

        //Get index to skip spaces until there are no more spaces to skip.
        while(index < line.length() && Character.isWhitespace(line.charAt(index)))
            index++;

        return index;
    }

    public Token getNextTok()
    {
        //Precondition: At least 1 token remains in the list.
        //Postcondition: Make the next token in the list the first one. (remove old first one)
        if(tokenList.isEmpty())
            throw new IllegalArgumentException("LexicalException: end of tokens");
        return tokenList.remove(0);
    }

    public Token getToken()
    {
        //Precondition: At least 1 token remains in the list.
        //Postcondition: Retrieve the first token in the list.
        if(tokenList.isEmpty())
            throw new IllegalArgumentException("LexicalException: no tokens to get");
        return tokenList.get(0);
    }
}
