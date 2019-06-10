namespace NetworkSimulation

/// Interface representing operating system
type IOperatingSysmtem =
    /// Gets probability of infection in network
    abstract member InfectionProbability : float

