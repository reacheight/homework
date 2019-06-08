namespace Homework

module AccuracyCalculatorWorkflow =
    open System
    
    type AccuracyCalculatorBuilder (accuracy : int) =
        member this.Bind(x : float, f) = f (Math.Round(x, accuracy))
        
        member this.Return(x : float) = Math.Round(x, accuracy)
    
    [<EntryPoint>]
    let main argv =
        let rounding = AccuracyCalculatorBuilder
        let result = rounding 3 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }
        
        printfn "Результат вычислений: %f" result
        
        0
