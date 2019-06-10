module Phonebook

open System
open System.IO

type Phonebook = Map<string, string>

let empty = Map.empty

/// Gets whether phonebook contains record with given name
let containsName name (phonebook : Phonebook) =
    phonebook |> Map.containsKey name

/// Adds new record to a phonebook
/// If phonebook contains record with given name, overrides it
let addRecord name phone (phonebook : Phonebook) : Phonebook =
    phonebook |> Map.add name phone
    
/// Finds phone by name
let findPhone name (phonebook : Phonebook) =
    if phonebook |> containsName name
        then Some (phonebook |> Map.find name)
        else None
        
/// Finds all names by phone
let findNames phone (phonebook : Phonebook) =
    let phoneNames = phonebook
                     |> Map.filter (fun key value -> value = phone)
                     |> Map.toList
                     |> List.map (fun (name, phone) -> name)
    if List.isEmpty phoneNames
        then None
        else Some (phoneNames)

/// Saves phonebook data to file
let saveToFile (path : string) (phonebook : Phonebook) =
    use writer = new StreamWriter (path)
    phonebook |> Map.iter (fun key value -> writer.WriteLine(sprintf "%s : %s" key value))
    
/// Loads phonebook data from file
let loadFromFile (path : string) : Phonebook =
    use reader = new StreamReader (path)
    seq { while not reader.EndOfStream do
            yield reader.ReadLine() }
    |> Seq.map (fun line ->
        let words = line.Split [|':'|]
        if words.Length <> 2
            then raise (FormatException("Файл содержит строки в некорректном формате"))
        (words.[0].Trim(), words.[1].Trim()))
    |> Map.ofSeq
