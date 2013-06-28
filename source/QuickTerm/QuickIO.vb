'Handles most file/folder reading and writing
Imports System.IO
Imports System.Console
'Imports System.IO.Packaging
Public Class QuickIO
    Public Sub Read2(ByVal TextFile As String) 'doesnt work on GNU/Linux
        Dim File2 As String = TextFile
        TextFile = Environment.CurrentDirectory & Slash & TextFile
        Try
            If Scripting = True Then
                GoTo a
            End If
            If File.Exists(TextFile) = False Then '<-- this is the part that doesnt work on GNU/Linux
                WriteLine("File not found")
                Main2()
            End If
            If TextFile.EndsWith(".txt") = False AndAlso TextFile.EndsWith(".qts") = False AndAlso TextFile.EndsWith(".qt") = False AndAlso TextFile.EndsWith(".log") = False _
                AndAlso TextFile.EndsWith(".htm") = False AndAlso TextFile.EndsWith(".html") = False AndAlso TextFile.EndsWith(".rtf") = False Then
                WriteLine(TextFile & " does not seem to be a plain text file")
                WriteLine("(which may make the terminal freeze)")
                WriteLine("Would you like to read it anyway? [y/n]")
b:
                Console.ForegroundColor = ConsoleColor.Cyan
                Write("> ")
                Console.ForegroundColor = ConsoleColor.Gray
                Dim cho As String = ReadLine()
                If cho.ToLower = "y" Then
                    GoTo a
                ElseIf cho.ToLower = "n" Then
                    Main2()
                Else
                    GoTo b
                End If
            End If
a:
            Console.ForegroundColor = ConsoleColor.White
            Dim TFile() As String = File.ReadAllLines(TextFile)
            Dim i As Integer = 0
            If TFile.Length > 250 Then
                WriteLine("File may be to big for console!")
                ReadKey()
            End If
            Clear()

            WriteLine("[" & File2 & "]")
            Do Until i = 250 Or i = TFile.Length
                WriteLine(TFile(i))
                i += 1
            Loop
            If i = TFile.Length Then
                Main2()
            Else
                ReadKey()
            End If
            Do Until i = 500 Or i = TFile.Length
                WriteLine(TFile(i))
                i += 1
            Loop
            If i = TFile.Length Then
                Main2()
            Else
                ReadKey()
            End If
            Do Until i = 750 Or i = TFile.Length
                WriteLine(TFile(i))
                i += 1
            Loop
            If i = TFile.Length Then
                Main2()
            Else
                ReadKey()
            End If
            Do Until i = 1000 Or i = TFile.Length
                WriteLine(TFile(i))
                i += 1
            Loop
            If i = TFile.Length Then
                Main2()
            Else
                ReadKey()
            End If
            Do Until i = 1250 Or i = TFile.Length
                WriteLine(TFile(i))
                i += 1
            Loop
            If i = TFile.Length Then
                Main2()
            Else
                ReadKey()
            End If
            Do Until i = 1500 Or i = TFile.Length
                WriteLine(TFile(i))
                i += 1
            Loop
            Console.ForegroundColor = ConsoleColor.Yellow
            WriteLine("Warning: File lines exceed 1500 lines (reads maximum limit)")
            WriteLine("The rest of the file will be dumped.")
            Console.ForegroundColor = ConsoleColor.White
            ReadKey()
            Do Until i = TFile.Length
                WriteLine(TFile(i))
                i += 1
            Loop
            Main2()
        Catch
            Er(Err.Number, Err.Description)
        End Try
    End Sub

    Public Sub WriteFile(ByVal FileName As String) 'doesnt work on GNU/Linux
        FileName = Environment.CurrentDirectory & Slash & FileName
        'File.Create(FileName)
        Dim inpt As String
        WriteLine("To exit type '~exit' in lower-case")
