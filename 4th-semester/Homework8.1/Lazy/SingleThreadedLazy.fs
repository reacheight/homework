namespace Lazy

/// Single threaded lazy implementation
type SingleThreadedLazy<'a>(supplier: unit -> 'a) =
    /// Evaluation result
    let mutable result = None
    
    interface ILazy<'a> with
        /// Gets evaluation result
        /// Supplier function is called only once
        member this.Get() =
            match result with
            | None -> result <- Some (supplier ())
                      result.Value
            | Some value -> value