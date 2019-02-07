'''File name: ArithmeticExpression.py
Created by Joshua Brock on 10/23/2018'''


from Exception import Exception
from ArithmeticOperator import ArithmeticOperator
from Memory import *


class ArithmeticExpression:
    '''An interface to be expanded upon by the following files:
Id
Constant
BinaryExpression'''

    def evaluate(self):
        return -1


class Id(ArithmeticExpression):
    def __init__(self, ch):
        '''Precondition: ch is of valid type
        Postcondition: default constructor - creates new Id'''
        assert (isinstance(ch, str))
        self.ch = ch

    def evaluate(self):
        '''Postcondition: returns memory value of character'''
        return fetch(self.ch)


class Constant(ArithmeticExpression):
    def __init__(self, value):
        '''Precondition: value is of valid type
        Postcondition: default constructor - creates new Constant'''
        assert (isinstance(value, int))
        self.value = value

    def evaluate(self):
        '''Postcondition: returns value'''
        return self.value


class BinaryExpression(ArithmeticExpression):
    def __init__(self, op, expr1, expr2):
        '''Precondition: op, expr1, and expr2 are of valid types and exist
        Postcondition: default constructor - creates new BinaryExpression'''
        assert (isinstance(op, ArithmeticOperator) and isinstance(expr1, ArithmeticExpression)
                and isinstance(expr2, ArithmeticExpression))
        if expr1 is None or expr2 is None:
            raise Exception('Null expression argument')

        self.op = op
        self.expr1 = expr1
        self.expr2 = expr2

    def evaluate(self):
        '''Postcondition: returns result of an equation determined by the op, expr1, and expr2'''
        if self.op == ArithmeticOperator.ADD_OP:
            return self.expr1.evaluate() + self.expr2.evaluate()
        elif self.op == ArithmeticOperator.SUB_OP:
            return self.expr1.evaluate() - self.expr2.evaluate()
        elif self.op == ArithmeticOperator.MULT_OP:
            return self.expr1.evaluate() * self.expr2.evaluate()
        elif self.op == ArithmeticOperator.DIV_OP:
            return self.expr1.evaluate() / self.expr2.evaluate()
        elif self.op == ArithmeticOperator.REMNDR_OP:
            return self.expr1.evaluate() % self.expr2.evaluate()
        elif self.op == ArithmeticOperator.REVDIV_OP:
            return self.expr2.evaluate() / self.expr1.evaluate()
        else:
            return pow(self.expr1.evaluate(), self.expr2.evaluate())
