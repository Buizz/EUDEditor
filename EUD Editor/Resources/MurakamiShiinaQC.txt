from eudplib import *
from struct import unpack
from math import ceil
import re


KeyCodeDict = {
    'LBUTTON': 0x01, 'RBUTTON': 0x02, 'CANCEL': 0x03, 'MBUTTON': 0x04,
    'XBUTTON1': 0x05, 'XBUTTON2': 0x06, 'BACK': 0x08, 'TAB': 0x09,
    'CLEAR': 0x0C, 'ENTER': 0x0D, 'NX5': 0x0E, 'SHIFT': 0x10,
    'LCTRL': 0x11, 'LALT': 0x12, 'PAUSE': 0x13, 'CAPSLOCK': 0x14,
    'RALT': 0x15, 'JUNJA': 0x17, 'FINAL': 0x18, 'RCTRL': 0x19, 'ESC': 0x1B,
    'CONVERT': 0x1C, 'NONCONVERT': 0x1D, 'ACCEPT': 0x1E, 'MODECHANGE': 0x1F,
    'SPACE': 0x20, 'PGUP': 0x21, 'PGDN': 0x22, 'END': 0x23, 'HOME': 0x24,
    'LEFT': 0x25, 'UP': 0x26, 'RIGHT': 0x27, 'DOWN': 0x28,  # 방향키
    'SELECT': 0x29, 'PRINTSCREEN': 0x2A, 'EXECUTE': 0x2B, 'SNAPSHOT': 0x2C,
    'INSERT': 0x2D, 'DELETE': 0x2E, 'HELP': 0x2F,
    '0': 0x30, '1': 0x31, '2': 0x32, '3': 0x33, '4': 0x34,
    '5': 0x35, '6': 0x36, '7': 0x37, '8': 0x38, '9': 0x39,
    'A': 0x41, 'B': 0x42, 'C': 0x43, 'D': 0x44, 'E': 0x45, 'F': 0x46,
    'G': 0x47, 'H': 0x48, 'I': 0x49, 'J': 0x4A, 'K': 0x4B, 'L': 0x4C,
    'M': 0x4D, 'N': 0x4E, 'O': 0x4F, 'P': 0x50, 'Q': 0x51, 'R': 0x52,
    'S': 0x53, 'T': 0x54, 'U': 0x55, 'V': 0x56, 'W': 0x57, 'X': 0x58,
    'Y': 0x59, 'Z': 0x5A,
    'LWIN': 0x5B, 'RWIN': 0x5C, '속성': 0x5D, 'SLEEP': 0x5F,
    'NUMPAD0': 0x60, 'NUMPAD1': 0x61, 'NUMPAD2': 0x62, 'NUMPAD3': 0x63,
    'NUMPAD4': 0x64, 'NUMPAD5': 0x65, 'NUMPAD6': 0x66, 'NUMPAD7': 0x67,
    'NUMPAD8': 0x68, 'NUMPAD9': 0x69,
    'NUMPAD*': 0x6A, 'NUMPAD+': 0x6B, 'SEPARATOR': 0x6C, 'NUMPAD-': 0x6D,
    'NUMPAD.': 0x6E, 'NUMPAD/': 0x6F,
    'F1': 0x70, 'F2': 0x71, 'F3': 0x72, 'F4': 0x73, 'F5': 0x74,
    'F6': 0x75, 'F7': 0x76, 'F8': 0x77, 'F9': 0x78, 'F10': 0x79,
    'F11': 0x7A, 'F12': 0x7B, 'F13': 0x7C, 'F14': 0x7D, 'F15': 0x7E,
    'F16': 0x7F, 'F17': 0x80, 'F18': 0x81, 'F19': 0x82, 'F20': 0x83,
    'F21': 0x84, 'F22': 0x85, 'F23': 0x86, 'F24': 0x87,
    'NUMLOCK': 0x90, 'SCROLL': 0x91, 'OEM_FJ_JISHO': 0x92,
    'OEM_FJ_MASSHOU': 0x93, 'OEM_FJ_TOUROKU': 0x94,
    'OEM_FJ_LOYA': 0x95, 'OEM_FJ_ROYA': 0x96,
    'LSHIFT': 0xA0, 'RSHIFT': 0xA1, 'LCONTROL': 0xA2, 'RCONTROL': 0xA3,
    'LMENU': 0xA4, 'RMENU': 0xA5,
    'BROWSER_BACK': 0xA6, 'BROWSER_FORWARD': 0xA7, 'BROWSER_REFRESH': 0xA8,
    'BROWSER_STOP': 0xA9, 'BROWSER_SEARCH': 0xAA, 'BROWSER_FAVORITES': 0xAB,
    'BROWSER_HOME': 0xAC,
    'VOLUME_MUTE': 0xAD, 'VOLUME_DOWN': 0xAE, 'VOLUME_UP': 0xAF,
    'MEDIA_NEXT_TRACK': 0xB0, 'MEDIA_PLAY_PAUSE': 0xB3,
    'MEDIA_PREV_TRACK': 0xB1, 'MEDIA_STOP': 0xB2,
    'LAUNCH_MAIL': 0xB4, 'LAUNCH_MEDIA_SELECT': 0xB5, 'LAUNCH_APP1': 0xB6,
    'LAUNCH_APP2': 0xB7,
    'SEMICOLON': 0xBA, '=': 0xBB, ',': 0xBC, '-': 0xBD, '.': 0xBE, '/': 0xBF,
    '`': 0xC0, 'ABNT_C1': 0xC1, 'ABNT_C2': 0xC2,
    '[': 0xDB, '|': 0xDC, ']': 0xDD, "'": 0xDE, 'OEM_8': 0xDF,
    'OEM_AX': 0xE1, 'OEM_102': 0xE2, 'ICO_HELP': 0xE3, 'ICO_00': 0xE4,
    'PROCESSKEY': 0xE5, 'ICO_CLEAR': 0xE6, 'PACKET': 0xE7, 'OEM_RESET': 0xE9,
    'OEM_JUMP': 0xEA, 'OEM_PA1': 0xEB, 'OEM_PA2': 0xEC, 'OEM_PA3': 0xED,
    'OEM_WSCTRL': 0xEE, 'OEM_CUSEL': 0xEF,
    'OEM_ATTN': 0xF0, 'OEM_FINISH': 0xF1, 'OEM_COPY': 0xF2, 'OEM_AUTO': 0xF3,
    'OEM_ENLW': 0xF4, 'OEM_BACKTAB': 0xF5, 'ATTN': 0xF6, 'CRSEL': 0xF7,
    'EXSEL': 0xF8, 'EREOF': 0xF9, 'PLAY': 0xFA, 'ZOOM': 0xFB, 'NONAME': 0xFC,
    'PA1': 0xFD, 'OEM_CLEAR': 0xFE, '_NONE_': 0xFF
}


