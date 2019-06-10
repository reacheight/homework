namespace Homework3.Tests

module BinaryTreeMapTests =
    open NUnit.Framework
    open FsUnit
    open Homework.BinaryTreeMap
    open System
    
    [<Test>]
    let ``empty tree should be mapped to empty tree`` () =
        map Tip id |> should equal Tip
    
    [<Test>]
    let ``one value tree should be mapped correctly`` () =
        map (Tree(3, Tip, Tip)) (fun x -> x * x) |> should equal (Tree(9, Tip, Tip))
    
    [<Test>]
    let ``tree should br mapped correctly`` () =
        let tree = Tree(10,
                        Tree(5,
                            Tip,
                            Tree(3, Tip, Tip)),
                        Tree(7,
                            Tree(2, Tip, Tip),
                            Tip))
        let squareTree = Tree(100,
                              Tree(25,
                                   Tip,
                                   Tree(9, Tip, Tip)),
                              Tree(49,
                                   Tree(4, Tip, Tip),
                                   Tip))
        map tree (fun x -> x * x) |> should equal squareTree
                                     