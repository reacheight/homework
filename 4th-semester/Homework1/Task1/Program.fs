﻿open System

module Factorial = 
    let factorial x = 
        let rec accFactorial n acc = 
            match n with 
            | 0 | 1 -> acc
            | _ -> accFactorial (n - 1) (acc * n)
        
        if x < 0 then
            raise (ArgumentOutOfRangeException())
        else
            accFactorial x 1
        
    Console.WriteLine("Enter an integer: ")
    let input : int = Console.ReadLine() |> int
    Console.WriteLine(if input < 0
        then "You entered a negative integer"
        else "Factorial: " + (input |> factorial |> string))
        
    
    