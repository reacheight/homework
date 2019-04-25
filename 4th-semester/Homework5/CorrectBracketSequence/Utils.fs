namespace CorrectBracketSequence

module Utils =
    open Stack
    
    let openingBrackets = ['('; '['; '{']
    let closingBrackets = [')'; '}'; ']']
    let isOpeningBracket c = openingBrackets |> List.contains c
    let isClosingBracket c = closingBrackets |> List.contains c
    
    let areSameKindBrackets first second =
        let areSameKindBrackets first second =
            first = '(' && second = ')' ||
            first = '[' &&  second = ']' ||
            first = '{' && second = '}'
         
        areSameKindBrackets first second || areSameKindBrackets second first
    
    let extractBracketSequence string =
        string |> Seq.filter (fun c -> isOpeningBracket c || isClosingBracket c)
    
    let isCorrectBracketSequence sequence =
        let rec isCorrectBracketSequence sequence (stack : Stack<char>)=
            match sequence with
            | a :: tail when isOpeningBracket a -> isCorrectBracketSequence tail (stack.Push a)
            | a :: tail when not stack.IsEmpty && areSameKindBrackets a stack.Top -> isCorrectBracketSequence tail stack.Pop
            | [] -> stack.IsEmpty
            | _ -> false
        
        isCorrectBracketSequence sequence Null

