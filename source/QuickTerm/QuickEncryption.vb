Imports System.Console
Imports System.Security
Public Class QuickEncryption
    Public Sub EUI(ByVal Args() As String)
        Dim encstring As String
        Console.WriteLine("Text to encrypt")
        Console.ForegroundColor = ConsoleColor.Cyan
        Console.Write("> ")
        Console.ForegroundColor = ConsoleColor.Gray
        encstring = ReadLine()
        Dim securePwd As New System.Security.SecureString()
        Dim key As ConsoleKeyInfo
        Console.ForegroundColor = ConsoleColor.Cyan
        Console.Write("Password: ")
        Console.ForegroundColor = ConsoleColor.Black
        Dim Deff As ConsoleColor = Console.BackgroundColor
        Console.BackgroundColor = ConsoleColor.Black
        Do
            key = Console.ReadKey(True)
            securePwd.AppendChar(key.KeyChar)
        Loop While key.Key <> ConsoleKey.Enter
        WriteLine()
        securePwd.MakeReadOnly()
        Console.BackgroundColor = Deff
        WriteLine(Encrypt(encstring, securePwd))
    End Sub

    Public Sub DUI(ByVal Args() As String, ByVal Command As String)
        Dim Deff As ConsoleColor = Console.BackgroundColor
        If Args.Length >= 2 Then
            Dim decfile As String
            If Args(0) = "dec" Then
                decfile = Command.Remove(0, 4)
            Else
                decfile = Command.Remove(0, 8)
            End If

            If System.IO.File.Exists(decfile) = False Then
                Console.WriteLine("File not found")
                Main2()
            End If
            Dim securePwd As New System.Security.SecureString()
            Dim key As ConsoleKeyInfo
            Console.ForegroundColor = ConsoleColor.Cyan
            Console.Write("Password: ")
            Console.ForegroundColor = ConsoleColor.Black
            Console.BackgroundColor = ConsoleColor.Black
            Do
                key = Console.ReadKey(True)
                securePwd.AppendChar(key.KeyChar)
            Loop While key.Key <> ConsoleKey.Enter
            securePwd.MakeReadOnly()
            WriteLine()
            Console.BackgroundColor = Deff
            WriteLine(Decrypt(System.IO.File.ReadAllText(decfile), securePwd))
        Else
            'Else
a2:
            Dim encstring As String
            Dim inpt2 As String
            Console.WriteLine("From file? [Y/N]")
a:
            Console.ForegroundColor = ConsoleColor.Cyan
            Console.Write("> ")
            Console.ForegroundColor = ConsoleColor.Gray
            inpt2 = ReadLine()
            If inpt2.ToLower = "y" Or inpt2.ToLower.StartsWith("y") Then
                Dim decfilee
                Console.ForegroundColor = ConsoleColor.Cyan
                Console.Write("File> ")
                Console.ForegroundColor = ConsoleColor.Gray
                decfilee = ReadLine()
                If System.IO.File.Exists(decfilee) = False Then
                    Console.WriteLine("File not found")

                End If
                Dim securePwd2 As New System.Security.SecureString()
                Dim key2 As ConsoleKeyInfo
                Dim decfilee2 As String = System.IO.File.ReadAllText(decfilee)
                Console.ForegroundColor = ConsoleColor.Cyan
                Console.Write("Password: ")
                Console.ForegroundColor = ConsoleColor.Black
                Console.BackgroundColor = ConsoleColor.Black
                Do
                    key2 = Console.ReadKey(True)
                    securePwd2.AppendChar(key2.KeyChar)
                Loop While key2.Key <> ConsoleKey.Enter
                WriteLine()
                securePwd2.MakeReadOnly()
                Console.BackgroundColor = Deff
                WriteLine(Decrypt(decfilee2, securePwd2))
            ElseIf inpt2.ToLower = "n" Or inpt2.ToLower.StartsWith("N") Then
            Else
                GoTo a
            End If
            Console.WriteLine("Text to decrypt")
            Console.ForegroundColor = ConsoleColor.Cyan
            Console.Write("> ")
            Console.ForegroundColor = ConsoleColor.Gray
            encstring = ReadLine()
b:
            Dim securePwd As New System.Security.SecureString()
            Dim key As ConsoleKeyInfo
            Console.ForegroundColor = ConsoleColor.Cyan
            Console.Write("Password: ")
            Console.ForegroundColor = ConsoleColor.Black
            Console.BackgroundColor = ConsoleColor.Black
            Do
                key = Console.ReadKey(True)
                securePwd.AppendChar(key.KeyChar)
            Loop While key.Key <> ConsoleKey.Enter
            WriteLine()
            securePwd.MakeReadOnly()
            Console.BackgroundColor = Deff
            WriteLine(Decrypt(encstring, securePwd))
        End If
    End Sub

    Private Function Encrypt(ByVal input As String, ByVal key As System.Security.SecureString) As String
        Console.ForegroundColor = ConsoleColor.White
        key.MakeReadOnly()
        Dim ptr As IntPtr
        ptr = Runtime.InteropServices.Marshal.SecureStringToBSTR(key)
        'Return Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr) = the key in string
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = Nothing
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr)))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            key.Dispose()
            Return encrypted
        Catch ex As Exception
            key.Dispose()
            Err.Clear()
            Return "Error Encrypting string. Info: " & Err.Description
        End Try
    End Function

    Private Function Decrypt(ByVal input As String, ByVal key As System.Security.SecureString) As String
        Console.ForegroundColor = ConsoleColor.White
        key.MakeReadOnly()
        Dim ptr As IntPtr
        ptr = Runtime.InteropServices.Marshal.SecureStringToBSTR(key)
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = Nothing
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr)))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            key.Dispose()
            Return decrypted
        Catch ex As Exception
            key.Dispose()
            Err.Clear()
            Return "Error decrypting string. Wrong password maybe?"
        End Try
    End Function

End Class
