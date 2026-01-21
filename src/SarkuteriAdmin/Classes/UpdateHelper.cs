#region namespaces

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

#endregion

namespace SarkuteriAdmin.Classes
{
    /// <summary>
    /// Uygulama güncelleme iþlemlerini yöneten yardýmcý sýnýf
    /// PowerShell ile otomatik güncelleme yapar
    /// </summary>
    internal static class UpdateHelper
    {
        private const string UPDATE_URL = "https://kurulum.dinamikotomasyon.com/downloads/SarkuteriAdmin.zip";

        internal static bool GuncellemeBasladi= false;
        /// <summary>
        /// PowerShell script ile güncelleme baþlatýr
        /// </summary>
        internal static void GuncellemeBaslat()
        {
            try
            {

                // Kullanýcýya onay sor
                var sonuc = MessageBox.Show(
                    "Program güncellenecek. Tüm açýk belgeler kapatýlacak.\n\nDevam etmek istiyor musunuz?",
                    "Güncelleme Onayý",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (sonuc != DialogResult.Yes)
                    return;
                GuncellemeBasladi = true;

                // PowerShell script'ini oluþtur
                string scriptPath = Path.Combine(Application.StartupPath, "Update-SarkuteriAdmin.ps1");
                CreateUpdateScript(scriptPath);

                // PowerShell script'ini çalýþtýr
                var processInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-ExecutionPolicy Bypass -WindowStyle Normal -File \"{scriptPath}\" -WorkingDirectory \"{Application.StartupPath}\"",
                    UseShellExecute = true
                };

                Process.Start(processInfo);
                

                // Mevcut programý kapat
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Güncelleme baþlatýlamadý:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// PowerShell güncelleme script'ini oluþturur
        /// </summary>
        private static void CreateUpdateScript(string scriptPath)
        {
            var script = new StringBuilder();
            string appName= "SarkuteriAdmin";

            script.AppendLine($"# {appName} Güncelleme Script");
            script.AppendLine("param([string]$WorkingDirectory)");
            script.AppendLine();
            script.AppendLine($"$UpdateUrl = '{UPDATE_URL}'");
            script.AppendLine($"$ZipFile = Join-Path $WorkingDirectory '{appName}_Update.zip'");
            script.AppendLine("$BackupFolder = Join-Path $WorkingDirectory ('_backup_' + (Get-Date -Format 'yyyyMMdd_HHmmss'))");
            script.AppendLine("$LogFile = Join-Path $WorkingDirectory ('log\\UpdateLog_' + (Get-Date -Format 'yyyyMMdd') + '.txt')");
            script.AppendLine();
            script.AppendLine("function Write-Log {");
            script.AppendLine("    param($Message)");
            script.AppendLine("    $LogMsg = \"$(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')`t$Message\"");
            script.AppendLine("    Write-Host $LogMsg");
            script.AppendLine("    try {");
            script.AppendLine("        $LogDir = Split-Path $LogFile -Parent");
            script.AppendLine("        if (!(Test-Path $LogDir)) { New-Item -ItemType Directory -Path $LogDir -Force | Out-Null }");
            script.AppendLine("        Add-Content -Path $LogFile -Value $LogMsg");
            script.AppendLine("    } catch { }");
            script.AppendLine("}");
            script.AppendLine();
            script.AppendLine("try {");
            script.AppendLine("    Write-Log '=== Güncelleme Baþladý ==='");
            script.AppendLine($"    Write-Progress -Activity '{appName} Güncelleniyor' -Status 'Ýndiriliyor...' -PercentComplete 10");
            script.AppendLine();
            script.AppendLine("    # Zip indir");
            script.AppendLine("    if (Test-Path $ZipFile) { Remove-Item $ZipFile -Force }");
            script.AppendLine("    $wc = New-Object System.Net.WebClient");
            script.AppendLine("    $wc.DownloadFile($UpdateUrl, $ZipFile)");
            script.AppendLine("    $wc.Dispose()");
            script.AppendLine("    Write-Log 'Dosya indirildi'");
            script.AppendLine();
            script.AppendLine("    # Process'leri kapat");
            script.AppendLine($"    Write-Progress -Activity '{appName} Güncelleniyor' -Status 'Kapatýlýyor...' -PercentComplete 30");
            script.AppendLine($"    @('{appName}') | ForEach-Object " + "{");
            script.AppendLine("        Get-Process -Name $_ -EA SilentlyContinue | ForEach-Object {");
            script.AppendLine("            $_.Kill(); $_.WaitForExit(5000); Write-Log \"$_ kapatýldý\"");
            script.AppendLine("        }");
            script.AppendLine("    }");
            script.AppendLine("    Start-Sleep -Seconds 2");
            script.AppendLine();
            script.AppendLine("    # Yedek");
            script.AppendLine($"    Write-Progress -Activity '{appName} Güncelleniyor' -Status 'Yedekleniyor...' -PercentComplete 40");
            script.AppendLine("    if (!(Test-Path $BackupFolder)) { New-Item -ItemType Directory -Path $BackupFolder -Force | Out-Null }");
            script.AppendLine();
            script.AppendLine("    # Zip aç");
            script.AppendLine($"    Write-Progress -Activity '{appName} Güncelleniyor' -Status 'Kuruluyor...' -PercentComplete 50");
            script.AppendLine("    Add-Type -AssemblyName System.IO.Compression.FileSystem");
            script.AppendLine("    $zip = [System.IO.Compression.ZipFile]::OpenRead($ZipFile)");
            script.AppendLine("    $total = $zip.Entries.Count");
            script.AppendLine("    $done = 0");
            script.AppendLine();
            script.AppendLine("    # Zip içindeki klasör yapýsýný kontrol et");
            script.AppendLine($"    $rootFolder = '{appName}'");
            script.AppendLine("    $rootFolderLength = $rootFolder.Length");
            script.AppendLine();
            script.AppendLine("    foreach ($entry in $zip.Entries) {");
            script.AppendLine("        # Klasör giriþlerini atla");
            script.AppendLine("        if ([string]::IsNullOrEmpty($entry.Name)) { continue }");
            script.AppendLine();
            script.AppendLine($"        # Dosya yolunu ayarla - {appName} klasörünü atla");
            script.AppendLine("        $relativePath = $entry.FullName");
            script.AppendLine("        if ($relativePath.StartsWith($rootFolder)) {");
            script.AppendLine("            $relativePath = $relativePath.Substring($rootFolderLength)");
            script.AppendLine("        }");
            script.AppendLine();
            script.AppendLine("        # Boþ path kontrolü");
            script.AppendLine("        if ([string]::IsNullOrWhiteSpace($relativePath)) { continue }");
            script.AppendLine();
            script.AppendLine("        $dest = Join-Path $WorkingDirectory $relativePath");
            script.AppendLine("        $dir = Split-Path $dest -Parent");
            script.AppendLine();
            script.AppendLine("        # Hedef klasörü oluþtur");
            script.AppendLine("        if (!(Test-Path $dir)) { New-Item -ItemType Directory -Path $dir -Force | Out-Null }");
            script.AppendLine();
            script.AppendLine("        # Eski dosyayý yedekle");
            script.AppendLine("        if (Test-Path $dest) {");
            script.AppendLine("            $bak = Join-Path $BackupFolder $relativePath");
            script.AppendLine("            $bakDir = Split-Path $bak -Parent");
            script.AppendLine("            if (!(Test-Path $bakDir)) { New-Item -ItemType Directory -Path $bakDir -Force | Out-Null }");
            script.AppendLine("            Copy-Item $dest $bak -Force");
            script.AppendLine("            Remove-Item $dest -Force");
            script.AppendLine("            Write-Log \"Yedeklendi: $relativePath\"");
            script.AppendLine("        }");
            script.AppendLine();
            script.AppendLine("        # Yeni dosyayý çýkar");
            script.AppendLine("        [System.IO.Compression.ZipFileExtensions]::ExtractToFile($entry, $dest, $true)");
            script.AppendLine("        Write-Log \"Güncellendi: $relativePath\"");
            script.AppendLine();
            script.AppendLine("        $done++");
            script.AppendLine("        $pct = 50 + [int](($done / $total) * 40)");
            script.AppendLine($"        Write-Progress -Activity '{appName} Güncelleniyor' -Status \"Kuruluyor... $done/$total\" -PercentComplete $pct");
            script.AppendLine("    }");
            script.AppendLine("    $zip.Dispose()");
            script.AppendLine();
            script.AppendLine("    # Temizlik");
            script.AppendLine($"    Write-Progress -Activity '{appName} Güncelleniyor' -Status 'Temizleniyor...' -PercentComplete 90");
            script.AppendLine("    if (Test-Path $ZipFile) { Remove-Item $ZipFile -Force }");
            script.AppendLine();
            script.AppendLine($"    Write-Progress -Activity '{appName} Güncelleniyor' -Status 'Tamamlandý!' -PercentComplete 100");
            script.AppendLine("    Write-Log '=== Güncelleme Tamamlandý ==='");
            script.AppendLine("    Start-Sleep -Seconds 1");
            script.AppendLine();
            script.AppendLine($"    # {appName} baþlat");
            script.AppendLine($"    $exe = Join-Path $WorkingDirectory '{appName}.exe'");
            script.AppendLine("    if (Test-Path $exe) { ");
            script.AppendLine($"        Write-Log '{appName} baþlatýlýyor...'");
            script.AppendLine("        Start-Process $exe -WorkingDirectory $WorkingDirectory");
            script.AppendLine("    }");
            script.AppendLine("    else {");
            script.AppendLine($"        Write-Log 'UYARI: {appName}.exe bulunamadý!'");
            script.AppendLine("    }");
            script.AppendLine();
            script.AppendLine("    Add-Type -AssemblyName System.Windows.Forms");
            //script.AppendLine("    [System.Windows.Forms.MessageBox]::Show('Güncelleme tamamlandý!`n`nMyShop baþlatýlýyor...', 'Baþarýlý', 0, 64)");
            script.AppendLine("}");
            script.AppendLine("catch {");
            script.AppendLine("    Write-Log \"HATA: $_\"");
            script.AppendLine("    Write-Log \"HATA Detay: $($_.Exception.Message)\"");
            script.AppendLine("    Add-Type -AssemblyName System.Windows.Forms");
            script.AppendLine("    [System.Windows.Forms.MessageBox]::Show(\"Güncelleme hatasý:`n`n$_`n`nDetaylar için log dosyasýna bakýn.\", 'Güncelleme Hatasý', 0, 16)");
            script.AppendLine("}");
            script.AppendLine("finally { Write-Progress -Activity 'MyShop Güncelleniyor' -Completed }");

            File.WriteAllText(scriptPath, script.ToString(), Encoding.UTF8);
            
        }

        /// <summary>
        /// Güncelleme kontrolü yapar (isteðe baðlý - gelecekte kullanýlabilir)
        /// </summary>
        /// <returns>Yeni sürüm varsa true, yoksa false</returns>
        internal static bool GuncellemeyiKontrolEt()
        {
            try
            {
                // Buraya gelecekte güncelleme kontrolü için web servis çaðrýsý eklenebilir
                
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
