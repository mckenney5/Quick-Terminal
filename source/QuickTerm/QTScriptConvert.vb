'Converts other scripting langs. to QTS
'Not finished
Imports System.IO
Imports System.Console
Public Class QTScriptConvert
    Public Sub ConvertScript(ByVal Filename As String, ByVal FileType As String)
        Try
            Filename = Environment.CurrentDirectory & "\" & Filename
            If IO.File.Exists(Filename) = False Then
                WriteLine("File not found")
                Main()
            End If
            If FileType = "batch" Then
                Batch(Filename)
            ElseIf FileType = "sh" Or FileType = "shell" Then
                Sh(Filename)
            ElseIf FileType = "bash" Then
                Bash(Filename)
            Else
                WriteLine("File type not supported")
                Main()
            End If
        Catch
            Er(Err.Number, Err.Description)
        End Try
        Main()
    End Sub

    Private Sub Batch(ByVal Filename As String)
        Try
            Dim i As UInteger = 0
            Dim fc() = File.ReadAllLines(Filename)
            Do Until i = fc.Length
                If fc(i) = "@echo off" Then
                    fc(i) = ""
                ElseIf fc(i).StartsWith("echo") = True Then

                End If
                i += 1
            Loop
        Catch
            Er(Err.Number, Err.Description)
        End Try
    End Sub

    Private Sub Sh(ByVal Filename As String)
        Try
            Dim i As UInteger = 0
            Dim fc() = File.ReadAllLines(Filename)
            Do Until i = fc.Length

                i += 1
            Loop
        Catch
            Er(Err.Number, Err.Description)
        End Try
    End Sub

    Private Sub Bash(ByVal Filename As String)
        Try
            Dim i As UInteger = 0
            Dim fc() = File.ReadAllLines(Filename)
            Do Until i = fc.Length

                i += 1
            Loop
        Catch
            Er(Err.Number, Err.Description)
        End Try
    End Sub
End Class
