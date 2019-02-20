namespace Reversion
open System

module Reversion =
    let rec reverse ls = 
        let rec accReverse ls acc = 
            match List.length ls with 
            | 0 -> acc
            | _ -> accReverse (List.tail ls) (List.head ls :: acc)
           
        accReverse ls []
    
    let ls = [4 .. 17] @ [3 .. 5] @ [153; 1235; -124]
    printfn "%A" (reverse ls)