def onInit():
    # get map size & human player
    global mapX, mapY, humans
    chkt = GetChkTokenized()
    dim, ownr = chkt.getsection('DIM'), chkt.getsection('OWNR')
    mapX, mapY = [k.bit_length() + 3 for k in unpack("<HH", dim[0:4])]
    humans = [p for p in range(12) if ownr[p] == 6]

    global QCUnitID, QCLoc, QC_X, QC_Y
    QCUnitID, QCLoc = 58, 0  # use Valkyrie, loc 0 in default
    QC_X, QC_Y = 8, 8  # create QC units at grid(2, 2)

    global DeathUnits, Trg, VTrg, VTrgLoc
    DeathUnits, Trg, VTrg, VTrgLoc = [[] for i in range(4)]

    for key, value in settings.items():
        V, Con, Ret, Point = [d.strip() for d in value.split(',')], '', '', ''
        if key == 'QCUnitID':
            QCUnitID = int(EncodeUnit(value.strip()))
            continue
        elif key == 'QC_XY':
            QC_X, QC_Y = int(V[0]), int(V[1])
            continue
        elif key == '마우스' or key.lower() == 'mouse':
            if len(V) >= len(humans):
                print('MouseMovelocation enabled: ',
                      ['P{}:{}'.format(h + 1, V[i]) for i, h in enumerate(humans)])
            else:
                print('※경고! 마우스 사용 불가: 플레이어 수만큼 로케이션을 지정해야합니다!',
                      '마우스 좌표가 데스값으로 저장됩니다! (%s)' % (v[0]))
            Point = re.sub(r'(0[xX][0-9a-fA-F]+)', r'f_dwread_epd_safe(EPD(\1))',
                           '0x62848C + 0x6CDDC4 + 65536 * (0x6284A8 + 0x6CDDC8) + 65537 * 64')
        else:
            conditions_ = [_.strip() for _ in key.split(';')]
            for condition_ in conditions_:
                for virtualkey, offset in KeyCodeDict.items():
                    if condition_.upper() == virtualkey:
                        a = 'f_dwread_epd_safe(EPD(0x596A18 + %u))' % (offset)
                        b = 256 ** (offset % 4)
                        Con += '%s & %u == %u,' % (a, b, b)
                        break
                else:
                    C = [_.strip() for _ in condition_.split(',')]
                    if C[0].lower() == 'xy':
                        try:
                            Point = '%s + 65536 * (%s) + 65537 * 64' % (C[1], C[2])
                        except IndexError:
                            Point = '{} + 65537 * 64'.format(C[1])
                        Point = re.sub(r'(0[xX][0-9a-fA-F]+)',
                                       r'f_dwread_epd(EPD(\1))', Point)
                    elif C[0].lower() == 'val':
                        Point = 'val: {0} % 1000 + ({0}) // 1000 * 65536 + 65537 * 64'.format(C[1])
                        Point = re.sub(r'(0[xX][0-9a-fA-F]+)',
                                       r'f_dwread_epd(EPD(\1))', Point)
                    elif re.match(r'0[xX][0-9a-fA-F]+', C[0]):
                        try:
                            Con += 'Memory(%s, %s, %s),' % (C[0], C[1], C[2])
                        except IndexError:
                            Con += 'f_dwread_epd_safe(EPD(%s))&%s==%s,' % (
                                C[0], C[1], C[1])
                    else:
                        Con += condition_ + ','
        if Point != '':
            if Con == '':
                Con = 'Always(),'
            if Point[:5] == 'val: ':
                Ret = '''PosX, PosY = f_dwbreak(PosXY)[0:2]
DoActions([
    SetDeaths(player, Add, PosX, {0}), SetDeaths(player, Add, PosY * 1000, {0})
])'''.format(EncodeUnit(V[0]))
                DeathUnits.extend([int(EncodeUnit(V[0]))])
                Point = Point[5:]
            elif len(V) >= len(humans):  # return locations
                V = [int(GetLocationIndex(v)) for v in V]
                Ret = '''PosX, PosY = f_dwbreak(PosXY)[0:2]
SetLocation(VTrgLoc[i][p], PosX, PosY)'''
            elif len(V) == 1:  # return death
                Ret = 'DoActions(SetDeaths(player, Add, PosXY, %s))' % (EncodeUnit(V[0]))
                DeathUnits.extend([int(EncodeUnit(V[0]))])
            else:  # return deaths
                Ret = '''PosX, PosY = f_dwbreak(PosXY)[0:2]
DoActions([
    SetDeaths(player, Add, PosX, {}), SetDeaths(player, Add, PosY, {})
])'''.format(*[EncodeUnit(V[_]) for _ in range(2)])
                DeathUnits.extend([int(EncodeUnit(V[_])) for _ in range(2)])
            VTrg.append((Point, Con, Ret))
            VTrgLoc.append(V)
        else:
            Ret = 'SetDeaths(player, Add, %s, %s)' % (V[1], EncodeUnit(V[0]))
            DeathUnits.extend([int(EncodeUnit(V[0]))])
            Trg.append((Con, Ret))
    global QCNum
    QCNum = len(VTrg) + ceil(len(Trg) / (mapX + mapY))
    DeathUnits = set(DeathUnits)

    print("[무라카미시이나QC] dim. %ux%u, %u humans, %u QCUnits per capita" %
          (2 ** (mapX - 4), 2 ** (mapY - 4), len(humans), QCNum))


