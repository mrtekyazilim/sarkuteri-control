@echo off
REM ========================================================================
REM  SarkuteriBizerba NSIS Setup Derleme Script
REM  Dinamik Otomasyon - 2025
REM ========================================================================

echo.
echo ========================================================================
echo   SARKUTERI BIZERBA - NSIS SETUP DERLEME
echo ========================================================================
echo.

REM NSIS yolunu kontrol et
set NSIS_PATH=C:\Program Files (x86)\NSIS\makensis.exe

if not exist "%NSIS_PATH%" (
    echo [HATA] NSIS bulunamadi!
    echo.
    echo NSIS kurulu degil veya farkli bir dizinde.
    echo Lutfen NSIS'i su adresten indirin:
    echo https://nsis.sourceforge.io/Download
    echo.
    echo Veya bu script'te NSIS_PATH degiskenini guncelleyin.
    echo.
    pause
    exit /b 1
)

echo [1/4] NSIS bulundu: %NSIS_PATH%
echo.

REM Proje dizinini al
set PROJECT_DIR=%~dp0..
set INSTALLER_DIR=%~dp0
set NSI_FILE=%INSTALLER_DIR%SarkuteriBizerba_Setup.nsi

echo [2/4] Script dosyasi: %NSI_FILE%
echo.

REM Debug klasorunu kontrol et
set DEBUG_DIR=%PROJECT_DIR%\bin\Debug

if not exist "%DEBUG_DIR%\SarkuteriBizerba.exe" (
    echo [UYARI] Debug klasorunde SarkuteriBizerba.exe bulunamadi!
    echo.
    echo Lutfen once Visual Studio'da projeyi Debug modunda derleyin:
    echo 1. Visual Studio'yu acin
    echo 2. Configuration Manager'dan 'Debug' secin
    echo 3. Build ^> Rebuild Solution
    echo.
    set /p choice="Devam etmek istiyor musunuz? (E/H): "
    if /i not "%choice%"=="E" (
        echo.
        echo Islem iptal edildi.
        pause
        exit /b 1
    )
) else (
    echo [3/4] Debug dosyalari bulundu: %DEBUG_DIR%
    echo.
)

REM NSIS ile derle
echo [4/4] NSIS ile derleme baslatiliyor...
echo.
echo ========================================================================
"%NSIS_PATH%" "%NSI_FILE%"

if %ERRORLEVEL% EQU 0 (
    echo ========================================================================
    echo.
    echo [BASARILI] Setup dosyasi olusturuldu!
    echo.
    echo Setup dosyasi: %INSTALLER_DIR%SarkuteriBizerba_Setup.exe
    echo.
    echo Simdi yapabilecekleriniz:
    echo 1. Setup dosyasini test edin
    echo 2. Hash olusturun: certutil -hashfile SarkuteriBizerba_Setup.exe SHA256
    echo 3. Dijital imza atin (opsiyonel)
    echo 4. Dagitima hazir!
    echo.
) else (
    echo ========================================================================
    echo.
    echo [HATA] Derleme sirasinda hata olustu!
    echo.
    echo Hata kodu: %ERRORLEVEL%
    echo.
    echo Lutfen yukaridaki hata mesajlarini kontrol edin.
    echo.
)

echo ========================================================================

xcopy SarkuteriBizerba_Setup.exe C:\product_setups\ /Y
