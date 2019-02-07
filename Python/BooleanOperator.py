'''File Name: BooleanOperator.py
Created by Joshua Brock on 10/23/2018'''

'''Contains a list of enumerated boolean operators.
        LSQU_TOK -> le_operator -> <=
        LSTN_TOK -> lt_operator -> <
        GREQ_TOK -> ge_operator -> >=
        GRTN_TOK -> gt_operator -> >
        EQL_TOK -> eq_operator -> = =
        NTEQL_TOK -> ne_operator -> !=
'''

from enum import Enum


class BooleanOperator(Enum):
    LSQU_OP = '<='
    LSTN_OP = '<'
    GREQ_OP = '>='
    GRTN_OP = '>'
    EQL_OP = '=='
    NTEQL_OP = '!='

