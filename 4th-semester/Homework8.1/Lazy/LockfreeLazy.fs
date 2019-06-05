namespace Lazy
open System.Threading

/// Thread-safe lazy lock-free implementation
type LockfreeLazy<'a>(supplier: unit -> 'a) =
    /// Evaluation result
    let mutable result = None
    
    interface ILazy<'a> with
        /// Gets evaluation result
        /// Supplier function is called only once
        member this.Get() =
            while result.IsNone do
                Interlocked.Exchange (ref result, Some (supplier())) |> ignore
            
            result.Value