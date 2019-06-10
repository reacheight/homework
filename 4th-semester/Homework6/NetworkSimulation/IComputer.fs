namespace NetworkSimulation

/// Interface representing computer
type IComputer =
    /// Gets computer id
    abstract member Id : int
    
    /// Gets computer operating system
    abstract member OperatingSystem : IOperatingSystem
    
    /// Gets and sets whether computer is infected
    abstract member Infected : bool with get, set

