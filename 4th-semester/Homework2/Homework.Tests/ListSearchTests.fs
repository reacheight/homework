namespace Homework.Tests

module ListSearchTests = 
    open NUnit.Framework
    open FsUnit
    open Homework.ListSearch
    
    [<Test>]
    let ``tryFind should return None on empty list`` () =
        tryFind [] 3 |> should equal None
    
    [<Test>]
    let ``tryFind should return Some if list contains number`` () =
        tryFind [3; 5; 3; 1; 5; 123; 543; 42] 1 |> should not' (equal None)
    
    [<Test>]
    let ``tryFind should return None if list does not contain number`` () =
        tryFind [5; 3; 1; 6; 7; 2; 1; 5; 2] 124 |> should equal None
    
    [<Test>]
    let ``tryFind should return first occurrence position if list contains number`` () =
        tryFind [3; 5; 3; 1; 5; 123; 543; 42] 5 |> should equal (Some 1)
    
    [<Test>]
    let ``tryFind should work right if number is the last list item`` () =
        tryFind [5; 1; 6; 123; 342; 65; 123; 6; 2; 1408] 1408 |> should equal (Some 9)
    
    [<Test>]
    let ``tryFind should work right if number is the first list item`` () =
        tryFind [5; 1; 6; 123; 342; 65; 123; 6; 2; 1408] 5 |> should equal (Some 0)
