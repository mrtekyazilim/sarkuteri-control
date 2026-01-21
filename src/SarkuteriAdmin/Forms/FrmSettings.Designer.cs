namespace SarkuteriAdmin.Forms
{
      partial class FrmSettings
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
                  this.groupBoxLabelTime = new System.Windows.Forms.GroupBox();
                  this.lblT2Info = new System.Windows.Forms.Label();
                  this.lblT1Info = new System.Windows.Forms.Label();
                  this.numT2 = new System.Windows.Forms.NumericUpDown();
                  this.numT1 = new System.Windows.Forms.NumericUpDown();
                  this.lblT2 = new System.Windows.Forms.Label();
                  this.lblT1 = new System.Windows.Forms.Label();
                  this.groupBoxAutoApprove = new System.Windows.Forms.GroupBox();
                  this.lblTimerInfo = new System.Windows.Forms.Label();
                  this.numTimerInterval = new System.Windows.Forms.NumericUpDown();
                  this.lblTimerInterval = new System.Windows.Forms.Label();
                  this.chkAutoApproveActive = new System.Windows.Forms.CheckBox();
                  this.groupBoxWeightTolerance = new System.Windows.Forms.GroupBox();
                  this.lblWeightToleranceInfo = new System.Windows.Forms.Label();
                  this.numWeightTolerance = new System.Windows.Forms.NumericUpDown();
                  this.lblWeightTolerance = new System.Windows.Forms.Label();
                  this.btnSave = new System.Windows.Forms.Button();
                  this.btnCancel = new System.Windows.Forms.Button();
                  this.groupBoxPassword = new System.Windows.Forms.GroupBox();
                  this.btnChangePassword = new System.Windows.Forms.Button();
                  this.txtNewPasswordConfirm = new System.Windows.Forms.TextBox();
                  this.lblNewPasswordConfirm = new System.Windows.Forms.Label();
                  this.txtNewPassword = new System.Windows.Forms.TextBox();
                  this.lblNewPassword = new System.Windows.Forms.Label();
                  this.groupBoxLabelTime.SuspendLayout();
                  ((System.ComponentModel.ISupportInitialize)(this.numT2)).BeginInit();
                  ((System.ComponentModel.ISupportInitialize)(this.numT1)).BeginInit();
                  this.groupBoxAutoApprove.SuspendLayout();
                  ((System.ComponentModel.ISupportInitialize)(this.numTimerInterval)).BeginInit();
                  this.groupBoxWeightTolerance.SuspendLayout();
                  ((System.ComponentModel.ISupportInitialize)(this.numWeightTolerance)).BeginInit();
                  this.groupBoxPassword.SuspendLayout();
                  this.SuspendLayout();
                  // 
                  // groupBoxLabelTime
                  // 
                  this.groupBoxLabelTime.Controls.Add(this.lblT2Info);
                  this.groupBoxLabelTime.Controls.Add(this.lblT1Info);
                  this.groupBoxLabelTime.Controls.Add(this.numT2);
                  this.groupBoxLabelTime.Controls.Add(this.numT1);
                  this.groupBoxLabelTime.Controls.Add(this.lblT2);
                  this.groupBoxLabelTime.Controls.Add(this.lblT1);
                  this.groupBoxLabelTime.Location = new System.Drawing.Point(12, 12);
                  this.groupBoxLabelTime.Name = "groupBoxLabelTime";
                  this.groupBoxLabelTime.Size = new System.Drawing.Size(460, 150);
                  this.groupBoxLabelTime.TabIndex = 0;
                  this.groupBoxLabelTime.TabStop = false;
                  this.groupBoxLabelTime.Text = "Etiket Otomatik Onaylama Zamanı Ayarları";
                  // 
                  // lblT2Info
                  // 
                  this.lblT2Info.AutoSize = true;
                  this.lblT2Info.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                  this.lblT2Info.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
                  this.lblT2Info.Location = new System.Drawing.Point(200, 110);
                  this.lblT2Info.Name = "lblT2Info";
                  this.lblT2Info.Size = new System.Drawing.Size(240, 13);
                  this.lblT2Info.TabIndex = 5;
                  this.lblT2Info.Text = "Pozitif değer: etiket zamanından kaç saniye sonra";
                  // 
                  // lblT1Info
                  // 
                  this.lblT1Info.AutoSize = true;
                  this.lblT1Info.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                  this.lblT1Info.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
                  this.lblT1Info.Location = new System.Drawing.Point(200, 50);
                  this.lblT1Info.Name = "lblT1Info";
                  this.lblT1Info.Size = new System.Drawing.Size(244, 13);
                  this.lblT1Info.TabIndex = 4;
                  this.lblT1Info.Text = "Negatif değer: etiket zamanından kaç saniye önce";
                  // 
                  // numT2
                  // 
                  this.numT2.Location = new System.Drawing.Point(200, 85);
                  this.numT2.Maximum = new decimal(new int[] {
            86400,
            0,
            0,
            0});
                  this.numT2.Name = "numT2";
                  this.numT2.Size = new System.Drawing.Size(120, 20);
                  this.numT2.TabIndex = 3;
                  this.numT2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                  this.numT2.Value = new decimal(new int[] {
            3600,
            0,
            0,
            0});
                  // 
                  // numT1
                  // 
                  this.numT1.Location = new System.Drawing.Point(200, 25);
                  this.numT1.Maximum = new decimal(new int[] {
            86400,
            0,
            0,
            0});
                  this.numT1.Minimum = new decimal(new int[] {
            86400,
            0,
            0,
            -2147483648});
                  this.numT1.Name = "numT1";
                  this.numT1.Size = new System.Drawing.Size(120, 20);
                  this.numT1.TabIndex = 2;
                  this.numT1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                  this.numT1.Value = new decimal(new int[] {
            300,
            0,
            0,
            -2147483648});
                  // 
                  // lblT2
                  // 
                  this.lblT2.AutoSize = true;
                  this.lblT2.Location = new System.Drawing.Point(15, 87);
                  this.lblT2.Name = "lblT2";
                  this.lblT2.Size = new System.Drawing.Size(112, 13);
                  this.lblT2.TabIndex = 1;
                  this.lblT2.Text = "T2 (Saniye - Üst Sınır):";
                  // 
                  // lblT1
                  // 
                  this.lblT1.AutoSize = true;
                  this.lblT1.Location = new System.Drawing.Point(15, 27);
                  this.lblT1.Name = "lblT1";
                  this.lblT1.Size = new System.Drawing.Size(108, 13);
                  this.lblT1.TabIndex = 0;
                  this.lblT1.Text = "T1 (Saniye - Alt Sınır):";
                  // 
                  // groupBoxAutoApprove
                  // 
                  this.groupBoxAutoApprove.Controls.Add(this.chkAutoApproveActive);
                  this.groupBoxAutoApprove.Controls.Add(this.lblTimerInfo);
                  this.groupBoxAutoApprove.Controls.Add(this.numTimerInterval);
                  this.groupBoxAutoApprove.Controls.Add(this.lblTimerInterval);
                  this.groupBoxAutoApprove.Location = new System.Drawing.Point(12, 168);
                  this.groupBoxAutoApprove.Name = "groupBoxAutoApprove";
                  this.groupBoxAutoApprove.Size = new System.Drawing.Size(460, 110);
                  this.groupBoxAutoApprove.TabIndex = 1;
                  this.groupBoxAutoApprove.TabStop = false;
                  this.groupBoxAutoApprove.Text = "Otomatik Onaylama Timer Ayarları";
                  // 
                  // lblTimerInfo
                  // 
                  this.lblTimerInfo.AutoSize = true;
                  this.lblTimerInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                  this.lblTimerInfo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
                  this.lblTimerInfo.Location = new System.Drawing.Point(200, 50);
                  this.lblTimerInfo.Name = "lblTimerInfo";
                  this.lblTimerInfo.Size = new System.Drawing.Size(240, 13);
                  this.lblTimerInfo.TabIndex = 2;
                  this.lblTimerInfo.Text = "Otomatik onaylama işleminin kaç dakikada bir çalışacağı";
                  // 
                  // numTimerInterval
                  // 
                  this.numTimerInterval.Location = new System.Drawing.Point(200, 25);
                  this.numTimerInterval.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
                  this.numTimerInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
                  this.numTimerInterval.Name = "numTimerInterval";
                  this.numTimerInterval.Size = new System.Drawing.Size(120, 20);
                  this.numTimerInterval.TabIndex = 1;
                  this.numTimerInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                  this.numTimerInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
                  // 
                  // lblTimerInterval
                  // 
                  this.lblTimerInterval.AutoSize = true;
                  this.lblTimerInterval.Location = new System.Drawing.Point(15, 27);
                  this.lblTimerInterval.Name = "lblTimerInterval";
                  this.lblTimerInterval.Size = new System.Drawing.Size(142, 13);
                  this.lblTimerInterval.TabIndex = 0;
                  this.lblTimerInterval.Text = "Timer Aralığı (Dakika):";
                  // 
                  // chkAutoApproveActive
                  // 
                  this.chkAutoApproveActive.AutoSize = true;
                  this.chkAutoApproveActive.Checked = true;
                  this.chkAutoApproveActive.CheckState = System.Windows.Forms.CheckState.Checked;
                  this.chkAutoApproveActive.Location = new System.Drawing.Point(18, 77);
                  this.chkAutoApproveActive.Name = "chkAutoApproveActive";
                  this.chkAutoApproveActive.Size = new System.Drawing.Size(189, 17);
                  this.chkAutoApproveActive.TabIndex = 3;
                  this.chkAutoApproveActive.Text = "Otomatik Onaylama Aktif";
                  this.chkAutoApproveActive.UseVisualStyleBackColor = true;
                  // 
                  // groupBoxWeightTolerance
                  // 
                  this.groupBoxWeightTolerance.Controls.Add(this.lblWeightToleranceInfo);
                  this.groupBoxWeightTolerance.Controls.Add(this.numWeightTolerance);
                  this.groupBoxWeightTolerance.Controls.Add(this.lblWeightTolerance);
                  this.groupBoxWeightTolerance.Location = new System.Drawing.Point(12, 284);
                  this.groupBoxWeightTolerance.Name = "groupBoxWeightTolerance";
                  this.groupBoxWeightTolerance.Size = new System.Drawing.Size(460, 90);
                  this.groupBoxWeightTolerance.TabIndex = 4;
                  this.groupBoxWeightTolerance.TabStop = false;
                  this.groupBoxWeightTolerance.Text = "Ağırlık Toleransı Ayarları";
                  // 
                  // lblWeightToleranceInfo
                  // 
                  this.lblWeightToleranceInfo.AutoSize = true;
                  this.lblWeightToleranceInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                  this.lblWeightToleranceInfo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
                  this.lblWeightToleranceInfo.Location = new System.Drawing.Point(200, 50);
                  this.lblWeightToleranceInfo.Name = "lblWeightToleranceInfo";
                  this.lblWeightToleranceInfo.Size = new System.Drawing.Size(250, 13);
                  this.lblWeightToleranceInfo.TabIndex = 2;
                  this.lblWeightToleranceInfo.Text = "0.002 kg = ±2 gram tolerans (Önerilen: 0.001 - 0.005)";
                  // 
                  // numWeightTolerance
                  // 
                  this.numWeightTolerance.DecimalPlaces = 3;
                  this.numWeightTolerance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
                  this.numWeightTolerance.Location = new System.Drawing.Point(200, 25);
                  this.numWeightTolerance.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
                  this.numWeightTolerance.Name = "numWeightTolerance";
                  this.numWeightTolerance.Size = new System.Drawing.Size(120, 20);
                  this.numWeightTolerance.TabIndex = 1;
                  this.numWeightTolerance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                  this.numWeightTolerance.Value = new decimal(new int[] {
            2,
            0,
            0,
            196608});
                  // 
                  // lblWeightTolerance
                  // 
                  this.lblWeightTolerance.AutoSize = true;
                  this.lblWeightTolerance.Location = new System.Drawing.Point(15, 27);
                  this.lblWeightTolerance.Name = "lblWeightTolerance";
                  this.lblWeightTolerance.Size = new System.Drawing.Size(140, 13);
                  this.lblWeightTolerance.TabIndex = 0;
                  this.lblWeightTolerance.Text = "Ağırlık Toleransı (kg):";
                  // 
                  // btnSave
                  // 
                  this.btnSave.Location = new System.Drawing.Point(316, 500);
                  this.btnSave.Name = "btnSave";
                  this.btnSave.Size = new System.Drawing.Size(75, 30);
                  this.btnSave.TabIndex = 4;
                  this.btnSave.Text = "Kaydet";
                  this.btnSave.UseVisualStyleBackColor = true;
                  this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
                  // 
                  // btnCancel
                  // 
                  this.btnCancel.Location = new System.Drawing.Point(397, 500);
                  this.btnCancel.Name = "btnCancel";
                  this.btnCancel.Size = new System.Drawing.Size(75, 30);
                  this.btnCancel.TabIndex = 5;
                  this.btnCancel.Text = "İptal";
                  this.btnCancel.UseVisualStyleBackColor = true;
                  this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
                  // 
                  // groupBoxPassword
                  // 
                  this.groupBoxPassword.Controls.Add(this.btnChangePassword);
                  this.groupBoxPassword.Controls.Add(this.txtNewPasswordConfirm);
                  this.groupBoxPassword.Controls.Add(this.lblNewPasswordConfirm);
                  this.groupBoxPassword.Controls.Add(this.txtNewPassword);
                  this.groupBoxPassword.Controls.Add(this.lblNewPassword);
                  this.groupBoxPassword.Location = new System.Drawing.Point(12, 380);
                  this.groupBoxPassword.Name = "groupBoxPassword";
                  this.groupBoxPassword.Size = new System.Drawing.Size(460, 105);
                  this.groupBoxPassword.TabIndex = 3;
                  this.groupBoxPassword.TabStop = false;
                  this.groupBoxPassword.Text = "Şifre Değiştir";
                  // 
                  // btnChangePassword
                  // 
                  this.btnChangePassword.Location = new System.Drawing.Point(340, 65);
                  this.btnChangePassword.Name = "btnChangePassword";
                  this.btnChangePassword.Size = new System.Drawing.Size(100, 25);
                  this.btnChangePassword.TabIndex = 4;
                  this.btnChangePassword.Text = "Şifre Değiştir";
                  this.btnChangePassword.UseVisualStyleBackColor = true;
                  this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
                  // 
                  // txtNewPasswordConfirm
                  // 
                  this.txtNewPasswordConfirm.Location = new System.Drawing.Point(200, 65);
                  this.txtNewPasswordConfirm.Name = "txtNewPasswordConfirm";
                  this.txtNewPasswordConfirm.PasswordChar = '*';
                  this.txtNewPasswordConfirm.Size = new System.Drawing.Size(120, 20);
                  this.txtNewPasswordConfirm.TabIndex = 3;
                  // 
                  // lblNewPasswordConfirm
                  // 
                  this.lblNewPasswordConfirm.AutoSize = true;
                  this.lblNewPasswordConfirm.Location = new System.Drawing.Point(15, 68);
                  this.lblNewPasswordConfirm.Name = "lblNewPasswordConfirm";
                  this.lblNewPasswordConfirm.Size = new System.Drawing.Size(125, 13);
                  this.lblNewPasswordConfirm.TabIndex = 2;
                  this.lblNewPasswordConfirm.Text = "Yeni Şifre (Tekrar):";
                  // 
                  // txtNewPassword
                  // 
                  this.txtNewPassword.Location = new System.Drawing.Point(200, 30);
                  this.txtNewPassword.Name = "txtNewPassword";
                  this.txtNewPassword.PasswordChar = '*';
                  this.txtNewPassword.Size = new System.Drawing.Size(120, 20);
                  this.txtNewPassword.TabIndex = 1;
                  // 
                  // lblNewPassword
                  // 
                  this.lblNewPassword.AutoSize = true;
                  this.lblNewPassword.Location = new System.Drawing.Point(15, 33);
                  this.lblNewPassword.Name = "lblNewPassword";
                  this.lblNewPassword.Size = new System.Drawing.Size(70, 13);
                  this.lblNewPassword.TabIndex = 0;
                  this.lblNewPassword.Text = "Yeni Şifre:";
                  // 
                  // FrmSettings
                  // 
                  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                  this.ClientSize = new System.Drawing.Size(484, 542);
                  this.Controls.Add(this.groupBoxPassword);
                  this.Controls.Add(this.groupBoxWeightTolerance);
                  this.Controls.Add(this.btnCancel);
                  this.Controls.Add(this.btnSave);
                  this.Controls.Add(this.groupBoxAutoApprove);
                  this.Controls.Add(this.groupBoxLabelTime);
                  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                  this.MaximizeBox = false;
                  this.MinimizeBox = false;
                  this.Name = "FrmSettings";
                  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                  this.Text = "Ayarlar";
                  this.Load += new System.EventHandler(this.FrmSettings_Load);
                  this.groupBoxLabelTime.ResumeLayout(false);
                  this.groupBoxLabelTime.PerformLayout();
                  this.groupBoxAutoApprove.ResumeLayout(false);
                  this.groupBoxAutoApprove.PerformLayout();
                  this.groupBoxWeightTolerance.ResumeLayout(false);
                  this.groupBoxWeightTolerance.PerformLayout();
                  this.groupBoxPassword.ResumeLayout(false);
                  this.groupBoxPassword.PerformLayout();
                  ((System.ComponentModel.ISupportInitialize)(this.numT2)).EndInit();
                  ((System.ComponentModel.ISupportInitialize)(this.numT1)).EndInit();
                  ((System.ComponentModel.ISupportInitialize)(this.numTimerInterval)).EndInit();
                  ((System.ComponentModel.ISupportInitialize)(this.numWeightTolerance)).EndInit();
                  this.ResumeLayout(false);

            }

            #endregion

            private System.Windows.Forms.GroupBox groupBoxLabelTime;
            private System.Windows.Forms.Label lblT2;
            private System.Windows.Forms.Label lblT1;
            private System.Windows.Forms.NumericUpDown numT2;
            private System.Windows.Forms.NumericUpDown numT1;
            private System.Windows.Forms.Button btnSave;
            private System.Windows.Forms.Button btnCancel;
            private System.Windows.Forms.Label lblT2Info;
            private System.Windows.Forms.Label lblT1Info;
            private System.Windows.Forms.GroupBox groupBoxAutoApprove;
            private System.Windows.Forms.NumericUpDown numTimerInterval;
            private System.Windows.Forms.Label lblTimerInterval;
            private System.Windows.Forms.Label lblTimerInfo;
            private System.Windows.Forms.CheckBox chkAutoApproveActive;
            private System.Windows.Forms.GroupBox groupBoxWeightTolerance;
            private System.Windows.Forms.NumericUpDown numWeightTolerance;
            private System.Windows.Forms.Label lblWeightTolerance;
            private System.Windows.Forms.Label lblWeightToleranceInfo;
            private System.Windows.Forms.GroupBox groupBoxPassword;
            private System.Windows.Forms.Button btnChangePassword;
            private System.Windows.Forms.TextBox txtNewPasswordConfirm;
            private System.Windows.Forms.Label lblNewPasswordConfirm;
            private System.Windows.Forms.TextBox txtNewPassword;
            private System.Windows.Forms.Label lblNewPassword;
      }
}
