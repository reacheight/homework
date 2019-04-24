// Learn more about F# at http://fsharp.org

open System

let printHelp () =
    printfn "Доступные команды: "
    printfn "0 - напечатать список досутпных команд"
    printfn "1 - выход"
    printfn "2 - добавить новую запись"
    printfn "3 - найти телефон по имени"
    printfn "4 - найти имя по телефону"
    printfn "5 - вывести всё текущее содержимое справочника"
    printfn "6 - сохранить текущие данные в файл"
    printfn "7 - считать данные из файла"
    
let rec programLoop () =
    printf "Введите команду: "
    let input = Console.ReadLine()
    match input with
    | "0" -> printHelp (); programLoop ()
    | "1" -> ()
    | _ -> printfn "not implemented"; programLoop ()

[<EntryPoint>]
let main argv =
    printHelp ()
    
    programLoop ()
        
    0

