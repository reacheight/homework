namespace FibonacciSum.Tests

module FibonacciSumTest =
    open FibonacciSum.FibonacciSum
    open NUnit.Framework
    open FsUnit
    
    let correctSum = 1089154
    
    [<Test>]
    let ``fibonacciSum should be equal to correct sum`` () =
        fibonacciSum |> should equal correctSum