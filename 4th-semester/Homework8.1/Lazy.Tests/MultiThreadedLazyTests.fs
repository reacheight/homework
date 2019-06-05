namespace Lazy.Tests

open System.Threading
open NUnit.Framework
open FsUnit
open Lazy

[<TestFixture>]
type ``multi threaded lazy should``() =
    let supplier = fun () -> "result"
    let mutable lazyObject = LazyFactory.CreateMultiThreadedLazy (supplier)
    
    let workOnMultipleThreads (supplier : unit -> 'a) =
        let results = ResizeArray<'a>(10)
        let threads = List.init results.Count (fun i -> Thread(fun () -> results.[i] <- supplier()))
        threads |> List.iter (fun thread -> thread.Start())
        threads |> List.iter (fun thread -> thread.Join())
        
        results |> Seq.toList
        
    
    [<SetUp>]
    member this.SetUp() =
        lazyObject <- LazyFactory.CreateMultiThreadedLazy (supplier)
        
    [<Test>]
    member this.``get result on first call`` () =
        lazyObject.Get() |> should equal (supplier ())
        
    [<Test>]
    member this.``get result on multiple calls`` () =
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
        
    [<Test>]
    member this.``get result in multiple threads`` () =
        let results = workOnMultipleThreads supplier
        results |> List.forall (fun result -> result = supplier()) |> should be True