namespace Homework

module MergeSort =
    let rec sort ls =
        let rec merge left right =
            match left, right with 
            | l, [] -> l
            | [], r -> r
            | lHead :: lTail, rHead :: rTail -> if (lHead < rHead)
                                                    then lHead :: (merge lTail right)
                                                    else rHead :: (merge left rTail)
        
        let divide ls =
            let rec divide left right =
                if (List.length left - List.length right <= -1)
                    then divide (left @ [List.head right]) (List.tail right)
                    else left, right
                
            divide [] ls
        
        match List.length ls with 
        | 0 | 1 -> ls
        | _ ->
            let left, right = divide ls
            merge (sort left) (sort right)