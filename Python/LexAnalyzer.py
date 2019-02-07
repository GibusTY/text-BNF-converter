'''File name: LexAnalyzer.py
Created by Joshua Brock on 10/25/2018'''

from Exception import Exception
from TokenType import TokenType
from Token import Token


class LexAnalyzer:

    def __init__(self, filename):
        '''Precondition: file is of valid type and exists
        Postcondition: default constructor - starts and runs a new lexical analyzer for the inputted file'''
        assert(isinstance(filename, str))
        if filename is None:
            raise Exception('File not found.')

        file_obj = open(filename, 'r')

        self.tokenList = []

        self.linePos = 0

        for line in file_obj:
            for word in line.split():
                self.workWord(word)

        self.tokenList.append(Token("EOS", TokenType.EOS_TOK))
        file_obj.close()

    def workWord(self, word):
        '''Precondition: inputted word exists
        Postcondition: creates and adds this word to our token list'''
        if word is None:
            raise Exception('Empty word passed')

        tokType = self.getType(word)

        self.tokenList.append(Token(word, tokType))

    def getType(self, word):
        '''Precondition: inputted word exists
        Postcondition: returns the type of the word inputted'''
        if word is None:
            raise Exception('Empty word passed')

        if not self.isDigit(word):
            if len(word) == 1:
                if word == '=':
                    return TokenType.ASSIGN_TOK
                elif word == '<':
                    return TokenType.LSTN_TOK
                elif word == '>':
                    return TokenType.GRTN_TOK
                elif word == '+':
                    return TokenType.ADD_TOK
                elif word == '-':
                    return TokenType.SUB_TOK
                elif word == '*':
                    return TokenType.MULT_TOK
                elif word == '/':
                    return TokenType.DIV_TOK
                elif word == '%':
                    return TokenType.REMNDR_TOK
                elif word == '\\':
                    return TokenType.REVDIV_TOK
                elif word == '^':
                    return TokenType.PWR_TOK
                elif word == '(':
                    return TokenType.INPUTSTRT_TOK
                elif word == ')':
                    return TokenType.INPUTEND_TOK
                elif word == ':':
                    return TokenType.COLON_TOK
                else:
                    return TokenType.ID_TOK
            else:
                if word == 'function':
                    return TokenType.METHOD_TOK
                elif word == 'if':
                    return TokenType.IF_TOK
                elif word == 'else':
                    return TokenType.ELSE_TOK
                elif word == 'while':
                    return TokenType.WHILE_TOK
                elif word == 'for':
                    return TokenType.FOR_TOK
                elif word == 'print':
                    return TokenType.PRINT_TOK
                elif word == 'end':
                    return TokenType.END_TOK
                elif word == '<=':
                    return TokenType.LSQU_TOK
                elif word == '>=':
                    return TokenType.GREQ_TOK
                elif word == '==':
                    return TokenType.EQL_TOK
                elif word == '!=':
                    return TokenType.NTEQL_TOK
                else:
                    raise Exception('LexicalException: invalid lexeme')
        else:
            return TokenType.CONST_TOK

    def isDigit(self, word):
        '''Postcondition: returns true if inputted string should be treated as an int, returns false otherwise'''
        if word[0] in ('0', '1', '2', '3', '4', '5', '6', '7', '8', '9'):
            return True
        else:
            return False

    def getNextTok(self):
        '''Precondition: tokenList contains at least 1 token
        Postcondition: removes first element from tokenList and returns it for reference'''
        if len(self.tokenList) == 0:
            raise Exception('LexicalException: end of token list')
        return self.tokenList.pop(0)

    def getToken(self):
        '''Precondition: tokenList contains at least 1 token
        Postcondition: returns first element from tokenList as reference (is not removed)'''
        if len(self.tokenList) == 0:
            raise Exception('LexicalException: no tokens to get')
        return self.tokenList[0]
