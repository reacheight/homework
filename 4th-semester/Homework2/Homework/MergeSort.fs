namespace Homework

module MergeSort =
    let rec sort ls =
        let merge left right =
            let rec accMerge left right acc =
                match left, right with 
                | l, [] -> acc @ l
                | [], r -> acc @ r
                | lHead :: lTail, rHead :: rTail -> if (lHead < rHead)
                                                        then accMerge lTail right (acc @ [lHead])
                                                        else accMerge left rTail (acc @ [rHead])
            accMerge left right []
        
        let divide ls =
            let rec divide ls left right =
                match ls with
                | [] -> left, right
                | [a] -> (a :: left), right
                | a::b::tail -> divide tail (a :: left) (b :: right)
                
            divide ls [] []
        
        match List.length ls with 
        | 0 | 1 -> ls
        | _ ->
            let left, right = divide ls
            merge (sort left) (sort right)