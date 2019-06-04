namespace Lazy
open System

/// Thread-safe lazy locking implementation
type MultiThreadedLazy<'a>(supplier: unit -> 'a) =
    /// Lock object
    let lockObject = Object
    /// Evaluation result
    let mutable result = None
    
    interface ILazy<'a> with
        /// Gets evaluation result
        /// Supplier function is called only once
        member this.Get() =
            if result.IsNone
                then lock lockObject (fun () -> if result.IsNone
                                                    then result <- Some (supplier ()))
            
            result.Value

