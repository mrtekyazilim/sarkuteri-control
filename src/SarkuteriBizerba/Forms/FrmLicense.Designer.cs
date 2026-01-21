namespace SarkuteriBizerba.Forms
{
  partial class FrmLicense
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.groupBoxInfo = new System.Windows.Forms.GroupBox();
      this.btnCopyHardwareID = new System.Windows.Forms.Button();
      this.txtHardwareID = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.lblExpiry = new System.Windows.Forms.Label();
      this.lblStatus = new System.Windows.Forms.Label();
      this.groupBoxActivation = new System.Windows.Forms.GroupBox();
      this.btnLoadFromFile = new System.Windows.Forms.Button();
      this.btnActivate = new System.Windows.Forms.Button();
      this.txtLicenseKey = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.panelButtons = new System.Windows.Forms.Panel();
      this.btnCancel = new System.Windows.Forms.Button();
      this.groupBoxInfo.SuspendLayout();
      this.groupBoxActivation.SuspendLayout();
      this.panelButtons.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBoxInfo
      // 
      this.groupBoxInfo.Controls.Add(this.btnCopyHardwareID);
      this.groupBoxInfo.Controls.Add(this.txtHardwareID);
      this.groupBoxInfo.Controls.Add(this.label1);
      this.groupBoxInfo.Controls.Add(this.lblExpiry);
      this.groupBoxInfo.Controls.Add(this.lblStatus);
      this.groupBoxInfo.Location = new System.Drawing.Point(12, 12);
      this.groupBoxInfo.Name = "groupBoxInfo";
      this.groupBoxInfo.Size = new System.Drawing.Size(660, 140);
      this.groupBoxInfo.TabIndex = 0;
      this.groupBoxInfo.TabStop = false;
      this.groupBoxInfo.Text = "Lisans Bilgileri";
      // 
      // btnCopyHardwareID
      // 
      this.btnCopyHardwareID.Location = new System.Drawing.Point(559, 98);
      this.btnCopyHardwareID.Name = "btnCopyHardwareID";
      this.btnCopyHardwareID.Size = new System.Drawing.Size(85, 27);
      this.btnCopyHardwareID.TabIndex = 4;
      this.btnCopyHardwareID.Text = "Kopyala";
      this.btnCopyHardwareID.UseVisualStyleBackColor = true;
      this.btnCopyHardwareID.Click += new System.EventHandler(this.btnCopyHardwareID_Click);
      // 
      // txtHardwareID
      // 
      this.txtHardwareID.Location = new System.Drawing.Point(16, 100);
      this.txtHardwareID.Name = "txtHardwareID";
      this.txtHardwareID.ReadOnly = true;
      this.txtHardwareID.Size = new System.Drawing.Size(537, 23);
      this.txtHardwareID.TabIndex = 3;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(16, 82);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(412, 15);
      this.label1.TabIndex = 2;
      this.label1.Text = "Hardware ID (Bu ID\'yi Dinamik Otomasyon\'a göndererek lisans alabilirsiniz):";
      // 
      // lblExpiry
      // 
      this.lblExpiry.AutoSize = true;
      this.lblExpiry.Location = new System.Drawing.Point(16, 50);
      this.lblExpiry.Name = "lblExpiry";
      this.lblExpiry.Size = new System.Drawing.Size(62, 15);
      this.lblExpiry.TabIndex = 1;
      this.lblExpiry.Text = "Geçerlilik: -";
      // 
      // lblStatus
      // 
      this.lblStatus.AutoSize = true;
      this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblStatus.ForeColor = System.Drawing.Color.Orange;
      this.lblStatus.Location = new System.Drawing.Point(16, 25);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(57, 15);
      this.lblStatus.TabIndex = 0;
      this.lblStatus.Text = "Durum: -";
      // 
      // groupBoxActivation
      // 
      this.groupBoxActivation.Controls.Add(this.btnLoadFromFile);
      this.groupBoxActivation.Controls.Add(this.btnActivate);
      this.groupBoxActivation.Controls.Add(this.txtLicenseKey);
      this.groupBoxActivation.Controls.Add(this.label2);
      this.groupBoxActivation.Location = new System.Drawing.Point(12, 158);
      this.groupBoxActivation.Name = "groupBoxActivation";
      this.groupBoxActivation.Size = new System.Drawing.Size(660, 220);
      this.groupBoxActivation.TabIndex = 1;
      this.groupBoxActivation.TabStop = false;
      this.groupBoxActivation.Text = "Lisans Aktivasyonu";
      // 
      // btnLoadFromFile
      // 
      this.btnLoadFromFile.Location = new System.Drawing.Point(16, 178);
      this.btnLoadFromFile.Name = "btnLoadFromFile";
      this.btnLoadFromFile.Size = new System.Drawing.Size(140, 32);
      this.btnLoadFromFile.TabIndex = 3;
      this.btnLoadFromFile.Text = "Dosyadan Yükle";
      this.btnLoadFromFile.UseVisualStyleBackColor = true;
      this.btnLoadFromFile.Click += new System.EventHandler(this.btnLoadFromFile_Click);
      // 
      // btnActivate
      // 
      this.btnActivate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
      this.btnActivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnActivate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.btnActivate.ForeColor = System.Drawing.Color.White;
      this.btnActivate.Location = new System.Drawing.Point(504, 178);
      this.btnActivate.Name = "btnActivate";
      this.btnActivate.Size = new System.Drawing.Size(140, 32);
      this.btnActivate.TabIndex = 2;
      this.btnActivate.Text = "Lisansı Aktifleştir";
      this.btnActivate.UseVisualStyleBackColor = false;
      this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
      // 
      // txtLicenseKey
      // 
      this.txtLicenseKey.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.txtLicenseKey.Location = new System.Drawing.Point(16, 45);
      this.txtLicenseKey.Multiline = true;
      this.txtLicenseKey.Name = "txtLicenseKey";
      this.txtLicenseKey.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtLicenseKey.Size = new System.Drawing.Size(628, 127);
      this.txtLicenseKey.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(16, 27);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(377, 15);
      this.label2.TabIndex = 0;
      this.label2.Text = "Lisans Anahtarı (Dinamik Otomasyon\'dan aldığınız License.key içeriği):";
      // 
      // panelButtons
      // 
      this.panelButtons.Controls.Add(this.btnCancel);
      this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panelButtons.Location = new System.Drawing.Point(0, 390);
      this.panelButtons.Name = "panelButtons";
      this.panelButtons.Size = new System.Drawing.Size(684, 50);
      this.panelButtons.TabIndex = 2;
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(584, 10);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(88, 32);
      this.btnCancel.TabIndex = 0;
      this.btnCancel.Text = "İptal";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // FrmLicense
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(684, 440);
      this.Controls.Add(this.panelButtons);
      this.Controls.Add(this.groupBoxActivation);
      this.Controls.Add(this.groupBoxInfo);
      this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FrmLicense";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "SarkuteriBizerba - Lisans Yönetimi";
      this.groupBoxInfo.ResumeLayout(false);
      this.groupBoxInfo.PerformLayout();
      this.groupBoxActivation.ResumeLayout(false);
      this.groupBoxActivation.PerformLayout();
      this.panelButtons.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBoxInfo;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Label lblExpiry;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtHardwareID;
    private System.Windows.Forms.Button btnCopyHardwareID;
    private System.Windows.Forms.GroupBox groupBoxActivation;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtLicenseKey;
    private System.Windows.Forms.Button btnActivate;
    private System.Windows.Forms.Panel panelButtons;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnLoadFromFile;
  }
}
