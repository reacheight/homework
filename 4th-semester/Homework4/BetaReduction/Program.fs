open System
open BetaReduction

[<EntryPoint>]
let main argv =
    betaReduction (Application(Abstraction('x', Variable 'y'), Application(Abstraction('x', Application(Variable 'x', Variable 'x')), Abstraction('x', Application(Variable 'x', Variable 'x'))))) |> printfn "%A"
    0