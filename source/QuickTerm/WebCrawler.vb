Imports System.IO
Imports System.Net
Imports System.Console
Imports System.Text.RegularExpressions

Public Class WebCrawler
    Dim Website3 As String
    Public Website As String
    Dim Depth As Integer
    Public Ver As String = "1.1"
    Public log As Boolean = False
    Public links As Integer = 0
    Public Agent As String = "qt"
    Public Delay As Integer = "500"
    Public nonThreaded As Boolean = False
    Public DoingWork As Boolean = False

    Public Sub CrawlHelp()
        WriteLine("========== Help Page ==========")
        WriteLine()
        WriteLine("Syntax:")
        WriteLine("Normal: crawl <Website> <Depth>")
        WriteLine("With Logs: crawl <cmd>")
        WriteLine()
        WriteLine("<Website>    has to be: a valid website on port 80")
        WriteLine("<Depth>      how deep to go in levels of links")
        WriteLine("<cmd>        can be: log list clear email url help none googlebot default qt custom")
        WriteLine()
        WriteLine("<Website>    A valid website on port 80")
        WriteLine("<Switch>     Special conditions in crawl")
        WriteLine("<Depth>      how deep to go in levels of links as integer")
        WriteLine("<cmd>        Special commands in crawl")
        WriteLine()
        WriteLine("========== Description ==========")
        WriteLine()
        WriteLine("Qt-Spider " & Ver)
        WriteLine("Created for searching the web for links and emails or anything")
        WriteLine("else of interest. Original coding designed by a man named Kobe (see credits)")
        WriteLine("and it is improved by me (see dev).")
        Main()
    End Sub

    Public Sub Start(ByVal Website2 As String, Optional Depth2 As Integer = 0, Optional ByVal ProxyServer As String = Nothing)
        Try
            Website3 = Website2
            Website = Website2.Remove(0, 7)
            If Depth2 >= 2 Then
                Console.ForegroundColor = ConsoleColor.Yellow
                Console.WriteLine("Warning: A depth > or = 2 will take a long time to complete!")
                Console.ForegroundColor = ConsoleColor.White
            End If
            Depth = Depth2
            'WriteLine("Clearing old search results...")
            'lstUrls.Clear()
            'lstEmails.Clear()
            If nonThreaded = True Then
                Console.WriteLine("Searching '" & Website & "'...")
                links = 0
                Start2()
            End If
            Dim workthread As New Threading.Thread(AddressOf Start2)
            workthread.Start()
            Console.WriteLine("Searching '" & Website & "'...")
            links = 0
        Catch
            Err.Clear()
        End Try
    End Sub

    Private Sub Start2()
        DoingWork = True
        Dim i As Integer = 0
        TRunning += 1
        'url must be in this format: http://www.example.com/
        'lstUrls.Clear()
        Dim aList As ArrayList = Spider(Website3, Depth)
        Try
            'If log = True Then
            'File.WriteAllText("Spider.log", Nothing)
            'End If
