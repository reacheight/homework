open System
open System.IO

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
let addPhonebookRecord (phonebook : Map<string, string>) =
    let addOrUpdateRecord name phone =
        if phonebook |> Map.containsKey name
        then printf "Найдена запись с именем %s. Перезаписать её? (1 -- перезаписать, всё остальное -- не переписывать): " name
             let answer = Console.ReadLine()
             match answer with
             | "1" -> phonebook |> Map.add name phone
             | _ -> phonebook
             
        else phonebook |> Map.add name phone
        
    printf "Введите имя: "
    let name = Console.ReadLine()
    printf "Введите номер: "
    let phone = Console.ReadLine()
    
    addOrUpdateRecord name phone

/// Finds and prints phone by name
let printPhoneByName (phonebook : Map<string, string>) =
    printf "Введите имя: "
    let name = Console.ReadLine()
    
    if phonebook |> Map.containsKey name
        then printfn "Телефон в записи с именем %s: %s" name (phonebook |> Map.find name)
        else printfn "Не удалось найти запись с именем %s." name
    
    phonebook

/// Finds and prints all names by phone
let printNameByPhone (phonebook : Map<string, string>) =
    printf "Введите номер: "
    let phone = Console.ReadLine()
    
    let numberRecords = phonebook |> Map.filter (fun key value -> value = phone)
    if Map.isEmpty numberRecords
        then printfn "Не удалось найти записей с номером %s." phone
        else printfn "Имена с номером %s: " phone
             numberRecords |> Map.iter (fun key value -> printfn "\t%s" key)
    
    phonebook

/// Prints all phonebook records
let printPhonebook (phonebook : Map<string, string>) =
    printfn "%s" <| if phonebook |> Map.isEmpty 
                        then "Справочник пуст."
                        else "Все записи справочника:\n\tимя : номер"
                        
    phonebook |> Map.iter (fun key value -> printfn "\t%s : %s" key value)
    phonebook

/// Saves phonebook data to a file
let savePhonebookToFile (phonebook : Map<string, string>) =
    printf "Введите путь до файла: "
    let path = Console.ReadLine()
    try
        use writer = new StreamWriter (path)
        phonebook |> Map.iter (fun key value -> writer.WriteLine(sprintf "%s : %s" key value))
    with
        | :? Exception -> printfn "Не удалось открыть файл."
    
    phonebook

/// Loads phonebook data from a file
let loadPhonebookFromFile (phonebook : Map<string, string>) =
    printf "Введите путь до файла: "
    let path = Console.ReadLine()
    if not(File.Exists(path))
        then printfn "Не удалось найти файл с таким именем."
             phonebook
        else
            use reader = new StreamReader (path)
            seq { while not reader.EndOfStream do
                    yield reader.ReadLine() }
            |> Seq.map (fun line -> 
                let words = line.Split [|':'|]
                (words.[0].Trim(), words.[1].Trim()))
            |> Map.ofSeq
                    
/// Main program loop
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
    | "6" -> savePhonebookToFile phonebook |> programLoop
    | "7" -> loadPhonebookFromFile phonebook |> programLoop
    | _ -> printfn "Команда не найдена."; programLoop phonebook

/// Program entry point
[<EntryPoint>]
let main argv =
    printHelp ()
    programLoop Map.empty
        
    0

