'''File Name: ArithmeticOperator.py
Created by Joshua Brock on 10/23/2018'''

'''Contains a list of enumerated mathematical operators.
        ADD_OP -> +
        SUB_OP -> -
        MUL_OP -> *
        DIV_OP -> /
        REMNDR_OP -> %
        VID_OP -> \\
        PWR_OP -> ^
'''

from enum import Enum


class ArithmeticOperator(Enum):
    ADD_OP = '+'
    SUB_OP = '-'
    MULT_OP = '*'
    DIV_OP = '/'
    REMNDR_OP = '%'
    REVDIV_OP = '\\'
    PWR_OP = '^'

