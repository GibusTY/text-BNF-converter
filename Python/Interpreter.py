'''File name: Interpreter.py
Created by Joshua Brock on 10/25/2018'''

from Memory import *
from Exception import Exception
from Parser import Parser

if __name__ == '__main__':
    '''Our main method to run our code through test files'''
    try:
        p = Parser('[[[insert file location here]]]')
        program = p.parse()
        program.execute()
        displayMemory()
        
    except Exception as e:
        print(e.message)