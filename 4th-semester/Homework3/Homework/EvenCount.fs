namespace Homework

module EvenCount =
    let evenCountFirst list =
        list |> Seq.filter (fun x -> (abs x) % 2 = 0) |> Seq.length
    
    let evenCountSecond list =
        list |> Seq.map (fun x -> (abs x + 1) % 2) |> Seq.sum
    
    let evenCountThird list =
        list |> Seq.fold (fun acc x -> acc + (abs x + 1) % 2) 0