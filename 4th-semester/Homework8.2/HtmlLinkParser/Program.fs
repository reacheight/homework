open FSharp.Collections.ParallelSeq
open HtmlAgilityPack
open System

let getAllHttpUrlsFromUrlHtml (url : string) =
    let htmlParser = HtmlWeb()
    let htmlDoc = htmlParser.Load(url)
    htmlDoc.DocumentNode.SelectNodes("//a")
        |> Seq.map (fun node -> node.Attributes.["href"].Value)
        |> Seq.filter (fun url -> url.StartsWith("http://"))
        |> Seq.toList

let getUrlHtmlSize (url : string) =
    HtmlWeb().Load(url).Text.Length
  
let isUrl (candidate : string) =
    let mutable uri = Uri("http://empty.com")
    Uri.TryCreate(candidate, UriKind.Absolute,  &uri)
        && (uri.Scheme = Uri.UriSchemeHttp || uri.Scheme = Uri.UriSchemeHttps);
   
let printAllUrlsFromUrlHtml (startUrl : string) =
    let containedUrls = startUrl |> getAllHttpUrlsFromUrlHtml
    let urlSizePairs = containedUrls
                       |> PSeq.map (fun url -> url, getUrlHtmlSize url)
                       |> PSeq.toList
    
    Console.WriteLine("All urls from " + startUrl + ":")
    urlSizePairs |> Seq.iter (fun (url, size) -> printfn "%s --- %i" url size)

[<EntryPoint>]
let main argv =
    Console.WriteLine("Enter URL address: ")
    let startUrl = Console.ReadLine();
    
    if startUrl |> isUrl
        then printAllUrlsFromUrlHtml startUrl
        else printfn "Wrong URL address"
    
    0