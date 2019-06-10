namespace HtmlParsing.Tests

module HtmlParsingTests =
    open NUnit.Framework
    open FsUnit
    open HtmlLinkParser.HtmlCrawling
    open System.Net
    open System.IO
    
    let url = "http://hwproj.me/courses/31/terms/4"

    
    [<Test>]
    let ``getAllHttpUrlsFromUrlHtml should get all http urls from url html`` () =
        url
        |> getAllHttpUrlsFromUrlHtml
        |> should equal ["http://www.papeeria.com"; "http://fsharpforfunandprofit.com/"; "http://fsharp.org/"]
     
    [<Test>]
    let ``getUrlHtmlSize should get url html size`` () =
        let request = WebRequest.Create(url)
        use response = request.GetResponse()
        use stream = response.GetResponseStream()
        use reader = new StreamReader(stream)
        let html = reader.ReadToEnd()
        
        url
        |> getUrlHtmlSize
        |> Async.RunSynchronously
        |> should equal html.Length
        
    [<TestCase("http://hwproj.me/courses/31/terms/4")>]
    [<TestCase("https://example.com")>]
    [<TestCase("http://iamurl.me")>]
    let ``isUrl should return true on correct url`` url =
        url |> isUrl |> should be True
        
    [<TestCase("htp://wrong.com")>]
    [<TestCase("randomletters")>]
    [<TestCase("http://@@@at.com")>]
    let ``isUrl should return false on invalid url`` url =
        url |> isUrl |> should be False