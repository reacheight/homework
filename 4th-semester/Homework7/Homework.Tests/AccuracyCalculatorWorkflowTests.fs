namespace Homework.Tests

module AccuracyCalculatorWorkflowTests =
    open NUnit.Framework
    open FsUnit
    open Homework.AccuracyCalculatorWorkflow
    
    let rounding = AccuracyCalculatorBuilder
    let tolerance = 0.00001
    let equalWithinTolerance = equalWithin tolerance
    
    [<Test>]
    let ``calculator should calculate and round simple workflow`` () =
        let result = rounding 3 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }
        
        result |> should equalWithinTolerance 0.048
    
    [<Test>]
    let ``calculator should correctly handle 0`` () =
        let result = rounding 3 {
            let! a = 5.3
            let! b = 9.7
            let! c = 9.7 + 5.3
            return c - a - b
        }
        
        result |> should equalWithinTolerance 0.0
    
    [<Test>]
    let ``calculator should correctly handle zero accuracy`` () =
        let result = rounding 0 {
            let! a = 5.0
            let! b = 3.45
            return a + b
        }
        
        result |> should equalWithinTolerance 8.0
        
    [<Test>]
    let ``rounding should happen on every calculation`` () =
        let result = rounding 0 {
            let! a = 5.4
            let! b = 5.4
            let! c = 10.8 - 2.0
            return a + b + c
        }
        
        result |> should equalWithinTolerance 19.0