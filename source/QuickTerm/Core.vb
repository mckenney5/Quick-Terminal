Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports System.Console
Public Class Core
    Public msgMbox As String = ""
    Public back2 As Boolean = False
    Dim CdromOpen As Boolean = False
    Public Type As String = "png"
    Public TimerStop As Boolean = False
    Public Function SaveScreen(Optional theFile As String = "ScreenShot") As Boolean
        Threading.Thread.Sleep(500)
        Try
            SendKeys.SendWait("%{PRTSC}")            '<alt + printscreen>
            Application.DoEvents()
            Dim data As IDataObject = Clipboard.GetDataObject()
            If data.GetDataPresent(GetType(System.Drawing.Bitmap)) Then
                Dim bmp As Bitmap = CType(data.GetData(GetType(System.Drawing.Bitmap)), Bitmap)
                If Type = "png" Then
                    bmp.Save(theFile & "." & Type, Imaging.ImageFormat.Png)
                ElseIf Type = "bmp" Then
                    bmp.Save(theFile & "." & Type, Imaging.ImageFormat.Bmp)
                ElseIf Type = "gif" Then
                    bmp.Save(theFile & "." & Type, Imaging.ImageFormat.Gif)
                ElseIf Type = "tiff" Then
                    bmp.Save(theFile & "." & Type, Imaging.ImageFormat.Tiff)
                ElseIf Type = "jpg" Then
                    bmp.Save(theFile & "." & Type, Imaging.ImageFormat.Jpeg)
                ElseIf Type = "ico" Then
                    bmp.Save(theFile & "." & Type, Imaging.ImageFormat.Icon)
                Else
                    Type = "png"
                    Return False
                End If
            End If
            Clipboard.SetDataObject(0)        'save memory by removing the image from the clipboard
            Type = "png"
            Return True
        Catch ex As Exception
            Type = "png"
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Public Function SaveWholeScreen(Optional theFile As String = "ScreenShot") As Boolean
        Try
            SendKeys.SendWait("{PRTSC}")            '<printscreen>
            Application.DoEvents()
            Dim data As IDataObject = Clipboard.GetDataObject()
            If data.GetDataPresent(GetType(System.Drawing.Bitmap)) Then
                Dim bmp As Bitmap = CType(data.GetData(GetType(System.Drawing.Bitmap)), Bitmap)
                If Type = "png" Then
                    bmp.Save(theFile & "." & Type, Imaging.ImageFormat.Png)
                ElseIf Type = "bmp" Then
                    bmp.Save(theFile & "." & Type, Imaging.ImageFormat.Bmp)
                ElseIf Type = "gif" Then
                    bmp.Save(theFile & "." & Type, Imaging.ImageFormat.Gif)
                ElseIf Type = "tiff" Then
                    bmp.Save(theFile & "." & Type, Imaging.ImageFormat.Tiff)
                ElseIf Type = "jpg" Then
                    bmp.Save(theFile & "." & Type, Imaging.ImageFormat.Jpeg)
                ElseIf Type = "ico" Then
                    bmp.Save(theFile & "." & Type, Imaging.ImageFormat.Icon)
                Else
                    Type = "png"
                    Return False
                End If
            End If
            Clipboard.SetDataObject(0)        'save memory by removing the image from the clipboard
            Type = "png"
            Return True
        Catch ex As Exception
            Type = "png"
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Public Sub Gentoo()
        ForegroundColor = ConsoleColor.Blue
        WriteLine("     .vir.                                d$b ")
        WriteLine("  .d$$$$$$b.    .cd$$b.     .d$$b.   d$$$$$$$$$$$b  .d$$b.      .d$$b. ")
        WriteLine("  $$$$( )$$$b d$$$()$$$.   d$$$$$$$b Q$$$$$$$P$$$P.$$$$$$$b.  .$$$$$$$b. ")
        WriteLine(" Q$$$$$$$$$$B$$$$$$$$P'  d$$$PQ$$$$b.   $$$$.   .$$$P' `$$$ .$$$P' `$$$ ")
        WriteLine("    '$$$$$$$P Q$$$$$$$b  d$$$P   Q$$$$b  $$$$b   $$$$b..d$$$ $$$$b..d$$$ ")
        WriteLine("  d$$$$$$P'   '$$$$$$$$ Q$$$     Q$$$$  $$$$$   `Q$$$$$$$P  `Q$$$$$$$P ")
        WriteLine("  $$$$$$$P       `'''''   ''        ''   Q$$$P     'Q$$$P'     'Q$$$P' ")
        WriteLine(" `Q$$P''                                  ''' ")
        WriteLine()
        ForegroundColor = ConsoleColor.White
        WriteLine("Welcome Gentoomen!")
        SaveScreen("4chan.g")
        Process.Start("4chan.g.png")
        Main()
    End Sub

    Public Sub dev()
        Try
            If File.Exists(Directory.GetCurrentDirectory & "\Dev.log") Then
                Dim Filee()
                Filee = File.ReadAllLines(Directory.GetCurrentDirectory & "\Dev.log")
                If Filee(0) = "--Begin of dev log" AndAlso Filee(1) = "(anything made before this is undocumented)" Then
                    Process.Start("Dev.log")
                Else
                    WriteLine("Dev.log is not found")
                    WriteLine(Filee(0) & vbNewLine & Filee(1) & vbNewLine & Filee(1))
                End If
            Else
                WriteLine("Dev.log is not found")
            End If
        Catch
            Er(Err.Number, Err.Description)
        End Try
        Main()
    End Sub

    Public Function Encrypt(ByVal input As String, ByVal key As System.Security.SecureString) As String
        key.MakeReadOnly()
        Dim ptr As IntPtr
        ptr = Runtime.InteropServices.Marshal.SecureStringToBSTR(key)
        'Return Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr) = the key in string
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""
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
            Return "<Error Encrypting string.>"
        End Try
    End Function

    Public Function Decrypt(ByVal input As String, ByVal key As System.Security.SecureString) As String
        key.MakeReadOnly()
        Dim ptr As IntPtr
        ptr = Runtime.InteropServices.Marshal.SecureStringToBSTR(key)
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
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
            Return "<Error Decrypting string.>"
        End Try
    End Function

    Public Sub Cmd2()
        Dim inpt As String
        If back2 = True Then
            back2 = False
            GoTo a
        End If
        WriteLine()
        WriteLine("Everything you type will be run via Shell (NOT AS CommandPrompt).")
        WriteLine("To exit type '~exit' in lower-case")
