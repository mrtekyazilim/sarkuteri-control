;--------------------------------
; SarkuteriBizerba NSIS Setup Script
; Dinamik Otomasyon - 2025
; Version 1.0.0
;--------------------------------

;--------------------------------
; Include Modern UI
!include "MUI2.nsh"
!include "FileFunc.nsh"
!include "LogicLib.nsh"

;--------------------------------
; General Settings

; Installer adi
Name "Sarkuteri Bizerba"

; Cikti dosyasi
OutFile "SarkuteriBizerba_Setup.exe"

; Unicode support
Unicode True

; Kurulum dizini
InstallDir "C:\DinamikOtomasyon\SarkuteriBizerba"

; Registry'den kurulum dizini al
InstallDirRegKey HKLM "Software\DinamikOtomasyon\SarkuteriBizerba" "InstallDir"

; Request admin rights
RequestExecutionLevel admin

; Sikistirma
SetCompressor /SOLID lzma

; Modern UI ayarlari
!define MUI_ABORTWARNING
!define MUI_ICON "..\sakuteri-bizerba.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

;--------------------------------
; Version Information
VIProductVersion "1.0.0.0"
VIAddVersionKey "ProductName" "Sarkuteri Bizerba"
VIAddVersionKey "CompanyName" "Dinamik Otomasyon"
VIAddVersionKey "LegalCopyright" "Copyright (c) 2025 Dinamik Otomasyon"
VIAddVersionKey "FileDescription" "Sarkuteri Bizerba Kurulum"
VIAddVersionKey "FileVersion" "1.0.0.0"
VIAddVersionKey "ProductVersion" "1.0.0"

;--------------------------------
; Pages

!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

;--------------------------------
; Languages
!insertmacro MUI_LANGUAGE "Turkish"

;--------------------------------
; Installer Sections

Section "Ana Program" SecMain
    SectionIn RO ; Read-only (zorunlu)
    
    ; Kurulum dizinini ayarla
    SetOutPath "$INSTDIR"
    
    ; Ana uygulama dosyalari
    File "..\bin\Debug\SarkuteriBizerba.exe"
    File "..\bin\Debug\SarkuteriBizerba.exe.config"
    File /nonfatal "..\bin\Debug\Ruhsat.dll"
    File /nonfatal "..\bin\Debug\TSTRANSDLL1.dll"
    
    ; DevExpress DLL'leri
    File /nonfatal "..\bin\Debug\DevExpress.*.dll"
    
    ; System DLL'leri
    File /nonfatal "..\bin\Debug\System.*.dll"
    
    ; Icon dosyasi
    File "..\sakuteri-bizerba.ico"
    
    ; Log klasoru olustur
    CreateDirectory "$INSTDIR\Logs"
    
    ; Reports klasoru olustur
    CreateDirectory "$INSTDIR\Reports"
    
    ; Data klasoru olustur
    CreateDirectory "$INSTDIR\Data"
    
    ; Registry'ye kurulum bilgileri yaz
    WriteRegStr HKLM "Software\DinamikOtomasyon\SarkuteriBizerba" "InstallDir" "$INSTDIR"
    WriteRegStr HKLM "Software\DinamikOtomasyon\SarkuteriBizerba" "Version" "1.0.0"
    
    ; Uninstaller olustur
    WriteUninstaller "$INSTDIR\Uninstall.exe"
    
    ; Add/Remove Programs'a ekle
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SarkuteriBizerba" "DisplayName" "Sarkuteri Bizerba"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SarkuteriBizerba" "UninstallString" "$INSTDIR\Uninstall.exe"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SarkuteriBizerba" "DisplayIcon" "$INSTDIR\sakuteri-bizerba.ico"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SarkuteriBizerba" "Publisher" "Dinamik Otomasyon"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SarkuteriBizerba" "DisplayVersion" "1.0.0"
    WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SarkuteriBizerba" "NoModify" 1
    WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SarkuteriBizerba" "NoRepair" 1
    
    ; Kurulum boyutunu hesapla
    ${GetSize} "$INSTDIR" "/S=0K" $0 $1 $2
    IntFmt $0 "0x%08X" $0
    WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SarkuteriBizerba" "EstimatedSize" "$0"
    
