﻿open System
open HtmlLinkParser.HttpParsing

[<EntryPoint>]
let main argv =
    Console.WriteLine("Enter URL address: ")
    let startUrl = Console.ReadLine();
    
    if startUrl |> isUrl
        then printAllUrlsFromUrlHtml startUrl
        else printfn "Wrong URL address"
    
    0