namespace Lazy

type LazyFactory() =
    member this.CreateSingleThreadedLazy<'a>(supplier: unit -> 'a) =
        SingleThreadedLazy supplier :> ILazy<'a>