namespace Lazy.Tests

open NUnit.Framework
open FsUnit
open System
open Lazy

[<TestFixture>]
type ``single threaded lazy should``() =
    let supplier = fun () -> "result"
    let mutable lazyObject = LazyFactory.CreateSingleThreadedLazy (supplier)
    
    [<SetUp>]
    member this.SetUp() =
        lazyObject <- LazyFactory.CreateSingleThreadedLazy (fun () -> "result")
     
    [<Test>]
    member this.``get result on first call`` () =
        lazyObject.Get() |> should equal (supplier ())
        
    [<Test>]
    member this.``get result on multiple calls`` () =
        lazyObject.Get() |> should equal (supplier ())
        lazyObject.Get() |> should equal (supplier ())
        lazyObject.Get() |> should equal (supplier ())
        
    [<Test>]
    member this.``evaluate supplier function only once`` () =
        let mutable evaluationCount = 0
        lazyObject <- LazyFactory.CreateSingleThreadedLazy
                          (fun () -> evaluationCount <- evaluationCount + 1
                                     supplier())
        lazyObject.Get() |> ignore
        lazyObject.Get() |> ignore
        lazyObject.Get() |> ignore
        
        evaluationCount |> should equal 1