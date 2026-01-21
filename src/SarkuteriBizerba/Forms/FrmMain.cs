using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using SarkuteriBizerba.Classes;

namespace SarkuteriBizerba.Forms
{


    public partial class FrmMain : Form
    {
        private Timer startupTimer;
        private Timer balloonTimer;
        private Timer syncTimer;
        private Timer displayTimer;
        private string logDirectory;
        private DateTime nextSyncTime;

        public FrmMain()
        {
            InitializeComponent();
            InitializeLogDirectory();
            InitializeTrayIcon();
            InitializeSyncTimer();
            InitializeDateControls();
        }

        private void InitializeDateControls()
        {
            try
            {
                // Tarih control'lerini bugünkü tarihle başlat
                dtpStart.Value = DateTime.Today;
                dtpEnd.Value = DateTime.Today;

                // Checkbox işaretli değilse tarih seçiciler disabled
                dtpStart.Enabled = chkDateRange.Checked;
                dtpEnd.Enabled = chkDateRange.Checked;
            }
            catch (Exception ex)
            {
                LogError($"Tarih control'leri başlatılamadı: {ex.Message}");
            }
        }

        private void chkDateRange_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Checkbox durumuna göre tarih seçicileri enable/disable yap
                dtpStart.Enabled = chkDateRange.Checked;
                dtpEnd.Enabled = chkDateRange.Checked;
            }
            catch (Exception ex)
            {
                LogError($"Tarih aralığı checkbox değiştirilirken hata: {ex.Message}");
            }
        }

        private void InitializeLogDirectory()
        {
            try
            {
                logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Log klasörü oluşturulamadı: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeTrayIcon()
        {
            // AutoSync durumuna göre tray'e küçültme davranışını belirle
            if (Helper.AutoSync)
            {
                // AutoSync açıksa 1 saniye bekleyip tray'e gönder
                startupTimer = new Timer();
                startupTimer.Interval = 1000; // 1 saniye
                startupTimer.Tick += (s, e) =>
                {
                    startupTimer.Stop();
                    startupTimer.Dispose();
                    HideToTray();
                };
                startupTimer.Start();
                LogMessage("AutoSync açık - Program başlangıçta tray'e küçültülecek.");
            }
            else
            {
                // AutoSync kapalıysa formda kal
                LogMessage("AutoSync kapalı - Program başlangıçta formda kalacak.");
            }
        }

        private void InitializeSyncTimer()
        {
            try
            {
                // Checkbox durumunu config'den yükle
                chkAutoSync.Checked = Helper.AutoSync;

                syncTimer = new Timer();
                // Helper.SyncIntervalMinutes dakikayı milisaniyeye çevir
                syncTimer.Interval = Helper.SyncIntervalMinutes * 60 * 1000;
                syncTimer.Tick += SyncTimer_Tick;

                // Display timer'ı başlat (her saniye güncelle)
                displayTimer = new Timer();
                displayTimer.Interval = 1000; // 1 saniye
                displayTimer.Tick += DisplayTimer_Tick;
                displayTimer.Start();

                // Checkbox durumuna göre timer'ı başlat veya durdur
                if (Helper.AutoSync)
                {
                    syncTimer.Start();
                    nextSyncTime = DateTime.Now.AddMinutes(Helper.SyncIntervalMinutes);
                    LogMessage($"Otomatik senkronizasyon {Helper.SyncIntervalMinutes} dakika aralıklarla başlatıldı.");
                }
                else
                {
                    LogMessage("Otomatik senkronizasyon devre dışı.");
                }

                UpdateRemainingTimeDisplay();
            }
            catch (Exception ex)
            {
                LogError($"Sync timer başlatılamadı: {ex.Message}");
            }
        }

        private void SyncTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // AutoSync durumu kontrol et
                if (!Helper.AutoSync || !chkAutoSync.Checked)
                {
                    syncTimer.Stop();
                    LogMessage("Otomatik senkronizasyon devre dışı bırakıldı.");
                    UpdateRemainingTimeDisplay();
                    return;
                }

                // Timer'ı durdur
                syncTimer.Stop();

                // Otomatik senkronizasyon başlat
                LogMessage("Otomatik senkronizasyon başlatılıyor...");
                PerformSync();
            }
            catch (Exception ex)
            {
                LogError($"Sync timer tick hatası: {ex.Message}");
            }
            finally
            {
                // Timer'ı tekrar başlat (hata olsa da) - sadece AutoSync açıksa
                try
                {
                    if (Helper.AutoSync && chkAutoSync.Checked)
                    {
                        syncTimer.Start();
                        nextSyncTime = DateTime.Now.AddMinutes(Helper.SyncIntervalMinutes);
                    }
                    UpdateRemainingTimeDisplay();
                }
                catch (Exception ex)
                {
                    LogError($"Timer yeniden başlatılamadı: {ex.Message}");
                }
            }
        }

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            UpdateRemainingTimeDisplay();
        }

        public void PauseSyncTimer()
        {
            try
            {
                if (syncTimer != null && syncTimer.Enabled)
                {
                    syncTimer.Stop();
                    LogMessage("Ayar formu açıldığı için sync timer duraklatıldı.");
                    UpdateRemainingTimeDisplay();
                }
            }
            catch (Exception ex)
            {
                LogError($"Sync timer duraklatılırken hata: {ex.Message}");
            }
        }

        public void ResumeSyncTimer()
        {
            try
            {
                // Config'den güncel interval değerini oku
                int newInterval = Helper.SyncIntervalMinutes * 60 * 1000;

                if (syncTimer != null)
                {
                    // Interval değişmişse güncelle
                    if (syncTimer.Interval != newInterval)
                    {
                        syncTimer.Interval = newInterval;
                        LogMessage($"Sync timer interval güncellendi: {Helper.SyncIntervalMinutes} dakika");
                    }

                    // AutoSync açıksa timer'ı tekrar başlat
                    if (Helper.AutoSync && chkAutoSync.Checked)
                    {
                        syncTimer.Start();
                        nextSyncTime = DateTime.Now.AddMinutes(Helper.SyncIntervalMinutes);
                        LogMessage($"Sync timer devam ediyor: {Helper.SyncIntervalMinutes} dakika aralıklarla");
                    }
                }

                UpdateRemainingTimeDisplay();
            }
            catch (Exception ex)
            {
                LogError($"Sync timer devam ettirilirken hata: {ex.Message}");
            }
        }

        private void UpdateRemainingTimeDisplay()
        {
            try
            {
                if (syncTimer != null && syncTimer.Enabled && Helper.AutoSync && chkAutoSync.Checked)
                {
                    // Timer çalışıyor - kalan süreyi hesapla ve göster
                    TimeSpan remaining = nextSyncTime - DateTime.Now;

                    if (remaining.TotalSeconds > 0)
                    {
                        int minutes = (int)remaining.TotalMinutes;
                        int seconds = remaining.Seconds;
                        lblRemainingTime.Text = $"Sonraki aktarım: {minutes:D2}:{seconds:D2}";
                        lblRemainingTime.Visible = true;
                    }
                    else
                    {
                        lblRemainingTime.Text = "Aktarım başlıyor...";
                        lblRemainingTime.Visible = true;
                    }
                }
                else
                {
                    // Timer çalışmıyor - label'ı gizle
                    lblRemainingTime.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogError($"Kalan süre görüntüleme hatası: {ex.Message}");
                lblRemainingTime.Visible = false;
            }
        }

        private void chkAutoSync_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Checkbox durumunu config'e kaydet
                Helper.AutoSync = chkAutoSync.Checked;

                if (chkAutoSync.Checked)
                {
                    // Timer'ı başlat
                    if (syncTimer != null)
                    {
                        syncTimer.Start();
                        nextSyncTime = DateTime.Now.AddMinutes(Helper.SyncIntervalMinutes);
                        LogMessage($"Otomatik senkronizasyon etkinleştirildi. {Helper.SyncIntervalMinutes} dakika aralıklarla çalışacak.");
                    }
                }
                else
                {
                    // Timer'ı durdur
                    if (syncTimer != null)
                    {
                        syncTimer.Stop();
                        LogMessage("Otomatik senkronizasyon devre dışı bırakıldı.");
                    }
                }

                UpdateRemainingTimeDisplay();
            }
            catch (Exception ex)
            {
                LogError($"AutoSync ayarı değiştirilirken hata: {ex.Message}");
            }
        }

        private void ShowForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.BringToFront();
            this.Activate();
        }

        private void HideToTray()
        {
            this.Hide();
            this.ShowInTaskbar = false;

            // Kullanıcıya bilgi mesajı göster
            notifyIcon1.ShowBalloonTip(1000, "Dinamik Bizerba", "SarkuteriBizerba simge durumunda çalışıyor", ToolTipIcon.Info);

            // 2 saniye sonra balloon tip'i kapat
            if (balloonTimer != null)
            {
                balloonTimer.Stop();
                balloonTimer.Dispose();
            }

            balloonTimer = new Timer();
            balloonTimer.Interval = 1000; // 2 saniye
            balloonTimer.Tick += (s, e) =>
            {
                notifyIcon1.Visible = false;
                notifyIcon1.Visible = true;
                balloonTimer.Stop();
                balloonTimer.Dispose();
                balloonTimer = null;
            };
            balloonTimer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PerformSync();
        }

        private void PerformSync()
        {
            try
            {
                // Timer'ı durdur (işlem sırasında) - sadece AutoSync açıksa
                if (syncTimer != null && Helper.AutoSync && chkAutoSync.Checked)
                    syncTimer.Stop();

                // Display güncelle
                UpdateRemainingTimeDisplay();

                // Tarih aralığını belirle
                DateTime startDate, endDate;
                if (chkDateRange.Checked)
                {
                    // Checkbox işaretliyse seçilen tarih aralığını kullan
                    startDate = dtpStart.Value.Date;
                    endDate = dtpEnd.Value.Date;
                }
                else
                {
                    // Checkbox işaretli değilse bugünkü tarihi kullan
                    startDate = DateTime.Today;
                    endDate = DateTime.Today;
                }

                string startMessage = string.Format("=== TERAZI SYNC İŞLEMİ BAŞLADI    {0} === (Tarih: {1} - {2})",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    startDate.ToString("yyyy-MM-dd"),
                    endDate.ToString("yyyy-MM-dd"));
                textBox1.Text = startMessage + Environment.NewLine;
                LogMessage(startMessage);

                // 10 terazi IP'sini kontrol et ve dolu olanları işle
                ProcessScaleIP("Terazi 1", Helper.BizerbaIP1, Helper.BizerbaName1, startDate, endDate);
                ProcessScaleIP("Terazi 2", Helper.BizerbaIP2, Helper.BizerbaName2, startDate, endDate);
                ProcessScaleIP("Terazi 3", Helper.BizerbaIP3, Helper.BizerbaName3, startDate, endDate);
                ProcessScaleIP("Terazi 4", Helper.BizerbaIP4, Helper.BizerbaName4, startDate, endDate);
                ProcessScaleIP("Terazi 5", Helper.BizerbaIP5, Helper.BizerbaName5, startDate, endDate);
                ProcessScaleIP("Terazi 6", Helper.BizerbaIP6, Helper.BizerbaName6, startDate, endDate);
                ProcessScaleIP("Terazi 7", Helper.BizerbaIP7, Helper.BizerbaName7, startDate, endDate);
                ProcessScaleIP("Terazi 8", Helper.BizerbaIP8, Helper.BizerbaName8, startDate, endDate);
                ProcessScaleIP("Terazi 9", Helper.BizerbaIP9, Helper.BizerbaName9, startDate, endDate);
                ProcessScaleIP("Terazi 10", Helper.BizerbaIP10, Helper.BizerbaName10, startDate, endDate);

                string endMessage = string.Format("=== TERAZI SYNC İŞLEMİ TAMAMLANDI {0} ===", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                textBox1.Text += endMessage + Environment.NewLine;
                LogMessage(endMessage);
            }
            catch (Exception ex)
            {
                string errorMessage = "Sync işlemi hatası: " + ex.Message;
                textBox1.Text += errorMessage + Environment.NewLine;
                LogError(errorMessage);
                MessageBox.Show("Hata oluştu." + Environment.NewLine + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Timer'ı tekrar başlat (hata olsa da) - sadece AutoSync açıksa
                try
                {
                    if (syncTimer != null && Helper.AutoSync && chkAutoSync.Checked)
                    {
                        syncTimer.Start();
                        nextSyncTime = DateTime.Now.AddMinutes(Helper.SyncIntervalMinutes);
                    }
                    UpdateRemainingTimeDisplay();
                }
                catch (Exception ex)
                {
                    LogError($"Timer yeniden başlatılamadı: {ex.Message}");
                }
            }
        }

        private void ProcessScaleIP(string scaleName, string scaleIP, string scaleNickname, DateTime startDate, DateTime endDate)
        {
            try
            {
                Application.DoEvents();
                // IP adresi boş veya null ise atla
                if (string.IsNullOrWhiteSpace(scaleIP))
                {

                    return;
                }

                if (!Helper.IsScaleReachable(scaleIP))
                {
                    string displayName = string.IsNullOrWhiteSpace(scaleNickname) ? scaleName : scaleNickname;
                    string message = $"[{displayName}] ({scaleIP}) IP erişilemez, atlanıyor.";
                    textBox1.Text += message + Environment.NewLine;
                    LogMessage(message);
                    return;
                }

                string displayScaleName = string.IsNullOrWhiteSpace(scaleNickname) ? scaleName : scaleNickname;
                string processingMessage = $"[{displayScaleName}] ({scaleIP}) işleniyor... (Tarih: {startDate:yyyy-MM-dd} - {endDate:yyyy-MM-dd})";
                textBox1.Text += processingMessage + Environment.NewLine;
                LogMessage(processingMessage);

                List<BizerbaTransaction> transactions = Bizerba.Transactions(scaleIP, startDate, endDate);
                if (transactions == null)
                {
                    string errorMessage = $"[{displayScaleName}] HATA: {Bizerba.lastError}";
                    textBox1.Text += errorMessage + Environment.NewLine;
                    LogError(errorMessage);
                    return;
                }

                string countMessage = $"[{displayScaleName}] TOPLAM KAYIT: {transactions.Count}";
                textBox1.Text += countMessage + Environment.NewLine;
                LogMessage(countMessage);

                if (transactions.Count > 0)
                {
                    // Veritabanına ekleme işlemi - scaleNickname'i de gönder
                    int insertedCount = DinamikBizerba.InsertTransactions(Helper.StoreNum, scaleIP, scaleNickname, transactions);
                    string insertMessage = $"[{displayScaleName}] Veritabanına eklenen kayıt sayısı: {insertedCount}";
                    textBox1.Text += insertMessage + Environment.NewLine;
                    LogMessage(insertMessage);

                    // Son 4-5 transaction'ı göster
                    var lastTransactions = transactions.Skip(Math.Max(0, transactions.Count - 5)).ToList();
                    for (int i = lastTransactions.Count - 1; i >= 0; i--)
                    {
                        var transaction = lastTransactions[i];
                        // Tarihi yyyy-MM-dd HH:mm:ss formatına çevir
                        string formattedDate = FormatTransactionDate(transaction.Date, transaction.Time);
                        string transactionMessage = $"  - Barcode: {transaction.Barcode}, PLU: {transaction.PLUNumber},Kilogram:{transaction.Kilogram},  Net: {transaction.Net}, UnitPrice: {transaction.UnitPrice}, TotalPrice: {transaction.TotalPrice}, Tarih: {formattedDate}";
                        textBox1.Text += transactionMessage + Environment.NewLine;
                        LogMessage(transactionMessage);
                    }

                    if (transactions.Count > 5)
                    {
                        string moreMessage = $"  ... ve {transactions.Count - 5} kayıt daha (son 5 kayıt gösteriliyor)";
                        textBox1.Text += moreMessage + Environment.NewLine;
                        LogMessage(moreMessage);
                    }
                }

                string completedMessage = $"[{displayScaleName}] İşlem tamamlandı.";
                textBox1.Text += completedMessage + Environment.NewLine + Environment.NewLine;
                LogMessage(completedMessage);
            }
            catch (Exception ex)
            {
                string displayScaleName = string.IsNullOrWhiteSpace(scaleNickname) ? scaleName : scaleNickname;
                string errorMessage = $"[{displayScaleName}] HATA: {ex.Message}";
                textBox1.Text += errorMessage + Environment.NewLine;
                LogError(errorMessage);
            }
        }

        private string FormatTransactionDate(string date, string time)
        {
            try
            {
                // Bizerba'dan gelen format: "dd-MM-yy" ve "HH:mm:ss"
                if (DateTime.TryParseExact(date, "dd-MM-yy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    if (TimeSpan.TryParse(time, out TimeSpan parsedTime))
                    {
                        DateTime fullDateTime = parsedDate.Add(parsedTime);
                        return fullDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        return parsedDate.ToString("yyyy-MM-dd") + " 00:00:00";
                    }
                }

                // Parse edilemezse orijinal formatı döndür
                return $"{date} {time}";
            }
            catch
            {
                // Hata durumunda orijinal formatı döndür
                return $"{date} {time}";
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DirectoryInfo path = new DirectoryInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
        //        string strIpPath = Path.Combine(path.FullName, @"ip.csv");
        //        textBox1.Text += "[IP Path] " + strIpPath + Environment.NewLine;
        //        IntPtr resultPtr = Bizerba.GetTransactionInformation(strIpPath, "label", "20251027", "20251027", "0000", "2359", 0);
        //        if (resultPtr == IntPtr.Zero)
        //        {
        //            textBox1.Text += "GetJsonReportData returned null pointer." + Environment.NewLine;
        //            return;
        //        }

        //        string show = Marshal.PtrToStringAnsi(resultPtr);
        //        show = show.Replace("},]", "}]");
        //        //textBox1.Text += "Raw JSON Data:" + Environment.NewLine + show + Environment.NewLine;

        //        // show değişkenini terazi1.json dosyasına kaydet
        //        try
        //        {
        //            string jsonFilePath = Path.Combine(path.FullName, "terazi1.json");
        //            File.WriteAllText(jsonFilePath, show, Encoding.UTF8);
        //            textBox1.Text += "[JSON Saved] " + jsonFilePath + Environment.NewLine;
        //        }
        //        catch (Exception saveEx)
        //        {
        //            textBox1.Text += "JSON kaydetme hatası: " + saveEx.Message + Environment.NewLine;
        //        }

        //        // JSON verisini parse et
        //        try
        //        {
        //            JavaScriptSerializer serializer = new JavaScriptSerializer();
        //            List<BizerbaTransaction> transactions = serializer.Deserialize<List<BizerbaTransaction>>(show);

        //            textBox1.Text += Environment.NewLine + "=== PARSED TRANSACTIONS ===" + Environment.NewLine;
        //            textBox1.Text += $"Toplam İşlem Sayısı: {transactions.Count}" + Environment.NewLine + Environment.NewLine;

        //            foreach (var transaction in transactions)
        //            {
        //                textBox1.Text += $"Barcode: {transaction.Barcode}" + Environment.NewLine;
        //                textBox1.Text += $"BarcodeFull: {transaction.BarcodeFull}" + Environment.NewLine;
        //                textBox1.Text += $"Seri No: {transaction.SerialNumber}" + Environment.NewLine;
        //                textBox1.Text += $"Tarih/Saat: {transaction.Date} {transaction.Time}" + Environment.NewLine;
        //                textBox1.Text += $"PLU No: {transaction.PLUNumber}" + Environment.NewLine;
        //                textBox1.Text += $"Ürün Kodu: {transaction.ProductCode}" + Environment.NewLine;
        //                textBox1.Text += $"Net Ağırlık: {transaction.Net}" + Environment.NewLine;
        //                textBox1.Text += $"Birim Fiyat: {transaction.UnitPrice}" + Environment.NewLine;
        //                textBox1.Text += $"Toplam Fiyat: {transaction.TotalPrice}" + Environment.NewLine;
        //                textBox1.Text += $"Satış Fiyatı: {transaction.SalePrice}" + Environment.NewLine;
        //                textBox1.Text += $"Satış Toplam: {transaction.SaleTotalPrice}" + Environment.NewLine;
        //                textBox1.Text += $"Geçerli Satış: {(transaction.SaleValid == 1 ? "Evet" : "Hayır")}" + Environment.NewLine;
        //                textBox1.Text += "----------------------------" + Environment.NewLine;
        //            }

        //            // İstatistikleri hesapla
        //            CalculateStatistics(transactions);
        //        }
        //        catch (Exception jsonEx)
        //        {
        //            textBox1.Text += "JSON Parse Hatası: " + jsonEx.Message + Environment.NewLine;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Hata oluştu." + Environment.NewLine + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        private void CalculateStatistics(List<BizerbaTransaction> transactions)
        {
            try
            {
                var validTransactions = transactions.Where(t => t.SaleValid == 1).ToList();

                decimal totalSalesAmount = validTransactions
                    .Sum(t => decimal.TryParse(t.SaleTotalPrice, out decimal price) ? price : 0);

                var pluGroups = validTransactions
                    .GroupBy(t => t.PLUNumber)
                    .Select(g => new
                    {
                        PLU = g.Key,
                        Count = g.Count(),
                        TotalAmount = g.Sum(t => decimal.TryParse(t.SaleTotalPrice, out decimal price) ? price : 0)
                    })
                    .OrderByDescending(x => x.TotalAmount)
                    .ToList();

                textBox1.Text += Environment.NewLine + "=== İSTATİSTİKLER ===" + Environment.NewLine;
                textBox1.Text += $"Toplam Geçerli İşlem: {validTransactions.Count}" + Environment.NewLine;
                textBox1.Text += $"Toplam Satış Tutarı: {totalSalesAmount:C2}" + Environment.NewLine;
                textBox1.Text += Environment.NewLine + "PLU Bazında Satışlar:" + Environment.NewLine;

                foreach (var pluGroup in pluGroups.Take(5)) // İlk 5'i göster
                {
                    textBox1.Text += $"PLU {pluGroup.PLU}: {pluGroup.Count} adet - {pluGroup.TotalAmount:C2}" + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                textBox1.Text += "İstatistik hesaplama hatası: " + ex.Message + Environment.NewLine;
            }
        }



        private void btnSettings_Click(object sender, EventArgs e)
        {
            try
            {
                // Timer'ı duraklat
                PauseSyncTimer();

                var f = new FrmSettings();
                f.ShowDialog();

                // Timer'ı devam ettir (interval değişmiş olabilir)
                ResumeSyncTimer();
            }
            catch (Exception ex)
            {
                // Hata olsa bile timer'ı devam ettir
                ResumeSyncTimer();
                MessageBox.Show("Hata oluştu." + Environment.NewLine + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDbConfig_Click(object sender, EventArgs e)
        {
            try
            {
                // Timer'ı duraklat
                PauseSyncTimer();

                var f = new FrmDb();
                f.ShowDialog();

                // Timer'ı devam ettir
                ResumeSyncTimer();
            }
            catch (Exception ex)
            {
                // Hata olsa bile timer'ı devam ettir
                ResumeSyncTimer();
                MessageBox.Show("Hata oluştu." + Environment.NewLine + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void gösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Gerçekten çıkmak için Application.Exit() kullan
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Form kapatılmaya çalışıldığında tray'e gönder, çıkış yapma
            e.Cancel = true;
            HideToTray();
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            // Form minimize edildiğinde tray'e gönder
            if (this.WindowState == FormWindowState.Minimized)
            {
                HideToTray();
            }
        }

        private void LogMessage(string message)
        {
            try
            {
                string fileName = $"log_{DateTime.Now:yyyyMMdd}.txt";
                string filePath = Path.Combine(logDirectory, fileName);
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";

                File.AppendAllText(filePath, logEntry + Environment.NewLine);
            }
            catch
            {
                // Log yazma hatalarını sessizce geç
            }
        }

        private void LogError(string errorMessage)
        {
            try
            {
                // Normal log'a da yaz
                LogMessage($"ERROR: {errorMessage}");

                // Error log dosyasına yaz
                string fileName = $"error_{DateTime.Now:yyyyMMdd}.txt";
                string filePath = Path.Combine(logDirectory, fileName);
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERROR: {errorMessage}";

                File.AppendAllText(filePath, logEntry + Environment.NewLine);
            }
            catch
            {
                // Log yazma hatalarını sessizce geç
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Timer'ları temizle
                if (startupTimer != null)
                {
                    startupTimer.Stop();
                    startupTimer.Dispose();
                }

                if (balloonTimer != null)
                {
                    balloonTimer.Stop();
                    balloonTimer.Dispose();
                }

                if (syncTimer != null)
                {
                    syncTimer.Stop();
                    syncTimer.Dispose();
                }

                if (displayTimer != null)
                {
                    displayTimer.Stop();
                    displayTimer.Dispose();
                }

                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Menu Event Handlers

        private void başlatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void çıkışToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            çıkışToolStripMenuItem_Click(sender, e);
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frmAbout = new FrmAbout())
                {
                    frmAbout.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                LogError($"Hakkında formu açılırken hata: {ex.Message}");
                MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #endregion
    }
}