onInit()

bw = EUDByteWriter()
pOrd = EUDVariable()
QC = EUDArray([0 for i in range(QCNum * len(humans))])  # 구석 유닛 배열


@EUDFunc
def SetLocation(locid, x, y):
    DoActions([
        SetMemoryEPD(EPD(0x58DC60) + locid * 5 + 0, SetTo, x),
        SetMemoryEPD(EPD(0x58DC60) + locid * 5 + 1, SetTo, y),
        SetMemoryEPD(EPD(0x58DC60) + locid * 5 + 2, SetTo, x),
        SetMemoryEPD(EPD(0x58DC60) + locid * 5 + 3, SetTo, y),
    ])


def onPluginStart():
    loc = EUDArray([0 for i in range(4)])

    DoActions([
        # Units.dat - Unit Dimensions
        SetMemory(0x6617C8 + QCUnitID * 8, SetTo, 0x20002),
        SetMemory(0x6617CC + QCUnitID * 8, SetTo, 0x20002),

        # Units.dat - Building Dimensions
        SetMemory(0x662860 + QCUnitID * 4, SetTo, 0),

        # temp location to create QCUnits
        SetMemory(0x6509B0, SetTo, EPD(loc)),
        SetDeaths(CurrentPlayer, SetTo, f_dwread_epd(
            EPD(0x58DC60) + QCLoc * 5), 0),
        SetMemory(0x6509B0, Add, 1),
        SetDeaths(CurrentPlayer, SetTo, f_dwread_epd(
            EPD(0x58DC60) + QCLoc * 5 + 1), 0),
        SetMemory(0x6509B0, Add, 1),
        SetDeaths(CurrentPlayer, SetTo, f_dwread_epd(
            EPD(0x58DC60) + QCLoc * 5 + 2), 0),
        SetMemory(0x6509B0, Add, 1),
        SetDeaths(CurrentPlayer, SetTo, f_dwread_epd(
            EPD(0x58DC60) + QCLoc * 5 + 3), 0),
    ])
    f_bwrite(0x6636B8 + QCUnitID, 130)  # Units.dat - Ground Weapon
    f_bwrite(0x6616E0 + QCUnitID, 130)  # Units.dat - Air Weapon
    f_bwrite(0x662DB8 + QCUnitID, 0)  # Units.dat - Seek Range
    f_bwrite(0x663238 + QCUnitID, 0)  # Units.dat - Sight Range
    SetLocation(QCLoc, QC_X * 32, QC_Y * 32)
    DoActions(MoveLocation(QCLoc + 1, 227, 11, QCLoc + 1))

    pID = f_dwread_epd(EPD(0x57F1B0))
    for p, player in enumerate(humans):
        if EUDIf()(pID == player):
            pOrd << p  # player X가 몇 번째 플레이어인지 (비공유)
        EUDEndIf()
        for n in EUDLoopRange(0, QCNum):
            CPtr, CEPD = f_dwepdread_epd(EPD(0x628438))
            DoActions(CreateUnit(1, QCUnitID, QCLoc + 1, 7))
            x, y = f_dwbreak(f_dwread_epd_safe(CEPD + 0x28 // 4))[0:2]
            SetLocation(QCLoc, x, y)
            DoActions([
                GiveUnits(1, QCUnitID, 7, QCLoc + 1, 8),
                SetMemory(0x6509B0, SetTo, CEPD + 0x10 // 4),
                SetDeaths(CurrentPlayer, SetTo, 64 * 65537, 0),  # 목적지 초기화
                SetMemory(0x6509B0, Add, (0x34 - 0x10) // 4),
                SetDeaths(CurrentPlayer, SetTo, 0, 0),  # 못움직이게 하기
                SetMemory(0x6509B0, Add, (0x4C - 0x34) // 4),
                SetDeaths(CurrentPlayer, Subtract, 8 - player, 0),  # 플레이어 변경
                SetMemory(0x6509B0, Add, (0xDC - 0x4C) // 4),
                SetDeaths(CurrentPlayer, Add, 0x4000000, 0),  # 무적
            ])
            QC[n + QCNum * p] = CPtr
    DoActions([
        SetMemoryEPD(EPD(0x58DC60) + QCLoc * 5, SetTo, loc[0]),
        SetMemoryEPD(EPD(0x58DC60) + QCLoc * 5 + 1, SetTo, loc[1]),
        SetMemoryEPD(EPD(0x58DC60) + QCLoc * 5 + 2, SetTo, loc[2]),
        SetMemoryEPD(EPD(0x58DC60) + QCLoc * 5 + 3, SetTo, loc[3]),
        MoveLocation(QCLoc + 1, 227, 11, QCLoc + 1)
    ])


@EUDFunc
def QueueGameCommand(buf, size):
    cmdqlen = f_dwread_epd(EPD(0x654AA0))
    f_memcpy(0x654880 + cmdqlen, buf, size)
    SetVariables(EPD(0x654AA0), cmdqlen + size)


@EUDFunc
def QueueGameCommand_Select(n, ptrList):
    buf = Db(b'\x090123456789012345678901234')
    bw.seekoffset(buf + 1)
    bw.writebyte(n)
    i = EUDVariable()
    i << 0
    if EUDWhile()(i < n):
        unitptr = f_dwread_epd(EPD(ptrList) + i)
        unitIndex = (unitptr - 0x59CCA8) // 336 + 1
        uniquenessIdentifier = f_bread(unitptr + 0xA5)
        targetID = unitIndex | f_bitlshift(uniquenessIdentifier, 11)
        b0, b1 = f_dwbreak(targetID)[2:4]
        bw.writebyte(b0)
        bw.writebyte(b1)
        i += 1
    EUDEndWhile()
    bw.flushdword()
    QueueGameCommand(buf, 2 * (n + 1))


SelNum, QGCSwitch = EUDCreateVariables(2)  # 유닛 선택 수
SelMem = Db(48)


def getQC(num):
    mod = num % (mapX + mapY)
    if mod < mapY:
        x = 2 ** (mapY - mod + 15)
    else:
        x = 2 ** (mapX + mapY - mod - 1)
    return x


def beforeTriggerExec():
    oldcp = f_getcurpl()

    # 0x6284B8에는 선택한 유닛의 구조오프셋이 들어있습니다. (4바이트 * 12)
    fin = Forward()
    for n in range(QCNum):  # 구석 유닛을 선택했으면 저장하지 않음
        EUDJumpIf(Memory(0x6284B8, Exactly, QC[n + QCNum * pOrd]), fin)
    SelNum << 0
    f_setcurpl(EPD(SelMem))
    for i in EUDLoopRange(12):  # 현재 선택 유닛 저장
        EUDJumpIf(Memory(0x6284B8 + 4 * i, Exactly, 0), fin)
        f_dwwrite_cp(0, f_dwread_epd_safe(EPD(0x6284B8) + i))
        DoActions(SetMemory(0x6509B0, Add, 1))
        SelNum << i + 1
    fin << NextTrigger()

    RC = Db(b'...\x14XXYY\0\0\xE4\0\x00')
    # SEL = Db(b'..\x09\x01ALPH')  # Select -> 09 01 알파아이디(2byte) -> 총 4바이트

    QGCSwitch << 0
    f_setcurpl(EPD(RC) + 1)
    for i, L in enumerate(VTrg):  # VTrg[n]: (Point, Con, Ret)
        if EUDIf()([eval(L[1])]):
            f_dwwrite_cp(0, eval(L[0]))
            QueueGameCommand_Select(1, QC + 4 * (i + QCNum * pOrd))
            QueueGameCommand(RC + 3, 10)  # RightClick
            QGCSwitch << 1
        EUDEndIf()

    for n in range(QCNum - len(VTrg)):  # Trg[n]: (Con, Ret)
        f_dwwrite_cp(0, 64 * 65537)
        for i, L in enumerate(Trg[n * (mapX + mapY):min(len(Trg), (n + 1) * (mapX + mapY))]):
            if EUDIf()([eval(L[0])]):  # 조건을 만족 할 경우 우클릭할 좌표 더함
                f_dwadd_cp(0, getQC(i))
            EUDEndIf()
        if EUDIf()(Deaths(CurrentPlayer, AtLeast, 64 * 65537 + 1, 0)):
            QueueGameCommand_Select(1, QC + 4 * (n + len(VTrg) + QCNum * pOrd))
            QueueGameCommand(RC + 3, 10)  # RightClick
            QGCSwitch << 1
        EUDEndIf()

    DoActions([SetDeaths(human, SetTo, 0, unit)
               for human in humans for unit in DeathUnits])
    for p, player in enumerate(humans):
        for i, L in enumerate(VTrg):  # VTrg[n]: ('Point', Con, Ret)
            f_setcurpl(EPD(QC[i + QCNum * p]) + 4)
            if EUDIf()(Deaths(CurrentPlayer, AtLeast, 64 * 65537 + 1, 0)):
                PosXY = f_dwread_cp(0) - 64 * 65537
                exec(L[2])
            EUDEndIf()
        for n in range(QCNum - len(VTrg)):  # Trg[n]: (Con, Ret)
            f_setcurpl(EPD(QC[n + len(VTrg) + QCNum * p]) + 4)
            if EUDIf()(Deaths(CurrentPlayer, AtLeast, 64 * 65537 + 1, 0)):
                DoActions(SetDeaths(CurrentPlayer, Subtract, 64 * 65537, 0))
                for i, L in enumerate(Trg[n * (mapX + mapY):min(len(Trg), (n + 1) * (mapX + mapY))]):
                    if EUDIf()(Deaths(CurrentPlayer, AtLeast, getQC(i), 0)):
                        DoActions([
                            eval(L[1]),
                            SetDeaths(CurrentPlayer, Subtract, getQC(i), 0),
                        ])
                    EUDEndIf()
            EUDEndIf()

    f_setcurpl(oldcp)


def afterTriggerExec():
    # 구석 유닛의 목적지가 64, 64가 아닐 경우(조건을 하나라도 만족했을 경우) -> 선택한 유닛 초기화, 목적지 초기화
    SelSwitch = EUDVariable()
    SelSwitch << 0
    for p in range(len(humans)):
        for n in range(QCNum):  # 목적지 초기화
            if EUDIfNot()(MemoryEPD(EPD(QC[n + QCNum * p]) + 4, Exactly, 64 * 65537)):
                f_dwwrite_epd(EPD(QC[n + QCNum * p]) + 4, 64 * 65537)
                if EUDIf()(pOrd == p):
                    SelSwitch << 1
                EUDEndIf()
            EUDEndIf()
    if EUDIf()(SelNum > 0):  # 변수에 있는 값을 현재 선택유닛에 대입
        if EUDIf()(EUDOr(SelSwitch == 1, QGCSwitch == 1)):
            QueueGameCommand_Select(SelNum, SelMem)
        EUDEndIf()
    EUDEndIf()
