namespace Lazy

/// Lazy evaluation interface
type ILazy<'a> =
    /// Gets result of evaluation
    abstract member Get: unit -> 'a