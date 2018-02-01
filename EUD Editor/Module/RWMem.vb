Imports System
Imports System.Runtime.InteropServices
Imports System.IO

Namespace WinAPI
    Public Class MemoryReader
        Private bytes() As Byte
        Private StartPos As UInteger
        Private Memstream As MemoryStream
        Private binaryreader As BinaryReader


        Private buffermaxsize As UInteger = &H4FFFF

        Public Sub New(point As UInteger)
            bytes = ReadValue(point, buffermaxsize, True)
            StartPos = point

            Memstream = New MemoryStream(bytes)
            binaryreader = New BinaryReader(Memstream)
            If bytes(0) = 0 Then
                MsgBox("실패")
            End If
            'binaryreader.ReadUInt16)

        End Sub
        Protected Overrides Sub Finalize()
            binaryreader.Close()
            Memstream.Close()
        End Sub

        Public Position As UInteger
        Public Function ReadByte() As Byte
            If StartPos <= Position And Position < StartPos + buffermaxsize - 1 Then
                Memstream.Position = Position - StartPos
                Dim value As Byte = binaryreader.ReadByte
                Position += 1
                Return value
            Else
                Dim value As Byte = ReadValue(Position, 1)
                Position += 1
                Return value
            End If
        End Function


        Public Function ReadBytes(count As UInteger) As Byte()
            If StartPos <= Position And Position < StartPos + buffermaxsize - count Then
                Memstream.Position = Position - StartPos
                Dim value() As Byte = binaryreader.ReadBytes(count)
                Position += count
                Return value
            Else
                Dim value() As Byte = ReadValue(Position, count, True)
                Position += count
                Return value
            End If
        End Function


        Public Function ReadUInt16() As UInt16
            If StartPos <= Position And Position < StartPos + buffermaxsize - 2 Then
                Memstream.Position = Position - StartPos
                Dim value As UInt16 = binaryreader.ReadUInt16
                Position += 2
                Return value
            Else
                Dim value As UInt16 = ReadValue(Position, 2)
                Position += 2
                Return value
            End If
        End Function

        Public Function ReadUInt32() As UInt32
            If StartPos <= Position And Position < StartPos + buffermaxsize - 4 Then
                Memstream.Position = Position - StartPos
                Dim value As UInt32 = binaryreader.ReadUInt32
                Position += 4
                Return value
            Else
                Dim value As UInt32 = ReadValue(Position, 4)
                Position += 4
                Return value
            End If
        End Function
    End Class


    Module RWMem

        Private Declare Function RtlAdjustPrivilege Lib "ntdll" (ByVal Privilege As Long, ByVal bEnablePribilege As Long, ByVal IsThreadPrivilege As Long, ByRef PreviousValue As Long) As Long

        Public Sub AdjustToken()
            RtlAdjustPrivilege(20, 1, 0, 0&)
        End Sub



        Private Declare Function GetWindowRect Lib "user32" Alias "GetWindowRect" (ByVal hwnd As Integer, ByRef lpRect As Rectangle) As Integer


        Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwProcessId As Integer) As Integer
        Private Declare Function WriteProcessMemory Lib "kernel32" Alias "WriteProcessMemory" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByRef lpBuffer As Byte, ByVal nSize As UInteger, ByRef lpNumberOfBytesWritten As Integer) As Integer
        Private Declare Function ReadProcessMemory Lib "kernel32" Alias "ReadProcessMemory" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByRef lpBuffer As Byte, ByVal nSize As UInteger, ByRef lpNumberOfBytesWritten As Integer) As Integer
        Public Declare Function VirtualProtectEx Lib "kernel32" Alias "VirtualProtectEx" (ByVal hProcess As Long, ByVal lpBaseAddress As Integer, ByVal nSize As Integer, ByVal flNewProtect As Long, lpflOldProtect As Long) As Long


        Const PROCESS_ALL_ACCESS = &H1F0FF
        Const PAGE_EXECUTE_READWRITE = &H40
        Const OldProtect = 0
        Public Function CheckAcess()
            'Dim temp As Long
            'temp = ReadMaster("Starcraft", &H57FE40, 4) - 1162106181
            'temp = temp + ReadMaster("Starcraft", &H57FE44, 4) - 1869900132
            'temp = temp + ReadMaster("Starcraft", &H57FE48, 1) - 114

            'If ReadMaster("Starcraft", &H57FE40, 4) = 1162106181 And ReadMaster("Starcraft", &H57FE44, 4) = 1869900132 And ReadMaster("Starcraft", &H57FE48, 1) = 114 Then
            Return True
            'End If
            'Return False
        End Function

        Public Function CheckProcess() As Boolean
            Dim ProcessName As String = "StarCraft"
            Dim MyP As Process() = Process.GetProcessesByName(ProcessName)
            If MyP.Length = 0 Then
                Return False
            End If

            Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)
            If hProcess = IntPtr.Zero Then
                Return False
            End If
            Return True
        End Function

        Public Sub WriteValue(ByVal Address As UInteger, ByVal Value As Byte())
            Dim nsize = Value.Length
            Dim ProcessName As String = "StarCraft"

            Dim MyP As Process() = Process.GetProcessesByName(ProcessName)

            Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)


            Dim hAddress As UInteger
            Dim vBuffer As Byte()

            hAddress = Address
            vBuffer = Value

            VirtualProtectEx(hProcess, hAddress, nsize, PAGE_EXECUTE_READWRITE, OldProtect)
            WriteProcessMemory(hProcess, hAddress, vBuffer(0), nsize, 0)
        End Sub

        Public Sub Write(ByVal Address As UInteger, ByVal Value As Byte)
            Dim nsize As Byte = 1
            Dim ProcessName As String = "StarCraft"

            Dim MyP As Process() = Process.GetProcessesByName(ProcessName)

            Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)


            Dim hAddress As UInteger
            Dim vBuffer(0) As Byte

            hAddress = Address

            vBuffer(0) = Value

            VirtualProtectEx(hProcess, hAddress, nsize, PAGE_EXECUTE_READWRITE, OldProtect)
            WriteProcessMemory(hProcess, hAddress, vBuffer(0), nsize, 0)
        End Sub
        Public Sub Write(ByVal Address As UInteger, ByVal Value As UInt16)
            Dim nsize As Byte = 2
            Dim ProcessName As String = "StarCraft"

            Dim MyP As Process() = Process.GetProcessesByName(ProcessName)

            Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)


            Dim hAddress As UInteger
            Dim vBuffer(1) As Byte

            hAddress = Address

            Dim memorystr As New MemoryStream(vBuffer)
            Dim Binarywri As New BinaryWriter(memorystr)
            Binarywri.Write(Value)

            Binarywri.Close()
            memorystr.Close()

            VirtualProtectEx(hProcess, hAddress, nsize, PAGE_EXECUTE_READWRITE, OldProtect)
            WriteProcessMemory(hProcess, hAddress, vBuffer(0), nsize, 0)
        End Sub
        Public Sub Write(ByVal Address As UInteger, ByVal Value As UInteger)
            Dim nsize As Byte = 4
            Dim ProcessName As String = "StarCraft"

            Dim MyP As Process() = Process.GetProcessesByName(ProcessName)

            Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)


            Dim hAddress As UInteger
            Dim vBuffer(3) As Byte

            hAddress = Address

            Dim memorystr As New MemoryStream(vBuffer)
            Dim Binarywri As New BinaryWriter(memorystr)
            Binarywri.Write(Value)

            Binarywri.Close()
            memorystr.Close()

            VirtualProtectEx(hProcess, hAddress, nsize, PAGE_EXECUTE_READWRITE, OldProtect)
            WriteProcessMemory(hProcess, hAddress, vBuffer(0), nsize, 0)
        End Sub
        Public Sub Write(ByVal Address As UInteger, ByVal Value As Integer)
            Dim nsize As Byte = 4
            Dim ProcessName As String = "StarCraft"

            Dim MyP As Process() = Process.GetProcessesByName(ProcessName)

            Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)


            Dim hAddress As UInteger
            Dim vBuffer(3) As Byte

            hAddress = Address

            Dim memorystr As New MemoryStream(vBuffer)
            Dim Binarywri As New BinaryWriter(memorystr)
            Binarywri.Write(Value)

            Binarywri.Close()
            memorystr.Close()

            VirtualProtectEx(hProcess, hAddress, nsize, PAGE_EXECUTE_READWRITE, OldProtect)
            WriteProcessMemory(hProcess, hAddress, vBuffer(0), nsize, 0)
        End Sub


        Public Function ReadValue(ByVal Address As Integer, ByVal nsize As UInteger, Optional isbytearray As Boolean = False, Optional issigned As Boolean = False)
            Dim ProcessName As String = "StarCraft"

            Dim MyP As Process() = Process.GetProcessesByName(ProcessName)

            Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)


            Dim hAddress As Integer
            hAddress = Address




            If isbytearray = False Then
                Select Case nsize
                    Case 1
                        Dim vBuffer As Byte()
                        ReDim vBuffer(nsize)

                        VirtualProtectEx(hProcess, hAddress, nsize, PAGE_EXECUTE_READWRITE, OldProtect)
                        ReadProcessMemory(hProcess, hAddress, vBuffer(0), nsize, 0)

                        Dim memoryStream As New MemoryStream(vBuffer)
                        Dim binaryReader As New BinaryReader(memoryStream)
                        If issigned = True Then
                            Dim value As SByte = binaryReader.ReadSByte
                            binaryReader.Close()
                            memoryStream.Close()
                            Return value
                        Else
                            Dim value As Byte = binaryReader.ReadByte
                            binaryReader.Close()
                            memoryStream.Close()
                            Return value
                        End If
                    Case 2
                        Dim vBuffer As Byte()
                        ReDim vBuffer(nsize)

                        VirtualProtectEx(hProcess, hAddress, nsize, PAGE_EXECUTE_READWRITE, OldProtect)
                        ReadProcessMemory(hProcess, hAddress, vBuffer(0), nsize, 0)

                        Dim memoryStream As New MemoryStream(vBuffer)
                        Dim binaryReader As New BinaryReader(memoryStream)

                        If issigned = True Then
                            Dim value As Int16 = binaryReader.ReadInt16
                            binaryReader.Close()
                            memoryStream.Close()
                            Return value
                        Else
                            Dim value As UInt16 = binaryReader.ReadUInt16
                            binaryReader.Close()
                            memoryStream.Close()
                            Return value
                        End If
                    Case 4
                        Dim vBuffer As Byte()
                        ReDim vBuffer(nsize)

                        VirtualProtectEx(hProcess, hAddress, nsize, PAGE_EXECUTE_READWRITE, OldProtect)
                        ReadProcessMemory(hProcess, hAddress, vBuffer(0), nsize, 0)

                        Dim memoryStream As New MemoryStream(vBuffer)
                        Dim binaryReader As New BinaryReader(memoryStream)

                        If issigned = True Then
                            Dim value As Int32 = binaryReader.ReadInt32
                            binaryReader.Close()
                            memoryStream.Close()
                            Return value
                        Else
                            Dim value As UInt32 = binaryReader.ReadUInt32
                            binaryReader.Close()
                            memoryStream.Close()
                            Return value
                        End If
                    Case Else
                        Dim vBuffer As Byte()
                        ReDim vBuffer(nsize)

                        VirtualProtectEx(hProcess, hAddress, nsize, PAGE_EXECUTE_READWRITE, OldProtect)
                        ReadProcessMemory(hProcess, hAddress, vBuffer(0), nsize, 0)
                        Return vBuffer
                End Select
            Else
                Dim vBuffer As Byte()
                ReDim vBuffer(nsize)

                VirtualProtectEx(hProcess, hAddress, nsize, PAGE_EXECUTE_READWRITE, OldProtect)
                ReadProcessMemory(hProcess, hAddress, vBuffer(0), nsize, 0)
                Return vBuffer
            End If

        End Function


        Public Function GetGandle()
            Dim ProcessName As String = "StarCraft"
            Dim MyP As Process() = Process.GetProcessesByName(ProcessName)


            Dim newrect As Rectangle

            GetWindowRect(MyP(0).MainWindowHandle, newrect)

            Return MyP(0).MainWindowHandle
        End Function
    End Module

End Namespace