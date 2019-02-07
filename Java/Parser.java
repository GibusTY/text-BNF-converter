//File Name: Parser.java
//Created by Joshua Brock on 9/20/2018

import java.io.FileNotFoundException;
import java.util.ArrayList;

public class Parser
{
    //The purpose of the Parser is to analyze the grammar and throw errors should bad grammar be detected in the file.
    //This will require the Lexical Analyzer to help it identify tokens.

    /*
    <program> -> function id ( ) <block> end
    <block> -> <statement> | <statement> <block>
    <statement> -> <if_statement> | <assignment_statement> | <while_statement> | <print_statement> | <for_statement>
    <if_statement> -> if <boolean_expression> <block> else <block> end
    <while_statement> -> while <boolean_expression> <block> end
    <assignment_statement> -> id <assignment_operator> <arithmetic_expression>
    <for_statement> -> for id = <iter> <block> end
    <print_statement> -> print ( <arithmetic_expression> )
    <iter> -> <arithmetic_expression> : <arithmetic_expression>
    <boolean_expression> -> <relative_op> <arithmetic_expression> <arithmetic_expression>
    <relative_op> -> le_operator | lt_operator | ge_operator | gt_operator | eq_operator | ne_operator
    <arithmetic_expression> -> <id> | <literal_integer> | <binary_expression>
    <binary_expression> -> <arithmetic_op> <arithmetic_expression> <arithmetic_expression>
    <arithmetic_op> -> add_operator | sub_operator | mul_operator | div_operator | mod_operator | exp_operator | rev_div_operator
    */

    private LexAnalyzer lex;

    public Parser(String file) throws FileNotFoundException
    {
        //Precondition: file exists.
        //Postcondition: Throw a Lexical Analyzer to get the token list we'll need.
        lex = new LexAnalyzer(file);
    }

    public Program parse()
    {
        //Postcondition: Implements our <program> -> function id ( ) <block> end

        //First we must look for the token for 'function', then we get the id and ()
		match (lex.getNextTok(), TokenType.METHOD_TOK);
		Id var = getId();
		match (lex.getNextTok(), TokenType.INPUTSTRT_TOK);
		match (lex.getNextTok(), TokenType.INPUTEND_TOK);
		
		//Next we pass the block to our getBlock method.
		Program program = new Program(getBlock());
		
		//Finally we check for our end token and return our assignment.
		match (lex.getNextTok(), TokenType.END_TOK);
		match (lex.getToken(), TokenType.EOS_TOK);
		return program;
    }

    public Block getBlock()
    {
        //Postcondition: Implements our <block> -> <statement> | <statement> <block>

        //First we check each of the first token to find which expression we'll be using (if, assign, while, print, for) and throw its respective method.
        //When that method concludes, check the next token after. If it is also an if/assign/while/print/for, recursive this method.

		ArrayList<Statemnt> statemnts = new ArrayList<Statemnt>();

		Token tok = lex.getToken();
		TokenType test = tok.getTokType();
		while(tok.getTokType() == TokenType.IF_TOK || tok.getTokType() == TokenType.ID_TOK || tok.getTokType() == TokenType.WHILE_TOK || tok.getTokType() == TokenType.PRINT_TOK || tok.getTokType() == TokenType.FOR_TOK)
		{
			statemnts.add(getStatement());
			tok = lex.getToken();
			test = tok.getTokType();
			//Testing
		}

		return new Block(statemnts);
		//We need something here to determine a statement. If multiple statements are present we need to find a way to group them all together.
    }

    public Statemnt getStatement()
    {
        //Postcondition: Implements our <statement> -> <if_statement> | <assignment_statement> | <while_statement> | <print_statement> | <for_statement>
		//We must check their starting token, all start with unique tokens so this should be easy to pass these to their proper method.
		Token tok = lex.getToken();
		TokenType test = tok.getTokType();
		
		if(tok.getTokType() == TokenType.IF_TOK)
			return new Statemnt(getIfStatement());
		else if(tok.getTokType() == TokenType.ID_TOK)
			return new Statemnt(getAssignStatement());
		else if(tok.getTokType() == TokenType.WHILE_TOK)
			return new Statemnt(getWhileStatement());
		else if(tok.getTokType() == TokenType.PRINT_TOK)
			return new Statemnt(getPrintStatement());
		else
			return new Statemnt(getForStatement());
    }

