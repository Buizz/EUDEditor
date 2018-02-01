Imports System
Imports System.Runtime.InteropServices

Namespace SFmpqapi
    Public Class SFmpq
        ' General error codes
        Const MPQ_ERROR_MPQ_INVALID As UInteger = 2233466981
        Const MPQ_ERROR_FILE_NOT_FOUND As UInteger = 2233466982
        Const MPQ_ERROR_DISK_FULL As UInteger = 2233466984
        'Physical write file to MPQ failed. Not sure of exact meaning
        Const MPQ_ERROR_HASH_TABLE_FULL As UInteger = 2233466985
        Const MPQ_ERROR_ALREADY_EXISTS As UInteger = 2233466986
        Const MPQ_ERROR_BAD_OPEN_MODE As UInteger = 2233466988
        'When MOAU_READ_ONLY is used without MOAU_OPEN_EXISTING
        Const MPQ_ERROR_COMPACT_ERROR As UInteger = 2234515457

        ' MpqOpenArchiveForUpdate flags
        Const MOAU_CREATE_NEW As UInteger = 0
        Const MOAU_CREATE_ALWAYS As UInteger = 8
        'Was wrongly named MOAU_CREATE_NEW
        Const MOAU_OPEN_EXISTING As UInteger = 4
        Const MOAU_OPEN_ALWAYS As UInteger = 32
        Const MOAU_READ_ONLY As UInteger = 16
        'Must be used with MOAU_OPEN_EXISTING
        Const MOAU_MAINTAIN_LISTFILE As UInteger = 1

        ' MpqAddFileToArchive flags
        Const MAFA_EXISTS As UInteger = 2147483648
        'Will be added if not present
        Const MAFA_UNKNOWN40000000 As UInteger = 1073741824
        Const MAFA_MODCRYPTKEY As UInteger = 131072
        Const MAFA_ENCRYPT As UInteger = 65536
        Const MAFA_COMPRESS As UInteger = 512
        Const MAFA_COMPRESS2 As UInteger = 256
        Const MAFA_REPLACE_EXISTING As UInteger = 1

        ' MpqAddFileToArchiveEx compression flags
        Const MAFA_COMPRESS_STANDARD As UInteger = 8
        'Standard PKWare DCL compression
        Const MAFA_COMPRESS_DEFLATE As UInteger = 2
        'ZLib's deflate compression
        Const MAFA_COMPRESS_WAVE As UInteger = 129
        'Standard wave compression
        Const MAFA_COMPRESS_WAVE2 As UInteger = 65
        'Unused wave compression
        ' Flags for individual compression types used for wave compression
        Const MAFA_COMPRESS_WAVECOMP1 As UInteger = 128
        'Main compressor for standard wave compression
        Const MAFA_COMPRESS_WAVECOMP2 As UInteger = 64
        'Main compressor for unused wave compression
        Const MAFA_COMPRESS_WAVECOMP3 As UInteger = 1
        'Secondary compressor for wave compression
        ' ZLib deflate compression level constants (used with MpqAddFileToArchiveEx and MpqAddFileFromBufferEx)
        Const Z_NO_COMPRESSION As UInteger = 0
        Const Z_BEST_SPEED As UInteger = 1
        Const Z_BEST_COMPRESSION As UInteger = 9
        Const Z_DEFAULT_COMPRESSION As Integer = (-1)

        ' MpqAddWaveToArchive quality flags
        Const MAWA_QUALITY_HIGH As UInteger = 1
        Const MAWA_QUALITY_MEDIUM As UInteger = 0
        Const MAWA_QUALITY_LOW As UInteger = 2

        ' SFileGetFileInfo flags
        Const SFILE_INFO_BLOCK_SIZE As UInteger = 1
        'Block size in MPQ
        Const SFILE_INFO_HASH_TABLE_SIZE As UInteger = 2
        'Hash table size in MPQ
        Const SFILE_INFO_NUM_FILES As UInteger = 3
        'Number of files in MPQ
        Const SFILE_INFO_TYPE As UInteger = 4
        'Is int a file or an MPQ?
        Const SFILE_INFO_SIZE As UInteger = 5
        'Size of MPQ or uncompressed file
        Const SFILE_INFO_COMPRESSED_SIZE As UInteger = 6
        'Size of compressed file
        Const SFILE_INFO_FLAGS As UInteger = 7
        'File flags (compressed, etc.), file attributes if a file not in an archive
        Const SFILE_INFO_PARENT As UInteger = 8
        'int of MPQ that file is in
        Const SFILE_INFO_POSITION As UInteger = 9
        'Position of file pointer in files
        Const SFILE_INFO_LOCALEID As UInteger = 10
        'Locale ID of file in MPQ
        Const SFILE_INFO_PRIORITY As UInteger = 11
        'Priority of open MPQ
        Const SFILE_INFO_HASH_INDEX As UInteger = 12
        'Hash index of file in MPQ
        ' SFileListFiles flags
        Const SFILE_LIST_MEMORY_LIST As UInteger = 1
        ' Specifies that lpFilelists is a file list from memory, rather than being a list of file lists
        Const SFILE_LIST_ONLY_KNOWN As UInteger = 2
        ' Only list files that the function finds a name for
        Const SFILE_LIST_ONLY_UNKNOWN As UInteger = 4
        ' Only list files that the function does not find a name for
        Const SFILE_TYPE_MPQ As UInteger = 1
        Const SFILE_TYPE_FILE As UInteger = 2

        Const SFILE_OPEN_HARD_DISK_FILE As UInteger = 0
        'Open archive without regard to the drive type it resides on
        Const SFILE_OPEN_CD_ROM_FILE As UInteger = 1
        'Open the archive only if it is on a CD-ROM
        Const SFILE_OPEN_ALLOW_WRITE As UInteger = 32768
        'Open file with write access
        Const SFILE_SEARCH_CURRENT_ONLY As UInteger = 0
        'Used with SFileOpenFileEx; only the archive with the int specified will be searched for the file
        Const SFILE_SEARCH_ALL_OPEN As UInteger = 1
        'SFileOpenFileEx will look through all open archives for the file
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure LCID
            <MarshalAs(UnmanagedType.LPArray, SizeConst:=4)> _
            Public lcLocale As Char()
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure SFMPQVERSION
            Public Major As UShort
            Public Minor As UShort
            Public Revision As UShort
            Public Subrevision As UShort
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure FILELISTENTRY
            Public dwFileExists As UInteger
            ' Nonzero if this entry is used
            <MarshalAs(UnmanagedType.LPArray, SizeConst:=4)> _
            Public lcLocale As Char()
            ' Locale ID of file
            Public dwCompressedSize As UInteger
            ' Compressed size of file
            Public dwFullSize As UInteger
            ' Uncompressed size of file
            Public dwFlags As UInteger
            ' Flags for file
            <MarshalAs(UnmanagedType.LPArray, SizeConst:=260)> _
            Public szFileName As Char()
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure MPQHEADER
            Public dwMPQID As UInteger
            '"MPQ\x1A" for mpq's, "BN3\x1A" for bncache.dat
            Public dwHeaderSize As UInteger
            ' Size of this header
            Public dwMPQSize As UInteger
            'The size of the mpq archive
            Public wUnused0C As UShort
            ' Seems to always be 0
            Public wBlockSize As UShort
            ' Size of blocks in files equals 512 << wBlockSize
            Public dwHashTableOffset As UInteger
            ' Offset to hash table
            Public dwBlockTableOffset As UInteger
            ' Offset to block table
            Public dwHashTableSize As UInteger
            ' Number of entries in hash table
            Public dwBlockTableSize As UInteger
            ' Number of entries in block table
        End Structure

        'Archive ints may be typecasted to this struct so you can access
        'some of the archive's properties and the decrypted hash table and
        'block table directly.
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure MPQARCHIVE
            ' Arranged according to priority with lowest priority first
            Public lpNextArc As IntPtr
            ' Pointer to the next ARCHIVEREC struct. Pointer to addresses of first and last archives if last archive
            Public lpPrevArc As IntPtr
            ' Pointer to the previous ARCHIVEREC struct. 0xEAFC5E23 if first archive
            <MarshalAs(UnmanagedType.LPArray, SizeConst:=260)> _
            Private szFileName As Char()
            ' Filename of the archive
            Public hFile As UInteger
            ' The archive's file int
            Public dwFlags1 As UInteger
            ' Some flags, bit 1 (0 based) seems to be set when opening an archive from a CD
            Public dwPriority As UInteger
            ' Priority of the archive set when calling SFileOpenArchive
            Public lpLastReadFile As IntPtr
            ' Pointer to the last read file's FILEREC struct. Only used for incomplete reads of blocks
            Public dwUnk As UInteger
            ' Seems to always be 0
            Public dwBlockSize As UInteger
            ' Size of file blocks in bytes
            Public lpLastReadBlock As IntPtr
            ' Pointer to the read buffer for archive. Only used for incomplete reads of blocks
            Public dwBufferSize As UInteger
            ' Size of the read buffer for archive. Only used for incomplete reads of blocks
            Public dwMPQStart As UInteger
            ' The starting offset of the archive
            Public lpMPQHeader As IntPtr
            ' Pointer to the archive header
            Public lpBlockTable As IntPtr
            ' Pointer to the start of the block table
            Public lpHashTable As IntPtr
            ' Pointer to the start of the hash table
            Public dwFileSize As UInteger
            ' The size of the file in which the archive is contained
            Public dwOpenFiles As UInteger
            ' Count of files open in archive + 1
            Public MpqHeader As MPQHEADER
            Public dwFlags As UInteger
            'The only flag that should be changed is MOAU_MAINTAIN_LISTFILE
            Public lpFileName As String
        End Structure

        'ints to files in the archive may be typecasted to this struct
        'so you can access some of the file's properties directly.
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure MPQFILE
            Public lpNextFile As IntPtr
            ' Pointer to the next FILEREC struct. Pointer to addresses of first and last files if last file
            Public lpPrevFile As IntPtr
            ' Pointer to the previous FILEREC struct. 0xEAFC5E13 if first file
            <MarshalAs(UnmanagedType.LPArray, SizeConst:=260)> _
            Private szFileName As Char()
            ' Filename of the archive
            Public hPlaceHolder As UInteger
            ' Always 0xFFFFFFFF
            Public lpParentArc As IntPtr
            ' Pointer to the ARCHIVEREC struct of the archive in which the file is contained
            Public lpBlockEntry As IntPtr
            ' Pointer to the file's block table entry
            Public dwCryptKey As UInteger
            ' Decryption key for the file
            Public dwFilePointer As UInteger
            ' Position of file pointer in the file
            Public dwUnk1 As UInteger
            ' Seems to always be 0
            Public dwBlockCount As UInteger
            ' Number of blocks in file
            Public lpdwBlockOffsets As IntPtr
            ' Offsets to blocks in file. There are 1 more of these than the number of blocks
            Public dwReadStarted As UInteger
            ' Set to 1 after first read
            Public dwUnk2 As UInteger
            ' Seems to always be 0
            Public lpLastReadBlock As IntPtr
            ' Pointer to the read buffer for file. Only used for incomplete reads of blocks
            Public dwBytesRead As UInteger
            ' Total bytes read from open file
            Public dwBufferSize As UInteger
            ' Size of the read buffer for file. Only used for incomplete reads of blocks
            Public dwConstant As UInteger
            ' Seems to always be 1
            Public lpHashEntry As IntPtr
            Public lpFileName As String
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure BLOCKTABLEENTRY
            Public dwFileOffset As UInteger
            ' Offset to file
            Public dwCompressedSize As UInteger
            ' Compressed size of file
            Public dwFullSize As UInteger
            ' Uncompressed size of file
            Public dwFlags As UInteger
            ' Flags for file
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure HASHTABLEENTRY
            Public dwNameHashA As UInteger
            ' First name hash of file
            Public dwNameHashB As UInteger
            ' Second name hash of file
            <MarshalAs(UnmanagedType.LPArray, SizeConst:=4)> _
            Public lcLocale As Char()
            ' Locale ID of file
            Public dwBlockTableIndex As UInteger
            ' Index to the block table entry for the file
        End Structure

        <DllImport("SFmpq.dll")> _
        Public Shared Function MpqGetVersionString() As String
        End Function

        <DllImport("SFmpq.dll")> _
        Public Shared Function MpqGetVersion() As Single
        End Function

        <DllImport("SFmpq.dll")> _
        Public Shared Function SFMpqGetVersionString() As String
        End Function

        ' SFMpqGetVersionString2's return value is the required length of the buffer plus
        ' the terminating null, so use SFMpqGetVersionString2(0, 0); to get the length.
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFMpqGetVersionString2(ByVal lpBuffer As IntPtr, ByVal dwBufferLength As UInteger) As UInteger
        End Function

        <DllImport("SFmpq.dll")> _
        Public Shared Function SFMpqGetVersion() As SFMPQVERSION
        End Function



        ' Storm functions implemented by this library
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileOpenArchive(ByVal lpFileName As String, ByVal dwPriority As UInteger, ByVal dwFlags As UInteger, ByRef hMPQ As Integer) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileCloseArchive(ByVal hMPQ As Integer) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileGetArchiveName(ByVal hMPQ As Integer, ByVal lpBuffer As String, ByVal dwBufferLength As UInteger) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileOpenFile(ByVal lpFileName As String, ByRef hFile As Integer) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileOpenFileEx(ByVal hMPQ As Integer, ByVal lpFileName As String, ByVal dwSearchScope As UInteger, ByRef hFile As Integer) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileCloseFile(ByVal hFile As Integer) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileGetFileSize(ByVal hFile As Integer, ByRef lpFileSizeHigh As Integer) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileGetFileArchive(ByVal hFile As Integer, ByRef hMPQ As Integer) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileGetFileName(ByVal hFile As Integer, ByVal lpBuffer As String, ByVal dwBufferLength As UInteger) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileSetFilePointer(ByVal hFile As Integer, ByVal lDistanceToMove As Integer, ByRef lplDistanceToMoveHigh As Integer, ByVal dwMoveMethod As UInteger) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileReadFile(ByVal hFile As Integer, ByVal lpBuffer As Byte(), ByVal nNumberOfBytesToRead As UInteger, ByRef lpNumberOfBytesRead As Integer, ByVal lpOverlapped As IntPtr) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileSetLocale(ByVal nNewLocale As LCID) As LCID
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileGetBasePath(ByVal lpBuffer As String, ByVal dwBufferLength As UInteger) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileSetBasePath(ByVal lpNewBasePath As String) As Integer
        End Function

        ' Extra storm-related functions
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileGetFileInfo(ByVal hFile As Integer, ByVal dwInfoType As UInteger) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileSetArchivePriority(ByVal hMPQ As Integer, ByVal dwPriority As UInteger) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileFindMpqHeader(ByVal hFile As Integer) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function SFileListFiles(ByVal hMPQ As Integer, ByVal lpFileLists As String, ByRef lpListBuffer As FILELISTENTRY, ByVal dwFlags As UInteger) As Integer
        End Function

        ' Archive editing functions implemented by this library
        <DllImport("SFmpq.dll")> _
        Public Shared Function MpqOpenArchiveForUpdate(ByVal lpFileName As String, ByVal dwFlags As UInteger, ByVal dwMaximumFilesInArchive As UInteger) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function MpqCloseUpdatedArchive(ByVal hMPQ As Integer, ByVal dwUnknown2 As UInteger) As UInteger
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function MpqAddFileToArchive(ByVal hMPQ As Integer, ByVal lpSourceFileName As String, ByVal lpDestFileName As String, ByVal dwFlags As UInteger) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function MpqAddWaveToArchive(ByVal hMPQ As Integer, ByVal lpSourceFileName As String, ByVal lpDestFileName As String, ByVal dwFlags As UInteger, ByVal dwQuality As UInteger) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function MpqRenameFile(ByVal hMPQ As Integer, ByVal lpcOldFileName As String, ByVal lpcNewFileName As String) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function MpqDeleteFile(ByVal hMPQ As Integer, ByVal lpFileName As String) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function MpqCompactArchive(ByVal hMPQ As Integer) As Integer
        End Function

        ' Extra archive editing functions
        <DllImport("SFmpq.dll")> _
        Public Shared Function MpqAddFileToArchiveEx(ByVal hMPQ As Integer, ByVal lpSourceFileName As String, ByVal lpDestFileName As String, ByVal dwFlags As UInteger, ByVal dwCompressionType As UInteger, ByVal dwCompressLevel As UInteger) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function MpqAddFileFromBufferEx(ByVal hMPQ As Integer, ByVal lpBuffer As Byte(), ByVal dwLength As UInteger, ByVal lpFileName As String, ByVal dwFlags As UInteger, ByVal dwCompressionType As UInteger, _
            ByVal dwCompressLevel As UInteger) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function MpqAddFileFromBuffer(ByVal hMPQ As Integer, ByVal lpBuffer As Byte(), ByVal dwLength As UInteger, ByVal lpFileName As String, ByVal dwFlags As UInteger) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function MpqAddWaveFromBuffer(ByVal hMPQ As Integer, ByVal lpBuffer As Byte(), ByVal dwLength As UInteger, ByVal lpFileName As String, ByVal dwFlags As UInteger, ByVal dwQuality As UInteger) As Integer
        End Function
        <DllImport("SFmpq.dll")> _
        Public Shared Function MpqSetFileLocale(ByVal hMPQ As Integer, ByVal lpFileName As String, ByVal nOldLocale As LCID, ByVal nNewLocale As LCID) As Integer
        End Function
    End Class
