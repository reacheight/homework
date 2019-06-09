namespace NetworkSimulation

open System
open NetworkSimulation
open System.Collections.Generic

type Network(connections : (IComputer * IComputer list) list) =
    member network.MakeTurn() =
        let willBeInfectedOnNextTurn (computer : IComputer) (connectedComputers : IComputer list) =
            if computer.Infected
            then true
            else
                let random = new Random()
                let countOfAttempts = 
                        connectedComputers 
                        |> List.filter (fun connectedComputer -> connectedComputer.Infected) 
                        |> List.length 
                    
                let mutable result = false
             
                for i = 1 to countOfAttempts do
                    if not computer.Infected && random.NextDouble() <= computer.OperatingSystem.InfectionProbability then
                        result <- true
                        
                result
                
        let infected = List<IComputer>()
        for computer, connectedComputers in connections do
            if willBeInfectedOnNextTurn computer connectedComputers
                then infected.Add computer
        
        for computer in infected do
            computer.Infected <- true
    
    member network.CurrentState =
        connections |> List.map (fun (computer, _) -> computer.Id, computer.Infected)