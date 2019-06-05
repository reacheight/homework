namespace Lazy.Tests

open NUnit.Framework
open FsUnit
open Lazy

[<TestFixture>]
type ``multi threaded lazy should``() =
    let supplier = fun () -> "result"
    let mutable lazyObject = LazyFactory.CreateMultiThreadedLazy (supplier)
    
    [<SetUp>]
    member this.SetUp() =
        lazyObject <- LazyFactory.CreateMultiThreadedLazy (fun () -> "result")