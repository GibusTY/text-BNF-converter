Text_BNF_Converter

BNF, or Backus-Naur Form, Grammar is a form of context free grammar meant to better simplify language syntaxes for human consumption, 
such as formats, documents, instructions and communication protocols. In this program it primarily demonstrates document flow, how text inputs are interpreted via the creation of objects within the program itself. 

The files included here were created as part of a project within my CS 4308 course (Concepts of Programming Language)
Each subfolder represents the same project done in different languages.

They are currently documented for purposes of evaluation.

The goal of each language's project is to take a text file as input, parse its words into a series of "tokens", 
create a series of objects meant to emulate the text's "code" through a BNF translation, before finally performing
the translated code via execution of the created objects.

expr1.txt through expr4.txt are all test files used the projects given. They should function according to program specifications if any are used as input within any language's respective 'Interpreter' file.

BNF Used:

\<program> -> function \<id> ( ) \<block> end

\<block> -> \<statement> | \<statement> \<block>

  \<statement> -> \<if_statement> | \<assignment_statement> | \<while_statement> | \<print_statement> | \<for_statement>
  
  \<if_statement> -> if \<boolean_expression> \<block> else \<block> end
    
  \<while_statement> -> while \<boolean_expression> \<block> end
    
  \<assignment_statement> -> id \<assignment_operator> \<arithmetic_expression>
    
  \<for_statement> -> for id = \<iter> \<block> end
    
  \<print_statement> -> print ( \<arithmetic_expression> )
    
  \<iter> -> \<arithmetic_expression> : \<arithmetic_expression>
  
  \<boolean_expression> -> \<relative_op> \<arithmetic_expression> \<arithmetic_expression>
  
  \<relative_op> -> le_operator | lt_operator | ge_operator | gt_operator | eq_operator | ne_operator
  
  \<arithmetic_expression> -> \<id> | \<literal_integer> | \<binary_expression>
  
  \<binary_expression> -> \<arithmetic_op> \<arithmetic_expression> \<arithmetic_expression>
  
  \<arithmetic_op> -> add_operator | sub_operator | mul_operator | div_operator | mod_operator | exp_operator | rev_div_operator
