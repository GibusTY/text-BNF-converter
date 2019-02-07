'''File name: Program.py
Created by Joshua Brock on 10/23/2018'''

from Block import Block


class Program:
    def __init__(self, block):
        '''Precondition: block is of valid type
        Postcondition: default constructor - creates a new program using block'''
        assert (isinstance(block, Block))
        self.block = block

    def execute(self):
        '''Postcondition: executes block'''
        self.block.execute()
