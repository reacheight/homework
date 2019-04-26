namespace Test.Tests

open NUnit.Framework
open FsUnit
open System
open Test

[<TestFixture>]
type ``queue should`` () =
    let mutable queue = PriorityQueue<int>()
    
    [<SetUp>]
    member this.SetUp() =
        queue <- new PriorityQueue<int>()

    [<Test>]
    member this.``be empty after creation`` () =
        queue.IsEmpty |> should be True
     
    [<Test>]
    member this.``not be empty after enqueue call`` () =
        queue.Enqueue(3, 10)
        queue.IsEmpty |> should be False
        
    [<Test>]
    member this.``be empty after queue-dequeue calls`` () =
        queue.Enqueue(3, 5)
        queue.Dequeue() |> ignore
        queue.IsEmpty |> should be True
    
    [<Test>]
    member this.``dequeue enqueued value after first enqueue call`` () =
        queue.Enqueue(213, 5)
        queue.Dequeue() |> should equal 213
    
    [<Test>]
    member this.``dequeue value with max weight`` () =
        queue.Enqueue(6, 9)
        queue.Enqueue(3, 10)
        queue.Enqueue(7, 4)
        
        queue.Dequeue() |> should equal 3
    
    [<Test>]
    member this.``dequeue value with max weight multiple times`` () =
        queue.Enqueue(7, 9)
        queue.Enqueue(3, 10)
        queue.Enqueue(6, 4)
        
        queue.Dequeue() |> should equal 3
        queue.Dequeue() |> should equal 7
        
    [<Test>]
    member this.``fail on dequeue call from empty queue`` () =
        (fun () -> queue.Dequeue() |> ignore)
            |> should throw typeof<InvalidOperationException> 