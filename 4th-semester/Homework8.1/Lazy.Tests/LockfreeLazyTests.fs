namespace Lazy.Tests

open System
open System.Threading
open NUnit.Framework
open FsUnit
open Lazy

[<TestFixture>]
type ``lock-free threaded lazy should``() =
    let supplier = fun () -> "result"
    let mutable lazyObject = LazyFactory.CreateLockfreeLazy supplier
    
    let workOnMultipleThreads (supplier : unit -> 'a) =
        let newLazyObject = LazyFactory.CreateLockfreeLazy supplier
        let results = Array.zeroCreate 10
        let threads = List.init results.Length (fun i -> Thread(fun () -> results.[i] <- newLazyObject.Get()))
        threads |> List.iter (fun thread -> thread.Start())
        threads |> List.iter (fun thread -> thread.Join())
        
        results
        
    [<SetUp>]
    member this.SetUp() =
        lazyObject <- LazyFactory.CreateLockfreeLazy (supplier)
        
    [<Test>]
    member this.``get correct result on first call`` () =
        lazyObject.Get() |> should equal (supplier ())
        
    [<Test>]
    member this.``get correct result on multiple calls`` () =
        for i in 1..10 do
            lazyObject.Get() |> should equal (supplier ())
        
    [<Test>]
    member this.``get correct result in multiple threads`` () =
        let results = workOnMultipleThreads supplier
        results |> Array.forall (fun result -> result = supplier()) |> should be True
        
    [<Test>]
    member this.``get the same object on every call from multiple threads`` () =
        let results = workOnMultipleThreads (fun () -> Object())
        results |> Array.forall (fun result -> result.Equals(results.[0])) |> should be True
