'''File name: BooleanExpression.py
Created by Joshua Brock on 10/25/2018'''

from BooleanOperator import BooleanOperator
from ArithmeticExpression import ArithmeticExpression
from Exception import Exception


class BooleanExpression:
    def __init__(self, op, expr1, expr2):
        '''Precondition: op, expr1 & expr2 are all of valid types and exist.
        Postcondition: default constructor - creates new BooleanExpression'''

        assert (isinstance(op, BooleanOperator) and isinstance(expr1, ArithmeticExpression) and
                isinstance(expr2, ArithmeticExpression))
        if expr1 is None or expr2 is None:
            raise Exception('Null expression argument(s)')

        self.op = op
        self.expr1 = expr1
        self.expr2 = expr2

    def execute(self):
        '''Postcondition: Returns a boolean based on an equation determined by op, expr1, and expr2'''
        if self.op == BooleanOperator.LSQU_OP:
            return self.expr1.evaluate() <= self.expr2.evaluate()
        elif self.op == BooleanOperator.LSTN_OP:
            return self.expr1.evaluate() < self.expr2.evaluate()
        elif self.op == BooleanOperator.GREQ_OP:
            return self.expr1.evaluate() >= self.expr2.evaluate()
        elif self.op == BooleanOperator.GRTN_OP:
            return self.expr1.evaluate() > self.expr2.evaluate()
        elif self.op == BooleanOperator.EQL_OP:
            return self.expr1.evaluate() == self.expr2.evaluate()
        else:
            return self.expr1.evaluate() != self.expr2.evaluate()