End Namespace

Namespace Stormapi
    Public Class Storm
        <DllImport("storm.dll", EntryPoint:="#272")> _
        Public Shared Function SFileSetLocale(ByVal hMPQ As UInt16) As Integer
        End Function


        <DllImport("storm.dll", EntryPoint:="#252")> _
        Public Shared Function SFileCloseArchive(ByVal hMPQ As Integer) As Integer
        End Function

        <DllImport("storm.dll", EntryPoint:="#253")> _
        Public Shared Function SFileCloseFile(ByVal hFile As Integer) As Integer
        End Function

        <DllImport("storm.dll", EntryPoint:="#262")> _
        Public Shared Function SFileDestroy() As Integer
        End Function

        <DllImport("storm.dll", EntryPoint:="#266")> _
        Public Shared Function SFileOpenArchive(ByVal lpFileName As String, ByVal dwPriority As UInteger, ByVal dwFlags As UInteger, ByRef hMPQ As Integer) As Integer
        End Function

        <DllImport("storm.dll", EntryPoint:="#268")> _
        Public Shared Function SFileOpenFileEx(ByVal hMPQ As Integer, ByVal lpFileName As String, ByVal dwSearchScope As UInteger, ByRef hFile As Integer) As Integer
        End Function

        <DllImport("storm.dll", EntryPoint:="#269")> _
        Public Shared Function SFileReadFile(ByVal hFile As Integer, ByVal lpBuffer As Byte(), ByVal nNumberOfBytesToRead As UInteger, ByRef lpNumberOfBytesRead As Integer, ByVal lpOverlapped As IntPtr) As Integer
        End Function

        <DllImport("storm.dll", EntryPoint:="#265")> _
        Public Shared Function SFileGetFileSize(ByVal hFile As Integer, ByRef lpFileSizeHigh As Integer) As Integer
        End Function
    End Class