SectionEnd

Section "Masaustu Kisayolu" SecDesktop
    CreateShortcut "$DESKTOP\Sarkuteri Bizerba.lnk" "$INSTDIR\SarkuteriBizerba.exe" "" "$INSTDIR\sakuteri-bizerba.ico"
SectionEnd

Section "Baslat Menusu Kisayolu" SecStartMenu
    CreateDirectory "$SMPROGRAMS\Dinamik Otomasyon"
    CreateShortcut "$SMPROGRAMS\Dinamik Otomasyon\Sarkuteri Bizerba.lnk" "$INSTDIR\SarkuteriBizerba.exe" "" "$INSTDIR\sakuteri-bizerba.ico"
    CreateShortcut "$SMPROGRAMS\Dinamik Otomasyon\Sarkuteri Bizerba Kaldir.lnk" "$INSTDIR\Uninstall.exe"
SectionEnd

;--------------------------------
; Section Descriptions

!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro MUI_DESCRIPTION_TEXT ${SecMain} "Ana program dosyalari (Zorunlu)"
    !insertmacro MUI_DESCRIPTION_TEXT ${SecDesktop} "Masaustune kisayol olusturur"
    !insertmacro MUI_DESCRIPTION_TEXT ${SecStartMenu} "Baslat menusune kisayol olusturur"
!insertmacro MUI_FUNCTION_DESCRIPTION_END

;--------------------------------
; Uninstaller Section

Section "Uninstall"
    
    ; Program dosyalarini sil
    Delete "$INSTDIR\SarkuteriBizerba.exe"
    Delete "$INSTDIR\SarkuteriBizerba.exe.config"
    Delete "$INSTDIR\Ruhsat.dll"
    Delete "$INSTDIR\TSTRANSDLL1.dll"
    Delete "$INSTDIR\*.dll"
    Delete "$INSTDIR\sakuteri-bizerba.ico"
    Delete "$INSTDIR\Uninstall.exe"
    
    ; Kullaniciya sor: Config dosyalarini sil?
    MessageBox MB_YESNO "Yapilandirma dosyalari ve loglar silinsin mi?" IDYES delete_config IDNO skip_delete_config
    delete_config:
        RMDir /r "$INSTDIR\Config"
        RMDir /r "$INSTDIR\Logs"
        RMDir /r "$INSTDIR\Reports"
        RMDir /r "$INSTDIR\Data"
    skip_delete_config:
    
    ; Kurulum dizinini sil (bossa)
    RMDir "$INSTDIR"
    RMDir "C:\DinamikOtomasyon"
    
    ; Kisayollari sil
    Delete "$DESKTOP\Sarkuteri Bizerba.lnk"
    Delete "$SMPROGRAMS\Dinamik Otomasyon\Sarkuteri Bizerba.lnk"
    Delete "$SMPROGRAMS\Dinamik Otomasyon\Sarkuteri Bizerba Kaldir.lnk"
    RMDir "$SMPROGRAMS\Dinamik Otomasyon"
    
    ; Registry kayitlarini sil
    DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SarkuteriBizerba"
    DeleteRegKey HKLM "Software\DinamikOtomasyon\SarkuteriBizerba"
    DeleteRegKey /ifempty HKLM "Software\DinamikOtomasyon"
    
SectionEnd

;--------------------------------
; Functions

Function .onInit
    ; Onceki kurulum kontrolu
    ReadRegStr $0 HKLM "Software\DinamikOtomasyon\SarkuteriBizerba" "InstallDir"
    ${If} $0 != ""
        MessageBox MB_YESNO "Sarkuteri Bizerba zaten kurulu gorunuyor.$\n$\nOnceki kurulumu kaldirip yeni surumu kurmak ister misiniz?" IDYES remove_old
        Abort
        remove_old:
            ExecWait '"$0\Uninstall.exe" /S _?=$0'
            Delete "$0\Uninstall.exe"
            RMDir "$0"
    ${EndIf}
FunctionEnd
