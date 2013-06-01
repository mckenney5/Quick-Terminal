Imports System.Net
Imports System.Text
Imports System.Console
Imports System.Net.Sockets
Public Class NetworkClass
    Public CurrentPort As Integer = 0
    Dim senddata As String
    Dim uClient As New TcpClient
    Dim DataStream As NetworkStream
    Dim Packet(1024) As Byte
    Dim Oports As New ArrayList
    Dim Address As String
    Dim Startport As Integer
    Dim Endport As Integer

    Public Sub DnsLookUp(ByVal Server As String)
        Console.WriteLine("Looking up " & Server)
        Dim Hostname As IPHostEntry = Dns.GetHostEntry(Server)
        Dim ip As IPAddress() = Hostname.AddressList
        Dim stat As String
        stat = ip(0).ToString
        Console.WriteLine(stat)
        Console.WriteLine()
        Main()
    End Sub

    Public Sub ReverseDnsLookUp(ByVal Server As IPAddress)
        ForegroundColor = ConsoleColor.Yellow
        WriteLine("Warning this is a beta version")
        ForegroundColor = ConsoleColor.White
        Console.WriteLine("Looking up " & Server.ToString)
        Dim Hostname As IPHostEntry = Dns.GetHostEntry(Server)
        Dim tuff() = Hostname.Aliases()
        Dim stat As String
        stat = Hostname.Aliases(0)
        Console.WriteLine(stat)
        Console.WriteLine()
        Main()
    End Sub

    Public Function Ping_Server(ByVal Server As String)
        Try
            Return My.Computer.Network.Ping(Server)
        Catch
            Er(Err.Number, Err.Description)
            Return "Error"
        End Try
    End Function

    Public Function GetLocalIpAddress() As System.Net.IPAddress
        Dim strHostName As String
        Dim addresses() As System.Net.IPAddress
        strHostName = System.Net.Dns.GetHostName()
        addresses = System.Net.Dns.GetHostAddresses(strHostName)
        ' Find an IpV4 address
        For Each address As System.Net.IPAddress In addresses
            If address.ToString.Contains(".") Then
                Return address
            End If
        Next
        ' No IpV4 address? Return the loopback address.
        Return System.Net.IPAddress.Loopback
    End Function

    Public Sub Scan(ByVal Address1 As String, Optional StartPort1 As Integer = 20, Optional EndPort1 As Integer = 82)
        Address = Address1
        Startport = StartPort1
        Endport = EndPort1
        WriteLine("Scanning started. You will get a message when its done.")
        WriteLine("This scan is slow, so don't hold your breath.")
        Dim t1 As New System.Threading.Thread(AddressOf ScanThread)
        t1.Priority = Threading.ThreadPriority.AboveNormal
        t1.Start()
        Main()
    End Sub
    Private Sub ScanThread()
        TRunning += 1
        Dim byteBuffer = Encoding.ASCII.GetBytes(vbNewLine)
        Dim OpenPorts As Integer = 0
        Dim ClosedPorts As Integer = 0
        CurrentPort = Startport
        Try
