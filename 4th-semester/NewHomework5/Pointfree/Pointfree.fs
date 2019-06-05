module Pointfree

let multiplyAll x l = List.map (fun y -> y * x) l

let multiplyAll'1 x = List.map (fun y -> y * x)

let multiplyAll'2 x = List.map (fun y -> (*) x y)

let multiplyAll'3 x = List.map ((*) x)

let multiplyAll'4 = List.map << (*)