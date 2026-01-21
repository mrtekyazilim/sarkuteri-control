namespace SarkuteriBizerba.Forms
{
  partial class FrmDb
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
      this.groupBoxSql = new System.Windows.Forms.GroupBox();
      this.lblServer = new System.Windows.Forms.Label();
      this.txtServer = new System.Windows.Forms.TextBox();
      this.lblDatabase = new System.Windows.Forms.Label();
      this.txtDatabase = new System.Windows.Forms.TextBox();
      this.chkNtAuth = new System.Windows.Forms.CheckBox();
      this.lblSqlUser = new System.Windows.Forms.Label();
      this.txtSqlUser = new System.Windows.Forms.TextBox();
      this.lblSqlPass = new System.Windows.Forms.Label();
      this.txtSqlPass = new System.Windows.Forms.TextBox();
      this.btnTestConnection = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.groupBoxSql.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBoxSql
      // 
      this.groupBoxSql.Controls.Add(this.btnTestConnection);
      this.groupBoxSql.Controls.Add(this.txtSqlPass);
      this.groupBoxSql.Controls.Add(this.lblSqlPass);
      this.groupBoxSql.Controls.Add(this.txtSqlUser);
      this.groupBoxSql.Controls.Add(this.lblSqlUser);
      this.groupBoxSql.Controls.Add(this.chkNtAuth);
      this.groupBoxSql.Controls.Add(this.txtDatabase);
      this.groupBoxSql.Controls.Add(this.lblDatabase);
      this.groupBoxSql.Controls.Add(this.txtServer);
      this.groupBoxSql.Controls.Add(this.lblServer);
      this.groupBoxSql.Location = new System.Drawing.Point(12, 12);
      this.groupBoxSql.Name = "groupBoxSql";
      this.groupBoxSql.Size = new System.Drawing.Size(400, 220);
      this.groupBoxSql.TabIndex = 0;
      this.groupBoxSql.TabStop = false;
      this.groupBoxSql.Text = "IBM Genius3 SQL Bağlantı Ayarları";
      // 
      // lblServer
      // 
      this.lblServer.AutoSize = true;
      this.lblServer.Location = new System.Drawing.Point(15, 30);
      this.lblServer.Name = "lblServer";
      this.lblServer.Size = new System.Drawing.Size(41, 13);
      this.lblServer.TabIndex = 0;
      this.lblServer.Text = "Server:";
      // 
      // txtServer
      // 
      this.txtServer.Location = new System.Drawing.Point(120, 27);
      this.txtServer.Name = "txtServer";
      this.txtServer.Size = new System.Drawing.Size(250, 20);
      this.txtServer.TabIndex = 1;
      // 
      // lblDatabase
      // 
      this.lblDatabase.AutoSize = true;
      this.lblDatabase.Location = new System.Drawing.Point(15, 60);
      this.lblDatabase.Name = "lblDatabase";
      this.lblDatabase.Size = new System.Drawing.Size(56, 13);
      this.lblDatabase.TabIndex = 2;
      this.lblDatabase.Text = "Database:";
      // 
      // txtDatabase
      // 
      this.txtDatabase.Location = new System.Drawing.Point(120, 57);
      this.txtDatabase.Name = "txtDatabase";
      this.txtDatabase.Size = new System.Drawing.Size(250, 20);
      this.txtDatabase.TabIndex = 3;
      // 
      // chkNtAuth
      // 
      this.chkNtAuth.AutoSize = true;
      this.chkNtAuth.Checked = true;
      this.chkNtAuth.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkNtAuth.Location = new System.Drawing.Point(120, 90);
      this.chkNtAuth.Name = "chkNtAuth";
      this.chkNtAuth.Size = new System.Drawing.Size(133, 17);
      this.chkNtAuth.TabIndex = 4;
      this.chkNtAuth.Text = "Windows Authentication";
      this.chkNtAuth.UseVisualStyleBackColor = true;
      this.chkNtAuth.CheckedChanged += new System.EventHandler(this.chkNtAuth_CheckedChanged);
      // 
      // lblSqlUser
      // 
      this.lblSqlUser.AutoSize = true;
      this.lblSqlUser.Enabled = false;
      this.lblSqlUser.Location = new System.Drawing.Point(15, 120);
      this.lblSqlUser.Name = "lblSqlUser";
      this.lblSqlUser.Size = new System.Drawing.Size(58, 13);
      this.lblSqlUser.TabIndex = 5;
      this.lblSqlUser.Text = "SQL User:";
      // 
      // txtSqlUser
      // 
      this.txtSqlUser.Enabled = false;
      this.txtSqlUser.Location = new System.Drawing.Point(120, 117);
      this.txtSqlUser.Name = "txtSqlUser";
      this.txtSqlUser.Size = new System.Drawing.Size(250, 20);
      this.txtSqlUser.TabIndex = 6;
      // 
      // lblSqlPass
      // 
      this.lblSqlPass.AutoSize = true;
      this.lblSqlPass.Enabled = false;
      this.lblSqlPass.Location = new System.Drawing.Point(15, 150);
      this.lblSqlPass.Name = "lblSqlPass";
      this.lblSqlPass.Size = new System.Drawing.Size(79, 13);
      this.lblSqlPass.TabIndex = 7;
      this.lblSqlPass.Text = "SQL Password:";
      // 
      // txtSqlPass
      // 
      this.txtSqlPass.Enabled = false;
      this.txtSqlPass.Location = new System.Drawing.Point(120, 147);
      this.txtSqlPass.Name = "txtSqlPass";
      this.txtSqlPass.PasswordChar = '*';
      this.txtSqlPass.Size = new System.Drawing.Size(250, 20);
      this.txtSqlPass.TabIndex = 8;
      // 
      // btnTestConnection
      // 
      this.btnTestConnection.Location = new System.Drawing.Point(120, 180);
      this.btnTestConnection.Name = "btnTestConnection";
      this.btnTestConnection.Size = new System.Drawing.Size(120, 30);
      this.btnTestConnection.TabIndex = 9;
      this.btnTestConnection.Text = "Bağlantıyı Test Et";
      this.btnTestConnection.UseVisualStyleBackColor = true;
      this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
      // 
      // btnSave
      // 
      this.btnSave.Location = new System.Drawing.Point(250, 250);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(80, 30);
      this.btnSave.TabIndex = 1;
      this.btnSave.Text = "Kaydet";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(340, 250);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(80, 30);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "İptal";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // FrmDb
      // 
      this.AcceptButton = this.btnSave;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(434, 302);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.groupBoxSql);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FrmDb";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "SQL Veritabanı Ayarları";
      this.Load += new System.EventHandler(this.FrmDb_Load);
      this.groupBoxSql.ResumeLayout(false);
      this.groupBoxSql.PerformLayout();
      this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.GroupBox groupBoxSql;
    private System.Windows.Forms.Label lblServer;
    private System.Windows.Forms.TextBox txtServer;
    private System.Windows.Forms.Label lblDatabase;
    private System.Windows.Forms.TextBox txtDatabase;
    private System.Windows.Forms.CheckBox chkNtAuth;
    private System.Windows.Forms.Label lblSqlUser;
    private System.Windows.Forms.TextBox txtSqlUser;
    private System.Windows.Forms.Label lblSqlPass;
    private System.Windows.Forms.TextBox txtSqlPass;
    private System.Windows.Forms.Button btnTestConnection;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;
  }
}