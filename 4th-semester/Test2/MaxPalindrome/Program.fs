namespace Test

/// Module for searching for the biggest palindrome that is product of two three-digit numbers
module MaxPalindrome = 
    open System
    
    /// Gets whether string is palindrome
    let isPalindrome string =
        let reverse = string |> Seq.rev |> Seq.take (String.length string) |> String.Concat
        string = reverse
    
    /// Gets cartesian product of two lists
    let cartesianProduct first second =
        first |> List.collect (fun x -> second |> List.map (fun y -> (x, y)))
    
    /// Finds the biggest palindrome that is product of two three-digit numbers
    let findMaxPalindrome =
        let threeDigitNumbers = seq { 111 .. 999 } |> Seq.toList
        let pairs = cartesianProduct threeDigitNumbers threeDigitNumbers
        
        pairs
        |> List.map (fun (x, y) -> (x * y).ToString())
        |> List.filter isPalindrome
        |> List.maxBy Int32.Parse
    
    [<EntryPoint>]
    let main argv =
        printfn "Cамый большой палиндром, полученный произведением двух трёхзначных чисел: %s" findMaxPalindrome
        0