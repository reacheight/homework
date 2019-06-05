namespace Lazy

/// Lazy object factory
type LazyFactory() =
    /// Creates single threaded lazy object
    static member CreateSingleThreadedLazy<'a>(supplier: unit -> 'a) =
        SingleThreadedLazy supplier :> ILazy<'a>
        
    static member CreateMultiThreadedLazy<'a>(supplier: unit -> 'a) =
        MultiThreadedLazy supplier :> ILazy<'a>
        
    static member CreateLockfreeLazy<'a>(supplier: unit -> 'a) =
        LockfreeLazy supplier :> ILazy<'a>