module Main

open NetworkSimulation
open System

type Linux() =
    interface IOperatingSystem with
        member this.InfectionProbability = 0.3

type Windows() =
    interface IOperatingSystem with
        member this.InfectionProbability = 0.7

[<EntryPoint>]
let main argv =
    let computers = [0 .. 6]
                    |> List.map (fun i -> Computer(i,
                                                   i % 3 = 0,
                                                   if i % 2 = 0
                                                       then Linux() :> IOperatingSystem
                                                       else Windows() :> IOperatingSystem
                                                       )
                    )
                    
    let connections = [(computers.[0] :> IComputer, [computers.[1] :> IComputer; computers.[3] :> IComputer]);
                       (computers.[1] :> IComputer, [computers.[0] :> IComputer; computers.[3] :> IComputer; computers.[6] :> IComputer]);
                       (computers.[2] :> IComputer, [computers.[4] :> IComputer]);
                       (computers.[3] :> IComputer, [computers.[0] :> IComputer]);
                       (computers.[4] :> IComputer, [computers.[2] :> IComputer]);
                       (computers.[5] :> IComputer, []);
                       (computers.[6] :> IComputer, [computers.[1] :> IComputer])]
    
    let network = Network(connections)
    
    let mutable loop = true
    while loop do
        printfn "Текущее состояние сети:"
        printfn "%A" network.CurrentState
        
        printfn "Введите команду:"
        printfn "0 -- выход, остальное -- сделать ход"
        let answer = Console.ReadLine()
        if answer = "0"
            then loop <- false
            else network.MakeTurn()
    0