a:
            Dim Client As New TcpClient
            Dim Data As NetworkStream
            Dim test(15) As Byte
            Client.Connect(Address, CurrentPort)
            Data = Client.GetStream
            Data.Write(byteBuffer, 0, byteBuffer.Length)
            Oports.Add(CurrentPort)
            OpenPorts = OpenPorts + 1
            Client.Close()
            Data.Close()
            Data.Flush()
            If CurrentPort < Endport Then
                CurrentPort = CurrentPort + 1
                GoTo a
            Else
                System.IO.File.WriteAllText(Address & ".log", "Scanned " & Endport - Startport & " ports. Open: " & OpenPorts & " Closed: " & ClosedPorts)
                System.IO.File.AppendAllText(Address & ".log", vbNewLine & "Open Ports: " & vbNewLine)
                Dim i As Integer = 0
                Do Until i = Oports.Count
                    System.IO.File.AppendAllText(Address & ".log", Oports.Item(i) & vbNewLine)
                    i += 1
                Loop
                MsgBox("Scan of " & Address & " finished!", MsgBoxStyle.Information, "Quick Terminal Scan")
                Dim result1 As Microsoft.VisualBasic.MsgBoxResult = MsgBox("Would you like to view the results?", MsgBoxStyle.YesNo, "Quick Terminal Scan")
                If result1 = MsgBoxResult.Yes Then
                    Process.Start(Address & ".log")
                End If
                CurrentPort = 0
                TRunning -= 1
                Exit Sub
            End If
        Catch
            'If Not Err.Number = 5 Then
            'WriteLine("Error (" & Err.Number & ") " & Err.Description)
            'End If
            If CurrentPort < Endport Then
                ClosedPorts = ClosedPorts + 1
                CurrentPort = CurrentPort + 1
                GoTo a
            Else
                System.IO.File.WriteAllText(Address & ".log", "Scanned " & Endport - Startport & " ports. Open: " & OpenPorts & " Closed: " & ClosedPorts)
                System.IO.File.AppendAllText(Address & ".log", vbNewLine & "Open Ports: " & vbNewLine)
                Dim i As Integer = 0
                Do Until i = Oports.Count
                    System.IO.File.AppendAllText(Address & ".log", Oports.Item(i) & vbNewLine)
                    i += 1
                Loop
                MsgBox("Scan of " & Address & " finished!", MsgBoxStyle.Information, "Quick Terminal Scan")
                Dim result1 As Microsoft.VisualBasic.MsgBoxResult = MsgBox("Would you like to view the results?", MsgBoxStyle.YesNo, "Quick Terminal Scan")
                If result1 = MsgBoxResult.Yes Then
                    Process.Start(Address & ".log")
                End If
                TRunning -= 1
                Exit Sub
            End If
            GoTo a
        End Try
        TRunning -= 1
    End Sub

    Public Sub Connect(ByVal Address As String, ByVal Port As Integer)
        ForegroundColor = ConsoleColor.Yellow
        WriteLine("Warning this is a beta version")
        ForegroundColor = ConsoleColor.White
        WriteLine("Connecting to " & Address & " on port " & Port)
        Dim inpt As String
        Try
            uClient.Connect(Address, Port)
            WriteLine("Connected. To disconnect type ~exit")
            Dim DataThread As Threading.Thread
            DataThread = New Threading.Thread(AddressOf GetData)
            DataThread.Start()
            Do While (True)
                DataStream = uClient.GetStream
                inpt = ReadLine()
                If inpt = "~exit" Then
                    uClient.Close()
                    DataStream.Close()
                    DataStream.Flush()
                    Main()
                ElseIf inpt = "" Then
                    inpt = vbNewLine
                    Dim byteBuffer = Encoding.ASCII.GetBytes(inpt)
                    DataStream.Write(byteBuffer, 0, byteBuffer.Length)
                Else
                    Dim byteBuffer = Encoding.ASCII.GetBytes(inpt)
                    DataStream.Write(byteBuffer, 0, byteBuffer.Length)
                End If
            Loop
        Catch
            Er(Err.Number, Err.Description)
        End Try
    End Sub

    Private Sub GetData()
        TRunning += 1
        'to only be used with Conn()
        If DataStream.CanRead Then
a:
            Dim myCompleteMessage As StringBuilder = New StringBuilder()
            Dim numberOfBytesRead As Integer = 0

            ' Incoming message may be larger than the buffer size. 
            Try
                Do 'fix loop
                    numberOfBytesRead = DataStream.Read(Packet, 0, CInt(uClient.ReceiveBufferSize))
                    myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(Packet, 0, numberOfBytesRead))
                    ' Print out the received message to the console.
                    Console.WriteLine(myCompleteMessage.ToString())
                Loop While DataStream.DataAvailable
                GoTo a
            Catch
                Err.Clear()
                TRunning -= 1
                Exit Sub
            End Try
        Else
            Console.WriteLine("Sorry.  You cannot read from this NetworkStream.")
        End If
        TRunning -= 1
    End Sub

    Public Sub Download(ByVal Address As String, ByVal FileName As String)
        Try
            Dim w As New WebClient
            w.DownloadFile("http://" & Address, FileName)
            WriteLine("Done.")
        Catch
            Er(Err.Number, Err.Description)
        End Try
    End Sub

    Public Sub Upload(ByVal Address As String, ByVal File2 As String)
        Try
            If IO.File.Exists(File2) = False Then
                WriteLine("File not found")
                Main()
            End If
            Dim w As New WebClient
            w.UploadFile(Address, File2)
        Catch
            Er(Err.Number, Err.Description)
        End Try
    End Sub
End Class
