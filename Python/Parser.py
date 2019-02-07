'''File name: Parser.py
Created by Joshua Brock on 10/25/2018'''

from LexAnalyzer import LexAnalyzer
from Program import Program
from Statement import IfStatement, WhileStatement, PrintStatement, ForStatement, AssignStatement
from Block import Block
from TokenType import TokenType
from BooleanExpression import BooleanExpression
from ArithmeticExpression import Id, Constant, BinaryExpression
from BooleanOperator import BooleanOperator
from ArithmeticOperator import ArithmeticOperator
from Iter import Iter
from Token import Token


class Parser:
    def __init__(self, file):
        '''Precondition: file exists
        Postcondition: Throw a lexical analyzer to get the tokenlist'''
        self.lex = LexAnalyzer(file)

    def parse(self):
        '''Postcondition: Implements program -> function ID ( ) -> <block>'''
        self.match(self.lex.getNextTok(), TokenType.METHOD_TOK)
        self.match(self.lex.getNextTok(), TokenType.ID_TOK)
        self.match(self.lex.getNextTok(), TokenType.INPUTSTRT_TOK)
        self.match(self.lex.getNextTok(), TokenType.INPUTEND_TOK)

        program = Program(self.getBlock())

        self.match(self.lex.getNextTok(), TokenType.END_TOK)
        self.match(self.lex.getNextTok(), TokenType.EOS_TOK)

        return program

    def getBlock(self):
        '''Postcondition: Implements <block> -> <statement> <block> || <statement>'''
        statements = []
        tok = self.lex.getToken()

        while tok.newtype in (TokenType.IF_TOK, TokenType.ID_TOK, TokenType.WHILE_TOK,
                              TokenType.FOR_TOK, TokenType. PRINT_TOK):
            statements.append(self.getStatement())
            tok = self.lex.getToken()

        return Block(statements)

    def getStatement(self):
        '''Postcondition: Implements <statement> -> <ifstate> || <assignstate> || <whilestate> || <printstate> ||
        <forstate>'''
        tok = self.lex.getToken()

        if tok.newtype == TokenType.IF_TOK:
            return self.getIfStatement()
        elif tok.newtype == TokenType.ID_TOK:
            return self.getAssignStatement()
        elif tok.newtype == TokenType.WHILE_TOK:
            return self.getWhileStatement()
        elif tok.newtype == TokenType.PRINT_TOK:
            return self.getPrintStatement()
        else:
            return self.getForStatement()

    def getIfStatement(self):
        '''Postcondition: Implements <ifstate> -> if <boolexpr> <block> <block>'''
        self.lex.getNextTok()

        bin_expr = self.getBoolExpression()
        block1 = self.getBlock()

        self.match(self.lex.getNextTok(), TokenType.ELSE_TOK)

        block2 = self.getBlock()

        self.match(self.lex.getNextTok(), TokenType.END_TOK)

        return IfStatement(bin_expr, block1, block2)

    def getAssignStatement(self):
        '''Postcondition: Implements <assignstate> -> <id> = <arithexpr>'''
        var = self.getId()
        self.match(self.lex.getNextTok(), TokenType.ASSIGN_TOK)

        expr = self.getArithmeticExpression()

        return AssignStatement(var, expr)

    def getWhileStatement(self):
        '''Postcondition: Implements <whilestate> -> while <boolexpr> <block>'''
        self.lex.getNextTok()

        bool_expr = self.getBoolExpression()
        block = self.getBlock()

        self.match(self.lex.getNextTok(), TokenType.END_TOK)

        return WhileStatement(bool_expr, block)

    def getForStatement(self):
        '''Postcondition: Implements <forstate> -> for <id> <iter> <block>'''
        self.lex.getNextTok()

        var = self.getId()
        self.match(self.lex.getNextTok(), TokenType.ASSIGN_TOK)
        iter = self.getIterator()
        block = self.getBlock()

        self.match(self.lex.getNextTok(), TokenType.END_TOK)

        return ForStatement(var, iter, block)

    def getPrintStatement(self):
        '''Postcondition: Implements <printstate> -> print ( <arithexpr> )'''
        self.lex.getNextTok()
        self.match(self.lex.getNextTok(), TokenType.INPUTSTRT_TOK)
        expr = self.getArithmeticExpression()
        self.match(self.lex.getNextTok(), TokenType.INPUTEND_TOK)

        return PrintStatement(expr)

    def getIterator(self):
        '''Postcondition: Implements <iter> -> <arithexpr> : <arithexpr>'''
        expr1 = self.getArithmeticExpression()
        self.match(self.lex.getNextTok(), TokenType.COLON_TOK)
        expr2 = self.getArithmeticExpression()

        return Iter(expr1, expr2)

    def getBoolExpression(self):
        '''Postcondition: Implements <boolexpr> -> <boolop> <arithexpr> <arithexpr>'''
        op = self.getRelativeOperator()

        expr1 = self.getArithmeticExpression()
        expr2 = self.getArithmeticExpression()

        return BooleanExpression(op, expr1, expr2)

    def getRelativeOperator(self):
        '''Postcondition: Defines <boolop> by using current token type'''
        tok = self.lex.getNextTok()

        if tok.newtype == TokenType.LSQU_TOK:
            return BooleanOperator.LSQU_OP
        elif tok.newtype == TokenType.LSTN_TOK:
            return BooleanOperator.LSTN_OP
        elif tok.newtype == TokenType.GREQ_TOK:
            return BooleanOperator.GREQ_OP
        elif tok.newtype == TokenType.GRTN_TOK:
            return BooleanOperator.GRTN_OP
        elif tok.newtype == TokenType.EQL_TOK:
            return BooleanOperator.EQL_OP
        elif tok.newtype == TokenType.NTEQL_TOK:
            return BooleanOperator.NTEQL_OP
        else:
            raise Exception('ParserException: boolean operator expected')

    def getArithmeticExpression(self):
        '''Postcondition: Implements <arithexpr> -> <id> || <const> || <binexpr>'''
        tok = self.lex.getToken()
        if tok.newtype == TokenType.ID_TOK:
            return self.getId()
        elif tok.newtype == TokenType.CONST_TOK:
            return self.getConstant()
        else:
            return self.getBinaryExpression()

    def getBinaryExpression(self):
        '''Postcondition: Implements <binexpr> -> <arithop> <arithexpr> <arithexpr>'''
        op = self.getArithmeticOperator()

        expr1 = self.getArithmeticExpression()
        expr2 = self.getArithmeticExpression()

        return BinaryExpression(op, expr1, expr2)

    def getArithmeticOperator(self):
        '''Postcondition: Defines <arithop> by using the current token type'''
        tok = self.lex.getNextTok()

        if tok.newtype == TokenType.ADD_TOK:
            return ArithmeticOperator.ADD_OP
        elif tok.newtype == TokenType.SUB_TOK:
            return ArithmeticOperator.SUB_OP
        elif tok.newtype == TokenType.MULT_TOK:
            return ArithmeticOperator.MULT_OP
        elif tok.newtype == TokenType.DIV_TOK:
            return ArithmeticOperator.DIV_OP
        elif tok.newtype == TokenType.REMNDR_TOK:
            return ArithmeticOperator.REMNDR_OP
        elif tok.newtype == TokenType.REVDIV_TOK:
            return ArithmeticOperator.REVDIV_OP
        elif tok.newtype == TokenType.PWR_TOK:
            return ArithmeticOperator.PWR_OP
        else:
            raise Exception('ParserException: arithmetic operator expected')

    def getId(self):
        '''Postcondition: creates a new Id to represent <id>'''
        tok = self.lex.getNextTok()
        self.match(tok, TokenType.ID_TOK)
        return Id(tok.newlex)

    def getConstant(self):
        '''Postcondition: creates a new Constant to represent <const>'''
        tok = self.lex.getNextTok()
        self.match(tok, TokenType.CONST_TOK)
        value = int(tok.newlex)

        return Constant(value)

    def match(self, tok, tokType):
        '''Precondition: tok and tokType exist and are of valid types
        Postcondition: raises Exception should tok's token type not match tokType'''
        assert (tok is not None and tokType is not None)
        assert (isinstance(tok, Token) and isinstance(tokType, TokenType))
        if tok.newtype != tokType:
            raise Exception('ParserException: \nExpected tokentype: ' + str(tokType) + '\nRecieved tokentype: ' + str(tok.newtype))
