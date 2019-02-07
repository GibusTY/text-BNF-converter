'''File name: Statement.py
Created by Joshua Brock on 10/23/2018'''


from ArithmeticExpression import *
from ArithmeticOperator import ArithmeticOperator
from BooleanExpression import BooleanExpression
from Iter import Iter
from Block import Block


class Statement:
    '''An interface to be expanded upon by the following files:
IfStatement
PrintStatement
ForStatement
WhileStatement
AssignStatement'''


class IfStatement(Statement):
    def __init__(self, bin_expr, block1, block2):
        '''Precondition: bin_expr, block1, and block2 are of valid types
        Postcondition: default constructor - creates a new IfStatement'''
        assert (isinstance(bin_expr, BooleanExpression) and isinstance(block1, Block) and isinstance(block2, Block))

        if bin_expr is None:
            raise Exception('Null binary expression')
        if block1 is None or block2 is None:
            raise Exception('Null expression argument(s)')

        self.bin_expr = bin_expr
        self.block1 = block1
        self.block2 = block2

    def execute(self):
        '''Postcondition: if bin_expr is true, executes block1, otherwise executes block2'''
        if self.bin_expr.execute():
            self.block1.execute()
        else:
            self.block2.execute()


class PrintStatement(Statement):
    def __init__(self, arith_expr):
        '''Precondition: arith_expr is of valid type
        Postcondition: default constructor - creates a new PrintStatement'''
        assert arith_expr, ArithmeticExpression
        self.arith_expr = arith_expr

    def execute(self):
        '''Postcondition: prints out arith_expr in a string format'''
        if self.arith_expr is None:
            print("")
        else:
            print(self.arith_expr.evaluate())


class ForStatement(Statement):
    def __init__(self, id, iter, block):
        '''Precondition: id, iter, and block are of valid types
        Postcondition: default constructor - creates a new ForStatement'''
        assert (isinstance(id, Id) and isinstance(iter, Iter) and isinstance(block, Block))
        if block is None:
            raise Exception('Null expression argument')
        self.id = id
        self.iter = iter
        self.block = block

    def execute(self):
        '''Postcondition: assigns id to iter's start value, then executes block. id's value is in/decremented
            Should exit loop once id is equal to iter's end value, otherwise we risk an infinite loop'''
        AssignStatement(self.id, self.iter.arithbegin).execute()

        if self.id.evaluate() < self.iter.arithend.evaluate():
            changeidval = BinaryExpression(ArithmeticOperator.ADD_OP, self.id, Constant(1))
        else:
            changeidval = BinaryExpression(ArithmeticOperator.SUB_OP, self.id, Constant(1))

        while self.id.evaluate() != self.iter.arithend.evaluate():
            self.block.execute()
            AssignStatement(self.id, changeidval).execute()


class WhileStatement(Statement):
    def __init__(self, bool_expr, block):
        '''Precondition: bool_expr and block are of valid types
        Postcondition: default constructor - creates a new WhileStatement'''
        assert (isinstance(bool_expr, BooleanExpression) and isinstance(block, Block))
        if bool_expr is None:
            raise Exception('Null boolean argument')
        if block is None:
            raise Exception('Null expression argument')
        self.bool_expr = bool_expr
        self.block = block

    def execute(self):
        '''Postcondition: executes block while bool_expr remains true
        WARNING: block should contribute to bool_expr becoming false else infinite loop can occur'''
        while self.bool_expr.execute():
            self.block.execute()


class AssignStatement(Statement):
    def __init__(self, var, expr):
        '''Precondition: var and expr are of valid types
        Postcondition: default constructor - creates a new AssignStatement'''
        assert (isinstance(var, Id) and isinstance(expr, ArithmeticExpression))
        if var is None:
            raise Exception('Null ID argument')
        if expr is None:
            raise Exception('Null expression argument')
        self.var = var
        self.expr = expr

    def execute(self):
        '''Postcondition: stores expr's value in memory alongside var's character'''
        store(self.var.ch, self.expr.evaluate())
