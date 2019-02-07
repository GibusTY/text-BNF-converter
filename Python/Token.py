'''File name: Token.py
Created by Joshua Brock on 9/23/2018'''

from TokenType import TokenType
from Exception import Exception


class Token:

    def __init__(self, newlex, newtype):
        '''Precondition: row, column, newlex, and newtype are of valid types and exist/are valid
        Postcondition: default constructor - creates new Token'''
        assert (isinstance(newlex, str) and isinstance(newtype, TokenType))
        if newlex is None or len(newlex) <= 0:
            raise Exception('empty lexeme')
        if newtype is None:
            raise Exception('no defined token type')

        self.newlex = newlex
        self.newtype = newtype
