open System

let printHelp () =
    printfn "Доступные команды: "
    printfn "0 - напечатать список досутпных команд"
    printfn "1 - выход"
    printfn "2 - добавить новую запись"
    printfn "3 - найти телефон по имени"
    printfn "4 - найти имена по телефону"
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
    printfn "%s" <| if Map.isEmpty phonebook
                        then "Справочник пуст."
                        else "Все записи справочника:\n\tимя : номер"
                        
    phonebook |> Map.iter (fun key value -> printfn "\t%s : %s" key value)
    phonebook
    
let printPhoneByName (phonebook : Map<string, string>) =
    printf "Введите имя: "
    let name = Console.ReadLine()
    
    if Map.containsKey name phonebook
        then printfn "Телефон в записи с именем %s: %s" name (Map.find name phonebook)
        else printfn "Не удалось найти запись с именем %s" name
    
    phonebook
    
let printNameByPhone (phonebook : Map<string, string>) =
    printf "Введите номер: "
    let number = Console.ReadLine()
    
    let numberRecords = Map.filter (fun key value -> value = number) phonebook
    if Map.isEmpty numberRecords
        then printfn "Не удалось найти записей с номером %s" number
        else printfn "Имена с номером %s: " number
             numberRecords |> Map.iter (fun key value -> printfn "\t%s" key)
    
    phonebook
    
let rec programLoop (phonebook : Map<string, string>) =
    printf "Введите команду: "
    let input = Console.ReadLine()
    match input with
    | "0" -> printHelp (); programLoop phonebook
    | "1" -> ()
    | "2" -> addPhonebookRecord phonebook |> programLoop
    | "3" -> printPhoneByName phonebook |> programLoop
    | "4" -> printNameByPhone phonebook |> programLoop
    | "5" -> printPhonebook phonebook |> programLoop
    | _ -> printfn "not implemented"; programLoop phonebook

[<EntryPoint>]
let main argv =
    printHelp ()
    programLoop Map.empty
        
    0

