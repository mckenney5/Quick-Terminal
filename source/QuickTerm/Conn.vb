Imports System
Imports System.Net
Imports System.Text
Imports System.Console
Imports System.Threading
Imports System.Net.Sockets
Class Conn
    Dim cl As New TcpClient
    Dim str As NetworkStream
    Dim Connected As Boolean = False

    Public Sub Connect(ByVal IP As String, ByVal Port As UInt16)
        Try
            cl.Connect(IP, Port)
            Connected = True
            str = cl.GetStream
            Dim Worker As New Thread(AddressOf ReadS)
            Worker.Start()
            Do Until Connected = False
                Dim Inpt = Console.ReadLine()
                Dim Msg As Byte() = Encoding.UTF8.GetBytes(Inpt)
                str.Write(Msg, 0, Msg.Length)
            Loop
            Main2()
        Catch
            cl.Close()
            str.Dispose()
            Connected = False
            Er(Err.Number, Err.Description)
        End Try
    End Sub

    Private Sub ReadS()
        TRunning += 1
        Do Until Connected = False
            Dim Buffer(cl.ReceiveBufferSize) As Byte
            str.Read(Buffer, 0, cl.ReceiveBufferSize)
            Console.WriteLine(vbNewLine & CleanString(Encoding.UTF8.GetChars(Buffer)))
            Thread.Sleep(100)
        Loop
        TRunning -= 1
    End Sub

    Public Sub Server(ByVal Port As UInt16)
        Dim srv As New TcpListener(IPAddress.Any, Port)
        srv.Start()
        Dim cl As TcpClient = srv.AcceptTcpClient
        Dim str As NetworkStream = cl.GetStream
        Dim buf(cl.ReceiveBufferSize) As Byte
        str.Read(buf, 0, cl.ReceiveBufferSize)
        Dim res As Byte() = Encoding.UTF8.GetBytes("Test") 'a msg
        str.Write(res, 0, res.Length)
    End Sub

    Public Function CleanString(ByRef Str As String)
        Return Str.Replace(Encoding.UTF8.GetChars({0, 0, 0, 0}), Nothing)
    End Function
End Class
