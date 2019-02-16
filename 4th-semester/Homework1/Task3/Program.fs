open System

module Reversion =
    let rec reversion ls = 
        let rec accReversion ls acc = 
            match List.length ls with 
            | 0 -> acc
            | _ -> accReversion (List.tail ls) (List.head ls :: acc)
           
        accReversion ls []
    
    let ls = [4 .. 17] @ [3 .. 5] @ [153; 1235; -124]
    printfn "%A" (reversion ls)