namespace Test.Tests

module MaxPalindromeTest =
    open NUnit.Framework
    open FsUnit
    open System
    open Test.MaxPalindrome
    
    let maxPalindrome = "906609"
    
    [<Test>]
    let ``findMaxPalindrome should return correct result`` () =
        findMaxPalindrome |> should equal maxPalindrome
    
    [<Test>]
    let ``cartesianProduct should work return correct resul on simple input`` () =
        cartesianProduct [1; 2; 3] [5; 6;] |> should equal [(1, 5); (1, 6); (2, 5); (2, 6); (3, 5); (3, 6)]
    
    [<Test>]
    let ``isPalindrome should return true on empty string`` () = 
        isPalindrome String.Empty |> should be True
    
    [<Test>]
    let ``isPalindrome should return true on one-length string`` () =
        isPalindrome "a" |> should be True
    
    [<Test>]
    let ``isPalindrome should return true on palindrome`` () =
        isPalindrome "abracarba" |> should be True
        
    [<Test>]
    let ``isPalindrome should return false on not palindrome`` () =
        isPalindrome "string" |> should be False
        
    [<Test>]
    let ``isPalindrome should return true on one-character string`` () =
        isPalindrome "bbbbbbbbbb" |> should be True