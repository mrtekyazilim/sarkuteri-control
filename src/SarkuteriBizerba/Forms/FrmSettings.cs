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
    public partial class FrmSettings : Form
    {
        private bool passwordVerified = false;

        public FrmSettings()
        {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
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
                // Genel ayarlar
                numInterval.Value = Helper.SyncIntervalMinutes;
                txtStoreNum.Text = Helper.StoreNum.ToString();
                txtAdminPassword.Text = Helper.AdminPassword;

                // Bizerba IP adresleri ve isimleri
                txtIP1.Text = Helper.BizerbaIP1;
                txtName1.Text = Helper.BizerbaName1;
                txtIP2.Text = Helper.BizerbaIP2;
                txtName2.Text = Helper.BizerbaName2;
                txtIP3.Text = Helper.BizerbaIP3;
                txtName3.Text = Helper.BizerbaName3;
                txtIP4.Text = Helper.BizerbaIP4;
                txtName4.Text = Helper.BizerbaName4;
                txtIP5.Text = Helper.BizerbaIP5;
                txtName5.Text = Helper.BizerbaName5;
                txtIP6.Text = Helper.BizerbaIP6;
                txtName6.Text = Helper.BizerbaName6;
                txtIP7.Text = Helper.BizerbaIP7;
                txtName7.Text = Helper.BizerbaName7;
                txtIP8.Text = Helper.BizerbaIP8;
                txtName8.Text = Helper.BizerbaName8;
                txtIP9.Text = Helper.BizerbaIP9;
                txtName9.Text = Helper.BizerbaName9;
                txtIP10.Text = Helper.BizerbaIP10;
                txtName10.Text = Helper.BizerbaName10;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ayarlar yüklenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtStoreNum.Text) || !int.TryParse(txtStoreNum.Text, out int storeNum))
                {
                    MessageBox.Show("Geçerli bir mağaza numarası giriniz (sayısal değer).", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtStoreNum.Focus();
                    return;
                }

                // Genel ayarları kaydet
                Helper.SyncIntervalMinutes = (int)numInterval.Value;
                Helper.StoreNum = int.Parse(txtStoreNum.Text.Trim());
                Helper.AdminPassword = txtAdminPassword.Text.Trim();

                // Bizerba IP adreslerini ve isimlerini kaydet
                Helper.BizerbaIP1 = txtIP1.Text.Trim();
                Helper.BizerbaName1 = txtName1.Text.Trim();
                Helper.BizerbaIP2 = txtIP2.Text.Trim();
                Helper.BizerbaName2 = txtName2.Text.Trim();
                Helper.BizerbaIP3 = txtIP3.Text.Trim();
                Helper.BizerbaName3 = txtName3.Text.Trim();
                Helper.BizerbaIP4 = txtIP4.Text.Trim();
                Helper.BizerbaName4 = txtName4.Text.Trim();
                Helper.BizerbaIP5 = txtIP5.Text.Trim();
                Helper.BizerbaName5 = txtName5.Text.Trim();
                Helper.BizerbaIP6 = txtIP6.Text.Trim();
                Helper.BizerbaName6 = txtName6.Text.Trim();
                Helper.BizerbaIP7 = txtIP7.Text.Trim();
                Helper.BizerbaName7 = txtName7.Text.Trim();
                Helper.BizerbaIP8 = txtIP8.Text.Trim();
                Helper.BizerbaName8 = txtName8.Text.Trim();
                Helper.BizerbaIP9 = txtIP9.Text.Trim();
                Helper.BizerbaName9 = txtName9.Text.Trim();
                Helper.BizerbaIP10 = txtIP10.Text.Trim();
                Helper.BizerbaName10 = txtName10.Text.Trim();

                MessageBox.Show("Ayarlar başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ayarlar kaydedilirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
