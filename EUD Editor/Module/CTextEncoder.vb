Imports System.Text.RegularExpressions

Module CTextEncoder
    Public Function CTextEncode(str As String) As String

        Dim codes As String = GetChar(str, "{P\d_D_\d+}")
        While (codes <> "0")
            Dim UnitNum As Integer = GetGroup(str, "P(\d)_D_(\d+)")(2)
            Dim PlayerNum As Integer = GetGroup(str, "P(\d)_D_(\d+)")(1) - 1


            str = Replace(str, codes, """, dwread_epd(" & UnitNum & " * 12 + " & PlayerNum & "), """,, 1)
            codes = GetChar(str, "{P\d_D_\d+}")
        End While
        codes = GetChar(str, "{P\d_K_\d+}")
        While (codes <> "0")
            Dim UnitNum As Integer = GetGroup(str, "P(\d)_D_(\d+)")(2)
            Dim PlayerNum As Integer = GetGroup(str, "P(\d)_D_(\d+)")(1) - 1

            str = Replace(str, codes, """, dwread_epd(EPD(0x5878A4) + " & UnitNum & " * 12 + " & PlayerNum & "), """,, 1)
            codes = GetChar(str, "{P\d_K_\d+}")
        End While

        For i = 0 To 7
            str = str.Replace("{P" & i + 1 & "_N}", """, tct.str(0x57EEEB + 36 * " & i & "), """)
        Next
        For i = 0 To 7
            str = str.Replace("{P" & i + 1 & "_O}", """, dwread_epd(EPD(0x57F0F0) + " & i & "), """)
        Next
        For i = 0 To 7
            str = str.Replace("{P" & i + 1 & "_G}", """, dwread_epd(EPD(0x57F120) + " & i & "), """)
        Next
        codes = GetChar(str, "{P\d_S_\d}")
        While (codes <> "0")
            Dim Index As Integer = GetGroup(str, "P(\d)_S_(\d+)")(2)
            Dim PlayerNum As Integer = GetGroup(str, "P(\d)_S_(\d+)")(1) - 1

            Dim offset As String = ""

            Select Case Index
                Case 0
                    offset = "0x582144"
                Case 1
                    offset = "0x582174"
                Case 2
                    offset = "0x5821A4"
                Case 3
                    offset = "0x5821D4"
                Case 4
                    offset = "0x582204"
                Case 5
                    offset = "0x582234"
                Case 6
                    offset = "0x582264"
                Case 7
                    offset = "0x582294"
                Case 8
                    offset = "0x5822C4"
            End Select


            str = Replace(str, codes, """, dwread_epd(EPD(" & offset & ") + " & PlayerNum & "), """,, 1)
            codes = GetChar(str, "{P\d_S_\d}")
        End While
        For i = 0 To 7
            str = str.Replace("{GT}", """, dwread_epd(EPD(0x57F23C)) / 24, """)
        Next


        codes = GetChar(str, "{C:[^}]+}")
        While (codes <> "0")
            Dim ValueName As String = GetGroup(str, "{C:([^}]+)}")(1)


            str = Replace(str, codes, """, " & ValueName & ", """,, 1)
            codes = GetChar(str, "{C:[^}]+}")
        End While



        '5878A4
        Return str
    End Function


    Private Function GetChar(str As String, pattern As String) As String
        Dim rgx As New Regex(pattern, RegexOptions.IgnoreCase)

        If rgx.Match(str).Success = True Then
            Return rgx.Match(str).Value
        End If
        Return rgx.Match(str).Index
    End Function
    Private Function GetGroup(str As String, pattern As String) As List(Of String)
        Dim rgx As New Regex(pattern, RegexOptions.IgnoreCase)

        Dim returnlist As New List(Of String)

        If rgx.Match(str).Success = True Then
            For Each values As Group In rgx.Match(str).Groups
                returnlist.Add(values.Value)
            Next
            Return returnlist
        End If
        Return returnlist
    End Function


    Private Function Getindex(str As String) As Integer
        Dim pattern As String = "<\w+>"
        Dim rgx As New Regex(pattern, RegexOptions.IgnoreCase)


        Return rgx.Match(str).Index
    End Function
End Module
