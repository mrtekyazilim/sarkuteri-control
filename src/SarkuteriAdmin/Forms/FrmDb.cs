using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SarkuteriAdmin.Classes;

namespace SarkuteriAdmin.Forms
{
  public partial class FrmDb : Form
  {
    public FrmDb()
    {
      InitializeComponent();
    }

    private void FrmDb_Load(object sender, EventArgs e)
    {
      LoadSettings();
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
