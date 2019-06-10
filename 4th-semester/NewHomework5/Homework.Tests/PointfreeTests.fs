namespace Homework.Tests

module PointfreeTests =
    open NUnit.Framework
    open FsCheck
    open Pointfree

    [<Test>]
    let ``original function and point-free function should return the same result`` () =
        Check.QuickThrowOnFailure(fun x l -> (multiplyAll x l) = (multiplyAll'4 x l))