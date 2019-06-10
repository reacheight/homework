namespace Homework5.Tests

module StackTests =
    open System
    open NUnit.Framework
    open CorrectBracketSequence.Stack
    open FsUnit
    open FsCheck
    
    [<Test>]
    let ``null stack should be empty`` () =
        let stack = Stack.Null
        stack.IsEmpty |> should be True
    
    [<Test>]
    let ``stack should be not empty after push`` () =
        let stack = Stack.Null.Push 3
        stack.IsEmpty |> should be False
        
    [<Test>]
    let ``stack should be empty after push and pop`` () =
        let stack = (Stack.Null.Push 3).Pop
        stack.IsEmpty |> should be True
    
    [<Test>]
    let ``null stack should fail when pop`` () =
        let stack = Stack.Null
        (fun () -> stack.Pop |> ignore)
            |> should throw typeof<InvalidOperationException>
            
    [<Test>]
    let ``null stack should fail when top`` () =
        let stack = Stack.Null
        (fun () -> stack.Top |> ignore)
            |> should throw typeof<InvalidOperationException>
    
    [<Test>]
    let ``top should return pushed value`` () =
        let TopShouldEqualPushedValue (value : int) = (Stack.Null.Push value).Top = value
        
        Check.QuickThrowOnFailure TopShouldEqualPushedValue
        
    [<Test>]
    let ``pop should remove pushed value`` () =
        let stack = ((Stack.Null.Push 3).Push 5).Push -1
        let PopShouldRemovePushedValue (value : int) = (stack.Push value).Pop = stack
        
        Check.QuickThrowOnFailure PopShouldRemovePushedValue
    
    [<Test>]
    let ``stack should not be changed after top`` () =
        let stack = ((Stack.Null.Push 3).Push 5).Push -1
        let newStack = stack
        
        stack.Top |> ignore
        
        newStack |> should equal stack
        