namespace NetworkSimulation

type IComputer =
    abstract member Id : int
    abstract member OperatingSystem : IOperatingSysmtem
    abstract member Infected : bool with get, set

