using System;
using System.Windows.Forms;
using SarkuteriAdmin.Classes;

namespace SarkuteriAdmin.Forms
{
  public partial class FrmSettings : Form
  {
    public FrmSettings()
    {
      InitializeComponent();
    }

    private void FrmSettings_Load(object sender, EventArgs e)
    {
      try
      {
        // Mevcut ayarları yükle
        numT1.Value = Helper.LabelTimeT1;
        numT2.Value = Helper.LabelTimeT2;
        numTimerInterval.Value = Helper.AutoApproveInterval;
        chkAutoApproveActive.Checked = Helper.AutoApproveActive;

        // WeightTolerance'ı yükle (kg cinsinden)
        numWeightTolerance.Value = Helper.WeightTolerance;
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Ayarlar yüklenirken hata: {ex.Message}", "Hata",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        int t1 = (int)numT1.Value;
        int t2 = (int)numT2.Value;
        int timerInterval = (int)numTimerInterval.Value;
        bool autoApproveActive = chkAutoApproveActive.Checked;
        decimal weightTolerance = numWeightTolerance.Value;

        // T1 ve T2 kontrolü
        if (t1 >= 0 && t2 >= 0 && t1 >= t2)
        {
          MessageBox.Show("T1 değeri T2'den küçük olmalıdır!", "Hata",
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }

        // Weight Tolerance kontrolü
        if (weightTolerance <= 0)
        {
          MessageBox.Show("Ağırlık toleransı sıfırdan büyük olmalıdır!", "Hata",
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }

        // Ayarları kaydet
        Helper.SaveLabelTimeSettings(t1, t2);
        Helper.SaveAutoApproveSettings(timerInterval, autoApproveActive);
        Helper.SaveWeightToleranceSettings(weightTolerance);

        string message = string.Format("Ayarlar başarıyla kaydedildi.\n\n" +
                        "T1: {0} saniye\n" +
                        "T2: {1} saniye\n" +
                        "Ağırlık Toleransı: {2:F3} kg (±{3:F0} gram)\n" +
                        "Timer Aralığı: {4} dakika\n" +
                        "Otomatik Onaylama: {5}",
                        t1, t2, weightTolerance, weightTolerance * 1000, timerInterval,
                        autoApproveActive ? "Aktif" : "Pasif");

        MessageBox.Show(message, "Başarılı",
            MessageBoxButtons.OK, MessageBoxIcon.Information);

        Helper.WriteLog(string.Format("Ayarlar güncellendi: T1={0}, T2={1}, WeightTolerance={2}, TimerInterval={3}, AutoApproveActive={4}",
            t1, t2, weightTolerance, timerInterval, autoApproveActive));

        this.DialogResult = DialogResult.OK;
        this.Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show(string.Format("Ayarlar kaydedilirken hata: {0}", ex.Message), "Hata",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        Helper.WriteErrorLog("FrmSettings - btnSave_Click hatası", ex);
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void btnChangePassword_Click(object sender, EventArgs e)
    {
      try
      {
        string newPassword = txtNewPassword.Text.Trim();
        string confirmPassword = txtNewPasswordConfirm.Text.Trim();

        if (string.IsNullOrEmpty(newPassword))
        {
          MessageBox.Show("Lütfen yeni şifre giriniz!", "Uyarı",
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
          txtNewPassword.Focus();
          return;
        }

        if (newPassword.Length < 4)
        {
          MessageBox.Show("Şifre en az 4 karakter olmalıdır!", "Uyarı",
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
          txtNewPassword.Focus();
          return;
        }

        if (newPassword != confirmPassword)
        {
          MessageBox.Show("Şifreler eşleşmiyor! Lütfen tekrar deneyiniz.", "Uyarı",
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
          txtNewPasswordConfirm.Focus();
          return;
        }

        Helper.SaveSettingsPassword(newPassword);

        MessageBox.Show("Şifre başarıyla değiştirildi!", "Başarılı",
            MessageBoxButtons.OK, MessageBoxIcon.Information);

        Helper.WriteLog($"Ayarlar şifresi değiştirildi.");

        txtNewPassword.Clear();
        txtNewPasswordConfirm.Clear();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Şifre değiştirilirken hata: {ex.Message}", "Hata",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        Helper.WriteErrorLog("FrmSettings - btnChangePassword_Click hatası", ex);
      }
    }
  }
}
