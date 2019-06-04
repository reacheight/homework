namespace Lazy

/// Lazy object factory
type LazyFactory() =
    /// Creates single threaded lazy object
    static member CreateSingleThreadedLazy<'a>(supplier: unit -> 'a) =
        SingleThreadedLazy supplier :> ILazy<'a>