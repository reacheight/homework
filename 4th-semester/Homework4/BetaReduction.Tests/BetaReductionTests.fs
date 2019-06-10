namespace BetaReduction.Tests

module BetaReductionTests =
    open NUnit.Framework
    open FsUnit
    open BetaReduction
    
    [<Test>]
    let ``getFreeVariables should return one-element set on variable`` () =
        getFreeVariables (Variable 'x') |> should equal (Set.ofList ['x'])
        
    [<Test>]
    let ``getFreeVariables on application should return union of terms free variables`` () =
        let application = Application(Variable 'x', Application(Variable 'x', Variable 'y'))
        getFreeVariables application |> should equal (Set.ofList ['x'; 'y'])
        
    [<Test>]
    let ``getFreeVariables on abstraction should return free variables of abst. term without abst. variable`` () =
        let abstraction = Abstraction('x', Application(Abstraction('x', Variable 'x'), Variable 'y'))
        getFreeVariables abstraction |> should equal (Set.ofList ['y'])
        
    [<Test>]
    let ``substitute on variable with the same new variable should return substituted term`` () =
        let term = Application(Variable 'x', Application(Variable 'x', Variable 'y'))
        Variable 'x' |> substitute 'x' term |> should equal term
        
    [<Test>]
    let ``substitute on variable with different new variable should return start variable`` () =
        let term = Application(Variable 'x', Application(Variable 'x', Variable 'y'))
        Variable 'x' |> substitute 'y' term |> should equal (Variable 'x')
        
    [<Test>]
    let ``substitute on application should return application of substituted terms`` () =
        let application = Application(Variable 'x', Application(Variable 'x', Variable 'y'))
        application |> substitute 'x' (Variable 'z')
        |> should equal (Application(Variable 'z', Application(Variable 'z', Variable 'y')))

    [<Test>]
    let ``substitute on abstraction when abstraction variable is the same as substituted variable should return start abstraction`` () =
        let abstraction = Abstraction('x', Application(Abstraction('x', Variable 'x'), Variable 'y'))
        abstraction |> substitute 'x' abstraction |> should equal abstraction
        
    [<Test>]
    let ``substitute on abstraction with different substituted variable should return correct result :)`` () =
        let abstraction = Abstraction('y', Variable 'x')
        abstraction |> substitute 'z' (Variable 'n') |> should equal (Abstraction('y', Variable 'x'))
        abstraction |> substitute 'x' (Application(Abstraction('x', Variable 'x'), Variable 'y'))
        |> should equal (Abstraction('z', Application(Abstraction('x', Variable 'x'), Variable 'y')))
        
    [<Test>]
    let ``betaReduction should reduce single variable correctly`` () =
        Variable 'a' |> betaReduction |> should equal (Variable 'a')
        
    [<Test>]
    let ``betaReduction should reduce ID application correctly`` () =
        let term = Application(Abstraction('x', Variable 'x'), Variable 'y')
        term |> betaReduction |> should equal (Variable 'y')
        
    [<Test>]
    let ``betaReduction should reduce with normal strategy`` () =
        let term = Application(Abstraction('x', Variable 'y'),
                               Application(
                                              Abstraction('x', Application(Variable 'x', Variable 'x')),
                                              Abstraction('x', Application(Variable 'x', Variable 'x'))
                                          ))
        term |> betaReduction |> should equal (Variable 'y')