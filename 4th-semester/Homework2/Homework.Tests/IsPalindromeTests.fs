namespace Homework.Tests

module IsPalindromeTests = 
    open System
    open NUnit.Framework
    open FsUnit
    open Homework.IsPalindrome
        
    [<Test>]
    let ``isPalindrome should return true on empty string`` () = 
        isPalindrome String.Empty |> should be True
    
    [<Test>]
    let ``isPalindrome should return true on one character string`` () =
        isPalindrome "a" |> should be True
    
    [<Test>]
    let ``isPalindrome should return true on palindrome`` () =
        isPalindrome "abracarba" |> should be True
        
    [<Test>]
    let ``isPalindrome should return false on not-palindrome`` () =
        isPalindrome "string" |> should be False