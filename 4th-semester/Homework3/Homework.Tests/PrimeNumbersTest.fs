namespace Homework3.Tests

module PrimeNumbersTest =
    open NUnit.Framework
    open FsUnit
    open Homework.PrimeNumbers
    
    [<Test>]
    let ``isPrime should return false on zero`` () =
        isPrime 0 |> should be False
        
    [<Test>]
    let ``isPrime should return false on one`` () =
        isPrime 1 |> should be False
    
    [<Test>]
    let ``isPrime should return true on two`` () =
        isPrime 2 |> should be True
    
    [<Test>]
    let ``isPrime should return true on prime number`` () =
        isPrime 29 |> should be True
     
    [<Test>]
    let ``isPrime should return false on not prime number`` () =
        isPrime 33 |> should be False
    
    [<Test>]
    let ``isPrime should return false on square of prime`` () =
        isPrime 49 |> should be False
    
    [<Test>]
    let ``any primes item should be prime`` () =
        primes |> Seq.take 100 |> Seq.forall isPrime |> should be True
    
    [<Test>]
    let ``first 10 primes items should be correct`` () =
        primes |> Seq.take 10 |> should equal [2; 3; 5; 7; 11; 13; 17; 19; 23; 29]