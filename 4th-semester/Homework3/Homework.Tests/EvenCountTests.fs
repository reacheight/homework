namespace Homework3.Tests

module EvenCountTests =
    open NUnit.Framework
    open FsUnit
    open Homework.EvenCount
    open FsCheck
    open FsCheck
    
    [<Test>]
    let ``evenCount functions should return the same result on the same input`` () =
        let evenCountFunctionsAreEqual (list : list<int>) =
            evenCountFirst list = evenCountSecond list && evenCountFirst list = evenCountThird list
        
        Check.QuickThrowOnFailure evenCountFunctionsAreEqual
    
    [<Test>]
    let ``evenCountFirst should return 0 on an empty list`` () =
        evenCountFirst [] |> should equal 0
    
    [<Test>]
    let ``evenCountFirst should work correct on natural numbers`` () =
        evenCountFirst [2; 1; 6; 1234; 435345; 3; 0; 8] |> should equal 5
        
    [<Test>]
    let ``evenCountFirst should work correct on integers`` () =
        evenCountFirst [2; 1; 6; 1234; 435345; 3; 0; 8; -6; -10] |> should equal 7