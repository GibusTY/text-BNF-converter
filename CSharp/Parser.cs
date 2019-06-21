using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBNFConverter
{
    public class Parser
    {
        //The purpose of the Parser is to analyze the grammar and throw errors should bad grammar be detected in the file.
        //This will require the Lexical Analyzer to help it identify tokens.
        /*  BNF Grammar Below:
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
        public Parser(string file)
        {
            //Postcondition: Create our Lexical Analyzer, which we will use to gather our list of tokens for parsing
            lex = new LexAnalyzer(file);
        }
        private void match(Token token, TokenType tok_type)
        {
            //This will be used to assert that proper BNF grammar is used
            if (token == null || tok_type == null)
                throw new Exception("match - Bad arguments");
            if (token.Token_Type != tok_type)
                throw new Exception($"Parser - {tok_type} was expected, received {token.Token_Type} instead");
        }
        public Program parse() {
            //Precondition: [Program] -> [function] [Id] ( ) [Block] end [EOS]
            //Postcondition: Creates a program using the text from the LexAnalyzer's file. Should run according to the BNF grammer
            match(lex.getNextToken(), TokenType.METHOD_TOK);
            getId(); //The Id from here doesn't necessarily matter towards the rest of the program, so casting it to a variable is not needed.
            match(lex.getNextToken(), TokenType.INPUTSTRT_TOK);
            match(lex.getNextToken(), TokenType.INPUTEND_TOK);

            //Get the block we'll be executing
            var program = new Program(getBlock());

            //Assure that proper conclusion grammer was used
            match(lex.getNextToken(), TokenType.END_TOK);
            match(lex.getToken(), TokenType.EOS_TOK);

            return program;
        }
        private Block getBlock()
        {
            //Precondition: [Block] -> [Statement] | [Statement] [Block]
            //Postcondition: Creates a block using remaining tokens. Can contain an arbitrary number of statements
            var statements = new List<Statement>();
            var token = lex.getToken();

            //If our current token shows signs of being a statement, loop. Otherwise we must be concluding
            while (new[] {TokenType.IF_TOK, TokenType.ID_TOK, TokenType.PRINT_TOK, TokenType.WHILE_TOK, TokenType.FOR_TOK}.Contains(token.Token_Type)){
                statements.Add(getStatement());
                token = lex.getToken();
            }

            return new Block(statements);
        }
        private Statement getStatement()
        {
            //Precondition: [Statement] -> [If Statement] | [Assign Statement] | [Print Statement] | [While Statement] | [For Statement]
            //Postcondition: Creates a new statement using other methods. Chooses constructing method based on current token's type
            Token token = lex.getToken();

            switch (token.Token_Type)
            {
                case TokenType.IF_TOK:
                    return getIf();
                case TokenType.ID_TOK:
                    return getAssign();
                case TokenType.PRINT_TOK:
                    return getPrint();
                case TokenType.WHILE_TOK:
                    return getWhile();
                case TokenType.FOR_TOK:
                    return getFor();
                default:
                    throw new Exception($"getStatement - Current token ({token.Token_Type}) matches no existing Statements");
            }
        }
        private IfStatement getIf()
        {
            //Precondition: [If Statement] -> If [BoolExpression] [Block] else [Block] end
            //Postcondition: Creates an if statement using remaining tokens

            //Skip the 'if' (we already know it is an IF_TOK)
            lex.getNextToken();

            //Get our boolean expression, followed by our blocks
            var bool_expr = getBoolExpression();
            var block1 = getBlock();

            //Check for 'else' in the form of ELSE_TOK
            match(lex.getNextToken(), TokenType.ELSE_TOK);

            var block2 = getBlock();

            //Check for 'end' in the form of END_TOK
            match(lex.getNextToken(), TokenType.END_TOK);

            return new IfStatement(block1, block2, bool_expr);
        }
        private AssignStatement getAssign()
        {
            //Precondition: [Assign Statement] -> [Id] = [ArithExpression]
            //Postcondition: Creates an assignment statement using remaining tokens
            var id = getId();
            
            //Check for '=' in the form of ASSIGN_TOK
            match(lex.getNextToken(), TokenType.ASSIGN_TOK);

            var arith = getArithExpression();

            return new AssignStatement(id, arith);
        }
        private PrintStatement getPrint()
        {
            //Precondition: [Print Statement] -> print ( [ArithExpression] )
            //Postcondition: Creates a print statement using remaining tokens
            //Skip the 'print' (we already know it is an PRINT_TOK)
            lex.getNextToken();
            //Check for '(' in the form of INPUTSTRT_TOK
            match(lex.getNextToken(), TokenType.INPUTSTRT_TOK);

            var arith = getArithExpression();

            //Check for ')' in the form of INPUTEND_TOK
            match(lex.getNextToken(), TokenType.INPUTEND_TOK);

            return new PrintStatement(arith);
        }
        private WhileStatement getWhile()
        {
            //Precondition: [While Statement] -> while [BoolExpression] [Block] end
            //Postcondition: Creates a print statement using remaining tokens
            //Skip the 'while' (we already know it is an WHILE_TOK)
            lex.getNextToken();

            //Acquire BoolExpression and Block
            var bool_expr = getBoolExpression();
            var block = getBlock();

            //Check for 'end' in the form of END_TOK
            match(lex.getNextToken(), TokenType.END_TOK);

            return new WhileStatement(bool_expr, block);
        }
        private ForStatement getFor()
        {
            //Precondition: [For Statement] -> for [Id] = [Iter] [Block] end
            //Postcondition: Creates a for statement using remaining tokens
            //Skip the 'for' (we already know it is a FOR_TOK)
            lex.getNextToken();

            var id = getId();

            //Check for '=' in the form of ASSIGN_TOK
            match(lex.getNextToken(), TokenType.ASSIGN_TOK);

            var iter = getIter();
            var block = getBlock();

            //Check for 'end' in the form of END_TOK
            match(lex.getNextToken(), TokenType.END_TOK);

            return new ForStatement(block, iter, id);
        }
        private Iter getIter()
        {
            //Precondition: [Iter] -> [ArithExpression] : [ArithExpression]
            //Postcondition: Creates an iter using remaining tokens
            var expr1 = getArithExpression();

            //Check for ':' in the form of COLON_TOK
            match(lex.getNextToken(), TokenType.COLON_TOK);

            var expr2 = getArithExpression();

            return new Iter(expr1, expr2);
        }
        private BoolExpression getBoolExpression()
        {
            //Precondition: [BoolExpression] -> [BoolOperator] [ArithExpression] [ArithExpression]
            //Postcondition: Creates a boolean expression using remaining tokens
            var op = getBoolOperator();

            var expr1 = getArithExpression();
            var expr2 = getArithExpression();

            return new BoolExpression(op, expr1, expr2);
        }
        private BoolOperator getBoolOperator()
        {
            //Precondition: [BoolOperator] -> < | > | <= | >= | == | !=
            //Postcondition: Gets our current token's boolean operator
            var token = lex.getNextToken();

            switch (token.Token_Type)
            {
                case TokenType.EQL_TOK:
                    return BoolOperator.EQL_OP;
                case TokenType.NTEQL_TOK:
                    return BoolOperator.NTEQL_OP;
                case TokenType.GRTN_TOK:
                    return BoolOperator.GRTN_OP;
                case TokenType.LSTN_TOK:
                    return BoolOperator.LSTN_OP;
                case TokenType.GREQ_TOK:
                    return BoolOperator.GREQ_OP;
                case TokenType.LSEQ_TOK:
                    return BoolOperator.LSQU_OP;
                default:
                    throw new Exception($"getBoolOperator - Current token ({token.Token_Type}) is not a valid Boolean Operator");
            }
        }
        private ArithExpression getArithExpression()
        {
            //Precondition: [ArithExpression] -> [Id] | [Constant] | [BinaryExpression]
            //Postcondition: Creates an arithmetic expression using remaining tokens
            Token token = lex.getToken();

            switch (token.Token_Type)
            {
                case TokenType.ID_TOK:
                    return getId();
                case TokenType.CONST_TOK:
                    return getConst();
                case TokenType.ADD_TOK:
                case TokenType.SUB_TOK:
                case TokenType.MULT_TOK:
                case TokenType.DIV_TOK:
                case TokenType.VID_TOK:
                case TokenType.REMNDR_TOK:
                case TokenType.PWR_TOK:
                    return getBinExpression();
                default:
                    throw new Exception($"getArithExpression - Current token ({token.Token_Type}) matches no existing ArithExpressions");
            }
        }
        private BinaryExpression getBinExpression()
        {
            //Precondition: [BinaryExpression] -> [ArithOperator] [ArithExpression] [ArithExpression]
            //Postcondition: Creates a binary expression using remaining tokens
            var op = getArithOperator();

            var arith1 = getArithExpression();
            var arith2 = getArithExpression();

            return new BinaryExpression(arith1, arith2, op);
        }
        private ArithOperator getArithOperator()
        {
            //Precondition: [ArithOperator] -> + | - | * | / | \ | ^ | %
            //Postcondition: Gets our current token's arithmetic operator
            var token = lex.getNextToken();

            switch (token.Token_Type)
            {
                case TokenType.ADD_TOK:
                    return ArithOperator.ADD_OP;
                case TokenType.SUB_TOK:
                    return ArithOperator.SUB_OP;
                case TokenType.MULT_TOK:
                    return ArithOperator.MUL_OP;
                case TokenType.DIV_TOK:
                    return ArithOperator.DIV_OP;
                case TokenType.VID_TOK:
                    return ArithOperator.DIV_OP;
                case TokenType.REMNDR_TOK:
                    return ArithOperator.REMNDR_OP;
                case TokenType.PWR_TOK:
                    return ArithOperator.PWR_OP;
                default:
                    throw new Exception($"getArithOperator - Current token ({token.Token_Type}) is not a valid Arithmetic Operator");
            }
        }
        private Id getId()
        {
            //Precondition: [Id] -> [Any Character]
            //Postcondition: Gets our current token's variable character
            var token = lex.getNextToken();
            match(token, TokenType.ID_TOK);

            return new Id(token.Lexeme[0]);
        }
        private Constant getConst()
        {
            //Precondition: [Constant] -> [Any Integer]
            //Postcondition: Gets our current token's constant value
            var token = lex.getNextToken();
            match(token, TokenType.CONST_TOK);

            //Try to get our value. If we fail to get a valid integer from the lexeme, we got a bad token
            try
            {
                var value = System.Convert.ToInt32(token.Lexeme);
                return new Constant(value);
            }
            catch (Exception)
            {
                throw new Exception($"getConstant - Current token value ({token.Lexeme}) is not a valid Integer");
            }
        }
    }
}
