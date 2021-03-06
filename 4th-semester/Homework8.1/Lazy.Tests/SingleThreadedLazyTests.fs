namespace Lazy.Tests

open NUnit.Framework
open FsUnit
open Lazy

[<TestFixture>]
type ``single threaded lazy should``() =
    let supplier = fun () -> "result"
    let mutable lazyObject = LazyFactory.CreateSingleThreadedLazy (supplier)
    
    [<SetUp>]
    member this.SetUp() =
        lazyObject <- LazyFactory.CreateSingleThreadedLazy (supplier)
     
    [<Test>]
    member this.``get correct result on first call`` () =
        lazyObject.Get() |> should equal (supplier ())
        
    [<Test>]
    member this.``get correct result on multiple calls`` () =
        for i in 1..10 do
            lazyObject.Get() |> should equal (supplier ())
        
    [<Test>]
    member this.``call supplier function only once`` () =
        let mutable evaluationCount = 0
        lazyObject <- LazyFactory.CreateSingleThreadedLazy
                          (fun () -> evaluationCount <- evaluationCount + 1
                                     supplier())
        for i in 1..10 do
            lazyObject.Get() |> ignore
        
        evaluationCount |> should equal 1