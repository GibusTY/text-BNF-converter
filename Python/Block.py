'''File name: Block.py
Created by Joshua Brock on 9/23/2018'''


class Block:

    def __init__(self, statements):
        '''Precondition: statements is valid
        Postcondition: default constructor - creates a new block with list of statements'''
        assert (isinstance(statements, list))
        self.statements = []
        self.statements.extend(statements)

    def execute(self):
        '''Postcondition: executes every statement in the list in the order they were added'''
        for x in self.statements:
            x.execute()
