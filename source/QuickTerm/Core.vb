'All built-in commands go in here
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
    Public Function SaveScreen(Optional ByVal theFile As String = "ScreenShot") As Boolean 'doesnt work on GNU/Linux
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
        Catch
            Type = "png"
            Er(Err.Number, Err.Description)
            Return False
        End Try
    End Function

    Public Function SaveWholeScreen(Optional ByVal theFile As String = "ScreenShot") As Boolean 'doesnt work on GNU/Linux
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
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Public Sub Gentoo() 'doesnt work on GNU/Linux
        Console.ForegroundColor = ConsoleColor.Blue
        Console.WriteLine("     .vir.                                d$b ")
        Console.WriteLine("  .d$$$$$$b.    .cd$$b.     .d$$b.   d$$$$$$$$$$$b  .d$$b.      .d$$b. ")
        Console.WriteLine("  $$$$( )$$$b d$$$()$$$.   d$$$$$$$b Q$$$$$$$P$$$P.$$$$$$$b.  .$$$$$$$b. ")
        Console.WriteLine(" Q$$$$$$$$$$B$$$$$$$$P'  d$$$PQ$$$$b.   $$$$.   .$$$P' `$$$ .$$$P' `$$$ ")
        Console.WriteLine("    '$$$$$$$P Q$$$$$$$b  d$$$P   Q$$$$b  $$$$b   $$$$b..d$$$ $$$$b..d$$$ ")
        Console.WriteLine("  d$$$$$$P'   '$$$$$$$$ Q$$$     Q$$$$  $$$$$   `Q$$$$$$$P  `Q$$$$$$$P ")
        Console.WriteLine("  $$$$$$$P       `'''''   ''        ''   Q$$$P     'Q$$$P'     'Q$$$P' ")
        Console.WriteLine(" `Q$$P''                                  ''' ")
        Console.WriteLine()
        Console.ForegroundColor = ConsoleColor.White
        Console.WriteLine("Welcome Gentoomen!")
        SaveScreen("gen2")
        Process.Start("gen2.png")
        Main()
    End Sub

    Public Sub dev()
        Try
            If File.Exists(Directory.GetCurrentDirectory & Slash & "Dev.log") Then
                Dim Filee() As String
                Filee = File.ReadAllLines(Directory.GetCurrentDirectory & Slash & "Dev.log")
                If Filee(0) = "--Begin of dev log" AndAlso Filee(1) = "(anything made before this is undocumented)" Then
                    Process.Start("Dev.log")
                Else
                    Console.WriteLine("Dev.log is not found")
                    Console.WriteLine(Filee(0) & vbNewLine & Filee(1) & vbNewLine & Filee(1))
                End If
            Else
                Console.WriteLine("Dev.log is not found")
            End If
        Catch
            Er(Err.Number, Err.Description)
        End Try
        Main()
    End Sub

    Public Sub Cmd2()
        Dim inpt As String
        If back2 = True Then
            back2 = False
            GoTo a
        End If
        Console.WriteLine()
        Console.WriteLine("Everything you type will be run via Shell (NOT AS CommandPrompt).")
        Console.WriteLine("To exit type '~exit' in lower-case")
a:
        Console.WriteLine()
        Console.ForegroundColor = ConsoleColor.Cyan
        Console.Write("Shell=> ")
        Console.ForegroundColor = ConsoleColor.Gray
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
        Try
            TRunning += 1
            MsgBox(msgMbox, MsgBoxStyle.DefaultButton1)
            TRunning -= 1
        Catch
            TRunning -= 1
            Er(Err.Number, Err.Description)
        End Try
    End Sub

    Public Sub cd() 'doesnt work on GNU/Linux
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
            Err.Clear()
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
                Console.WriteLine(Program & " has been terminated.")
                term = True
            Next

        Catch
            Er(Err.Number, Err.Description)
        End Try
        If term = False Then
            Console.WriteLine(Program & " not found.")
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
            Console.WriteLine(intt / 60 & " minutes!", MsgBoxStyle.Information)
        Else
            Console.WriteLine(intt & " seconds!", MsgBoxStyle.Information)
        End If
        intt = 0
        TimerStop = False
        TRunning -= 1
    End Sub
End Class
