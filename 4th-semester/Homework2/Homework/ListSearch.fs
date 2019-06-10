namespace Homework

module ListSearch =
    let tryFind ls number = 
        let rec accTryFind ls number i = 
            match ls with 
            | [] -> None
            | head :: tail -> if (head = number)
                                  then Some(i)
                                  else accTryFind tail number (i + 1)
        
        accTryFind ls number 0