a:
        WriteLine()
        BackgroundColor = ConsoleColor.Black
        ForegroundColor = ConsoleColor.Cyan
        Write("Shell=> ")
        ForegroundColor = ConsoleColor.Gray
        inpt = ReadLine()
        If inpt = "~exit" Then
            back2 = False
            Main()
        ElseIf inpt = "cls" Or inpt = "clear" Then
            Clear()
            GoTo a
        Else
            Try
                Shell(inpt)
            Catch ex As Exception
                back2 = True
                Er(Err.Number, Err.Description)
            End Try
            GoTo a
        End If
    End Sub

    Public Sub msg3()
        TRunning += 1
        MsgBox(msgMbox, MsgBoxStyle.DefaultButton1)
        TRunning -= 1
    End Sub

    Public Sub cd()
        TRunning += 1
        Try
            If CdromOpen = False Then
                mciSendString("set CDAudio door open", 0, 0, 0)
                CdromOpen = True
            Else
                mciSendString("set CDAudio door closed", 0, 0, 0)
                CdromOpen = False
            End If
        Catch

        End Try
        TRunning -= 1
    End Sub

    Public Sub ProcKill(ByVal Program As String)
        Dim term As Boolean = False
        Try
            Dim Proc() As Process = Process.GetProcessesByName(Program)
            '[KILL ALL Listed PROCESSES]

            For Each Process As Process In Proc
                Process.Kill()
                Process.WaitForExit()
                WriteLine(Program & " has been terminated.")
                term = True
            Next

        Catch
            Er(Err.Number, Err.Description)
        End Try
        If term = False Then
            WriteLine(Program & " not found.")
        End If
    End Sub

    Public Sub Timerz()
        TRunning += 1
        Dim intt As Integer = 0
        Do Until TimerStop = True
            intt += 1
            Threading.Thread.Sleep(1000)
        Loop
        If intt >= 60 Then
            MsgBox(intt / 60 & " minutes!", MsgBoxStyle.Information)
        Else
            MsgBox(intt & " seconds!", MsgBoxStyle.Information)
        End If
        intt = 0
        TimerStop = False
        TRunning -= 1
    End Sub
End Class
