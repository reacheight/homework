namespace NetworkSimulation.Tests

module NetworkSimulationTests =
    open NUnit.Framework
    open FsUnit
    open NetworkSimulation
    open Foq
    
    let invinsibleOperatingSystem = Mock<IOperatingSystem>()
                                        .Setup(fun x -> <@ x.InfectionProbability @>).Returns(0.0)
                                        .Create()
    let infectedOperatingSystem = Mock<IOperatingSystem>()
                                      .Setup(fun x -> <@ x.InfectionProbability @>).Returns(1.0)
                                      .Create()
    
    [<Test>]
    let ``infection should behave as breadth first search with infected operating systems computer`` () =
        let computers = [0 .. 10]
                        |> List.map
                            (fun i -> Computer(i, (if i = 0 then true else false), infectedOperatingSystem) :> IComputer)
        
        let connections = [computers.[0], [computers.[1]; computers.[2]];
                           computers.[1], [computers.[0]; computers.[3]; computers.[4]];
                           computers.[2], [computers.[0]; computers.[5]; computers.[6]];
                           computers.[3], [computers.[1]; computers.[7]; computers.[8]];
                           computers.[4], [computers.[1]; computers.[9]; computers.[10]]]
        
        let network = Network(connections)
        
        for i in [1; 3; 6; 11] do
            network.CurrentState
            |> List.forall (fun (id, infected) -> if id < i then infected else not infected)
            |> should be True
            
            network.MakeTurn()

    [<Test>]
    let ``computers with invinsible operating system should not be infected`` () =
        let computers = [0 .. 10]
                        |> List.map
                            (fun i -> Computer(i, (if i = 0 then true else false), invinsibleOperatingSystem) :> IComputer)
        
        let connections = [computers.[0], [computers.[1]; computers.[2]];
                           computers.[1], [computers.[0]; computers.[3]; computers.[4]];
                           computers.[2], [computers.[0]; computers.[5]; computers.[6]];
                           computers.[3], [computers.[1]; computers.[7]; computers.[8]];
                           computers.[4], [computers.[1]; computers.[9]; computers.[10]]]
        
        let network = Network(connections)
        
        for i = 1 to 100 do
            network.CurrentState
            |> List.forall (fun (id, infected) -> if id < 1 then infected else not infected)
            |> should be True
            
            network.MakeTurn()