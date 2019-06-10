open System
open Reversion

let powersOfTwo n m = 
    let rec accPowersOfTwo m i acc_list =
        if i = m
            then acc_list
            else accPowersOfTwo m (i + 1) ((List.head acc_list * 2) :: acc_list)
    
    if n < 0 || m < 0 then
        raise (ArgumentOutOfRangeException())
        
    accPowersOfTwo m 0 [pown 2 n]
    
let n = 2
let m = 5
printfn "%A" (powersOfTwo n m |> Reversion.reverse)