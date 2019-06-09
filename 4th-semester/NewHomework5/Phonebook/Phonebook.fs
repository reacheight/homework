module Phonebook

open System
open System.IO
open System.Text.RegularExpressions

type Phonebook = Map<string, string>

/// Adds new record to a phonebook
/// If phonebook contains record with given name, overrides it
let addRecord (phonebook : Phonebook) name phone : Phonebook=
    phonebook |> Map.add name phone
    
/// Finds phone by name
let findPhone (phonebook : Phonebook) name =
    if phonebook |> Map.containsKey name
        then Some (phonebook |> Map.find name)
        else None
        
/// Finds all names by phone
let findNames (phonebook : Phonebook) phone =
    let phoneRecords = phonebook |> Map.filter (fun key value -> value = phone)
    if Map.isEmpty phoneRecords
        then None
        else Some (phoneRecords)

/// Gets all phonebook records as list
let getAllRecords (phonebook : Phonebook) =
    phonebook |> Map.toList

/// Saves phonebook data to file
let saveToFile (phonebook : Phonebook) (path : string) =
    use writer = new StreamWriter (path)
    phonebook |> Map.iter (fun key value -> writer.WriteLine(sprintf "%s : %s" key value))
    
/// Loads phonebook data from file
let loadPhonebookFromFile (path : string) : Phonebook =
    use reader = new StreamReader (path)
    seq { while not reader.EndOfStream do
            yield reader.ReadLine() }
    |> Seq.map (fun line ->
        let words = line.Split [|':'|]
        if words.Length <> 2
            then raise (FormatException("File contains string in wrong format."))
        (words.[0].Trim(), words.[1].Trim()))
    |> Map.ofSeq
