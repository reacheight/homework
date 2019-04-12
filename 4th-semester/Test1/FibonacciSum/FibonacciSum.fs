namespace FibonacciSum

/// Module for implementing fibonacci sequence sum
module FibonacciSum =
    /// Sum of even fibonacci sequence items that less then one million
    let fibonacciSum =
        let rec fibonacciSum prev current sum =
            match current with
            | number when number <= 1000000 -> let addition = if number % 2 = 0 then number else 0
                                               fibonacciSum current (prev + current) (sum + addition)
            | _ -> sum
        
        fibonacciSum 0 1 0