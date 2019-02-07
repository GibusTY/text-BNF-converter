'''File name: Iter.py
Created by Joshua Brock on 10/23/2018'''


from ArithmeticExpression import ArithmeticExpression
from Exception import Exception


class Iter:
    def __init__(self, arithbegin, arithend):
        '''Precondition: arithbegin & arithend exist and are of valid type
        Postcondition: default constructor - creates new Iter'''
        assert (isinstance(arithbegin, ArithmeticExpression) and isinstance(arithend, ArithmeticExpression))
        if arithbegin is None or arithend is None:
            raise Exception('Null inputted values')

        self.arithbegin = arithbegin
        self.arithend = arithend
