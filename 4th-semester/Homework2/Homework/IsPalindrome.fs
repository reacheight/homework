namespace Homework

module IsPalindrome =
    open System.Linq

    let reverse ls = 
        let rec accReverse ls acc = 
            match ls with
            | [] -> acc
            | head :: tail -> accReverse tail (head :: acc)
           
        accReverse ls []
    
    let rec areEqual first second =
        if List.length first <> List.length second
            then
                false
            else  
                match first, second with 
                | [], [] -> true
                | fHead :: fTail, sHead :: sTail when fHead = sHead -> areEqual fTail sTail
                | _ -> false
            
    let isPalindrome string =
        let ls = Seq.toList string
        areEqual ls (reverse ls)