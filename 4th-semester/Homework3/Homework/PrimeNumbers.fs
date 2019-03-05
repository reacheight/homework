namespace Homework

module PrimeNumbers =
    let isPrime n =
            let sqrt' = n |> double |> sqrt |> int
            n > 1 && {2 .. sqrt'} |> Seq.forall (fun m -> n % m <> 0)
            
    let primes = Seq.initInfinite int |> Seq.filter isPrime