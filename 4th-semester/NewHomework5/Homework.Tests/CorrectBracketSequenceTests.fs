namespace Homework5.Tests

module CorrectBracketSequenceTests =
    open System
    open NUnit.Framework
    open CorrectBracketSequence.Utils
    open FsUnit
    open Microsoft.VisualStudio.TestPlatform.ObjectModel

    [<Test>]
    let ``extractBracketSequence should extract sequence from string with opening brackets only`` () =
        extractBracketSequence "( asdfasfdsa{{{[ asdf(" |> should equal "({{{[("
    
    [<Test>]
    let ``extractBracketSequence should extract sequence from string with closing brackets only`` () =
        extractBracketSequence "sdf )] sdf }]) sdf ) ]   }" |> should equal ")]}]))]}"
    
    [<Test>]
    let ``extractBracketSequence should return emprt string on string without brackets`` () =
        extractBracketSequence "dsfs fsdf1231 sdfsdf" |> should equal String.Empty
    
    [<Test>]
    let ``extractBracketSequence should extract sequence from string with brackets of one kind`` ()=
        extractBracketSequence "(s sdfsd ( )) sdf () dsfs (()" |> should equal "(())()(()"
    
    [<Test>]
    let ``extractBracketSequence should extract sequence from string with brackets of three kinds`` () =
        extractBracketSequence "{(dsf )) }][ (" |> should equal "{())}][("
        
    [<TestCase('(')>]
    [<TestCase('[')>]
    [<TestCase('{')>]
    let ``isOpeningBracket should be true on opening brackets`` c =
        isOpeningBracket c |> should be True
        
    [<TestCase(')')>]
    [<TestCase(']')>]
    [<TestCase('}')>]
    let ``isOpeningBracket should be false on closing brackets`` c =
        isOpeningBracket c |> should be False
    
    [<Test>]
    let ``isOpeningBracket should be false on non-bracket character`` () =
        isOpeningBracket 'd' |> should be False
    
    [<TestCase('(')>]
    [<TestCase('[')>]
    [<TestCase('{')>]
    let ``isClosingBracket should be false on opening brackets`` c =
        isClosingBracket c |> should be False
        
    [<TestCase(')')>]
    [<TestCase(']')>]
    [<TestCase('}')>]
    let ``isClosingBracket should be true on closing brackets`` c =
        isClosingBracket c |> should be True
    
    [<Test>]
    let ``isClosingBracket should be false on non-bracket character`` () =
        isOpeningBracket '6' |> should be False
        
    [<TestCase('(', ')')>]
    [<TestCase('[', ']')>]
    [<TestCase('{', '}')>]
    [<TestCase(')', '(', TestName = "result should not depend on order")>]
    let ``areSameKindBrackets should be true on the same kind brackets`` first second =
        areSameKindBrackets first second |> should be True
    
    [<TestCase('(', ']', TestName = "on different kind brackets")>]
    [<TestCase('(', '(', TestName = "on two opening brackets of the same kind")>]
    let ``areSameKindBrackets should be false`` first second =
        areSameKindBrackets first second |> should be False
    
    [<TestCase("((()())())", TestName = "on correct sequence of one kind brackets")>]
    [<TestCase("([({}[])])", TestName = "on correct sequence of three kind brackets")>]
    let ``isCorrectBracketSequence should be true`` string =
        string |> Seq.toList |> isCorrectBracketSequence |> should be True
    
    [<TestCase("(()()()(())", TestName = "on wrong sequence of one kind brackets")>]
    [<TestCase("[]{}()([})[]}{", TestName = "on wrong sequence of three kind brackets")>]
    [<TestCase("(]", TestName = "on sequence with pair of opening and closing brackets of different kinds")>]
    [<TestCase(")[]()", TestName = "on sequence with first closing bracket")>]
    let ``isCorrectBracketSequence should be false`` string =
        string |> Seq.toList |> isCorrectBracketSequence |> should be False