namespace Homework.Tests

module MergeSortTests = 
    open NUnit.Framework
    open FsUnit
    open FsCheck
    open Homework.MergeSort
    
    [<Test>]
    let ``sort should return the same list if it is sorted`` () =
        let sortedList = [1; 6; 7; 12; 43; 56; 71; 1235]
        sort sortedList |> should equal sortedList
    
    [<Test>]
    let ``sort should sort list`` () =
        let ls = [2; 7; -1; 3; 34; 657; 5; 1]
        sort ls |> should equal (List.sort ls)
    
    [<Test>]
    let ``sort should sort random list`` () =
        let sortWorksRight (ls : list<int>) = sort ls = List.sort ls
        Check.QuickThrowOnFailure sortWorksRight