namespace CorrectBracketSequence

/// Module for stack implementation
module Stack =
    /// Stack implementation
    type Stack<'a> =
        | Null
        | Stack of 'a * Stack<'a>
        with
            /// Gets whether stack is empty
            member stack.IsEmpty =
                match stack with
                | Null -> true
                | _ -> false
            
            /// Creates new stack with given value on top
            member stack.Push value =
                Stack(value, stack)
            
            /// Gets stack top
            member stack.Top =
                match stack with
                | Null -> invalidOp "The stack is empty."
                | Stack(value, _) -> value
            
            /// Creates new stack with stack top removed
            member stack.Pop =
                match stack with
                | Null -> invalidOp "The stack is empty."
                | Stack(value, tail) -> tail