namespace Homework3.Tests

module ParseTreeEvalTest =
    open NUnit.Framework
    open FsUnit
    open Homework.ParseTree
    open System
    
    let tolerance = 0.01
    
    [<Test>]
    let ``value should be evaluated correctly`` () =
        eval (Value(3.)) |> should (equalWithin tolerance) 3.
    
    [<Test>]
    let ``simple expression should be evaluated correctly`` () =
        let expression = (Addition(
                                   Addition(Value(2.), Substraction(Multiplication(Value(5.), Value(5.)), Value(4.5))),
                                   Division(Value(10.), Value(5.))))
        let result = (2. + (5. * 5. - 4.5)) + (10. / 5.)
        eval expression |> should equal result