using Ruhsat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SarkuteriAdmin.Classes
{
    public class License
    {
        /// <summary>
        /// Örnek 1: Basit lisans kontrolü
        /// </summary>
        public static async Task<bool> SimpleValidationExample()
        {
            try
            {
                // Ürün kodunuz ile LicenseManager başlatın
                var licenseManager = new LicenseManager("BARCODE");

                // Lisansı doğrula
                var result = await licenseManager.ValidateLicenseAsync();

                if (result.IsValid && result.IsActive)
                {
                    MessageBox.Show(
                        $"Hoş geldiniz!\n\n" +
                        $"Müşteri: {result.CustomerName}\n" +
                        $"Ürün: {result.ProductName}\n" +
                        $"Lisans Tipi: {result.LicenseType}\n" +
                        $"{result.Message}",
                        "Lisans Geçerli",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return true;
                }
                else
                {
                    MessageBox.Show(
                        $"Lisans Hatası!\n\n{result.Message}\n\nHata Kodu: {result.ErrorCode}",
                        "Lisans Geçersiz",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lisans kontrolü sırasında hata:\n{ex.Message}",
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
        }

        /// <summary>
        /// Örnek 2: Uygulama başlatma kontrolü
        /// </summary>
        public static async Task<bool> ApplicationStartupCheck(string productCode)
        {
            try
            {
                var licenseManager = new LicenseManager(productCode);
                var result = await licenseManager.ValidateLicenseAsync();

                if (!result.IsValid || !result.IsActive)
                {
                    // Lisans geçersiz - kullanıcıya bildir
                    DialogResult dialogResult = MessageBox.Show(
                        $"{result.Message}\n\n" +
                        $"Uygulama kapatılacak. Lisans bilgilerinizi güncellemek için sistem yöneticinizle iletişime geçin.\n\n" +
                        $"Lisans ayarlarını şimdi düzenlemek ister misiniz?",
                        "Lisans Gerekli",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (dialogResult == DialogResult.Yes)
                    {
                        // Lisans ayarları formunu aç
                        ShowLicenseSettingsForm(licenseManager);
                    }

                    return false;
                }

                // Online/Offline kontrolü mesajı
                string connectionStatus = result.ValidatedOnline ? "Online" : "Offline (Cache)";
                Console.WriteLine($"Lisans doğrulandı: {connectionStatus}");
                Console.WriteLine($"Son kontrol: {result.LastCheckDate}");

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lisans kontrolü başarısız:\n{ex.Message}",
                    "Kritik Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
        }

        /// <summary>
        /// Örnek 3: Hardware ID alma ve gösterme
        /// </summary>
        public static void ShowHardwareIdExample()
        {
            try
            {
                var licenseManager = new LicenseManager("DEMO");
                string hardwareId = licenseManager.GetHardwareId();

                MessageBox.Show(
                    $"Bu bilgisayarın Hardware ID'si:\n\n{hardwareId}\n\n" +
                    $"Bu kodu lisans almak için sistem yöneticinize iletin.",
                    "Hardware ID",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Hardware ID alınamadı:\n{ex.Message}",
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Örnek 4: Lisans ayarları formu
        /// </summary>
        private static void ShowLicenseSettingsForm(LicenseManager licenseManager)
        {
            Form settingsForm = new Form
            {
                Text = "Lisans Ayarları",
                Width = 500,
                Height = 300,
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // API URL
            Label lblApiUrl = new Label
            {
                Text = "API URL:",
                Left = 20,
                Top = 20,
                Width = 100
            };
            TextBox txtApiUrl = new TextBox
            {
                Left = 130,
                Top = 20,
                Width = 320,
                Text = "http://localhost:10501/api"
            };

            // License Key
            Label lblLicenseKey = new Label
            {
                Text = "Lisans Anahtarı:",
                Left = 20,
                Top = 60,
                Width = 100
            };
            TextBox txtLicenseKey = new TextBox
            {
                Left = 130,
                Top = 60,
                Width = 320,
                Multiline = true,
                Height = 80
            };

            // Hardware ID (Read-only)
            Label lblHardwareId = new Label
            {
                Text = "Hardware ID:",
                Left = 20,
                Top = 150,
                Width = 100
            };
            TextBox txtHardwareId = new TextBox
            {
                Left = 130,
                Top = 150,
                Width = 320,
                ReadOnly = true,
                BackColor = System.Drawing.Color.LightGray,
                Text = licenseManager.GetHardwareId()
            };

            // Save button
            Button btnSave = new Button
            {
                Text = "Kaydet",
                Left = 270,
                Top = 200,
                Width = 80
            };
            btnSave.Click += (s, e) =>
            {
                try
                {
                    licenseManager.SaveConfiguration(txtApiUrl.Text, txtLicenseKey.Text);
                    MessageBox.Show(
                              "Lisans ayarları kaydedildi!\nLütfen uygulamayı yeniden başlatın.",
                              "Başarılı",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information
                          );
                    settingsForm.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                              $"Kayıt başarısız:\n{ex.Message}",
                              "Hata",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error
                          );
                }
            };

            // Cancel button
            Button btnCancel = new Button
            {
                Text = "İptal",
                Left = 360,
                Top = 200,
                Width = 80
            };
            btnCancel.Click += (s, e) => settingsForm.Close();

            // Copy Hardware ID button
            Button btnCopyHwId = new Button
            {
                Text = "Kopyala",
                Left = 460,
                Top = 150,
                Width = 80
            };
            btnCopyHwId.Click += (s, e) =>
            {
                Clipboard.SetText(txtHardwareId.Text);
                MessageBox.Show("Hardware ID kopyalandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            settingsForm.Controls.Add(lblApiUrl);
            settingsForm.Controls.Add(txtApiUrl);
            settingsForm.Controls.Add(lblLicenseKey);
            settingsForm.Controls.Add(txtLicenseKey);
            settingsForm.Controls.Add(lblHardwareId);
            settingsForm.Controls.Add(txtHardwareId);
            settingsForm.Controls.Add(btnSave);
            settingsForm.Controls.Add(btnCancel);
            settingsForm.Controls.Add(btnCopyHwId);

            settingsForm.ShowDialog();
        }

        /// <summary>
        /// Örnek 5: Program.cs'de kullanım
        /// </summary>
        public static class ProgramExample
        {
            /*
            [STAThread]
            static async Task Main()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Lisans kontrolü
                bool isLicenseValid = await LicenseManagerExample.ApplicationStartupCheck("BARCODE");

                if (!isLicenseValid)
                {
                    // Lisans geçersiz - uygulamayı kapat
                    MessageBox.Show(
                        "Geçerli lisans bulunamadı. Uygulama kapatılıyor.",
                        "Lisans Hatası",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                // Lisans geçerli - ana formu başlat
                Application.Run(new MainForm());
            }
            */
        }

        /// <summary>
        /// Örnek 6: Periyodik lisans kontrolü
        /// </summary>
        public static class PeriodicCheckExample
        {
            private static System.Threading.Timer licenseCheckTimer;

            public static void StartPeriodicLicenseCheck(string productCode)
            {
                var licenseManager = new LicenseManager(productCode);

                // Her 1 saatte bir kontrol et
                licenseCheckTimer = new System.Threading.Timer(
                    async _ => await CheckLicense(licenseManager),
                    null,
                    TimeSpan.Zero,  // Hemen başlat
                    TimeSpan.FromHours(1)  // Her 1 saat
                );
            }

            private static async Task CheckLicense(LicenseManager licenseManager)
            {
                try
                {
                    var result = await licenseManager.ValidateLicenseAsync();

                    if (!result.IsValid || !result.IsActive)
                    {
                        // Lisans geçersiz oldu - kullanıcıyı bilgilendir
                        MessageBox.Show(
                            $"Lisans durumu değişti!\n\n{result.Message}\n\n" +
                            $"Uygulama kapatılacak.",
                            "Lisans Uyarısı",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );

                        // Uygulamayı kapat
                        Application.Exit();
                    }
                }
                catch
                {
                    // Hata durumunda sessizce devam et (offline mode)
                }
            }
        }
    }

}
