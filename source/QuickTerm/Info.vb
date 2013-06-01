Imports System.Console
Public Class Info
    Public Sub BetaCommands()
        WriteLine("Everything not in this list should* work 100%")
        WriteLine("VERSION: " & QuickTerminal.ver & vbNewLine _
                         & "con                 (connects to a server on a specified port)" & vbNewLine _
                         & "crawl thread        (toggles multi-threading) Bugged" & vbNewLine _
                         & "rdns                (looks up an IP for aliases)" & vbNewLine _
                         )
        Main()
    End Sub

    Public Sub Credits()
        WriteLine("Credits")
        WriteLine("This whole program is created by Adam McKenney with the exception of")
        WriteLine("update, lock and IP (all of their Sources is Unknown)" & vbNewLine & "and dir & ls command found at:")
        WriteLine("http://www.developerfusion.com/code/3681/list-files-in-a-directory/")
        WriteLine("Crawl found at:")
        WriteLine("http://tech.reboot.pro/showthread.php?tid=320")
        WriteLine("Zip file creater found at:")
        WriteLine("http://www.codeproject.com/Articles/28107/Zip-Files-Easy")
        Main()
    End Sub

    Public Sub Privacy()
        WriteLine("Privacy")
        WriteLine("This whole program is designed to keep your information a secret.")
        WriteLine("We DO NOT sell your information, nor do we keep it.")
        WriteLine("Information sent includes: your IP if you're not using a proxy")
        WriteLine("(your IP is used in crawl and connect and or any thing that uses a tcp connection)")
        WriteLine("and your port scan is logged to the computer, if you want to delete it, I suggest using delete3 command.")
        Main()
    End Sub

    Public Sub Syntax()
        WriteLine("Only displaying commands that require a syntax")
        WriteLine("VERSION: " & QuickTerminal.ver & vbNewLine _
                         & "cap <IMG type> (IMG type= png, jpg, gif, bmp)" & vbNewLine _
                         & "cd <Directory>" & vbNewLine _
                         & "copy <File>|<Location> (File= file you want to copy)" & vbNewLine _
                         & "crawl (see 'crawl help')" & vbNewLine _
                         & "ddos (see 'ddos help')" & vbNewLine _
                         & "dec <filename> (filename= a text file [Optional])" & vbNewLine _
                         & "define <word> (a word to look up)" & vbNewLine _
                         & "delete <filename> (filename= file on computer)" & vbNewLine _
                         & "delete2 <filename> (filename= file on computer)" & vbNewLine _
                         & "delete3 <filename> (filename= file on computer)" & vbNewLine _
                         & "dns <Address> (Address= website address)" & vbNewLine _
                         & "download <WebAddress> <FileName>" & vbNewLine _
                         & "echo <input> (input= string)" & vbNewLine _
                         & "firewall <Enable/Disable>" & vbNewLine _
                         & "kill <program_name> (porgram_name= a running program)" & vbNewLine _
                         & "move <File>|<Location> (File= file you want to move)" & vbNewLine _
                         & "msg <message> (message= message as string)" & vbNewLine _
                         & "ping <Server> (Server= Web Address or IP)" & vbNewLine _
                         & "prompt <Prompt String>" & vbNewLine _
                         & "qwrite <File>|<Text> (File= file name, Text= text the file will contain)" & vbNewLine _
                         & "random <Lowest> <Highest> (Low/High= numbers for random num gen.)" & vbNewLine _
                         & "read <filename> (filename= file on computer)" & vbNewLine _
                         & "run <filename> (filename= file on computer)" & vbNewLine _
                         & "sc <IMG Name> (IMG name= ScreenShot)" & vbNewLine _
                         & "scan <Target> <Start> <End> (target= server start/end = port)" & vbNewLine _
                         & "timer <On/Off>" & vbNewLine _
                         & "upload <WebAddress> <FileName>" & vbNewLine _
                         & "write <filename> (filename= file on computer)" & vbNewLine _
                         & "zip <zipfile>|<File-To-Archive>" & vbNewLine _
                         )
        Main()
    End Sub

    Public Sub Search(ByVal Term As String)
        If Term.StartsWith("bet") = True Then
            WriteLine("Did you mean beta?")
        ElseIf Term.StartsWith("cd") = True Then
            WriteLine("Did you mean cd?")
            WriteLine("Did you mean cdtray?")
        ElseIf Term.StartsWith("cl") = True Then
            WriteLine("Did you mean cls or clear?")
        ElseIf Term.StartsWith("ca") = True Then
            WriteLine("Did you mean cap?")
        ElseIf Term.StartsWith("cra") = True Then
            WriteLine("Did you mean crawl?")
        ElseIf Term.StartsWith("cre") = True Then
            WriteLine("Did you mean cred or credits?")
        ElseIf Term.StartsWith("cm") = True Then
            WriteLine("Did you mean cmd?")
        ElseIf Term.StartsWith("co") = True Then
            WriteLine("Did you mean copy?")
        ElseIf Term.StartsWith("dat") = True Then
            WriteLine("Did you mean date?")
        ElseIf Term.StartsWith("dd") = True Then
            WriteLine("Did you mean ddos?")
        ElseIf Term.StartsWith("dex") = True Or Term.StartsWith("ded") = True Then
            WriteLine("Did you mean dec or decrypt?")
        ElseIf Term.StartsWith("del") = True AndAlso Term.Contains("2") = True Then
            WriteLine("Did you mean delete2?")
        ElseIf Term.StartsWith("del") = True AndAlso Term.Contains("3") = True Then
            WriteLine("Did you mean delete3?")
        ElseIf Term.StartsWith("del") = True AndAlso Term.Contains("2") = False Or Term.StartsWith("del") = True AndAlso Term.Contains("3") = False Then
            WriteLine("Did you mean delete?")
        ElseIf Term.StartsWith("deb") = True Then
            WriteLine("Did you mean dev?")
        ElseIf Term.StartsWith("di") = True Then
            WriteLine("Did you mean dir?")
        ElseIf Term.StartsWith("dn") = True Then
            WriteLine("Did you mean dns?")
        ElseIf Term.StartsWith("dow") = True Then
            WriteLine("Did you mean download?")
        ElseIf Term.StartsWith("ech") = True Then
            WriteLine("Did you mean echo?")
        ElseIf Term.StartsWith("en") = True Then
            WriteLine("Did you mean enc or encrypt?")
        ElseIf Term.StartsWith("er") = True Then
            WriteLine("Did you mean errors?")
        ElseIf Term.StartsWith("ex") = True Then
            WriteLine("Did you mean exit?")
        ElseIf Term.StartsWith("fir") = True Then
            WriteLine("Did you mean firewall?")
        ElseIf Term.StartsWith("fol") = True Then
            WriteLine("Did you mean folder?")
        ElseIf Term.StartsWith("hel") = True Then
            WriteLine("Did you mean help?")
        ElseIf Term.StartsWith("inf") = True Then
            WriteLine("Did you mean info?")
        ElseIf Term.StartsWith("ins") = True Then
            WriteLine("Did you mean install?")
        ElseIf Term.StartsWith("i[") = True Then
            WriteLine("Did you mean ip?")
        ElseIf Term.StartsWith("ki") = True Then
            WriteLine("Did you mean kill?")
        ElseIf Term.StartsWith("loc") = True Then
            WriteLine("Did you mean lock?")
        ElseIf Term.StartsWith("log") = True Then
            WriteLine("Did you mean logoff?")
        ElseIf Term.StartsWith("lk") = True Then
            WriteLine("Did you mean ls?")
        ElseIf Term.StartsWith("ms") = True Then
            WriteLine("Did you mean msg?")
        ElseIf Term.StartsWith("mo") = True Then
            WriteLine("Did you mean move?")
        ElseIf Term.StartsWith("md") = True Then
            WriteLine("Did you mean md5?")
            WriteLine("Did you mean md5log?")
        ElseIf Term.StartsWith("p") = True Then
            WriteLine("Did you mean pi?")
            WriteLine("Did you mean pi2?")
        ElseIf Term.StartsWith("pin") = True Then
            WriteLine("Did you mean ping?")
        ElseIf Term.StartsWith("pr") = True Then
            WriteLine("Did you mean prompt?")
        ElseIf Term.StartsWith("pw") = True Then
            WriteLine("Did you mean pwd?")
        ElseIf Term.StartsWith("qw") = True Then
            WriteLine("Did you mean qwrite?")
        ElseIf Term.StartsWith("ra") = True Then
            WriteLine("Did you mean ran or random?")
        ElseIf Term.StartsWith("res") = True Then
            WriteLine("Did you mean restart?")
        ElseIf Term.StartsWith("rea") = True Then
            WriteLine("Did you mean read?")
        ElseIf Term.StartsWith("ref") = True Then
            WriteLine("Did you mean refresh?")
        ElseIf Term.StartsWith("rem") = True Then
            WriteLine("Did you mean remove?")
        ElseIf Term.StartsWith("ru") = True Then
            WriteLine("Did you mean run?")
        ElseIf Term.StartsWith("sf") = True Then
            WriteLine("Did you mean sc?")
        ElseIf Term.StartsWith("sca") = True Then
            WriteLine("Did you mean scan?")
        ElseIf Term.StartsWith("sea") = True Then
            WriteLine("Did you mean search?")
        ElseIf Term.StartsWith("s[") = True Then
            WriteLine("Did you mean sp?")
        ElseIf Term.StartsWith("sy") = True Then
            WriteLine("Did you mean syntax?")
        ElseIf Term.StartsWith("tel") = True AndAlso Term.Contains("2") = True Then
            WriteLine("Did you mean telnet2?")
        ElseIf Term.StartsWith("tel") = True AndAlso Term.Contains("2") = False Then
            WriteLine("Did you mean telnet?")
        ElseIf Term.StartsWith("tim") = True Then
            WriteLine("Did you mean time?")
            WriteLine("Did you mean timer?")
        ElseIf Term.StartsWith("up") = True Then
            WriteLine("Did you mean upload?")
        ElseIf Term.StartsWith("ve") = True Then
            WriteLine("Did you mean ver?")
        ElseIf Term.StartsWith("vs") = True Then
            WriteLine("Did you mean vscan?")
        ElseIf Term.StartsWith("who") = True Then
            WriteLine("Did you mean whoami?")
        ElseIf Term.StartsWith("wri") = True Then
            WriteLine("Did you mean write?")
        ElseIf Term.StartsWith("z") = True Then
            WriteLine("Did you mean zip?")
        End If
        Main()
    End Sub

    Public Sub Help()
        WriteLine("VERSION: " & QuickTerminal.ver & vbNewLine _
                                  & "beta                (shows beta commands, use the commands with caution)" & vbNewLine _
                                  & "cap                 (captures QuickTerm)" & vbNewLine _
                                  & "cd                  (changes current directory)" & vbNewLine _
                                  & "cdtray              (opens/closes cd tray)" & vbNewLine _
                                  & "cls                 (clears screan)" & vbNewLine _
                                  & "clear               (clears screan)" & vbNewLine _
                                  & "crawl               (searches a webpage for links/emails)" & vbNewLine _
                                  & "cred                (shows credits and sources)" & vbNewLine _
                                  & "cmd                 (runs a shell command)" & vbNewLine _
                                  & "copy                (copys a file to another location)" & vbNewLine _
                                  & "date                (displays date)" & vbNewLine _
                                  & "ddos                (stress tests a network)" & vbNewLine _
                                  & "dec                 (decrypts a string with AES)" & vbNewLine _
                                  & "define              (looks up a word via free dictionary)" & vbNewLine _
                                  & "delete              (deletes a file)" & vbNewLine _
                                  & "delete2             (deletes a file without freezing console)" & vbNewLine _
                                  & "delete3             (deletes and overwrites 16 times)" & vbNewLine _
                                  & "dev                 (shows the dev log)" & vbNewLine _
                                  & "dir                 (list files in the local directory)" & vbNewLine _
                                  & "dns                 (does a dns look up of a server)" & vbNewLine _
                                  & "download            (downloads a file via internet)" & vbNewLine _
                                  & "echo                (echos what you type)" & vbNewLine _
                                  & "enc                 (encrypts a string with AES)" & vbNewLine _
                                  & "errors              (displays all errors that has happened)" & vbNewLine _
                                  & "exit                (closes the program)" & vbNewLine _
                                  & "firewall            (controls windows firewall, may require admin)" & vbNewLine _
                                  & "folder              (creates a new folder)" & vbNewLine _
                                  & "help                (this stuff)" & vbNewLine _
                                  & "info                (shows computer info)" & vbNewLine _
                                  & "install             (makes .qts files be owned by QuickTerm)" & vbNewLine _
                                  & "ip                  (displays local IPv4 Address)" & vbNewLine _
                                  & "kill                (ends a procsess)" & vbNewLine _
                                  & "lock                (locks computer)" & vbNewLine _
                                  & "logoff              (logs off computer)" & vbNewLine _
                                  & "ls                  (list files in the local directory)" & vbNewLine _
                                  & "md5                 (shows the md5 hash of a file)" & vbNewLine _
                                  & "md5log              (logs all md5 hashes to a file in the current dir)" & vbNewLine _
                                  & "move                (moves a file to another location)" & vbNewLine _
                                  & "msg                 (shows a message box)" & vbNewLine _
                                  & "pi                  (prints pi as a Decimal to 28 places)" & vbNewLine _
                                  & "pi2                 (prints pi as a string to 500 places)" & vbNewLine _
                                  & "ping                (pings a server with 32 bytes of data)" & vbNewLine _
                                  & "prompt              (change default prompt string '==> ' with a user defined one)" & vbNewLine _
                                  & "pwd                 (prints the working directory)" & vbNewLine _
                                  & "qwrite              (quicky writes a text file)" & vbNewLine _
                                  & "random              (generates random number between 1 - 1000)" & vbNewLine _
                                  & "read                (opens an reads a text file)" & vbNewLine _
                                  & "refresh             (closes current console then opens a new one)" & vbNewLine _
                                  & "remove              (removes a directory)" & vbNewLine _
                                  & "restart             (restarts computer)" & vbNewLine _
                                  & "run                 (runs a program)" & vbNewLine _
                                  & "sc                  (captures the whole screen and saves as .png)" & vbNewLine _
                                  & "scan                (scans a computer for open ports)" & vbNewLine _
                                  & "search              (searches the web via duckduckgo)" & vbNewLine _
                                  & "sp                  (displays current port being scanned)" & vbNewLine _
                                  & "syntax              (displays command syntax)" & vbNewLine _
                                  & "telnet              (uses program nc 'net cat' for t-server)" & vbNewLine _
                                  & "telnet2             (uses program ncat for t-server)" & vbNewLine _
                                  & "test                (for debugging only)" & vbNewLine _
                                  & "time                (displays current time)" & vbNewLine _
                                  & "timer               (starts/stops a timer and displays time in seconds)" & vbNewLine _
                                  & "upload              (uploads a file via internet)" & vbNewLine _
                                  & "ver                 (current program version)" & vbNewLine _
                                  & "vscan               (scans the current dir for viruses)" & vbNewLine _
                                  & "whoami              (tells you who you are, very simular to bash)" & vbNewLine _
                                  & "write               (creates and edits a text file [OVERWRITES])" & vbNewLine _
                                  & "zip                 (creates a zip archive)" & vbNewLine _
                                  )
        Main()
    End Sub
End Class
