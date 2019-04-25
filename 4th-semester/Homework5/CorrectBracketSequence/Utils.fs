namespace CorrectBracketSequence

/// Utils functions for checking whether bracket sequence is correct
module Utils =
    open Stack
    
    let openingBrackets = ['('; '['; '{']
    let closingBrackets = [')'; '}'; ']']
    
    /// Gets whether character is opening bracket
    let isOpeningBracket c = openingBrackets |> List.contains c
    
    /// Gets whether character is closing bracket
    let isClosingBracket c = closingBrackets |> List.contains c
    
    /// Gets whether given characters are opening and closing brackets of the same kind
    let areSameKindBrackets first second =
        let areSameKindBrackets first second =
            first = '(' && second = ')' ||
            first = '[' &&  second = ']' ||
            first = '{' && second = '}'
         
        areSameKindBrackets first second || areSameKindBrackets second first
    
    /// Gets string that contains only bracket characters
    let extractBracketSequence string =
        string |> Seq.filter (fun c -> isOpeningBracket c || isClosingBracket c)
    
    /// Gets whether list of bracket-characters is correct bracket sequence
    let isCorrectBracketSequence sequence =
        let rec isCorrectBracketSequence sequence (stack : Stack<char>)=
            match sequence with
            | a :: tail when isOpeningBracket a -> isCorrectBracketSequence tail (stack.Push a)
            | a :: tail when not stack.IsEmpty && areSameKindBrackets a stack.Top -> isCorrectBracketSequence tail stack.Pop
            | [] -> stack.IsEmpty
            | _ -> false
        
        isCorrectBracketSequence sequence Null
