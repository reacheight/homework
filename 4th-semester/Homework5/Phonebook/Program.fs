// Learn more about F# at http://fsharp.org

open System

let printMenu () =
    printfn "1 - выход"
    printfn "2 - добавить новую запись"
    printfn "3 - найти телефон по имени"
    printfn "4 - найти имя по телефону"
    printfn "5 - вывести всё текущее содержимое справочника"
    printfn "6 - сохранить текущие данные в файл"
    printfn "7 - считать данные из файла"

[<EntryPoint>]
let main argv =
    printMenu ()
    0 // return an integer exit code

