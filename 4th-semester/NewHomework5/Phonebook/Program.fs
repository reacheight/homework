module Main

open System
open System.IO
open Phonebook

/// Prints command list
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

/// Adds new record to a phonebook
let addPhonebookRecord (phonebook : Phonebook) =
    let addOrUpdateRecord name phone =
        if phonebook |> Phonebook.containsName name
        then printf "Найдена запись с именем %s. Перезаписать её? (1 -- перезаписать, всё остальное -- не переписывать): " name
             let answer = Console.ReadLine()
             match answer with
             | "1" -> phonebook |> Phonebook.addRecord name phone
             | _ -> phonebook
             
        else phonebook |> Phonebook.addRecord name phone
        
    printf "Введите имя: "
    let name = Console.ReadLine()
    printf "Введите номер: "
    let phone = Console.ReadLine()
    
    addOrUpdateRecord name phone

/// Finds and prints phone by name
let printPhoneByName (phonebook : Phonebook) =
    printf "Введите имя: "
    let name = Console.ReadLine()
    
    match phonebook |> Phonebook.findPhone name with
    | Some phone -> printfn "Телефон в записи с именем %s: %s" name phone
    | None -> printfn "Не удалось найти запись с именем %s." name
    
    phonebook

/// Finds and prints all names by phone
let printNameByPhone (phonebook : Phonebook) =
    printf "Введите номер: "
    let phone = Console.ReadLine()
    
    match phonebook |> Phonebook.findNames phone with
    | Some names -> printfn "Имена с номером %s: " phone
                    names |> List.iter (fun name -> printfn "\t%s" name)
    | None -> printfn "Не удалось найти записей с номером %s." phone
    
    phonebook

/// Prints all phonebook records
let printPhonebook (phonebook : Phonebook) =
    printfn "%s" <| if phonebook |> Map.isEmpty 
                        then "Справочник пуст."
                        else "Все записи справочника:\n\tимя : номер"
                        
    phonebook |> Map.iter (fun key value -> printfn "\t%s : %s" key value)
    phonebook

/// Saves phonebook data to a file
let savePhonebookToFile (phonebook : Phonebook) =
    printf "Введите путь до файла: "
    let path = Console.ReadLine()
    try
        phonebook |> Phonebook.saveToFile path
    with
        | _ -> printfn "Не удалось сохранить записи справочника в файл."
    
    phonebook

/// Loads phonebook data from a file
let loadPhonebookFromFile () =
    printf "Введите путь до файла: "
    let path = Console.ReadLine()
    if not(File.Exists(path))
        then printfn "Не удалось найти файл с таким именем."
             Map.empty
        else
            try
                Phonebook.loadFromFile path
            with
                | :? FormatException as ex -> printfn "%s" ex.Message
                                              Map.empty
                | _ -> printfn "Не удалось загрузить данные из файла."
                       Map.empty
                    
/// Main program loop
let rec programLoop (phonebook : Phonebook) =
    printf "Введите команду: "
    let input = Console.ReadLine()
    match input with
    | "0" -> printHelp ()
             phonebook |> programLoop
    | "1" -> ()
    | "2" -> addPhonebookRecord phonebook |> programLoop
    | "3" -> printPhoneByName phonebook |> programLoop
    | "4" -> printNameByPhone phonebook |> programLoop
    | "5" -> printPhonebook phonebook |> programLoop
    | "6" -> savePhonebookToFile phonebook |> programLoop
    | "7" -> loadPhonebookFromFile () |> programLoop
    | _ -> printfn "Команда не найдена."
           phonebook |> programLoop

/// Program entry point
[<EntryPoint>]
let main argv =
    printHelp ()
    programLoop Map.empty
        
    0