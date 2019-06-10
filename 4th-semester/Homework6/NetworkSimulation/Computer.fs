namespace NetworkSimulation

/// Implements computer
type Computer(id, infected, operatingSystem : IOperatingSysmtem) =
    let mutable infected = infected
    
    interface IComputer with
        /// Gets computer id
        member this.Id = id
        
        /// Gets and sets whether computer is infected
        member this.Infected
            with get() = infected
            and set(value) = infected <- value
        
        /// Gets computer operating system
        member this.OperatingSystem = operatingSystem

