namespace Homework5.Tests

module CorrectBracketSequenceTests =
    open System
    open NUnit.Framework
    open CorrectBracketSequence.Utils
    open FsUnit

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
