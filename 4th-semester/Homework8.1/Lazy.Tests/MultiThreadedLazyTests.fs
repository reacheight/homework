namespace Lazy.Tests

open System.Threading
open NUnit.Framework
open FsUnit
open Lazy

[<TestFixture>]
type ``multi threaded lazy should``() =
    let supplier = fun () -> "result"
    let mutable lazyObject = LazyFactory.CreateMultiThreadedLazy supplier
    
    let workOnMultipleThreads (supplier : unit -> 'a) =
        let newLazyObject = LazyFactory.CreateMultiThreadedLazy supplier
        let results = Array.zeroCreate 10
        let threads = List.init results.Length (fun i -> Thread(fun () -> results.[i] <- newLazyObject.Get()))
        threads |> List.iter (fun thread -> thread.Start())
        threads |> List.iter (fun thread -> thread.Join())
        
        results
        
    
    [<SetUp>]
    member this.SetUp() =
        lazyObject <- LazyFactory.CreateMultiThreadedLazy (supplier)
        
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
        
    [<Test>]
    member this.``get correct result in multiple threads`` () =
        let results = workOnMultipleThreads supplier
        results |> Array.forall (fun result -> result = supplier()) |> should be True
        
    [<Test>]
    member this.``call supplier function only once from multiple threads`` () =
        let mutable counter = 0
        workOnMultipleThreads (fun () -> counter <- counter + 1
                                         supplier()) |> ignore
        counter |> should equal 1       
