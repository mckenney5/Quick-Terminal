'Handles remote connections
'May get deleted at some point
Imports System.Net.Sockets
Imports System.Text
Imports System.Console
Public Class Remote 'this doesnt work/buggy
    Dim requestCount As Integer
    Dim port As Int16 = 80
    Dim clientSocket As TcpClient
    Dim serverSocket As New TcpListener(port)
    Public RemoteRunning As Boolean = False
    Dim back As Boolean = False

    Public Sub Listen(ByVal port As Integer)
        'Try
        If back = True Then
            GoTo a
        End If
        back = True
        serverSocket.Start()
        WriteLine("Server Started")
        clientSocket = serverSocket.AcceptTcpClient()
        'do log in here
        WriteLine("Accepted connection from client")
        RemoteRunning = True
        requestCount = 0
        Dim networkStream As NetworkStream = _
                clientSocket.GetStream()
b:

        requestCount = requestCount + 1
        Dim bytesFrom(10024) As Byte
        networkStream.Read(bytesFrom, 0, CInt(clientSocket.ReceiveBufferSize))
        Dim dataFromClient As String = _
                System.Text.Encoding.ASCII.GetString(bytesFrom)
        dataFromClient = _
    dataFromClient.Substring(0, dataFromClient.IndexOf("$"))
        msg("Data from client -  " + dataFromClient)
        'do your dance feebee
        Commands(dataFromClient.Trim())
a:
        Dim networkStream2 As NetworkStream = _
        clientSocket.GetStream()
        Dim serverResponse As String = _
            "Server response " + Convert.ToString(requestCount)
        Dim sendBytes As [Byte]() = _
            Encoding.ASCII.GetBytes(serverResponse)
        networkStream2.Write(sendBytes, 0, sendBytes.Length)
        networkStream2.Flush()
        msg(serverResponse)
        GoTo b
        'Catch ex As Exception
        'If Err.Number = 5 Then
        'Err.Clear()
        'serverSocket.Stop()
        ' clientSocket.Close()
        'back = False
        ' Listen(port)
        'End If
        'MsgBox(Err.Number & " " & Err.Description)
        'End Try

        ReadLine()
        clientSocket.Close()
        serverSocket.Stop()
        msg("exit")
        Console.Read()
        Clear()
        RemoteRunning = False
        Main()
    End Sub

    Private Sub msg(ByVal mesg As String)
        mesg.Trim()
        WriteLine(">> " + mesg)
    End Sub
End Class
