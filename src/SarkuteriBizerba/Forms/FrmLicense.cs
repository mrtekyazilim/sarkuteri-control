using System;
using System.IO;
using System.Windows.Forms;
using SarkuteriBizerba.Classes;

namespace SarkuteriBizerba.Forms
{
  public partial class FrmLicense : Form
  {
    public bool LicenseActivated { get; private set; }

    public FrmLicense()
    {
      InitializeComponent();
      LoadCurrentInfo();
    }

    private void LoadCurrentInfo()
    {
      try
      {
        // Mevcut hardware ID'yi göster
        string hardwareID = License.GetHardwareID();
        txtHardwareID.Text = hardwareID;

        // Mevcut lisans bilgilerini kontrol et
        LicenseInfo licenseInfo = License.ValidateLicense("SarkuteriBizerba");

        if (licenseInfo.IsValid)
        {
          if (licenseInfo.LicenseType == LicenseType.Full)
          {
            lblStatus.Text = $"Durum: Lisanslı - {licenseInfo.CustomerName}";
            lblStatus.ForeColor = System.Drawing.Color.Green;
            lblExpiry.Text = $"Geçerlilik: {licenseInfo.ExpiryDate:dd.MM.yyyy} ({licenseInfo.DaysRemaining} gün kaldı)";
            btnActivate.Text = "Lisansı Güncelle";
          }
          else if (licenseInfo.LicenseType == LicenseType.Trial)
          {
            lblStatus.Text = "Durum: Deneme Sürümü";
            lblStatus.ForeColor = System.Drawing.Color.Orange;
            lblExpiry.Text = $"Kalan: {licenseInfo.DaysRemaining} gün";
          }
        }
        else
        {
          lblStatus.Text = "Durum: Lisanssız";
          lblStatus.ForeColor = System.Drawing.Color.Red;
          lblExpiry.Text = licenseInfo.Message;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Bilgi yüklenirken hata: {ex.Message}", "Hata",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btnCopyHardwareID_Click(object sender, EventArgs e)
    {
      try
      {
        if (!string.IsNullOrEmpty(txtHardwareID.Text))
        {
          Clipboard.SetText(txtHardwareID.Text);
          MessageBox.Show("Hardware ID kopyalandı!", "Başarılı",
              MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Kopyalama hatası: {ex.Message}", "Hata",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btnActivate_Click(object sender, EventArgs e)
    {
      try
      {
        string licenseKey = txtLicenseKey.Text.Trim();

        if (string.IsNullOrEmpty(licenseKey))
        {
          MessageBox.Show("Lütfen lisans anahtarını girin!", "Uyarı",
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
          txtLicenseKey.Focus();
          return;
        }

        // Lisans anahtarını doğrula ve License.key dosyası oluştur
        if (ActivateLicense(licenseKey))
        {
          MessageBox.Show("Lisans başarıyla aktifleştirildi!\n\nUygulama yeniden başlatılacak.",
              "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

          LicenseActivated = true;
          DialogResult = DialogResult.OK;
          this.Close();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lisans aktivasyonu başarısız: {ex.Message}", "Hata",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private bool ActivateLicense(string licenseKeyContent)
    {
      try
      {
        // License.key dosyasını oluştur
        string licenseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "License.key");

        // Lisans içeriğini kontrol et (en az 5 satır olmalı)
        string[] lines = licenseKeyContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        if (lines.Length < 5)
        {
          MessageBox.Show("Geçersiz lisans formatı! Tam lisans anahtarını yapıştırın.", "Hata",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
          return false;
        }

        // Dosyaya yaz
        File.WriteAllText(licenseFilePath, licenseKeyContent);

        // Lisansı doğrula
        LicenseInfo info = License.ValidateLicense("SarkuteriBizerba");

        if (!info.IsValid)
        {
          // Geçersiz lisans, dosyayı sil
          File.Delete(licenseFilePath);
          MessageBox.Show($"Lisans geçersiz!\n\n{info.Message}", "Hata",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
          return false;
        }

        return true;
      }
      catch (Exception ex)
      {
        throw new Exception($"Lisans dosyası oluşturulamadı: {ex.Message}");
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      LicenseActivated = false;
      DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void btnLoadFromFile_Click(object sender, EventArgs e)
    {
      try
      {
        using (OpenFileDialog ofd = new OpenFileDialog())
        {
          ofd.Title = "Lisans Dosyası Seçin";
          ofd.Filter = "License Files (*.key)|*.key|All Files (*.*)|*.*";
          ofd.FilterIndex = 1;

          if (ofd.ShowDialog() == DialogResult.OK)
          {
            string content = File.ReadAllText(ofd.FileName);
            txtLicenseKey.Text = content;
            MessageBox.Show("Lisans dosyası yüklendi!", "Başarılı",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Dosya yüklenirken hata: {ex.Message}", "Hata",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
