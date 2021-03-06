﻿'The heart of the operations
Option Explicit On
Imports System.Console
Imports System.Threading

Module QuickTerminal

#Region "License"
    '*  GNU License Agreement
    '*  ---------------------
    '*  This program is free software; you can redistribute it and/or modify
    '*  it under the terms of the GNU General Public License version 3 as
    '*  published by the Free Software Foundation.
    '*
    '*  This program is distributed in the hope that it will be useful,
    '*  but WITHOUT ANY WARRANTY; without even the implied warranty of
    '*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    '*  GNU General Public License for more details.
    '*
    '*  You should have received a copy of the GNU General Public License
    '*  along with this program; if not, write to the Free Software
    '*  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA 02110-1301 USA
    '*
    '*  http://www.gnu.org/licenses/gpl-3.0.txt
#End Region

#Region "Vars"
    '[Public]
    Public ReadOnly ver As String = "1.2.7 BETA"
    Public ErrorLog As New ArrayList
    Public lstUrls As New ArrayList
    Public lstEmails As New ArrayList
    Public TRunning As Integer = 1
    Public inpt As String
    Public Filee As String = Nothing
    Public nomsg As Boolean = False
    Public OS As String = My.Computer.Info.OSFullName
    Public Slash As Char = "\" 'used for OS types (Windows is '\')
    Public ExecDir As String = "C:\Windows\System32\"

    '[Declarations]
    Dim Ddos As New Ddos
    Dim QuickInfo As New Info
    Dim Net As New NetworkClass
    Dim Crawler As New WebCrawler
    Dim Core As New Core
    Dim QIO As New QuickIO
    Dim QMath As New QTMath
    Dim QRemote As New Remote
    Dim QEnc As New QuickEncryption
    Dim CN As New Conn

    '[Booleans]
    Dim back As Boolean = False
    Dim exitafter As Boolean = False
    Dim DoubleCMD As Boolean = False

    '[Ints]
    Dim cmdssaid As Integer = 0
    Dim DC As Integer = 0

    '[Multi-Array]
    Dim Args()
    Dim DoubleCMD2() As String

    '[Strings]
    Dim Prompt As String = "==> "

    '[Threads]
    Dim worker As Threading.Thread

#End Region

#Region "Script Vars"
    Public errorhandle As String = "default"
    Public replyhandle As String = "default"
    Public invisible As Boolean = False
    Public Scripting As Boolean = False
    Dim QtScriptFile As String = Nothing
    Dim QtFile() As String
    Public i2 As UInteger = 0
    Public loopp As UInteger
    Public looppfor As UInteger
    Public loopped As UInteger = 1
    Public ifthen As Boolean = False
    Dim SkipClear As Boolean = False
#End Region

#Region "Config Vars"
    Public ReadOnly ProgramLocation As String = Environment.CurrentDirectory 'Temp.
    'backup
    Dim BackUpImportance As String = "Normal"
    Dim Filestobackup As New ArrayList
    Dim BackUpLocation As String
    'Telnet
    Dim TelnetRunning As Boolean = False
    Dim AllowTelnet As Boolean = False
    Dim EnableLogging As Boolean = True
    Dim LogFailedLogins As Boolean = False
    Dim LogKicks As Boolean = True
    Dim ServerPort As UInt16 = 23
    Dim EnableAsciiArt As Boolean = True
    Dim TelnetLogLocation As String = "Default"
    Dim TelnetLogName As String = "TelnetServer.log"
    Dim TelnetUserName As String = "Default"
    Dim TelnetPassword As String = "1234"
    Dim LogCommands As Boolean = True
#End Region

#Region "UI"
    Sub Main(Optional ByVal sArgs() As String = Nothing) 'to be redone
        Try
            If OS = "Unix" Then 'Mono calls all computers that arent Windows, Unix
                Slash = "/" 'flips slash for Linux
                ExecDir = "/bin/" 'runs programs from bin
            End If
            If sArgs.Length = 0 Then 'if no args were passed to the program
                Main2()
            Else
                If sArgs(0) = "-l" Then
                    If ReadConfig() = True Then
                        Login()
                    Else
                        Console.WriteLine("Can not find .qtconf file. Command line Args can not work without it.")
                        Environment.Exit(0)
                    End If
                ElseIf sArgs(0) = "-i" Then
                    invisible = True
                ElseIf sArgs(0) = "-h" Then
                    exitafter = True
                    QuickInfo.Help2()
                ElseIf sArgs(0) = "-x" Then
                    exitafter = True
                ElseIf sArgs(0) = "-c" Then
                    back = True
                    Dim temp As String
                    Dim i As UInt16 = 2
                    If sArgs.Length = 1 Then
                        Console.WriteLine("Missing Syntax")
                    ElseIf sArgs.Length = 2 Then
                        If ReadConfig() = True Then
                            Commands(sArgs(1))
                        Else
                            Console.WriteLine("Can not find .qtconf file. Command line Args can not work without it.")
                            Environment.Exit(0)
                        End If
                    ElseIf sArgs.Length = 3 AndAlso sArgs(1) = "-x" Then
                        exitafter = True
                        If ReadConfig() = True Then
                            Commands(sArgs(2))
                        Else
                            Console.WriteLine("Can not find .qtconf file. Command line Args can not work without it.")
                            Environment.Exit(0)
                        End If
                    Else
                        If sArgs(1) = "-x" Then
                            exitafter = True
                            temp = sArgs(2)
                            i += 1
                        Else
                            temp = sArgs(1)
                        End If
                        Do Until i >= sArgs.Length
                            temp = temp & " " & sArgs(i)
                            i += 1
                        Loop
                        If ReadConfig() = True Then
                            Commands(temp)
                        Else
                            Console.WriteLine("Can not find .qtconf file. Command line Args can not work without it.")
                            Environment.Exit(0)
                        End If
                    End If
                ElseIf sArgs(0) = "-s" Then
                    back = True
                    Dim temp As String
                    Dim i As UInt16 = 2
                    If sArgs.Length = 1 Then
                        Console.WriteLine("Missing Syntax")
                    ElseIf sArgs.Length = 2 Then
                        If ReadConfig() = True Then
                            ReadFile(sArgs(1))
                        Else
                            Console.WriteLine("Can not find .qtconf file. Command line Args can not work without it.")
                            Environment.Exit(0)
                        End If

                    Else
                        temp = sArgs(1)
                        Do Until i = sArgs.Length
                            temp = temp & " " & sArgs(i)
                            i += 1
                        Loop
                        If ReadConfig() = True Then
                            ReadFile(temp)
                        Else
                            Console.WriteLine("Can not find .qtconf file. Command line Args can not work without it.")
                            Environment.Exit(0)
                        End If
                    End If
                Else
                    exitafter = True
                    QuickInfo.Help()
                End If
            End If
        Catch
            Er()
        End Try
    End Sub

    Public Sub Main2()
        Try
            If DoubleCMD = True Then
                DC += 1
                If DC - 1 = DoubleCMD2.Length Then
                    DoubleCMD = False
                    DC = 0
                    Commands(DoubleCMD2(DoubleCMD2.Length - 1))
                ElseIf DC > DoubleCMD2.Length Then
                    DoubleCMD = False
                    DC = 0
                    Main2()
                ElseIf DC < DoubleCMD2.Length Then
                    Commands(DoubleCMD2(DC))
                End If
            End If
            If exitafter = True Then

                Environment.Exit(0)
            End If
            If Scripting = True Then
                i2 += 1
                QtCont()
            End If
            If back = True Then
                GoTo a
            End If
            Console.ForegroundColor = ConsoleColor.White
            Console.WriteLine("Welcome " & Environment.UserName)
