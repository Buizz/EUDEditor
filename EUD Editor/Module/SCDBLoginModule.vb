Module SCDBLoginModule
    Private Function StrEncrpty(str As String, count As Byte) As String
        Dim strcount As Integer = str.Length

        Dim strbulider As New System.Text.StringBuilder

        'azcd
        If strcount Mod 2 = 0 Then
            For i = 0 To strcount / 2 - 1
                strbulider.Append(str(i))
                strbulider.Append(str(strcount - i - 1))
            Next
        Else
            For i = 0 To strcount / 2 - 1
                strbulider.Append(str(i))
                strbulider.Append(str(strcount - i - 1))
            Next
            strbulider.Append(str(Math.Floor(strcount / 2)))
        End If

        If count <> 0 Then
            Return StrEncrpty(strbulider.ToString, count - 1)
        Else
            Return strbulider.ToString
        End If
    End Function
    Private Function StrDecrypt(str As String, count As Byte) As String
        Dim strcount As Integer = str.Length

        Dim strbulider As New System.Text.StringBuilder

        'azcd
        If strcount Mod 2 = 0 Then
            For i = 0 To strcount / 2 - 1
                strbulider.Append(str(i * 2))
            Next
            For i = 0 To strcount / 2 - 1
                strbulider.Append(str(strcount - i * 2 - 1))
            Next
        Else
            For i = 0 To strcount / 2 - 1
                strbulider.Append(str(i * 2))
            Next
            strbulider.Append(str(strcount - 1))
            For i = 0 To strcount / 2 - 1
                strbulider.Append(str(strcount - (i + 1) * 2))
            Next
        End If

        If count <> 0 Then
            Return StrDecrypt(strbulider.ToString, count - 1)
        Else
            Return strbulider.ToString
        End If
    End Function

    Private Const Key As String = "Public Function StripNullCharacters(ByVal vstrStringWithNulls As String) As String"
    Private Function Encrpty(str As String) As String
        Return EncryptString128Bit(StrEncrpty(str, str.Count / 2), Key)
    End Function
    Private Function Decrypt(str As String) As String
        Return StrDecrypt(DecryptString128Bit(str, Key), DecryptString128Bit(str, Key).Count / 2)
    End Function


    Private userlist As New List(Of User)
    Private Structure User
        Public name As String
        Public Password As String
        Public serial As String

        Public Sub New(n As String, p As String, s As String)
            name = n
            Password = p
            serial = s
        End Sub
    End Structure
    Public Function SCDBGetuserData()
        userlist.Clear()
        Dim data As String


        With CreateObject("WinHttp.WinHttpRequest.5.1")
            .Open("GET", "http://blog.naver.com/PostView.nhn?blogId=sksljh2091&logNo=221232271109")
            .Send
            .WaitForResponse

            data = .ResponseText

            Dim reges As New Text.RegularExpressions.Regex("(DATA{)(.*)(})")

            Dim text As String
            Try
                text = Decrypt(reges.Match(data).Groups(2).Value.Split(";")(1).Trim)
            Catch ex As Exception
                text = Decrypt(reges.Match(data).Groups(2).Value.Trim)
            End Try



            Dim templist As List(Of String) = text.Split(",").ToList

            For i = 0 To (templist.Count / 3) - 1
                userlist.Add(New User(templist(i * 3), templist(i * 3 + 1), templist(i * 3 + 2)))
            Next
        End With


        Return True
    End Function

    Public Function SCDBCheckID(ID As String) As Boolean
        For i = 0 To userlist.Count - 1
            If userlist(i).name = ID Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function SCDBCheckPW(ID As String, Pw As String) As Boolean
        For i = 0 To userlist.Count - 1
            If userlist(i).name = ID And userlist(i).Password = Pw Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function SCDBGetSerial(ID As String, Pw As String) As String
        For i = 0 To userlist.Count - 1
            If userlist(i).name = ID And userlist(i).Password = Pw Then
                Return userlist(i).serial
            End If
        Next
        Return 0
    End Function
End Module
