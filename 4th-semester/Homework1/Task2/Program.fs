open System

let rec fibonacci n = 
    let rec accFibonacci n cur prev = 
        match n with 
        | 0 | 1 -> cur
        | _ -> accFibonacci (n - 1) (cur + prev) cur
       
    if n < 0
        then raise (ArgumentOutOfRangeException())
        else accFibonacci n 1 1
        
Console.WriteLine("Enter an integer: ")
let input : int = Console.ReadLine() |> int
Console.WriteLine(if input < 0
    then "You entered a negative integer"
    else "Fibonacci: " + (fibonacci input |> string))