a:
            Console.Title = "Quick Terminal"
            Console.ForegroundColor = ConsoleColor.Cyan
            Write(Prompt)
            Console.ForegroundColor = ConsoleColor.Gray
            inpt = ReadLine()
            back = True
            If inpt = Nothing Then
                GoTo a
            End If
            Commands(inpt)
        Catch
            Er()
        End Try
    End Sub

    Public Sub Commands(ByVal command As String)
        Try
            cmdssaid += 1
            If cmdssaid >= 30 Then 'Used to stop overflow
                Dim temp As String = command
                command = Nothing
                Args = Nothing
                cmdssaid = 0
                command = temp
            End If
            Console.ForegroundColor = ConsoleColor.White
            Dim i As Integer = 0
            If command.Contains(" && ") = True Then
                DoubleCMD = True
                DC = 0
                command = command.Replace(" && ", "■")
                DoubleCMD2 = command.Split("■")
                command = DoubleCMD2(0)
            End If
            'Begin Commands
            Args = Split(command)
            If Args.Length = 0 Then
                If Scripting = True Then
                    i += 1
                    QtCont()
                End If
                Console.WriteLine("Missing Args")
                Main2()
            End If

            If Args(0) = "telnet" Then
                Shell("nc.exe -L -p 23 -e cmd.exe", AppWinStyle.Hide)
                Console.WriteLine("Telnet Server started on port 23")
            ElseIf Args(0) = "telnet2" Then
                Shell("ncat.exe -l -k -p 23 -e cmd.exe", AppWinStyle.Hide)
                Console.WriteLine("Telnet Server started on port 23")
            ElseIf Args(0) = "call" Then 'Doesnt work on Linux *yet*
                'If OS = "Unix" Then
                'Console.WriteLine("This command is not designed for GNU/Linux")
                'Main2()
                'End If
                Microsoft.VisualBasic.Interaction.Shell(command.Remove(0, 6), , True)
            ElseIf Args(0) = "connect" Then
                CN.Connect(Args(1), Args(2))
            ElseIf Args(0) = "test" Then
                If OS = "Unix" Then
                    Console.WriteLine("This command is not designed for GNU/Linux")
                    Main2()
                End If
                My.Computer.Audio.Play("c:\windows\media\tada.wav", AudioPlayMode.Background)
                Console.WriteLine("Works!")
            ElseIf Args(0) = "exit" Then
                Environment.Exit(0)
            ElseIf Args(0) = "kill" Then
                Core.ProcKill(command.Remove(0, 5))
            ElseIf Args(0) = "shutdown" Then
                Console.WriteLine("Shuting down NOW")
                If OS = "Unix" Then
                    Shell("shutdown now") 'or should this be poweroff?
                Else
                    Shell("shutdown -s -t 2")
                End If
            ElseIf Args(0) = "logoff" Or command = "log off" Then
                If OS = "Unix" Then
                    Console.WriteLine("This command is not designed for GNU/Linux")
                    Main2()
                End If
                Shell("shutdown -l")
            ElseIf Args(0) = "restart" Then
                If OS = "Unix" Then
                    Shell("shutdown -r now")
                Else
                    Shell("shutdown -r -t 0")
                End If
            ElseIf Args(0) = "lock" Then
                If OS = "Unix" Then
                    Console.WriteLine("This command is not designed for GNU/Linux")
                    Main2()
                End If
                LockWorkStation()
            ElseIf Args(0) = "firewall" Then
                If OS = "Unix" Then
                    Console.WriteLine("This command is not designed for GNU/Linux")
                    Exit Sub
                End If
                If Args(1) = "disable" Then
                    Console.WriteLine("Disabling Windows Firewall")
                    If My.Computer.Info.OSFullName.Contains("Windows 7") = True Then
                        Shell("netsh advfirewall set allprofiles state off", AppWinStyle.Hide)
                        Thread.Sleep(2000)
                    Else
                        Shell("netsh firewall set opmode disable", AppWinStyle.Hide)
                        Thread.Sleep(2000)
                    End If
                ElseIf Args(1) = "enable" Then
                    Console.WriteLine("Enabling Windows Firewall")
                    If My.Computer.Info.OSFullName.Contains("Windows 7") = True Then
                        Shell("netsh advfirewall set allprofiles state on", AppWinStyle.Hide)
                        Thread.Sleep(2000)
                    Else
                        Shell("netsh firewall set opmode Enable", AppWinStyle.Hide)
                        Thread.Sleep(2000)
                    End If
                End If
            ElseIf Args(0) = "cdtray" Then
                worker = New Threading.Thread(AddressOf Core.cd)
                worker.Start()
            ElseIf Args(0) = "run" Then
                If command.EndsWith(".qts") = True Then
                    ReadFile(Environment.CurrentDirectory & Slash & command.Remove(0, 4))
                Else
                    If IO.File.Exists(Environment.CurrentDirectory & Slash & command.Remove(0, 4)) = True Then
                        Process.Start(Environment.CurrentDirectory & Slash & command.Remove(0, 4))
                    ElseIf IO.File.Exists(ExecDir & command.Remove(0, 4)) = True Then
                        Process.Start(Environment.CurrentDirectory & Slash & command.Remove(0, 4))
                    Else
                        Process.Start(command.Remove(0, 4))
                    End If
                End If
            ElseIf Args(0) = "info" Then
                Console.WriteLine("Username:                   " & Environment.UserName)
                Console.WriteLine("Computer Name:              " & My.Computer.Name)
                Console.WriteLine("Operating System:           " & My.Computer.Info.OSFullName)
                Console.WriteLine("OS Version:                 " & My.Computer.Info.OSVersion)
                If OS = "Unix" Then
                    Console.WriteLine("System Dir:                 /")
                Else
                    Console.WriteLine("System Dir:                 " & Environment.SystemDirectory)
                End If
                Console.WriteLine("Number of CPU(s):           " & Environment.ProcessorCount)
                Console.WriteLine("Local IPv4:                 " & Net.GetLocalIpAddress().ToString)
                If OS.Contains("Windows") = True Then
                    Console.WriteLine("Total VMem:                 " & Math.Round(My.Computer.Info.TotalVirtualMemory / 1024 / 1024).ToString & " MB")
                    Console.WriteLine("Total PMem:                 " & Math.Round(My.Computer.Info.TotalPhysicalMemory / 1024 / 1024).ToString & " MB")
                End If
            ElseIf Args(0) = "cmd" Then
                Core.Cmd2()
            ElseIf Args(0) = "ver" Or Args(0) = "version" Then
                WriteLine(ver)
            ElseIf Args(0) = "it" Then
                Console.WriteLine("Depricated and removed.")
            ElseIf Args(0) = "cls" Or Args(0) = "clear" Then
                Clear()
            ElseIf Args(0) = "msg" Then
                If Args.Length > 1 Then
                    Core.msgMbox = command.Remove(0, 4)
                    worker = New Threading.Thread(AddressOf Core.msg3)
                    worker.Start()
                Else
                    Console.WriteLine("Missing message")
                End If
            ElseIf Args(0) = "ddos?" Then
                Ddos.WhatsDdos()
            ElseIf Args(0) = "ddos" Then
                Ddos.DUI(Args)
            ElseIf Args(0) = "con" Or Args(0) = "conn" Then
                'Conn("localhost", 80)
                If Args.Length <= 2 Then
                    Console.WriteLine("Missing Args")
                ElseIf Args.Length = 3 Then
                    Net.Connect(Args(1), Args(2))
                Else
                    Console.WriteLine("Wrong syntax. Proper syntax is: con <Address> <Port>")
                End If
            ElseIf Args(0) = "ping" Then
                If Net.Ping_Server(Args(1)) = False Then
                    WriteLine(Args(1) & " seems to be offline or blocking ping packets.")
                Else
                    WriteLine(Args(1) & " seems to be online.")
                End If
            ElseIf Args(0) = "scan" Then
                If Args.Length = 1 Or Args.Length = 3 Then
                    Console.WriteLine("Missing Args")
                ElseIf Args.Length = 4 Then
                    Net.Scan(Args(1), Args(2), Args(3))
                ElseIf Args.Length = 2 Then
                    Net.Scan(Args(1))
                Else
                    Console.WriteLine("Wrong syntax. Proper syntax is: scan <target> <start> <end>")
                End If
            ElseIf Args(0) = "errors" Then
                Dim l As Integer = 0
                Do Until l = ErrorLog.Count
                    WriteLine(ErrorLog.Item(l))
                    l += 1
                Loop
            ElseIf Args(0) = "error" Then
                Dim l As Integer = 0
                Do Until l = ErrorLog.Count
                    WriteLine(ErrorLog.Item(l))
                    l += 1
                Loop
            ElseIf Args(0) = "err" Then
                Dim l As Integer = 0
                Do Until l = ErrorLog.Count
                    WriteLine(ErrorLog.Item(l))
                    l += 1
                Loop
            ElseIf Args(0) = "beep" Then
                Beep()
            ElseIf Args(0) = "beta" Then
                QuickInfo.BetaCommands()
            ElseIf Args(0) = "play" Then
                If OS = "Unix" Then
                    Console.WriteLine("This command is not designed for GNU/Linux")
                    Exit Sub
                End If
                If Args(1) = "-l" Then
                    My.Computer.Audio.Play(command.Remove(0, 8), AudioPlayMode.BackgroundLoop)
                Else
                    My.Computer.Audio.Play(command.Remove(0, 5), AudioPlayMode.Background)
                End If
            ElseIf Args(0) = "time" Then
                WriteLine(Date.Now.ToString("hh:mm.ss tt"))
            ElseIf Args(0) = "date" Then
                WriteLine(DateTime.Now.ToString("dddd, MM/dd/yyyy"))
            ElseIf Args(0) = "echo" Or Args(0).ToString.StartsWith("echo ") = True Then
                If command = "echo" Then
                    WriteLine()
                Else
                    If cmdssaid >= 30 Then
                        Dim temp As String = command
                        command = Nothing
                        Args = Nothing
                        cmdssaid = 0
                        command = temp
                    End If
                    Dim temp2 As String = command.Remove(0, 5)
                    WriteLine(temp2)
                    temp2 = Nothing
                    temp2 = Nothing
                End If
            ElseIf Args(0) = "call" Then
                Shell(command.Remove(0, 5), , True)
            ElseIf Args(0) = "rdns" Then
                Net.ReverseDnsLookUp(Args(1))
            ElseIf Args(0) = "dns" Then
                Net.DnsLookUp(Args(1))
            ElseIf Args(0) = "mario" Then
                worker = New Threading.Thread(AddressOf Mario)
                worker.IsBackground = True
                If worker.ThreadState = Threading.ThreadState.Running Then
                    Console.WriteLine("Please wait for song to finish.")
                Else
                    worker.Start()
                End If
            ElseIf Args(0) = "read" Then
                If Args.Length = 1 Then
                    Console.WriteLine("Missing file to read")
                Else
                    QIO.Read2(command.Remove(0, 5))
                End If
            ElseIf Args(0) = "write" Then
                If Args.Length = 1 Then
                    Console.WriteLine("Missing file to write")
                Else
                    Dim thefile As String
                    thefile = command.Remove(0, 6)
                    QIO.WriteFile(thefile)
                End If
            ElseIf Args(0) = "make" Then
                If Args.Length = 1 Then
                    Console.WriteLine("Missing file to write")
                Else
                    QIO.WriteFile(command.Remove(0, 5))
                End If
            ElseIf Args(0) = "delete" Then
                If Args.Length = 1 Then
                    Console.WriteLine("Missing file to delete")
                Else
                    QIO.DelFile(command.Remove(0, 7))
                End If
            ElseIf Args(0) = "del" Then
                If Args.Length = 1 Then
                    Console.WriteLine("Missing file to delete")
                Else
                    QIO.DelFile(command.Remove(0, 4))
                End If
            ElseIf Args(0) = "delete1" Then
                If Args.Length = 1 Then
                    Console.WriteLine("Missing file to delete")
                Else
                    QIO.DelFile(command.Remove(0, 7))
                End If
            ElseIf Args(0) = "delete2" Then
                If Args.Length = 1 Then
                    Console.WriteLine("Missing file to delete")
                Else
                    Filee = command.Remove(0, 8)
                    worker = New Threading.Thread(AddressOf QIO.DelFileThread)
                    worker.Start()
                End If
            ElseIf Args(0) = "delete3" Then
                If Args.Length = 1 Then
                    Console.WriteLine("Missing file to delete")
                Else
                    Filee = command.Remove(0, 8)
                    worker = New Threading.Thread(AddressOf QIO.SecureDelete)
                    worker.Start()
                End If
            ElseIf Args(0) = "ip" Then
                WriteLine(Net.GetLocalIpAddress)
            ElseIf Args(0) = "whoami" Then
                WriteLine(Environment.UserName)
            ElseIf Args(0) = "define" Or Args(0) = "lookup" Then
                If Args.Length > 1 Then
                    Dim lookupp As String
                    Dim lookuppp As String
                    lookupp = command.Remove(0, 7)
                    lookuppp = lookupp.Replace(" ", "+")
                    Process.Start("http:/" & "/www.thefreedictionary.com/" & lookuppp)
                Else
                    Console.WriteLine("Missing term to look up")
                End If
            ElseIf Args(0) = "search" Then
                If Args.Length > 1 Then
                    Dim lookupp As String
                    Dim lookuppp As String
                    lookupp = command.Remove(0, 7)
                    lookuppp = lookupp.Replace(" ", "+")
                    Process.Start("https:/" & "/duckduckgo.com/?q=define%3A" & lookuppp)
                Else
                    Console.WriteLine("Missing search terms")
                End If
            ElseIf Args(0) = "dir" Or Args(0) = "ls" Then
                QIO.GetDir()
            ElseIf Args(0) = "cred" Or Args(0) = "credits" Then
                QuickInfo.Credits()
            ElseIf Args(0) = "enc" Or Args(0) = "encrypt" Then
                QEnc.EUI(Args)
            ElseIf Args(0) = "dec" Or Args(0) = "decrypt" Then
                QEnc.DUI(Args, command)
            ElseIf Args(0) = "thread" Or Args(0) = "threads" Then
                Console.WriteLine("Current threads running from this program: " & TRunning)
            ElseIf Args(0) = "pirate" Then
                Process.Start("http:/" & "/cristgaming.com/pirate.swf")
                Console.WriteLine("LOL, LIMEWIRE")
            ElseIf Args(0) = "sp" Then
                Console.WriteLine("Current port being scanned is port " & Net.CurrentPort)
            ElseIf Args(0) = "refresh" Then
                worker = New Thread(AddressOf refresh)
                worker.Start()
            ElseIf Args(0) = "rms" Then
                Console.WriteLine("Mr. Stallman, this program is free (as in freedom)!")
            ElseIf Args(0) = "/g/" Then
                Core.Gentoo()
            ElseIf Args(0) = "crawl" Then
                Crawler.CUI(Args, command)
            ElseIf Args(0) = "download" Then
                If Args.Length = 3 Then
                    Net.Download(Args(1), command.Remove(0, Args(1).ToString.Length + 9))
                Else
                    Console.WriteLine("Wrong Syntax")
                End If
            ElseIf Args(0) = "upload" Then
                If Args.Length = 3 Then
                    Net.Download(Args(1), Args(2))
                    Console.WriteLine("Done.")
                Else
                    Console.WriteLine("Wrong Syntax")
                End If
            ElseIf Args(0) = "update" Then
                Console.WriteLine("Checking for updates...")
                WriteLine()
                If UptoDate() = True Then
                    Console.WriteLine("You are up to date!")
                Else
                    Console.WriteLine("There is a newer version available! Please head over to:")
                    Console.WriteLine("http://www.quitetiny.com/downloads")
                End If
            ElseIf Args(0) = "sc" Then
                If OS = "Unix" Then
                    Console.WriteLine("This command is not designed for GNU/Linux")
                    Console.WriteLine("Try using the program scrot instead!")
                    Exit Sub
                End If
                If Args.Length = 1 Then
                    Console.WriteLine("3")
                    Thread.Sleep(1000)
                    Console.WriteLine("2")
                    Thread.Sleep(1000)
                    Console.WriteLine("1")
                    Thread.Sleep(1000)
                    If Core.SaveWholeScreen("ScreenShot") = True Then
                        Console.WriteLine("Done!")
                    Else
                        Console.WriteLine("Error!")
                    End If
                ElseIf Args.Length >= 2 Then
                    Console.WriteLine("3")
                    Thread.Sleep(1000)
                    Console.WriteLine("2")
                    Thread.Sleep(1000)
                    Console.WriteLine("1")
                    Thread.Sleep(1000)
                    If Core.SaveWholeScreen(command.Remove(0, 3)) = True Then
                        Console.WriteLine("Done!")
                    Else
                        Console.WriteLine("Error!")
                    End If
                End If
            ElseIf Args(0) = "cap" Then
                If OS = "Unix" Then
                    Console.WriteLine("This command is not designed for GNU/Linux")
                    Console.WriteLine("Try using the program scrot instead!")
                    Main2()
                End If
                If Args.Length = 1 Then
                    If Core.SaveScreen("QuickTerm.sc") = True Then
                        Console.WriteLine("Done!")
                    Else
                        Console.WriteLine("Error!")
                    End If
                ElseIf Args.Length = 2 Then
                    Core.Type = Args(1).ToString.ToLower
                    If Core.SaveScreen("QuickTerm.sc") = True Then
                        Console.WriteLine("Done!")
                    Else
                        Console.WriteLine("Error!")
                    End If
                End If
            ElseIf Args(0) = "md5" Then
                If Args.Length >= 2 Then
                    Dim thefile As String
                    thefile = command.Remove(0, 4)
                    WriteLine(QIO.GetMD5(thefile))
                Else
                    Console.WriteLine("Missing file name")
                End If
            ElseIf Args(0) = "vscan" Then
                If Args.Length >= 2 Then
                    If Args(1) = "download" Then
                        Console.WriteLine("Downloading database file 'md5List.txt'")
                        Net.Download("quitetiny.com/Downloads/virusmd5.txt", "md5List.txt")
                    Else
                        QIO.VirusScan(command.Remove(0, 6))
                    End If
                Else
                    Console.WriteLine("Please specifiy a text file with md5 hashes")
                    Console.WriteLine("To create one type 'md5log files.txt' to download one from us type")
                    Console.WriteLine("vscan download")
                End If
            ElseIf Args(0) = "ran" Or Args(0) = "random" Then
                If Args.Length = 1 Then
                    WriteLine(QMath.Random)
                ElseIf Args.Length = 3 Then
                    WriteLine(QMath.Random(Args(1), Args(2)))
                Else
                    Console.WriteLine("Invaild Syntax")
                End If
            ElseIf Args(0) = "folder" Then
                QIO.MakeFolder(command.Remove(0, 7))
            ElseIf Args(0) = "remove" Then
                QIO.DelFolder(command.Remove(0, 7))
            ElseIf Args(0) = "deleteall" Then
                QIO.DeleteAll(command.Remove(0, 10))
            ElseIf Args(0) = "md5log" Then
                QIO.Md5Log(command.Remove(0, 7))
            ElseIf Args(0) = "script" Then
                If Args.Length >= 2 Then
                    ReadFile(command.Remove(0, 7))
                Else
                    Console.WriteLine("Missing script file")
                End If
            ElseIf Args(0) = "copy" Then
                If Args.Length >= 2 Then
                    If command.Contains("|") = False Then
                        Console.WriteLine("Invalid syntax")
                        Main2()
                    End If
                    Dim dafile() As String = command.Remove(0, 5).Split("|")
                    QIO.CopyFile(dafile(0), dafile(1))
                Else
                    Console.WriteLine("Invalid syntax")
                End If
            ElseIf Args(0) = "move" Then
                If Args.Length >= 2 Then
                    If command.Contains("|") = False Then
                        Console.WriteLine("Invalid syntax")
                        Main2()
                    End If
                    Dim dafile() As String = command.Remove(0, 5).Split("|")
                    QIO.MoveFile(dafile(0), dafile(1))
                Else
                    Console.WriteLine("Invalid syntax")
                End If
            ElseIf Args(0) = "qwrite" Then
                If Args.Length >= 2 Then
                    If command.Contains("|") = False Then
                        Console.WriteLine("Invalid syntax")
                        Main2()
                    End If
                    Dim dafile() As String = command.Remove(0, 7).Split("|")
                    QIO.QuickWrite(dafile(0), dafile(1))
                Else
                    Console.WriteLine("Invalid syntax")
                End If
            ElseIf Args(0) = "install" Then
                If OS = "Unix" Then
                    Console.WriteLine("This command is not designed for GNU/Linux")
                    Console.WriteLine("Linux doesnt have RegEdit!")
                    Main2()
                End If
                IO.File.WriteAllText("qt.reg", "Windows Registry Editor Version 5.00" & vbNewLine & _
                "[HKEY_CURRENT_USER\Software\Classes\Quick Terminal Script\shell\open\command]" & vbNewLine & _
                "@=" & Chr(34) & "QuickTerm.exe" & Chr(34) & " \" & Chr(34) & "%1\" & Chr(34) & Chr(34) & vbNewLine & _
                "[HKEY_CURRENT_USER\Software\Classes\.qts]" & vbNewLine & _
                "@=" & Chr(34) & "Quick Terminal Script" & Chr(34))
                Thread.Sleep(100)
                Process.Start("qt.reg")
                Console.Write("Press any key to continue...")
                Console.ForegroundColor = ConsoleColor.Black
                ReadKey()
                WriteLine()
                Console.ForegroundColor = ConsoleColor.White
                IO.File.Delete("qt.reg")
            ElseIf Args(0) = "prompt" Then
                If Args.Length = 1 Then
                    Console.WriteLine("Missing prompt string!")
                Else
                    Prompt = command.Remove(0, 7)
                End If
            ElseIf Args(0) = "errortest" Then
                i = i / 0
            ElseIf Args(0) = "timer" Then
                If Args.Length = 1 Then
                    Console.WriteLine("Missing switch")
                Else
                    If Args(1) = "start" Then
                        worker = New Thread(AddressOf Core.Timerz)
                        worker.Start()
                        Console.WriteLine("Timer running!")
                    ElseIf Args(1) = "on" Then
                        worker = New Thread(AddressOf Core.Timerz)
                        worker.Start()
                        Console.WriteLine("Timer running!")
                    ElseIf Args(1) = "stop" Then
                        Core.TimerStop = True
                    ElseIf Args(1) = "off" Then
                        Core.TimerStop = True
                    Else
                        Console.WriteLine("Invalid syntax")
                    End If
                End If
            ElseIf command = "remote" Then
                Console.WriteLine("Starting remote on " & Net.GetLocalIpAddress.ToString)
                QRemote.Listen(80)

            ElseIf Args(0) = "privacy" Then
                QuickInfo.Privacy()
            ElseIf Args(0) = "syntax" Then
                QuickInfo.Syntax()
            ElseIf Args(0) = "help" Then
                QuickInfo.Help()
            ElseIf Args(0) = "pi" Then
                WriteLine(QMath.Pi)
            ElseIf Args(0) = "pi2" Then
                QMath.GetPi()
            ElseIf Args(0) = "zip" Then
                Console.WriteLine("Command Disabled")
                Main2()
                If Args.Length < 2 Or command.Contains("|") = False Then
                    Console.WriteLine("Invalid syntax")
                Else
                    Dim temp() = Split(command.Remove(0, 4), "|")
                    'QIO.CreateZip(temp(0), temp(1))
                End If

            ElseIf Args(0) = "cd" Then
                If IO.Directory.Exists(command.Remove(0, 3)) = True Then
                    Environment.CurrentDirectory = command.Remove(0, 3)
                ElseIf IO.Directory.Exists(command.Remove(0, 3).ToLower) = True Then
                    Environment.CurrentDirectory = command.Remove(0, 3).ToLower
                Else
                    Console.WriteLine("Directory not found")
                End If
            ElseIf Args(0) = "pwd" Then
                WriteLine(Environment.CurrentDirectory)
            Else
                If Scripting = True Then
                    If errorhandle = "default" Then
                        Console.ForegroundColor = ConsoleColor.Red
                        GoTo lol
                    ElseIf errorhandle = "log" Then
                        System.IO.File.AppendAllText("QtErrors.log", "The command '" & command & "' is not found." & vbNewLine)
                    ElseIf errorhandle = "msg" Then
                        Core.msgMbox = "The command '" & command & "' is not found."
                        worker = New Threading.Thread(AddressOf Core.msg3)
                        worker.Start()
                    ElseIf errorhandle = "hide" Then

                    End If
                    i2 += 1
                    QtCont()
                End If
