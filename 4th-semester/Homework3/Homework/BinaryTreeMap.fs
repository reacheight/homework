namespace Homework

/// Module for implementing simple binary tree map function
module BinaryTreeMap =
    /// Represents a binary tree
    type Tree<'a> =
        | Tree of 'a * Tree<'a> * Tree<'a>
        | Tip
    
    /// Applies mapper-function for every tree element
    let rec map tree mapper =
        match tree with 
        | Tip -> Tip
        | Tree(value, left, right) -> Tree(mapper value, map left mapper, map right mapper)