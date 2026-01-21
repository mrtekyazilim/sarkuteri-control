using SarkuteriBizerba.Forms;
using SarkuteriBizerba.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SarkuteriBizerba
{
    internal static class Program
    {
        private static Mutex mutex = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            const string mutexName = "SarkuteriBizerba_SingleInstance_Mutex";
            bool createdNew;

            mutex = new Mutex(true, mutexName, out createdNew);

            if (!createdNew)
            {
                // Uygulama zaten çalışıyor
                DialogResult result = MessageBox.Show(
                    "SarkuteriBizerba uygulaması zaten çalışıyor.\n\n" +
                    "Eski uygulamayı kapatıp yenisini başlatmak ister misiniz?",
                    "Uygulama Zaten Çalışıyor",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Eski process'i bul ve kapat
                    if (KillExistingProcess())
                    {
                        // Mutex'i serbest bırak ve yeniden oluştur
                        mutex.Close();
                        mutex = new Mutex(true, mutexName, out createdNew);

                        if (!createdNew)
                        {
                            MessageBox.Show("Eski uygulama kapatıldı ancak yeni başlatılamadı. Lütfen tekrar deneyin.",
                                          "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Eski uygulama kapatılamadı. Lütfen manuel olarak kapatıp tekrar deneyin.",
                                      "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    // Kullanıcı hayır dedi, uygulamayı kapat
                    return;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            // Database connection'ını aç
            if (!Helper.OpenConnection())
            {
                // Bağlantı başarısız, FrmDb ile konfigürasyon yap
                using (var frmDb = new FrmDb())
                {
                    if (frmDb.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("Veritabanı bağlantısı yapılandırılamadı. Uygulama kapatılıyor.",
                                      "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Tekrar bağlantı dene
                if (!Helper.OpenConnection())
                {
                    MessageBox.Show("Veritabanı bağlantısı kurulamadı. Uygulama kapatılıyor.",
                                  "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Bağlantı başarılı, DINAMIK_BIZERBA tablosunu kontrol et ve oluştur
            try
            {
                DinamikBizerba.EnsureDinamikBizerbaTableExists();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanı tablosu oluşturulurken hata:\n{ex.Message}",
                              "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            bool isLicensed = await Ruhsat.LicenseManager.CheckLicense("SARKUTERI_BIZERBA");

            if (isLicensed)
            {
                Application.Run(new FrmMain());
            }
            // Uygulama kapanırken connection'ı kapat
            Helper.CloseConnection();

            // Mutex'i serbest bırak
            if (mutex != null)
            {
                try
                {
                    mutex.ReleaseMutex();
                    mutex.Close();

                }
                catch { }
            }
        }

        private static bool KillExistingProcess()
        {
            try
            {
                // Şu anki process'in adını al
                string currentProcessName = Process.GetCurrentProcess().ProcessName;
                int currentProcessId = Process.GetCurrentProcess().Id;

                // Aynı isimdeki tüm process'leri bul
                Process[] processes = Process.GetProcessesByName(currentProcessName);

                foreach (Process process in processes)
                {
                    // Kendi process'imizi atla
                    if (process.Id != currentProcessId)
                    {
                        try
                        {
                            process.Kill();
                            process.WaitForExit(5000); // 5 saniye bekle
                            return true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Process kapatılırken hata: {ex.Message}",
                                          "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eski uygulama aranırken hata: {ex.Message}",
                              "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
