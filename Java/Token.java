//File Name: token.java
//Created by Joshua Brock on 9/17/2018

public class Token
{
    //The token's job is to define parts of a Julia code.
    //It will contain the token's position, specific string value, and its type.

    private int rowNum;
    private int columnNum;
    private String lex;
    private TokenType type;

    public Token(int row, int column, String newlex, TokenType newtype)
    {
        //Precondition: row and column numbers are valid, string exists and there's a defined type.
        //Postcondition: creates a new token with the given values.
        if(row <= 0)
            throw new IllegalArgumentException("invalid row");
        if(column <= 0)
            throw new IllegalArgumentException("invalid column");
        if(newlex == null || newlex.length() == 0)
            throw new IllegalArgumentException("empty lexeme");
        if(newtype == null)
            throw new IllegalArgumentException("undefined token type");

        //Assigns values
        rowNum = row;
        columnNum = column;
        lex = newlex;
        type = newtype;
    }

    //Public methods to acquire private variable values.
    public int getRowNumber()
    {
        return rowNum;
    }
    public int getColumnNumber()
    {
        return columnNum;
    }
    public String getLexeme()
    {
        return lex;
    }
    public TokenType getTokType()
    {
        return type;
    }
}
