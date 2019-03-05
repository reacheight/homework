namespace Homework

/// Module for implementing functions that count amount of even numbers in a list
module EvenCount =
    /// Counts amount of even numbers in a list using Seq.filter
    let evenCountFirst list =
        list |> Seq.filter (fun x -> (abs x) % 2 = 0) |> Seq.length
    
    /// Counts amount of even numbers in a list using Seq.map
    let evenCountSecond list =
        list |> Seq.map (fun x -> (abs x + 1) % 2) |> Seq.sum
    
    /// Counts amount of even numbers in a list using Seq.fold
    let evenCountThird list =
        list |> Seq.fold (fun acc x -> acc + (abs x + 1) % 2) 0