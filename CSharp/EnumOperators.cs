using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBNFConverter
{
    //Contains all of the Enumerated values within our project
    public enum ArithOperator
    {
        //Enumerated types of all mathematical operators present
        ADD_OP, SUB_OP, MUL_OP, DIV_OP, REMNDR_OP, VID_OP, PWR_OP
    }
    public enum BoolOperator
    {
        //Enumerated types of all boolean operators present
        LSQU_OP, LSTN_OP, GREQ_OP, GRTN_OP, EQL_OP, NTEQL_OP
    }
    public enum TokenType
    {
        //Enumerated types of all tokens present (see commented list below for reference)
        ID_TOK, CONST_TOK, ASSIGN_TOK, LSEQ_TOK, LSTN_TOK, GREQ_TOK, GRTN_TOK, EQL_TOK, NTEQL_TOK,
        ADD_TOK, SUB_TOK, MULT_TOK, DIV_TOK, REMNDR_TOK, VID_TOK, PWR_TOK, EOS_TOK,
        METHOD_TOK, INPUTSTRT_TOK, INPUTEND_TOK, IF_TOK, ELSE_TOK, WHILE_TOK, FOR_TOK, PRINT_TOK, COLON_TOK, END_TOK
        /*
            ID_TOK -> id -> letter
            CONST_TOK -> literal_integer -> digit literal_integer | digit
            ASSIGN_TOK -> assignment_operator -> =
            LSQU_TOK -> le_operator -> <=
            LSTN_TOK -> lt_operator -> <
            GREQ_TOK -> ge_operator -> >=
            GRTN_TOK -> gt_operator -> >
            EQL_TOK -> eq_operator -> = =
            NTEQL_TOK -> ne_operator -> !=
            ADD_TOK -> add_operator -> +
            SUB_TOK -> sub_operator -> -
            MULT_TOK -> mul_operator -> *
            DIV_TOK -> div_operator -> /
            REMNDR_TOK -> mod_operator -> %
            VID_TOK -> rev_div_operator -> \
            PWR_TOK -> exp_operator -> ^
            EOS_TOK -> helps indicate the end of the list of tokens
            The following tokens are added as part of a method.
            METHOD_TOK -> function
            INPUTSTRT_TOK -> (
            INPUTEND_TOK -> )
            IF_TOK -> if
            ELSE_TOK -> else
            WHILE_TOK -> while
            FOR_TOK -> for
            PRINT_TOK -> print
            END_TOK -> end
            COLON_TOK -> :
        */
    }
}