lol:
                If command.EndsWith(".qts") = True Then
                    ReadFile(command)
                Else
                    Console.WriteLine("The command '" & command & "' is not found.")
                    Console.ForegroundColor = ConsoleColor.White
                    QuickInfo.Search(command)
                End If
            End If
        Catch
            Er()
        End Try
        Main2()
    End Sub
#End Region

#Region "Using .dlls"
    Private Declare Function LockWorkStation Lib "user32.dll" () As Long
    Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" _
    (ByVal lpszCommand As String, ByVal lpszReturnString As String, _
    ByVal cchReturnLength As Long, ByVal hwndCallback As Long) As Long
#End Region

#Region "Misc"
    Private Function ReadConfig() As Boolean
        Dim i As UInt16 = 0
        Dim fc() As String
        Try
            If OS = "Unix" Then
                If IO.File.Exists("~/.qtconf") = True Then
                    fc = System.IO.File.ReadAllLines("~/.qtconf")
                ElseIf IO.File.Exists("/etc/.qtconf") = True Then
                    fc = System.IO.File.ReadAllLines("/etc/.qtconf")
                Else
                    fc = System.IO.File.ReadAllLines(".qtconf")
                End If
            Else
                If IO.File.Exists("C:\Windows\System32\.qtconf") = True Then
                    fc = System.IO.File.ReadAllLines("C:\Windows\System32\.qtconf")
                Else
                    fc = System.IO.File.ReadAllLines(".qtconf")
                End If
            End If
            Do Until i = fc.Length
                Dim f As String = fc(i).ToLower
                If f.StartsWith("BackUpImportance: ".ToLower) = True Then
                    BackUpImportance = f.Remove(0, 18)
                ElseIf f.StartsWith("#") = True Then
                    'foo
                ElseIf f.StartsWith("--- # files to backup") = True Then
                    i += 1
                    Do Until f.StartsWith("- ") = False
                        Filestobackup.Add(f.Remove(0, 2))
                        i += 1
                    Loop
                ElseIf f.StartsWith("BackUpLocation: ".ToLower) = True Then
                    BackUpLocation = f.Remove(0, 16)
                ElseIf f.StartsWith("AllowTelnet: ".ToLower) = True Then
                    If f.Remove(0, 13) = "true" Then
                        AllowTelnet = True
                    ElseIf f.Remove(0, 13) = "false" Then
                        AllowTelnet = False
                    End If
                ElseIf f.StartsWith("EnableLogging: ") = True Then
                    If f.Remove(0, 15) = True Then
                        EnableLogging = True
                    ElseIf f.Remove(0, 15) = False Then
                        EnableLogging = False
                    End If
                ElseIf f.StartsWith("LogFailedLogins: ") = True Then
                    If f.Remove(0, 16) = "true" Then
                        LogFailedLogins = True
                    ElseIf f.Remove(0, 16) = "false" Then
                        LogFailedLogins = False
                    End If
                ElseIf f.StartsWith("LogKicks: ") = True Then
                    If f.Remove(0, 10) = "true" Then
                        LogKicks = True
                    ElseIf f.Remove(0, 10) = "false" Then
                        LogKicks = False
                    End If
                ElseIf f.StartsWith("ServerPort: ") = True Then
                    ServerPort = f.Remove(0, 12)
                ElseIf f.StartsWith("EnableAsciiArt: ") = True Then
                    If f.Remove(0, 16) = "true" Then
                        EnableAsciiArt = True
                    ElseIf f.Remove(0, 16) = "false" Then
                        EnableAsciiArt = False
                    End If
                ElseIf f.StartsWith("TelnetLogLocation: ") = True Then
                    TelnetLogLocation = f.Remove(0, 19)
                ElseIf f.StartsWith("LogCommands: ") = True Then
                    If f.Remove(0, 13) = "true" Then
                        LogCommands = True
                    ElseIf f.Remove(0, 13) = "false" Then
                        LogCommands = False
                    End If
                ElseIf f.StartsWith("TelnetLogName: ") = True Then
                    TelnetLogName = f.Remove(0, 15)
                ElseIf f.StartsWith("TelnetUserName: ") = True Then
                    TelnetUserName = f.Remove(0, 16)
                ElseIf f.StartsWith("TelnetPassword: ") = True Then
                    TelnetPassword = f.Remove(0, 16)
                End If
                i += 1
            Loop
            Return True
        Catch
            Return False
            Er()
        End Try
    End Function

    Private Sub Mario()
        Console.Beep(659, 100)
        Console.Beep(659, 100)
        Thread.Sleep(100)
        Console.Beep(659, 125)
        Thread.Sleep(137)
        Console.Beep(523, 125)
        Console.Beep(659, 125)
        Thread.Sleep(125)
        Console.Beep(784, 125)
    End Sub

    Private Function UptoDate() As Boolean
        'Disabled Until my server is back up
        Return True
        'Try
        'Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("EXAMPLE")
        'Dim response As System.Net.HttpWebResponse = request.GetResponse()

        'Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())

        'Dim newestversion As String = sr.ReadToEnd()
        'Dim currentversion As String = ver

        'If newestversion.Contains(currentversion) Then
        'MessageBox.Show("You have the current version")
        'Return True
        'Else
        'Return False
        'End If
        'Catch
        'Er()
        'Return Nothing
        'End Try
    End Function

    Private Sub refresh()
        System.Diagnostics.Process.Start("QuickTerm.exe")
        Thread.Sleep(100)
        Environment.Exit(0)
        Exit Sub
    End Sub

    Private Sub Login()
        If AllowTelnet = False Then
            Console.WriteLine("Telnet Server is disabled on this server")
            ReadKey()
            Environment.Exit(0)
        End If
        TLog("(?) Connection Established.")
        Try
            If EnableAsciiArt = False Then
                GoTo a
            End If
            WriteLine()
            Console.WriteLine("                  |ZZzzz>")
            Console.WriteLine("                  |")
            Console.WriteLine("                  |")
            Console.WriteLine("     |ZZzzz>     /^\            |ZZzzz>")
            Console.WriteLine("     |          |~~~|           |")
            Console.WriteLine("     |        |^^^^^^^|        / \")
            Console.WriteLine("    /^\       |[]+    |       |~~~|")
            Console.WriteLine(" |^^^^^^^|    |    +[]|       |   |")
            Console.WriteLine(" |    +[]|/\/\/\/\^/\/\/\/\/|^^^^^^^|")
            Console.WriteLine(" |+[]+   |~~~~~~~~~~~~~~~~~~|    +[]|")
            Console.WriteLine(" |       |  []   /^\   []   |+[]+   |")
            Console.WriteLine(" |   +[]+|  []  || ||  []   |   +[]+|")
            Console.WriteLine(" |[]+    |      || ||       |[]+    |")
            Console.WriteLine(" |_______|------------------|_______|")
            Console.WriteLine("══════════════════════════════════════════")
            Console.WriteLine("Quick Term Server Edition Ver. " & ver)
            Console.WriteLine("══════════════════════════════════════════")
A:
            Dim i As UInt16 = 0
            Dim ran As New Random
            Do Until i >= ran.Next(2, 5)
                WriteLine()
                Console.Write("User: ")
                Dim user As String = ReadLine()
                'Dim moo As Int16 = 4 'for error test
                'moo = moo / 0
                WriteLine()
                Thread.Sleep(100)
                WriteLine()
                Write(user & "'s Password: ")
                Dim Pass As String = ReadLine()
                If TelnetUserName.ToLower = "default" Then
                    TelnetUserName = Environment.UserName
                End If
                'Console.WriteLine("(" & user & ") (" & Pass & ")")
                If user = TelnetUserName Then
                    If Pass = TelnetPassword Then
                        TLog("(?) Correct login.")
                        TelnetRunning = True
                        Console.WriteLine("Welcome to " & user & " on " & My.Computer.Name)
                        'Read()
                        'Clear()
                        WriteLine()
                        back = True
                        Main2()
                    Else
                        i += 1
                    End If
                Else
                    i += 1
                End If
                If LogFailedLogins = True Then
                    If TLog("(!) Invaild login.") = False Then
                        Console.WriteLine("An internal error has occured!")
                        Environment.Exit(10)
                    End If
                End If
                Console.WriteLine("Fail")
            Loop
            WriteLine()
            TLog("(!) Invaild login, client kicked from server.")
            Console.WriteLine("Too many attempts! This has been logged.")
            Environment.Exit(0)
        Catch ex As Exception
            If TLog("(E) " & ex.Message) = False Then
                Console.WriteLine("An internal error has occured!")
                Environment.Exit(10)
            End If
B:
            Console.WriteLine("An internal error has occured!")
            Environment.Exit(10)
        End Try
    End Sub

    Private Function TLog(ByVal Messagee As String) As Boolean
        Try
            If EnableLogging = True Then
                If TelnetLogLocation = "Default" Then
                    TelnetLogLocation = ProgramLocation
                End If
                System.IO.File.AppendAllText(TelnetLogLocation & Slash & TelnetLogName _
                                             , "[" & Date.Now.ToString("hh:mm.ss tt") & "] " & Messagee & vbNewLine)
            Else

            End If
            Return True
        Catch
            Return False
        End Try
    End Function

    Public Sub Er(Optional ByVal ErrorNum As UInt16 = Nothing, Optional ByVal ErrorDesc As String = Nothing, Optional ByVal EndOnFinish As Boolean = False)
        If Not ErrorNum = Nothing AndAlso Not ErrorDesc = Nothing Then
            Err.Number = ErrorNum
            Err.Description = ErrorDesc
        End If
        If Err.Number = 5 AndAlso Err.Description = Nothing AndAlso OS = "Unix" Then
            'stops random error 5 in Linux
            ErrorLog.Add("Error: (" & Err.Number & ") " & Err.Description)
            If Scripting = True Then
                i2 += 1
                QtCont()
            Else
                Main2()
            End If
        End If
        If Err.Number = 91 AndAlso Err.Description = "Error: (91) Object reference not set to an instance of an object." AndAlso OS <> "Unix" Then
            'stops random error 91 in Linux
            ErrorLog.Add("Error: (" & Err.Number & ") " & Err.Description)
            If Scripting = True Then
                i2 += 1
                QtCont()
            Else
                Main2()
            End If
        End If
        ErrorLog.Add("Error: (" & Err.Number & ") " & Err.Description)
        If Scripting = True Then
            If errorhandle = "default" Then
                GoTo a
            ElseIf errorhandle = "log" Then
                System.IO.File.AppendAllText("QtErrors.log", "Error: (" & Err.Number & ") " & Err.Description & vbNewLine)
            ElseIf errorhandle = "msg" Then
                Core.msgMbox = "Error: (" & Err.Number & ") " & Err.Description
                worker = New Threading.Thread(AddressOf Core.msg3)
                worker.Start()
            ElseIf errorhandle = "hide" Then

            End If
            i2 += 1
            QtCont()
        End If
a:
        Console.ForegroundColor = ConsoleColor.Red
        If Err.Number = 0 Or Err.Number = Nothing Then
            Console.Error.WriteLine("Error: (" & Err.Number & ") Internal QuickTerminal error.")
            Console.Error.WriteLine("Please submit a bug report; it would help a lot! Type bug for more info.")
        ElseIf Err.Number = 9 Then
            Console.Error.WriteLine("Error: (9) Missing Args")
        ElseIf Err.Number = 6 AndAlso Err.Description = "Arithmetic operation resulted in an overflow." Then
            Console.Error.WriteLine("Error: (6) The number is too high and resulted in an overflow.")
        ElseIf Err.Description.StartsWith("Index was out of range. Must be non-negative and less than the size") = True Then
            Console.Error.WriteLine("Search may not yet be completed (if this error keeps occuring then turn on auto logging)")
        Else
            Console.Error.WriteLine("Error: (" & Err.Number & ") " & Err.Description)
        End If
        Err.Clear()
        If EndOnFinish = True Then
            Console.ForegroundColor = ConsoleColor.Black
            Read()
            Environment.Exit(1000)
        End If
        If Core.back2 = True Then
            Core.Cmd2()
        End If
        Console.ForegroundColor = ConsoleColor.White
        Main2()
    End Sub
#End Region

#Region "Scripting"
    Public Sub ReadFile(ByVal QtScript As String) 'opens the qts file for reading
        Try
            QtFile = IO.File.ReadAllLines(QtScript)
            Scripting = True 'tells UI that the scripting is true so after running command it goes back to scripting class
            QtScriptFile = QtScript
            IO.File.WriteAllText(ProgramLocation & Slash & "vars.tmp", Nothing) 'overwrites/creates a tmp file to hold vars
            IO.File.SetAttributes(ProgramLocation & Slash & "vars.tmp", IO.FileAttributes.Temporary) 'makes vars a temp file
            QtCont() 'do work
        Catch
            Er(Err.Number, Err.Description)
        End Try
    End Sub

    Public Sub QtCont()
        Try
a:
            Do Until i2 >= QtFile.Length
                'reads lines
                Do Until QtFile(i2).StartsWith(vbTab) = False 'ignores tabs
                    QtFile(i2) = QtFile(i2).Replace(vbTab, "")
                Loop
                If QtFile(i2).StartsWith("//") = True Then 'comment
                    i2 += 1
                    GoTo a
                ElseIf QtFile(i2).StartsWith("/*") = True Then 'multi line comment
                    Do Until QtFile(i2).EndsWith("*/") = True
                        i2 += 1
                    Loop
                    i2 += 1
                    GoTo a
                ElseIf QtFile(i2).StartsWith("FIXME ") = True Then 'fixme for errors (e.x FIXME echo ~moo)
                    i2 += 1
                    GoTo a

                    '<System Vars>
                ElseIf QtFile(i2).Contains("~user") = True AndAlso QtFile(i2).Contains("'~user'") = False Then
                    QtFile(i2) = QtFile(i2).Replace("~user", Environment.UserName)
                    GoTo a 'to check for more vars
                ElseIf QtFile(i2).Contains("~ver") = True AndAlso QtFile(i2).Contains("'~ver'") = False Then
                    QtFile(i2) = QtFile(i2).Replace("~ver", ver) 'Replace ver to the var that contains TTOS ver number
                    GoTo a
                ElseIf QtFile(i2).Contains("~time") = True AndAlso QtFile(i2).Contains("'~time'") = False Then
                    QtFile(i2) = QtFile(i2).Replace("~time", Date.Now.ToString("hh:mm.ss tt"))
                    GoTo a
                ElseIf QtFile(i2).Contains("~backupname") = True AndAlso QtFile(i2).Contains("'~backupname'") = False Then
                    QtFile(i2) = QtFile(i2).Replace("~backupname", Date.Now.ToString(".hh.mm.") & DateTime.Now.ToString("MM.dd.yyyy."))
                    GoTo a
                ElseIf QtFile(i2).Contains("~date") = True AndAlso QtFile(i2).Contains("'~date'") = False Then
                    QtFile(i2) = QtFile(i2).Replace("~date", DateTime.Now.ToString("dddd, MM/dd/yyyy"))
                    GoTo a
                ElseIf QtFile(i2).Contains("~ip") = True AndAlso QtFile(i2).Contains("'~ip'") = False Then
                    QtFile(i2) = QtFile(i2).Replace("~ip", Net.GetLocalIpAddress.ToString)
                    GoTo a
                    '</System Vars>

                    '<User Vars>
                ElseIf IO.File.ReadAllLines(ProgramLocation & Slash & "vars.tmp").Length > 0 AndAlso QtFile(i2).Contains("~") = True AndAlso QtFile(i2).Contains("'~") = False Then
                    Dim temp() As String = IO.File.ReadAllLines(ProgramLocation & "" & Slash & "vars.tmp")
                    If temp.Length > 65535 Then
                        Console.ForegroundColor = ConsoleColor.Red
                        Console.WriteLine("Error! You can not have more then 65535 vars!")
                        ClearVars()
                        Main2() 'goes back to the UI
                    End If
                    Dim ii As UInt16 = 0
                    Dim davars() As String = Split(QtFile(i2), "~")
                    Do Until ii = temp.Length
                        If temp(ii).StartsWith(davars(1).Trim & "▬") = True Then
                            Dim tempp() As String = Split(temp(ii), "▬")
                            QtFile(i2) = QtFile(i2).Replace("~" & davars(1).Trim, tempp(1))
                            GoTo a
                        End If
                        ii += 1
                    Loop
                Else
                    'if it isnt a var then go to the commands
                    GoTo b
                End If
                '</User Vars>
b:
                'commands
                If QtFile(i2) = Nothing Then 'if line is empty then
                    i2 += 1 'goes to next line
                    GoTo a 'checks for vars
                ElseIf QtFile(i2).StartsWith("#e.") = True Then 'handles errors
                    If QtFile(i2) = "#e.hide" Then
                        errorhandle = "hide"
                    ElseIf QtFile(i2) = "#e.msg" Then
                        errorhandle = "msg"
                    ElseIf QtFile(i2) = "#e.log" Then
                        errorhandle = "log"
                    ElseIf QtFile(i2) = "#e.default" Then
                        errorhandle = "default"
                    End If
                ElseIf QtFile(i2).StartsWith("#skipclear") = True Then 'Stops QT from clearing variables
                    SkipClear = True
                ElseIf QtFile(i2) = "#invisable" Then 'Runs the script in the background
                    If invisible = False Then
                        Shell("QuickTerm.exe -i -s " & QtScriptFile, AppWinStyle.Hide)
                    End If
                    i2 += 1
                    GoTo a
                ElseIf QtFile(i2).StartsWith("#r.") = True Then 'Finish
                    If QtFile(i2) = "#r.msg" Then
                        replyhandle = "msg"
                    ElseIf QtFile(i2) = "#r.log" Then
                        replyhandle = "log"
                    End If
                ElseIf QtFile(i2).StartsWith(":") = True Then
                    'foo
                ElseIf QtFile(i2).StartsWith("goto ") = True Then
                    Dim temp = QtFile(i2).Remove(0, 5)
                    i2 = 0
                    Do Until QtFile(i2) = ":" & temp
                        i2 += 1
                    Loop
                    i2 += 1
                    GoTo a
                ElseIf QtFile(i2).StartsWith("title ") = True Then
                    Console.Title = QtFile(i2).Remove(0, 6)
                ElseIf QtFile(i2) = "do" Then 'do loop forever
                    loopp = i2
                ElseIf QtFile(i2).StartsWith("do ") = True Then 'do loop for specific ammount
                    looppfor = QtFile(i2).Remove(0, 3)
                    loopp = i2
                ElseIf QtFile(i2) = "loop" Then 'end of a loop
                    If looppfor <> Nothing Then
                        If loopped = looppfor Then
                            i2 += 1
                            GoTo a
                        Else
                            loopped += 1
                            i2 = loopp
                        End If
                    Else
                        i2 = loopp
                    End If
                ElseIf QtFile(i2).StartsWith("if ") = True Then 'if statement
                    QtFile(i2) = QtFile(i2).Remove(0, 3) 'removes "if "
                    Dim newReplace = QtFile(i2).Replace(" = ", "■") 'so a string can contain an equals
                    Dim statement() As String = newReplace.Split("■") 'splits vars to tell them apart
                    Dim s1 = statement(0).ToString() '1st var
                    Dim s2 = statement(1).ToString() '2nd var
                    If s1.ToString() = s2.ToString() Then 'if var1 == var2
                        i2 += 1 'continue running code
                        GoTo a 'top of code
                    Else 'if var1 =/= var2
                        Do Until QtFile(i2) = "end if" 'ignore code until end if
                            If QtFile(i2) = "else" Then 'or ignore code until an else is found
                                i2 += 1 'goes to next line
                                GoTo a 'top of code
                            End If
                            i2 += 1
                        Loop
                    End If
                ElseIf QtFile(i2) = "end if" Then
                    'foo
                ElseIf QtFile(i2) = "else" Then
                    Do Until QtFile(i2) = "end if" 'ignore code until end if
                        i2 += 1
                    Loop
                    GoTo a
                ElseIf QtFile(i2).StartsWith("input ") = True Then
                    Dim ytemp As String = QtFile(i2).Remove(0, 6)
                    ytemp = ytemp.Replace(" ", "")
                    Console.ForegroundColor = ConsoleColor.Cyan
                    Console.Write("> ")
                    Console.ForegroundColor = ConsoleColor.Gray
                    Dim xtemp As String = ReadLine()
                    Console.ForegroundColor = ConsoleColor.White
                    IO.File.AppendAllText(ProgramLocation & Slash & "vars.tmp", ytemp & "▬" & xtemp & vbNewLine)
                ElseIf QtFile(i2).StartsWith("dim ") = True Then 'creates vars
                    QtFile(i2) = QtFile(i2).Remove(0, 4)
                    QtFile(i2) = QtFile(i2).Replace(" = ", "▬")
                    Dim temp() As String = Split(QtFile(i2), "▬")
                    IO.File.AppendAllText(ProgramLocation & Slash & "vars.tmp", QtFile(i2) & vbNewLine)
                ElseIf QtFile(i2).Contains("~random") = True AndAlso QtFile(i2).Contains("'~random'") = False Then 'random number gen
                    Dim ran As New Random
                    QtFile(i2) = QtFile(i2).Replace("~random", ran.Next(1000)) 'replaces with a random number between 1 - 1000
                ElseIf QtFile(i2) = "break" Then 'like pause
                    Console.Write("Press any key to continue...")
                    Console.ForegroundColor = ConsoleColor.Black
                    ReadKey()
                    WriteLine()
                    Console.ForegroundColor = ConsoleColor.White
                ElseIf QtFile(i2) = "pause" Then 'like pause in cmd
                    Console.Write("Press any key to continue...")
                    Console.ForegroundColor = ConsoleColor.Black
                    ReadKey()
                    WriteLine()
                    Console.ForegroundColor = ConsoleColor.White
                ElseIf QtFile(i2).StartsWith("wait ") = True Then 'sleeps
                    Threading.Thread.Sleep(QtFile(i2).Remove(0, 5))
                ElseIf QtFile(i2) = "refresh" Then
                    Console.WriteLine("Cant do that in scripting!")
                ElseIf QtFile(i2).StartsWith("write") = True Then
                    Console.WriteLine("Cant do that in scripting! Use qwrite instead!")
                Else
                    Threading.Thread.Sleep(100) 'to help prevent stack overflows
                    Commands(QtFile(i2)) 'goes to the UI and does the command that is in the line
                End If
                i2 += 1
            Loop
        Catch
            Console.ForegroundColor = ConsoleColor.Red
            Console.Write("(")
            Console.ForegroundColor = ConsoleColor.White
            Console.Write("!")
            Console.ForegroundColor = ConsoleColor.Red
            Console.Write(") Script Stopped! Error on line ")
            Console.ForegroundColor = ConsoleColor.White
            WriteLine(i2 + 1)
            ClearVars() 'allows for more scripts to be run later
            Er(Err.Number, Err.Description)
        End Try
        If SkipClear = False Then
            ClearVars()
        End If
        Main2() 'goes back to the UI
    End Sub

    Public Function ClearVars()
        'refreshes all vars back to default values
        i2 = 0
        QtScriptFile = Nothing
        errorhandle = "default"
        replyhandle = "default"
        invisible = False
        Scripting = False
        QtFile = Nothing
        loopp = Nothing
        loopped = 1
        looppfor = Nothing
        If IO.File.Exists(ProgramLocation & Slash & "vars.tmp") = True Then
            Dim worker As New Thread(AddressOf QIO.SecureDelete)
            'deletes vars.tmp
            Filee = ProgramLocation & Slash & "vars.tmp"
            nomsg = True
            worker.Priority = ThreadPriority.Highest
            On Error Resume Next
            worker.Start()
        End If
        Return "Done"
    End Function
#End Region

End Module
