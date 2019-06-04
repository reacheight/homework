module HtmlLinkParser.HttpParsing

open HtmlAgilityPack
open System
open System.IO
open System.Net

let getAllHttpUrlsFromUrlHtml (url : string) =
    let htmlParser = HtmlWeb()
    let htmlDoc = htmlParser.Load(url)
    htmlDoc.DocumentNode.SelectNodes("//a")
        |> Seq.map (fun node -> node.Attributes.["href"].Value)
        |> Seq.filter (fun url -> url.StartsWith("http://"))
        |> Seq.toList

let getUrlHtmlSize (url : string) =
    async {
        let request = WebRequest.Create(url)
        use! response = request.AsyncGetResponse()
        use stream = response.GetResponseStream()
        use reader = new StreamReader(stream)
        let html = reader.ReadToEnd()
        return html.Length
    }
  
let isUrl (candidate : string) =
    let mutable uri = Uri("http://empty.com")
    Uri.TryCreate(candidate, UriKind.Absolute,  &uri)
        && (uri.Scheme = Uri.UriSchemeHttp || uri.Scheme = Uri.UriSchemeHttps);
   
let printAllUrlsFromUrlHtml (startUrl : string) =
    let containedUrls = startUrl |> getAllHttpUrlsFromUrlHtml
    let urlSizePairs = containedUrls
                       |> Seq.map (fun url -> async {
                           let! length = getUrlHtmlSize url
                           return url, length
                       })
                       |> Async.Parallel
                       |> Async.RunSynchronously
    
    Console.WriteLine("All urls from " + startUrl + ":")
    urlSizePairs |> Seq.iter (fun (url, size) -> printfn "%s --- %i" url size)