End Namespace

Public Class SFMpq
    Public SFHmpq As Integer
    Public SFHfile As Integer

    Public StHmpq As Integer
    Public StHfile As Integer
    Public Ptr As Integer = 0

    Public ISMPQOPNE As Boolean = False
    Public ISFILEOPNE As Boolean = False
    Public Buffer(0) As Byte


    'SFmpq로 먼저 읽고
    '안되면 Storm으로 읽는거야.

    'SFmpq로 읽었을때 False가 나오면 다시 Strom으로 도전하는 거지.
    Public Sub Reset()
        ReDim Buffer(0)
        SFHmpq = 0
        SFHfile = 0

        StHmpq = 0
        StHfile = 0
        Ptr = 0
    End Sub


    Public Function ReaddatFile(Filename As String) As Byte()

        Dim status As Boolean = False


        ReDim Buffer(0)
        For i = 0 To 3
            If CheckFileExist(ProgramSet.DatMPQDirec(i)) Then
                MsgBox("MPQ 파일이 없습니다. 직접 선택하세요.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                SetMPQForm.ShowDialog()
            End If
            If MPQandFileOpen(ProgramSet.DatMPQDirec(i), Filename) = True Then
                Exit For
            End If


            'If MPQandFileOpen(ProgramSet.StarDirec.Replace("StarCraft.exe", "") & DatMPQ(i), Filename) = True Then
            '    Exit For
            'End If
        Next

        Return Buffer
    End Function

    Public Function MPQandFileOpen(MPQname As String, Filename As String) As Boolean
        Dim status As Boolean = False
        'MsgBox("SFmpq사용")
        If MPQopenSF(MPQname) = True Then
            'MsgBox("MPQ열기 성공")
            If FileOpenSF(Filename) = True Then
                'MsgBox("파일열기 성공")
                status = True
                SFmpqapi.SFmpq.SFileCloseFile(SFHfile)
                SFmpqapi.SFmpq.SFileCloseArchive(SFHmpq)
            Else
                'MsgBox("파일열기 실패")
                SFmpqapi.SFmpq.SFileCloseArchive(SFHmpq)
            End If
        Else
            'MsgBox("MPQ열기 실패")
        End If


        Stormapi.Storm.SFileDestroy()
        Stormapi.Storm.SFileSetLocale(&H409)

        If status = False Then
            'MsgBox("스톰사용")
            If MPQopenSt(MPQname) = True Then
                'SCompDecompress()
                'MsgBox("MPQ열기 성공")
                If FileOpenSt(Filename) = True Then
                    'MsgBox("파일열기 성공")
                    status = True
                    Stormapi.Storm.SFileCloseFile(StHfile)
                    Stormapi.Storm.SFileCloseArchive(StHmpq)
                Else
                    'MsgBox("파일열기 실패")
                    Stormapi.Storm.SFileCloseArchive(StHmpq)
                End If
            Else
                'MsgBox("MPQ열기 실패")
            End If
        End If

        Return status
    End Function

    Public Function MPQopenSF(Filename As String) As Boolean
        Dim status As Boolean
        'SFmpqapi.SFmpq.SFileDestroy()
        status = SFmpqapi.SFmpq.SFileOpenArchive(Filename, 0, 0, SFHmpq) 'SFmpqapi.SFmpq.SFileOpenArchive(Filename, 0, 0, Hmpq)

        Return status
    End Function
    Public Function FileOpenSF(Filename As String) As Boolean
        Dim status As Boolean
        Dim readbytes As Integer
        Dim size As Integer

        status = SFmpqapi.SFmpq.SFileOpenFileEx(SFHmpq, Filename, 0, SFHfile) 'SFmpqapi.SFmpq.SFileOpenFileEx
        If status = False Then
            Return status
        End If
        size = SFmpqapi.SFmpq.SFileGetFileSize(SFHfile, 0) 'SFmpqapi.SFmpq.SFileGetFileSize(Hfile, Nothing)


        ReDim Buffer(size)
        SFmpqapi.SFmpq.SFileReadFile(SFHfile, Buffer, size, readbytes, 0) 'SFmpqapi.SFmpq.SFileReadFile(Hfile, Buffer, size, readbytes, 0)

        Return status
    End Function

    Public Function MPQopenSt(Filename As String) As Boolean
        Dim status As Boolean

        status = Stormapi.Storm.SFileOpenArchive(Filename, 0, 0, StHmpq) 'SFmpqapi.SFmpq.SFileOpenArchive(Filename, 0, 0, Hmpq)
        Return status
    End Function
    Public Function FileOpenSt(Filename As String) As Boolean
        Dim status As Boolean
        Dim readbytes As Integer
        Dim size As Integer


        status = Stormapi.Storm.SFileOpenFileEx(StHmpq, Filename, 0, StHfile) 'SFmpqapi.SFmpq.SFileOpenFileEx
        If status = False Then
            Return status
        End If
        size = Stormapi.Storm.SFileGetFileSize(StHfile, 0) 'SFmpqapi.SFmpq.SFileGetFileSize(Hfile, Nothing)

        ReDim Buffer(size)

        Stormapi.Storm.SFileReadFile(StHfile, Buffer, size, readbytes, 0) 'SFmpqapi.SFmpq.SFileReadFile(Hfile, Buffer, size, readbytes, 0)



        Return status
    End Function




    Public Function Read(pos As Integer, size As Integer, Optional Type As Integer = 0)
        Dim value As Integer
        Dim text As String = ""
        Ptr = pos + size
        Select Case Type
            Case 0 '숫자
                For i = 0 To size - 1
                    value = value + Buffer(i + pos) * 256 ^ i
                Next
                Return value
            Case 1 '문자
                For i = 0 To size - 1
                    text = text & Chr(Buffer(i + pos))
                Next
                Return text
        End Select

        Return 0
    End Function
    Public Function ReadNext(size As Integer, Optional Type As Integer = 0)
        Dim value As Integer
        Dim text As String = ""

        Select Case Type
            Case 0 '숫자
                For i = 0 To size - 1
                    value = value + Buffer(i + Ptr) * 256 ^ i
                Next
                Ptr = Ptr + size
                Return value
            Case 1 '문자
                For i = 0 To size - 1
                    text = text & Chr(Buffer(i + Ptr))
                Next
                Ptr = Ptr + size
                Return text
        End Select


        Return 0
    End Function

End Class