    public IfStatement getIfStatement()
    {
        //Postcondition: Implements our <if_statement> -> if <boolean_expression> <block> else <block> end

        //First, we pass the boolean_expression to its method.
		lex.getNextTok();
		
		BooleanExpression bin_expr = getBoolExpression();
		Block block1 = getBlock();
		
		match(lex.getNextTok(), TokenType.ELSE_TOK);
		
		Block block2 = getBlock();
		
		match(lex.getNextTok(), TokenType.END_TOK);
        
		return new IfStatement(bin_expr, block1, block2);
    }
	
	public AssignStatement getAssignStatement()
    {
        //Postcondition: Implements our <assignment_statement> -> id <assignment_operator> <arithmetic_expression>

        //First we check for ID (this should already be confirmed but check anyway)
        
		Id var = getId();
		match (lex.getNextTok(), TokenType.ASSIGN_TOK);
		
		//Pass arithmetic_expression
		ArithmeticExpression expr = getArithmeticExpression();

		return new AssignStatement (var, expr);
    }

	 public WhileStatement getWhileStatement()
    {
        //Postcondition: Implements our <while_statement> -> while <boolean_expression> <block> end

        //First we check for a for token (this should already be confirmed but check anyway) then check the id afterward and the assign operator.
        //Pass iter to its method.
        //Pass block to its method again.
        //Check for end.

		lex.getNextTok();

		BooleanExpression booleanExpression = getBoolExpression();
		Block block = getBlock();

		match(lex.getNextTok(), TokenType.END_TOK);
        
		return new WhileStatement(booleanExpression, block);
    }
	
    public ForStatement getForStatement()
    {
        //Postcondition: Implements our <for_statement> -> for id = <iter> <block> end

        //First we check for a for token (this should already be confirmed but check anyway) then check the id afterward and the assign operator.
        //Pass iter to its method.
        //Pass block to its method again.
        //Check for end.
		
		lex.getNextTok();
		
		Id var = getId();
		match (lex.getNextTok(), TokenType.ASSIGN_TOK);
		Iter iter = getIterator();
		Block block = getBlock();
		
		match(lex.getNextTok(), TokenType.END_TOK);
        
		return new ForStatement(var, iter, block);
    }

    public PrintStatement getPrintStatement()
    {
        //Postcondition: Implements our <print_statement> -> print ( <arithmetic_expression> )

        //First we check for print (this should already be confirmed but check anyway) then check for '('
        //Pass arithmetic_expression to its method.
        //Check for ')'

		//TokenType test = lex.getNextTok().getTokType();
		lex.getNextTok();
		match (lex.getNextTok(), TokenType.INPUTSTRT_TOK);
		ArithmeticExpression expr = getArithmeticExpression();
		match (lex.getNextTok(), TokenType.INPUTEND_TOK);
        
		return new PrintStatement(expr);
    }

    public Iter getIterator()
    {
        //Postcondition: Implements our <iter> -> <arithmetic_expression> : <arithmetic_expression>

        //Pass arithmetic_expression 1 to its method.
        //Check for ':'
        //Pass arithmetic_expression 2 to its method.
		ArithmeticExpression expr1 = getArithmeticExpression();
		match (lex.getNextTok(), TokenType.COLON_TOK);
		ArithmeticExpression expr2 = getArithmeticExpression();
		
		return new Iter(expr1, expr2);
    }

    public BooleanExpression getBoolExpression()
    {
        //Postcondition: Implements our <boolean_expression> -> <relative_op> <arithmetic_expression> <arithmetic_expression>

        //Pass relative_op to its method.
        //Pass arithmetic_expression 1 & 2 to their methods.
		
		BooleanOperator op = getRelativeOperator();

        ArithmeticExpression expr1 = getArithmeticExpression();
		ArithmeticExpression expr2 = getArithmeticExpression();
		
		return new BooleanExpression(op, expr1, expr2);
    }

