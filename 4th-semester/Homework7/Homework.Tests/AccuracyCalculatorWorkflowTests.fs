namespace Homework.Tests

module AccuracyCalculatorWorkflowTests =
    open NUnit.Framework
    open FsUnit
    open Homework.AccuracyCalculatorWorkflow
    
    let rounding = AccuracyCalculatorBuilder
    let tolerance = 0.00001
    
    [<Test>]
    let ``calculator should calculate simple workflow`` () =
        let result = rounding 3 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }
        
        result |> should (equalWithin tolerance) 0.048