// Learn more about F# at http://fsharp.org

open System

let printHelp () =
    printfn "Доступные команды: "
    printfn "0 - напечатать список досутпных команд"
    printfn "1 - выход"
    printfn "2 - добавить новую запись"
    printfn "3 - найти телефон по имени"
    printfn "4 - найти имя по телефону"
    printfn "5 - напечатать всё текущее содержимое справочника"
    printfn "6 - сохранить текущие данные в файл"
    printfn "7 - считать данные из файла"
    
let addPhonebookRecord (phonebook : Map<string, string>) =
    let addOrUpdateRecord name number =
        if Map.containsKey name phonebook
        then printf "Найдена запись с именем %s. Перезаписать её? (1 -- перезаписать, всё остальное -- не переписывать): " name
             let answer = Console.ReadLine()
             match answer with
             | "1" -> Map.add name number phonebook
             | _ -> phonebook
             
        else Map.add name number phonebook
        
    printf "Введите имя: "
    let name = Console.ReadLine()
    printf "Введите номер: "
    let number = Console.ReadLine()
    
    addOrUpdateRecord name number
    
let printPhonebook (phonebook : Map<string, string>) =
    let rec printPairList ls =
        match ls with
        | [] -> ()
        | (key, value) :: tail -> printfn "\t%s : %s" key value; printPairList tail
    
    printfn "%s" <| if Map.isEmpty phonebook
                        then "Справочник пуст."
                        else "Все записи справочника:\n\tимя : номер"
    printPairList (Map.toList phonebook)
    phonebook
    
let rec programLoop (phonebook : Map<string, string>) =
    printf "Введите команду: "
    let input = Console.ReadLine()
    match input with
    | "0" -> printHelp (); programLoop phonebook
    | "1" -> ()
    | "2" -> addPhonebookRecord phonebook |> programLoop
    | "5" -> printPhonebook phonebook |> programLoop
    | _ -> printfn "not implemented"; programLoop phonebook

[<EntryPoint>]
let main argv =
    printHelp ()
    programLoop Map.empty
        
    0