    public BooleanOperator getRelativeOperator()
    {
        //Postcondition: Figures our which boolean operator this token falls under.
		BooleanOperator op;
		Token tok = lex.getNextTok();
		if (tok.getTokType() == TokenType.LSEQ_TOK)
		{
			op = BooleanOperator.LSQU_OP;
		}
		else if (tok.getTokType() == TokenType.LSTN_TOK)
		{
			op = BooleanOperator.LSTN_OP;
		}
		else if (tok.getTokType() == TokenType.GREQ_TOK)
		{
			op = BooleanOperator.GREQ_OP;
		}
		else if (tok.getTokType() == TokenType.GRTN_TOK)
		{
			op = BooleanOperator.GRTN_OP;
		}
		else if (tok.getTokType() == TokenType.EQL_TOK)
		{
			op = BooleanOperator.EQL_OP;
		}
		else if (tok.getTokType() == TokenType.NTEQL_TOK)
		{
			op = BooleanOperator.NTEQL_OP;
		}
		else
			throw new IllegalArgumentException("ParserException: operator expected at row " + tok.getRowNumber() +" and column "  + tok.getColumnNumber());
		
		return op;
    }

    public ArithmeticExpression getArithmeticExpression()
    {
        //Postcondition: Implements our <arithmetic_expression> -> <id> | <literal_integer> | <binary_expression>
		ArithmeticExpression expr;
		
		Token tok = lex.getToken();
		if (tok.getTokType() == TokenType.ID_TOK)
			expr = getId();
		else if(tok.getTokType() == TokenType.CONST_TOK)
			expr = getConstant();
		else
			expr = getBinaryExpression();
		
		return expr;
    }

    public BinaryExpression getBinaryExpression()
    {
        //Postcondition: <binary_expression> -> <arithmetic_op> <arithmetic_expression> <arithmetic_expression>
		
		ArithmeticOperator op = getArithmeticOperator();

		ArithmeticExpression expr1 = getArithmeticExpression();
		ArithmeticExpression expr2 = getArithmeticExpression();
		
		return new BinaryExpression(op, expr1, expr2);
    }

    public ArithmeticOperator getArithmeticOperator()
    {
        //Postcondition: Figures our which mathematical operator this token falls under.
		
		ArithmeticOperator op;
		Token tok = lex.getNextTok();
		if (tok.getTokType() == TokenType.ADD_TOK)
		{
			op = ArithmeticOperator.ADD_OP;
		}
		else if (tok.getTokType() == TokenType.SUB_TOK)
		{
			op = ArithmeticOperator.SUB_OP;
		}
		else if (tok.getTokType() == TokenType.MULT_TOK)
		{
			op = ArithmeticOperator.MUL_OP;
		}
		else if (tok.getTokType() == TokenType.DIV_TOK)
		{
			op = ArithmeticOperator.DIV_OP;
		}
		else if (tok.getTokType() == TokenType.REMNDR_TOK)
		{
			op = ArithmeticOperator.REMNDR_OP;
		}
		else if (tok.getTokType() == TokenType.VID_TOK)
		{
			op = ArithmeticOperator.VID_OP;
		}
		else if (tok.getTokType() == TokenType.PWR_TOK)
		{
			op = ArithmeticOperator.PWR_OP;
		}
		else
			throw new IllegalArgumentException("ParserException: operator expected at row " + tok.getRowNumber() +" and column "  + tok.getColumnNumber());
		
		return op;
    }

	private Id getId()
	{
		Token tok = lex.getNextTok();
		match (tok, TokenType.ID_TOK);
		return new Id (tok.getLexeme().charAt(0));
	}

	private Constant getConstant()
	{
		Token tok = lex.getToken();
		match (tok, TokenType.CONST_TOK);
		int value = Integer.parseInt(tok.getLexeme());
		lex.getNextTok();
		return new Constant (value);
	}

	private void match(Token tok, TokenType tokType)
	{
		assert (tok != null && tokType != null);
		if (tok.getTokType() != tokType)
			throw new IllegalArgumentException ("ParserException: " + tokType.name() + " expected at row " +
					tok.getRowNumber() +" and column "  + tok.getColumnNumber());
	}
}
