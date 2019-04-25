open System

type StringCalculatorBuilder() =
    member this.Bind(x : string, f) =
        match Double.TryParse(x) with
        | (true, number) -> f number
        | _ -> None
    
    member this.Return(x) = Some x

[<EntryPoint>]
let main argv =
    let stringCalculator = new StringCalculatorBuilder()
    let result = stringCalculator {
        let! x = "20"
        let! y = "7"
        let z = x * y
        return z
    }
    
    match result with
    | None -> printfn "Не удалось выполнить вычисление."
    | Some(number) -> printfn "Результат вычисления: %f" number
    
    0
