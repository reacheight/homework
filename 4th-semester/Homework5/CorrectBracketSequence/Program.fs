namespace CorrectBracketSequence

/// Program entry point
module EntryPoint =
    open System
    open Utils
    
    [<EntryPoint>]
    let main argv =
        printf "Введите строку, содержащуюю последовательность скобок: "
        let input = Console.ReadLine()
        
        if input |> extractBracketSequence |> Seq.toList |> isCorrectBracketSequence
            then printfn "Последовательность скобок корректна."
            else printfn "Последовательность скобок некорректна."
            
        0 
