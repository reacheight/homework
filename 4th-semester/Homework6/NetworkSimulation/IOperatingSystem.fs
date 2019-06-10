namespace NetworkSimulation

/// Interface representing operating system
type IOperatingSystem =
    /// Gets probability of infection in network
    abstract member InfectionProbability : float

