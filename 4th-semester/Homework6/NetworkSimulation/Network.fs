namespace NetworkSimulation

open System
open NetworkSimulation
open System.Collections.Generic

/// Represents computer network simulation
type Network(connections : (IComputer * IComputer list) list) =
    /// Refreshes infected computers
    member network.MakeTurn() =
        let willBeInfectedOnNextTurn (computer : IComputer) (connectedComputers : IComputer list) =
            if computer.Infected
            then true
            else
                let random = new Random()
                let infectedConnections = 
                        connectedComputers 
                        |> List.filter (fun connectedComputer -> connectedComputer.Infected) 
                        |> List.length 
                    
                let mutable result = false
             
                for i = 1 to infectedConnections do
                    if random.NextDouble() < computer.OperatingSystem.InfectionProbability
                        then result <- true
                        
                result
                
        let infected = List<IComputer>()
        for computer, connectedComputers in connections do
            if willBeInfectedOnNextTurn computer connectedComputers
                then infected.Add computer
        
        for computer in infected do
            computer.Infected <- true
    
    /// Gets netwrok current state
    member network.CurrentState =
        connections |> List.map (fun (computer, _) -> computer.Id, computer.Infected)