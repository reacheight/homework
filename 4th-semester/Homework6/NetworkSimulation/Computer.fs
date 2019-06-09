namespace NetworkSimulation

type Computer(id, infected, operatingSystem : IOperatingSysmtem) =
    let mutable infected = infected
    
    interface IComputer with
        member this.Id = id
        
        member this.Infected
            with get() = infected
            and set(value) = infected <- value
        
        member this.OperatingSystem = operatingSystem