a:
            Do Until i >= aList.Count
                For Each url As String In aList
                    If aList.Item(i).ToString.StartsWith("mailto:") = True Then
                        lstEmails.Add(aList.Item(i).ToString.Remove(0, 7))
                        i += 1
                        GoTo a
                    ElseIf aList.Item(i).ToString.StartsWith("javascript:") = True Then
                        i += 1
                        GoTo a
                    End If
                    If log = True Then
                        File.AppendAllText("Spider.log", url & vbNewLine)
                    Else
                        lstUrls.Add(url)
                    End If
                    If nonThreaded = True Then
                        WriteLine(lstUrls(i))
                    End If
                    i += 1
                Next
            Loop
        Catch
            i += 1
            GoTo a
        End Try
        If nonThreaded = True Then
            WriteLine()
            WriteLine("Done.")
            TRunning -= 1
            Exit Sub
        End If
        If Scripting = True Then
            DoingWork = False
        Else
            MsgBox("Crawl Completed! Type either 'crawl list' or 'crawl email' to see the results!", MsgBoxStyle.Information)
            DoingWork = False
        End If
        TRunning -= 1
    End Sub

    Private Function Spider(ByVal url As String, ByVal depth As Integer) As ArrayList
        'aReturn is used to hold the list of urls
        Dim aReturn As New ArrayList
        'aStart is used to hold the new urls to be checked
        Dim aStart As ArrayList = GrabUrls(url)
        'temp array to hold data being passed to new arrays
        Dim aTemp As ArrayList
        'aNew is used to hold new urls before being passed to aStart
        Dim aNew As New ArrayList
        'add the first batch of urls
        aReturn.AddRange(aStart)
        'if depth is 0 then only return 1 page
        If depth < 1 Then Return aReturn
        'loops through the levels of urls
        For i = 1 To depth
            'grabs the urls from each url in aStart
            For Each tUrl As String In aStart
                'grabs the urls and returns non-duplicates
                aTemp = GrabUrls(tUrl, aReturn, aNew)
                'add the urls to be check to aNew
                aNew.AddRange(aTemp)
            Next
            'swap urls to aStart to be checked
            aStart = aNew
            'add the urls to the main list
            aReturn.AddRange(aNew)
            'clear the temp array
            aNew = New ArrayList
        Next
        Return aReturn
    End Function
    Private Overloads Function GrabUrls(ByVal url As String) As ArrayList
        Dim strSource As String
        'will hold the urls to be returned
        Dim aReturn As New ArrayList
        Try
            'regex string used: thanks google
            Dim strRegex As String = "<a.*?href=""(.*?)"".*?>(.*?)</a>"
            'Dim strRegex2 As String = "*.html*"
            'i used a webclient to get the source
            'web requests might be faster
            Dim wc As New WebClient
            If Agent = "qt" Then
                wc.Headers(HttpRequestHeader.UserAgent) = "Mozilla/5.0 (compatible; Qt-Spider/" & Ver & "; +http://www.quitetiny.com/bot.html)"
                wc.Headers(HttpRequestHeader.Accept) = "*/*"
            ElseIf Agent = "googlebot" Then
                wc.Headers(HttpRequestHeader.UserAgent) = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)"
                wc.Headers(HttpRequestHeader.Accept) = "*/*"
            ElseIf Agent = "none" Then
                wc.Headers(HttpRequestHeader.UserAgent) = Nothing
            Else 'Custom
                wc.Headers(HttpRequestHeader.UserAgent) = Agent
                wc.Headers(HttpRequestHeader.Accept) = "*/*"
            End If
            Threading.Thread.Sleep(Delay)
            'put the source into a string
            strSource = wc.DownloadString(url)
            Dim HrefRegex As New Regex(strRegex, RegexOptions.IgnoreCase Or RegexOptions.Compiled)
            'Dim HrefRegex2 As New Regex(strRegex2, RegexOptions.IgnoreCase Or RegexOptions.Compiled)
            'parse the urls from the source
            Dim HrefMatch As Match = HrefRegex.Match(strSource)
            'used later to get the base domain without subdirectories or pages
            Dim BaseUrl As New Uri(url)
            'while there are urls
            While HrefMatch.Success = True
                'loop through the matches
                Dim sUrl As String = HrefMatch.Groups(1).Value
                'if it's a page or sub directory with no base url (domain)
                If Not sUrl.Contains("http://") AndAlso Not sUrl.Contains("www") Then
                    'add the domain plus the page
                    Dim tURi As New Uri(BaseUrl, sUrl)
                    sUrl = tURi.ToString
                End If
                'if it's not already in the list then add it
                If Not aReturn.Contains(sUrl) Then aReturn.Add(sUrl)
                'go to the next url
                HrefMatch = HrefMatch.NextMatch
            End While
        Catch ex As Exception
            If ex.Message.Contains("The remote name could not be resolved: ") = True Then
                aReturn.Add("(!) Webserver Not Found [" & url & "]")
            Else
                aReturn.Add("(!) " & ex.Message & " [" & url & "]")
            End If
        End Try
        Return aReturn
        links += 1
    End Function
    Private Overloads Function GrabUrls(ByVal url As String, ByRef aReturn As ArrayList, ByRef aNew As ArrayList) As ArrayList
        'overloads function to check duplicates in aNew and aReturn
        'temp url arraylist
        Dim tUrls As ArrayList = GrabUrls(url)
        'used to return the list
        Dim tReturn As New ArrayList
        'check each item to see if it exists, so not to grab the urls again
        For Each item As String In tUrls
            If Not aReturn.Contains(item) AndAlso Not aNew.Contains(item) Then
                tReturn.Add(item)
            End If
        Next
        Return tReturn
    End Function
End Class
