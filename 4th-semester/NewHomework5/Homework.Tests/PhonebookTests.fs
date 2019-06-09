namespace Homework.Tests
open System

module PhonebookTests =
    open NUnit.Framework
    open FsUnit
    open System.IO
    
    [<Test>]
    let ``addRecord should return not empty phonebook`` () =
        Phonebook.empty |> Phonebook.addRecord "name" "phone" |> Map.isEmpty |> should be False
    
    [<Test>]
    let ``addRecord should return phonebook with added record`` () =
        Phonebook.empty |> Phonebook.addRecord "name" "phone" |> Map.find "name" |> should equal "phone"
        
    [<Test>]
    let ``addRecord should override record with added record name`` () =
        Phonebook.empty
        |> Phonebook.addRecord "name" "phone1"
        |> Phonebook.addRecord "name" "phone2"
        |> Map.find "name" |> should equal "phone2"
        
    [<Test>]
    let ``containsName should return true on contained record name`` () =
        Phonebook.empty |> Phonebook.addRecord "name" "phone" |> Phonebook.containsName "name" |> should be True
        
    [<Test>]
    let ``containsName should return false on name that is not contained in phonebook`` () =
        Phonebook.empty |> Phonebook.addRecord "name" "phone" |> Phonebook.containsName "not name" |> should be False
    
    [<Test>]
    let ``findPhone should return correct Some option result on contained record name`` () =
        Phonebook.empty
        |> Phonebook.addRecord "name" "phone"
        |> Phonebook.findPhone "name"
        |> should equal (Some "phone")
        
    [<Test>]
    let ``findPhone should return None on name that is not contained in phonebook`` () =
        Phonebook.empty
        |> Phonebook.addRecord "name" "phone"
        |> Phonebook.findPhone "not name"
        |> should equal None
        
    [<Test>]
    let ``findNames should return correct Some option result on single phone record`` () =
        Phonebook.empty
        |> Phonebook.addRecord "name" "phone"
        |> Phonebook.findNames "phone"
        |> should equal (Some ["name"])
        
    [<Test>]
    let ``findNames should return correct Some option result on multiple phone records`` () =
        Phonebook.empty
        |> Phonebook.addRecord "name1" "phone"
        |> Phonebook.addRecord "name2" "phone"
        |> Phonebook.findNames "phone"
        |> should equal (Some ["name1"; "name2"])
        
    [<Test>]
    let ``findNames should return None on phone that is not contained in phonebook`` () =
        Phonebook.empty
        |> Phonebook.addRecord "name" "phone"
        |> Phonebook.findNames "not phone"
        |> should equal None
        
    [<Test>]
    let ``saveToFile should save phonebook to file on correct filename`` () =
        let path = "test_save.txt"
        
        Phonebook.empty
        |> Phonebook.addRecord "name1" "phone1"
        |> Phonebook.addRecord "name2" "phone2"
        |> Phonebook.saveToFile path
        
        use reader = new StreamReader (path)
        reader.ReadToEnd () |> should equal "name1 : phone1\nname2 : phone2\n"
    
    [<Test>]
    let ``loadFromFile should load phonebook from correct file`` () =
        let path = "test_load.txt"
        let phonebook = Phonebook.empty
                        |> Phonebook.addRecord "name1" "phone2"
                        |> Phonebook.addRecord "name2" "phone2"
                        
        use writer = new StreamWriter (path)
        phonebook |> Map.iter (fun key value -> writer.WriteLine(sprintf "%s : %s" key value))
        writer.Close ()
        
        Phonebook.loadFromFile path |> should equal phonebook
        
    [<Test>]
    let ``loadFromFile should throw FormatException on incorrect format file`` () =
        let path = "test_load.txt"
                        
        use writer = new StreamWriter (path)
        writer.WriteLine("name : phone")
        writer.WriteLine("name1 - phone2")
        writer.Close ()
        
        (fun () -> Phonebook.loadFromFile path |> ignore) |> should throw typeof<FormatException>
