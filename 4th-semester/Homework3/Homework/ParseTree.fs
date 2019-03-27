namespace Homework

/// Module for implementing parse tree and function of parse tree evaluation
module ParseTree =
    open System

    /// Represents a parse tree of arithmetic expression
    type ParseTree =
        | Value of double
        | Addition of ParseTree * ParseTree
        | Substraction of ParseTree * ParseTree
        | Multiplication of ParseTree * ParseTree
        | Division of ParseTree * ParseTree
    
    /// Evaluates arithmetic expression
    let rec eval tree = 
        match tree with 
        | Value(value) -> value
        | Addition(left, right) -> eval left + eval right
        | Substraction(left, right) -> eval left - eval right
        | Multiplication(left, right) -> eval left * eval right
        | Division(left, right) -> if abs(eval right) < 0.0001
                                        then raise (DivideByZeroException())
                                        else eval left / eval right