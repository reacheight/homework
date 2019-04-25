namespace Homework.Tests

module StringCalculatorWorkflowTests =
    open NUnit.Framework
    open FsUnit
    open Homework.StringCalculatorWorkflow
    
    let stringCalculator = new StringCalculatorBuilder()
    
    [<Test>]
    let ``calculator should calculate correct sum of two integers`` () =
        let result = stringCalculator {
            let! x = "6"
            let! y = "4"
            let z = x + y
            return z
        }
        
        result |> should equal (Some 10.0)
    
    [<Test>]
    let ``calculator should calculate expression with multiple operations`` () =
        let result = stringCalculator {
            let! a = "5"
            let! b = "10"
            let! c = "3"
            let! d = "2"
            return (a + b) / c * d
        }
        
        result |> should equal (Some 10.0)
    
    [<Test>]
    let ``calculator should calculate expression with float numbers`` () =
        let result = stringCalculator {
            let! a = "10"
            let! b = "0.5"
            let! c = "7.5"
            let! d = "4.9"
            return a * b + c / d
        }
        
        match result with
        | None -> Assert.Fail()
        | Some value -> value |> should (equalWithin 0.00001) (10.0 * 0.5 + 7.5 / 4.9)
        
    [<Test>]
    let ``calculator should return none with on wrong expression`` () =
        let result = stringCalculator {
            let! a = "5.4"
            let! b = "h"
            let! c = "3"
            return a + b + c
        }
        
        result |> should equal None
