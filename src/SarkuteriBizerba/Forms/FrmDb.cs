using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SarkuteriBizerba.Classes;

namespace SarkuteriBizerba.Forms
{
  public partial class FrmDb : Form
  {
    private bool passwordVerified = false;

    public FrmDb()
    {
      InitializeComponent();
    }

    private void FrmDb_Load(object sender, EventArgs e)
    {
      // Önce şifre kontrolü yap
      if (!CheckPassword())
      {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
        return;
      }

      LoadSettings();
    }

    private bool CheckPassword()
    {
      try
      {
        string savedPassword = Helper.AdminPassword;

        // Şifre yoksa kontrol yapma
        if (string.IsNullOrWhiteSpace(savedPassword))
        {
          passwordVerified = true;
          return true;
        }

        // Şifre varsa kullanıcıdan şifre iste
        using (var passwordForm = new Form())
        {
          passwordForm.Text = "Şifre Gerekli";
          passwordForm.Size = new Size(350, 150);
          passwordForm.StartPosition = FormStartPosition.CenterParent;
          passwordForm.FormBorderStyle = FormBorderStyle.FixedDialog;
          passwordForm.MaximizeBox = false;
          passwordForm.MinimizeBox = false;

          var label = new Label { Left = 20, Top = 20, Text = "Yönetici Şifresi:", AutoSize = true };
          var textBox = new TextBox { Left = 20, Top = 45, Width = 290, UseSystemPasswordChar = true };
          var btnOk = new Button { Text = "Tamam", Left = 150, Width = 80, Top = 75, DialogResult = DialogResult.OK };
          var btnCancel = new Button { Text = "İptal", Left = 235, Width = 80, Top = 75, DialogResult = DialogResult.Cancel };

          passwordForm.Controls.Add(label);
          passwordForm.Controls.Add(textBox);
          passwordForm.Controls.Add(btnOk);
          passwordForm.Controls.Add(btnCancel);
          passwordForm.AcceptButton = btnOk;
          passwordForm.CancelButton = btnCancel;

          if (passwordForm.ShowDialog() == DialogResult.OK)
          {
            if (textBox.Text == savedPassword)
            {
              passwordVerified = true;
              return true;
            }
            else
            {
              MessageBox.Show("Hatalı şifre!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
              return false;
            }
          }
          else
          {
            return false;
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Şifre kontrolü yapılırken hata oluştu: {ex.Message}", "Hata",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }
    }

    private void LoadSettings()
    {
      try
      {
        txtServer.Text = Helper.Server;
        txtDatabase.Text = Helper.Database;
        chkNtAuth.Checked = Helper.NtAuth;
        txtSqlUser.Text = Helper.SqlUser;
        txtSqlPass.Text = Helper.SqlPass;

        UpdateAuthControls();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Ayarlar yüklenirken hata oluştu: {ex.Message}", "Hata",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void chkNtAuth_CheckedChanged(object sender, EventArgs e)
    {
      UpdateAuthControls();
    }

    private void UpdateAuthControls()
    {
      bool enableSqlAuth = !chkNtAuth.Checked;

      lblSqlUser.Enabled = enableSqlAuth;
      txtSqlUser.Enabled = enableSqlAuth;
      lblSqlPass.Enabled = enableSqlAuth;
      txtSqlPass.Enabled = enableSqlAuth;
    }

    private void btnTestConnection_Click(object sender, EventArgs e)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(txtServer.Text))
        {
          MessageBox.Show("Server adresini giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          txtServer.Focus();
          return;
        }

        if (string.IsNullOrWhiteSpace(txtDatabase.Text))
        {
          MessageBox.Show("Database adını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          txtDatabase.Focus();
          return;
        }

        if (!chkNtAuth.Checked && string.IsNullOrWhiteSpace(txtSqlUser.Text))
        {
          MessageBox.Show("SQL kullanıcı adını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          txtSqlUser.Focus();
          return;
        }

        btnTestConnection.Enabled = false;
        btnTestConnection.Text = "Test ediliyor...";

        // Ayarları geçici olarak güncelle ve bağlantıyı test et
        Helper.Server = txtServer.Text.Trim();
        Helper.Database = txtDatabase.Text.Trim();
        Helper.NtAuth = chkNtAuth.Checked;
        Helper.SqlUser = txtSqlUser.Text.Trim();
        Helper.SqlPass = txtSqlPass.Text;

        Helper.ReloadConnection();

        if (Helper.TestConnection())
        {
          MessageBox.Show("Bağlantı başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
          MessageBox.Show("Bağlantı başarısız! Lütfen ayarları kontrol ediniz.", "Hata",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Bağlantı test edilirken hata oluştu: {ex.Message}", "Hata",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      finally
      {
        btnTestConnection.Enabled = true;
        btnTestConnection.Text = "Bağlantıyı Test Et";
      }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(txtServer.Text))
        {
          MessageBox.Show("Server adresini giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          txtServer.Focus();
          return;
        }

        if (string.IsNullOrWhiteSpace(txtDatabase.Text))
        {
          MessageBox.Show("Database adını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          txtDatabase.Focus();
          return;
        }

        if (!chkNtAuth.Checked && string.IsNullOrWhiteSpace(txtSqlUser.Text))
        {
          MessageBox.Show("SQL kullanıcı adını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          txtSqlUser.Focus();
          return;
        }

        // Ayarları kaydet
        Helper.Server = txtServer.Text.Trim();
        Helper.Database = txtDatabase.Text.Trim();
        Helper.NtAuth = chkNtAuth.Checked;
        Helper.SqlUser = txtSqlUser.Text.Trim();
        Helper.SqlPass = txtSqlPass.Text;

        // Bağlantıyı yeniden yükle
        Helper.ReloadConnection();

        MessageBox.Show("SQL bağlantı ayarları başarıyla Config.ini dosyasına kaydedildi.", "Başarılı",
            MessageBoxButtons.OK, MessageBoxIcon.Information);

        this.DialogResult = DialogResult.OK;
        this.Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Ayarlar kaydedilirken hata oluştu: {ex.Message}", "Hata",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}