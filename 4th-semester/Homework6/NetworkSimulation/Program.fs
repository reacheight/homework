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
                                                       ) :> IComputer
                    )
                    
    let connections = [(computers.[0], [computers.[1]; computers.[3]]);
                       (computers.[1], [computers.[0]; computers.[3]; computers.[6]]);
                       (computers.[2], [computers.[4]]);
                       (computers.[3], [computers.[0]]);
                       (computers.[4], [computers.[2]]);
                       (computers.[5], []);
                       (computers.[6], [computers.[1]])]
    
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
