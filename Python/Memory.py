'''File name: Memory.py
Created by Joshua Brock on 10/25/2018'''


idlst = []
mem = []


def fetch(ch):
    '''Precondition: ch is valid and is in our list
    Postcondition: returns the value associated with our ch'''
    assert (isinstance(ch, str) and getIndex(ch) != -1)
    return mem[getIndex(ch)]


def store(ch, value):
    '''Precondition: ch and value are valid
    Postcondition: creates new entries for ch and value, if ch already exists, replace its current value'''
    assert (isinstance(ch, str) and isinstance(value, int))
    if getIndex(ch) == -1:
        idlst.append(ch)
        mem.append(value)
    else:
        mem[getIndex(ch)] = value


def getIndex(ch):
    '''Precondition: ch is valid
    Postcondition: returns index of ch in our idlst. If not there returns -1'''
    assert (isinstance(ch, str))
    for x in range(len(idlst)):
        if idlst[x] == ch:
            return x
    return -1


def displayMemory():
    '''Postcondition: Returns all of the chars with their associated values'''
    for x in range(len(idlst)):
        print(str(idlst[x]) + ": " + str(mem[x]))
