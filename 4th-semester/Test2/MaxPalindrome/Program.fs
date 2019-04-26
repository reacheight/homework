namespace Test

module MaxPalindrome = 
    open System
    
    let isPalindrome string =
        let reverse = string |> Seq.rev |> Seq.take (String.length string) |> String.Concat
        string = reverse
    
    let cartesianProduct first second =
        first |> List.collect (fun x -> second |> List.map (fun y -> (x, y)))
    
    let findMaxPalindrome =
        let allNumbers = seq { 111 .. 999 } |> Seq.toList
        let allPairs = cartesianProduct allNumbers allNumbers
        allPairs
        |> List.map (fun (x, y) -> (x * y).ToString())
        |> List.filter isPalindrome
        |> List.maxBy Int32.Parse
    
    [<EntryPoint>]
    let main argv =
        printfn "Наибольший палиндром: %s" findMaxPalindrome
        0