a:
        inpt = ReadLine()
        If inpt = "~exit" Then

            Main2()
        ElseIf inpt = "" Then
            inpt = vbNewLine
            File.AppendAllText(FileName, inpt)
            GoTo a
        End If
        'write
        File.AppendAllText(FileName, inpt & vbNewLine)
        GoTo a
    End Sub

    Public Sub DelFile(ByVal FileName As String) 'doesnt work on GNU/Linux
        FileName = Environment.CurrentDirectory & Slash & FileName
        Try
            If File.Exists(FileName) = True Then
                File.Delete(FileName)
                WriteLine(FileName & " deleted successfully.")
            Else
                WriteLine("File not found.")
            End If
        Catch
            Er(Err.Number, Err.Description)
        End Try
        Main2()
    End Sub

    Public Sub DelFileThread() 'doesnt work on GNU/Linux
        TRunning += 1
        Filee = Environment.CurrentDirectory & Slash & Filee
        Try
            If File.Exists(Filee) = True Then
                File.Delete(Filee)
                Console.WriteLine("File deleted successfully.")
            Else
                Console.WriteLine("File not found.")
            End If
        Catch
            Console.WriteLine()
            Er(Err.Number, Err.Description)
        End Try
        TRunning -= 1
    End Sub

    Public Sub SecureDelete() 'doesnt work on GNU/Linux
        TRunning += 1
        Filee = Environment.CurrentDirectory & Slash & Filee
        Try
            Dim ran As New Random
            If File.Exists(Filee) = True Then
                File.AppendAllText(Filee, "♀")
                File.AppendAllText(Filee, "39r0j0j09u09u0xc9uv0ef0-ue0wfu3" & ver & vbNewLine & "y5q3a3yy54ragaz" & Environment.Version.ToString)
                File.WriteAllText(Filee, My.Computer.Info.AvailablePhysicalMemory)
                File.WriteAllText(Filee, My.Computer.Info.AvailableVirtualMemory)
                Rename(Filee, "asfsdfewafdfvasdfew")
                Filee = Environment.CurrentDirectory & Slash & "asfsdfewafdfvasdfew"
                File.WriteAllText(Filee, My.Computer.Info.AvailableVirtualMemory & My.Computer.Info.OSVersion & My.Computer.Info.OSFullName & My.Computer.Info.InstalledUICulture.Calendar.AlgorithmType)
                File.WriteAllText(Filee, My.Computer.Info.AvailablePhysicalMemory & My.Computer.Info.AvailablePhysicalMemory & vbNewLine _
                                  & My.Computer.Info.AvailableVirtualMemory & My.Computer.Info.AvailablePhysicalMemory * 1398 ^ 4 * Environment.CurrentDirectory.Length & _
                                  My.Computer.Info.AvailablePhysicalMemory & My.Computer.Info.AvailableVirtualMemory & _
                                  My.Computer.Info.AvailablePhysicalMemory & My.Computer.Info.AvailablePhysicalMemory & _
                                  My.Computer.Info.AvailableVirtualMemory & TimeOfDay.Second & TimeOfDay.Millisecond & _
                                  My.Computer.Info.AvailablePhysicalMemory & vbNewLine & My.Computer.Info.AvailableVirtualMemory & _
                                  My.Computer.Info.AvailablePhysicalMemory & My.Computer.Info.AvailablePhysicalMemory & _
                                  My.Computer.Info.AvailableVirtualMemory & My.Computer.Info.AvailablePhysicalMemory & _
                                  My.Computer.Info.AvailablePhysicalMemory & My.Computer.Info.AvailableVirtualMemory & _
                                  TimeOfDay.Second & TimeOfDay.Millisecond)
                File.WriteAllText(Filee, ran.Next(1000, 10000))
                File.WriteAllText(Filee, My.Computer.Info.TotalVirtualMemory / My.Computer.Info.AvailablePhysicalMemory * TRunning)
                Rename(Filee, "iexplore.exe_")
                Filee = Environment.CurrentDirectory & Slash & "iexplore.exe_"
                File.WriteAllText(Filee, My.Computer.Info.AvailablePhysicalMemory & My.Computer.Info.AvailablePhysicalMemory & vbNewLine _
                                  & My.Computer.Info.AvailableVirtualMemory & My.Computer.Info.AvailablePhysicalMemory * 1398 ^ 4 * Environment.CurrentDirectory.Length & _
                                  My.Computer.Info.AvailablePhysicalMemory & My.Computer.Info.AvailableVirtualMemory ^ 3 & _
                                  My.Computer.Info.AvailablePhysicalMemory & My.Computer.Info.AvailablePhysicalMemory & _
                                  My.Computer.Info.AvailableVirtualMemory & TimeOfDay.Second & TimeOfDay.Millisecond & ran.Next(1, 1000))
                File.WriteAllText(Filee, TimeOfDay.Second)
                File.WriteAllText(Filee, My.Computer.Info.AvailableVirtualMemory)
                File.WriteAllText(Filee, My.Computer.Info.AvailablePhysicalMemory)
                File.WriteAllText(Filee, My.Computer.Info.AvailableVirtualMemory / 1024 ^ 2)
                File.WriteAllText(Filee, DateAndTime.Now)
                File.WriteAllText(Filee, TimeOfDay.Millisecond + TimeOfDay.Second)
                File.Delete(Filee)
                Console.WriteLine("File deleted successfully.")
            Else
                Console.WriteLine("File not found.")
            End If
        Catch
            Console.WriteLine()
            Er(Err.Number, Err.Description)
        End Try
        TRunning -= 1
    End Sub

    Public Sub DeleteAll(ByVal FileExtention As String) 'doesnt work on GNU/Linux
        Dim FilesDeleted As Integer = 0
        Try
            Dim files2beDeleted As New ArrayList
            Dim di As New IO.DirectoryInfo(Environment.CurrentDirectory)
            Dim diar1 As IO.FileInfo() = di.GetFiles
            Dim dra As IO.FileInfo
            'list the names of all files in the specified directory
            For Each dra In diar1
                files2beDeleted.Add(dra.FullName)
            Next
            Dim i As Integer = 0
            Do Until i = files2beDeleted.Count
                If files2beDeleted.Item(i).ToString.EndsWith(FileExtention) = True Then
                    File.Delete(files2beDeleted.Item(i))
                    FilesDeleted += 1
                End If
                i += 1
            Loop
            WriteLine("Done.")
            WriteLine("Deleted " & FilesDeleted & " files.")
            Main2()
        Catch
            Er(Err.Number, Err.Description)
        End Try
    End Sub

    Public Sub GetDir(Optional ByVal ShowHidden As Boolean = False) 'maybe if Unix then not display hidden files unless an arg is passed
        Dim di As New IO.DirectoryInfo(Environment.CurrentDirectory)
        Dim diar2 As IO.DirectoryInfo() = di.GetDirectories
        Dim dra2 As IO.DirectoryInfo
        WriteLine("--Directories")
        For Each dra2 In diar2
            If OS = "Unix" Then
                Dim temp() As String = dra2.ToString.Split("/")
                WriteLine(temp(temp.Length - 1))
            Else
                WriteLine(dra2)
            End If
        Next
        Dim diar1 As IO.FileInfo() = di.GetFiles
        Dim dra As IO.FileInfo
        WriteLine()
        WriteLine("--Files")
        'list the names of all files in the specified directory
        For Each dra In diar1
            If OS = "Unix" Then
                Dim temp() As String = dra.ToString.Split("/")
                WriteLine(temp(temp.Length - 1))
            Else
                WriteLine(dra)
            End If
        Next
        Main2()
    End Sub

    Public Function GetMD5(ByVal filename As String) As String
        Try
            filename = Environment.CurrentDirectory & Slash & filename
            Dim md5 As New System.Security.Cryptography.MD5CryptoServiceProvider
            Dim f As New FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, &H2000)
            f = New FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, &H2000)
            md5.ComputeHash(f)
            Dim hash As Byte() = md5.Hash
            Dim buff As New System.Text.StringBuilder
            Dim hashByte As Byte
            For Each hashByte In hash
                buff.Append(String.Format("{0:X2}", hashByte))
            Next
            f.Close()
            f.Close()
            Return buff.ToString
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Sub VirusScan(ByVal DataBase As String)
        DataBase = Environment.CurrentDirectory & Slash & DataBase
        Dim FilesScanned As Integer = 0
        Dim FilesInfected As Integer = 0
        Dim md5 As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim f
        Try
            WriteLine("Loading data-base...")
            Dim f2 As String = File.ReadAllText(DataBase)
            WriteLine("Done.")
            WriteLine("Scanning..." & vbNewLine)
            WriteLine("--Viruses")
            Dim ScanList As New ArrayList
            Dim di As New IO.DirectoryInfo(Environment.CurrentDirectory)
            Dim diar1 As IO.FileInfo() = di.GetFiles
            Dim dra As IO.FileInfo
            'list the names of all files in the specified directory
            For Each dra In diar1
                ScanList.Add(dra.Name)
            Next
            Dim i As Integer = 0
            Do Until i = ScanList.Count
                f = New FileStream(ScanList.Item(i), FileMode.Open, FileAccess.Read, FileShare.Read, &H2000)
                md5.ComputeHash(f)
                Dim hash As Byte() = md5.Hash
                Dim buff As New System.Text.StringBuilder
                Dim hashByte As Byte
                For Each hashByte In hash
                    buff.Append(String.Format("{0:X2}", hashByte))
                Next
                f.Close()
                If f2.Contains(buff.ToString) = True Then
                    Console.ForegroundColor = ConsoleColor.Red
                    WriteLine(ScanList.Item(i))
                    FilesInfected += 1
                Else
                    'foo
                End If
                FilesScanned += 1
                i += 1
            Loop
            Console.ForegroundColor = ConsoleColor.White
            WriteLine("---------")
            WriteLine()
            WriteLine("Scanned: " & FilesScanned)
            Write("Infected: ")
            Console.ForegroundColor = ConsoleColor.Red
            WriteLine(FilesInfected)
            Console.ForegroundColor = ConsoleColor.White
            WriteLine("Done.")
            Main2()
        Catch
            Er(Err.Number, Err.Description)
        End Try
    End Sub

    Public Sub MakeFolder(ByVal FolderName As String)
        Try
            If FolderName.ToLower = "con" Then
                WriteLine("You can not create a folder named con.")
                Main2()
            End If
            MkDir(Environment.CurrentDirectory & Slash & FolderName)
            WriteLine("Folder created successfully")
        Catch
            Er(Err.Number, Err.Description)
        End Try
    End Sub

    Public Sub DelFolder(ByVal FolderName As String)
        Try
            RmDir(Environment.CurrentDirectory & Slash & FolderName)
            WriteLine("Folder deleted successfully")
        Catch
            Er(Err.Number, Err.Description)
        End Try
    End Sub

    Public Sub Md5Log(Optional ByVal DataBaseName As String = "md5List.txt")
        Dim QtHash As String = GetMD5("Quickterm.exe")
        Dim FilesScanned As Integer = 0
        Dim md5 As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim f
        Try
            Dim ScanList As New ArrayList
            Dim Md5List As New ArrayList
            Dim di As New IO.DirectoryInfo(Environment.CurrentDirectory)
            Dim diar1 As IO.FileInfo() = di.GetFiles
            Dim dra As IO.FileInfo
            'list the names of all files in the specified directory
            For Each dra In diar1
                ScanList.Add(dra.Name)
            Next
            Dim i As Integer = 0
            Do Until i = ScanList.Count
                f = New FileStream(ScanList.Item(i), FileMode.Open, FileAccess.Read, FileShare.Read, &H2000)
                md5.ComputeHash(f)
                Dim hash As Byte() = md5.Hash
                Dim buff As New System.Text.StringBuilder
                Dim hashByte As Byte
                For Each hashByte In hash
                    buff.Append(String.Format("{0:X2}", hashByte))
                Next
                f.Close()
                Md5List.Add(buff.ToString)
                buff.ToString()
                FilesScanned += 1
                i += 1
            Loop
            i = 0
            Do Until i = Md5List.Count
                If Md5List.Item(i) = QtHash Then
                    'foo
                Else
                    File.AppendAllText(DataBaseName, Md5List(i) & vbNewLine)
                End If
                i += 1
            Loop
            Console.ForegroundColor = ConsoleColor.White
            WriteLine("Scanned: " & FilesScanned)
            WriteLine("Done.")
            Main2()
        Catch
            Er(Err.Number, Err.Description)
        End Try
    End Sub

    Public Sub CopyFile(ByVal File As String, ByVal newFile As String)
        Try
            FileCopy(Environment.CurrentDirectory & Slash & File, Environment.CurrentDirectory & Slash & newFile)
            IO.File.Move(File, newFile)
            WriteLine("File copied successfully")
        Catch
            Er(Err.Number, Err.Description)
        End Try
        Main2()
    End Sub

    Public Sub MoveFile(ByVal File As String, ByVal newFile As String)
        Try
            IO.File.Move(Environment.CurrentDirectory & Slash & File, Environment.CurrentDirectory & Slash & newFile)
            WriteLine("File moved successfully")
        Catch
            Er(Err.Number, Err.Description)
        End Try
        Main2()
    End Sub

    Public Sub QuickWrite(ByVal File As String, ByVal Text As String)
        Try
            IO.File.AppendAllText(Environment.CurrentDirectory & Slash & File, Text & vbNewLine)
            WriteLine("Done.")
        Catch
            Er(Err.Number, Err.Description)
        End Try
        Main2()
    End Sub

    'P

    Public Sub RenameFile(ByVal Filename As String, ByVal NewName As String)
        Try
            Rename(Filename, NewName)
        Catch
            Er(Err.Number, Err.Description)
        End Try

    End Sub
End Class
