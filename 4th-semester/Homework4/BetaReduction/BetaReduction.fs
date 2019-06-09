module BetaReduction

type Variable = char

type LambdaTerm =
    | Variable of Variable
    | Application of LambdaTerm * LambdaTerm
    | Abstraction of Variable * LambdaTerm

let rec getFreeVariables expression =
    match expression with
    | Variable x -> Set.empty |> Set.add x
    | Application(leftTerm, rightTerm) -> (getFreeVariables leftTerm) |> Set.union (getFreeVariables rightTerm)
    | Abstraction(variable, term) -> getFreeVariables term |> Set.remove variable

let rec substitute variable term expression =
    let alphaConversionSubstitude variable term expression =
        let getNewVariable used =
            let names = Set.ofSeq['a'..'z']
            (used |> Set.difference names) |> Set.maxElement
            
        match expression with
        | Abstraction(abstractionVar, abstractionTerm) ->
            let abstractionTermFreeVariables = abstractionTerm |> getFreeVariables
            let substitutionTermFreeVariables = term |> getFreeVariables
            
            if (substitutionTermFreeVariables |> Set.contains abstractionVar &&
                abstractionTermFreeVariables |> Set.contains variable)
                then
                    let freeName = (abstractionTermFreeVariables |> Set.union substitutionTermFreeVariables)
                                   |> getNewVariable
                    let substituted = abstractionTerm
                                      |> substitute abstractionVar (Variable freeName)
                    (freeName, substituted)
                else
                    (abstractionVar, abstractionTerm)
        | _ -> failwith "only lambda abstraction can be alfa-conversioned"
    
    match expression with
    | Variable x when x = variable -> term
    | Variable x -> Variable x
    | Application(leftTerm, rightTerm) ->
        let leftSubstituted = leftTerm |> substitute variable term
        let rightSubstituted = rightTerm |> substitute variable term
        Application(leftSubstituted, rightSubstituted)
    | Abstraction(var, _) when var = variable -> expression
    | Abstraction _ ->
        let (newVar, newTerm) = expression |> alphaConversionSubstitude variable term
        Abstraction(newVar, newTerm |> substitute variable term)
                
let rec betaReduction expression =
    match expression with
    | Variable x -> Variable x
    | Abstraction(variable, term) -> Abstraction(variable, term |> betaReduction)
    | Application(Abstraction(variable, term), rightTerm) ->
        term |> substitute variable rightTerm |> betaReduction
    | Application(leftTerm, rightTerm) ->
        Application(leftTerm |> betaReduction, rightTerm |> betaReduction)