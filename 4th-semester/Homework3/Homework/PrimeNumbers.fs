namespace Homework

/// Module for implementing infinite sequence of prime numbers
module PrimeNumbers =
    /// Checks whether number is prime
    let isPrime n =
            let sqrt' = n |> double |> sqrt |> int
            n > 1 && {2 .. sqrt'} |> Seq.forall (fun m -> n % m <> 0)
    
    /// Infinite prime number sequence
    let primes = Seq.initInfinite int |> Seq.filter isPrime