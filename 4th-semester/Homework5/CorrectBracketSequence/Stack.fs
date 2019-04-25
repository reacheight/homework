namespace CorrectBracketSequence.Stack

module Stack =
    type Stack<'a> =
        | Null
        | Stack of 'a * Stack<'a>
        with
            member stack.IsEmpty =
                match stack with
                | Null -> true
                | _ -> false
            
            member stack.Push value =
                Stack(value, stack)
            
            member stack.Top =
                match stack with
                | Null -> failwith "The stack is empty."
                | Stack(value, _) -> value
            
            member stack.Pop =
                match stack with
                | Null -> failwith "The stack is empty."
                | Stack(value, tail) -> tail