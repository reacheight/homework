namespace Lazy

type LazyFactory() =
    static member CreateSingleThreadedLazy<'a>(supplier: unit -> 'a) =
        SingleThreadedLazy supplier :> ILazy<'a>