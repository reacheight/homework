namespace Test

open System.Collections.Generic
open System.Linq

/// Mutable priority queue implementation
type PriorityQueue<'T>() =
    let mutable queue = new List<'T * int>()
    
    /// Gets whether queue is empty
    member this.IsEmpty
        with get() = queue.Count = 0
    
    /// Enqueues value with weight
    member this.Enqueue(value, weigth) =
        let index = queue.FindIndex(fun (v, w) -> w > weigth)
        match index with
        | -1 -> queue.Add(value, weigth)
        | i -> queue.Insert(i, (value, weigth))
        
    /// Deques value with max weight
    member this.Dequeue() =
        if queue.Count = 0
            then
                failwith (invalidOp "Queue is empty!")
            else
                let result = queue.Last() |> fst
                queue.RemoveAt(queue.Count - 1)
                result