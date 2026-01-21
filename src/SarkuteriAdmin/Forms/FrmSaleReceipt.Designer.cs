namespace SarkuteriAdmin.Forms
{
  partial class FrmSaleReceipt
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
      this.panelTop = new System.Windows.Forms.Panel();
      this.lblPos = new System.Windows.Forms.Label();
      this.lblStore = new System.Windows.Forms.Label();
      this.lblTransDate = new System.Windows.Forms.Label();
      this.lblReceiptNumber = new System.Windows.Forms.Label();
      this.panelBottom = new System.Windows.Forms.Panel();
      this.btnExportExcel = new System.Windows.Forms.Button();
      this.btnPrint = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.lblNetAmount = new System.Windows.Forms.Label();
      this.lblVatAmount = new System.Windows.Forms.Label();
      this.lblTotalAmount = new System.Windows.Forms.Label();
      this.lblItemCount = new System.Windows.Forms.Label();
      this.gridControl1 = new DevExpress.XtraGrid.GridControl();
      this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
      this.panelTop.SuspendLayout();
      this.panelBottom.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
      this.SuspendLayout();
      // 
      // panelTop
      // 
      this.panelTop.BackColor = System.Drawing.Color.WhiteSmoke;
      this.panelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panelTop.Controls.Add(this.lblPos);
      this.panelTop.Controls.Add(this.lblStore);
      this.panelTop.Controls.Add(this.lblTransDate);
      this.panelTop.Controls.Add(this.lblReceiptNumber);
      this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelTop.Location = new System.Drawing.Point(0, 0);
      this.panelTop.Name = "panelTop";
      this.panelTop.Padding = new System.Windows.Forms.Padding(10);
      this.panelTop.Size = new System.Drawing.Size(1200, 100);
      this.panelTop.TabIndex = 0;
      // 
      // lblPos
      // 
      this.lblPos.AutoSize = true;
      this.lblPos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblPos.Location = new System.Drawing.Point(13, 70);
      this.lblPos.Name = "lblPos";
      this.lblPos.Size = new System.Drawing.Size(45, 17);
      this.lblPos.TabIndex = 3;
      this.lblPos.Text = "Kasa: ";
      // 
      // lblStore
      // 
      this.lblStore.AutoSize = true;
      this.lblStore.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblStore.Location = new System.Drawing.Point(13, 50);
      this.lblStore.Name = "lblStore";
      this.lblStore.Size = new System.Drawing.Size(62, 17);
      this.lblStore.TabIndex = 2;
      this.lblStore.Text = "Mağaza: ";
      // 
      // lblTransDate
      // 
      this.lblTransDate.AutoSize = true;
      this.lblTransDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblTransDate.Location = new System.Drawing.Point(13, 30);
      this.lblTransDate.Name = "lblTransDate";
      this.lblTransDate.Size = new System.Drawing.Size(44, 17);
      this.lblTransDate.TabIndex = 1;
      this.lblTransDate.Text = "Tarih: ";
      // 
      // lblReceiptNumber
      // 
      this.lblReceiptNumber.AutoSize = true;
      this.lblReceiptNumber.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblReceiptNumber.ForeColor = System.Drawing.Color.DarkBlue;
      this.lblReceiptNumber.Location = new System.Drawing.Point(12, 10);
      this.lblReceiptNumber.Name = "lblReceiptNumber";
      this.lblReceiptNumber.Size = new System.Drawing.Size(63, 20);
      this.lblReceiptNumber.TabIndex = 0;
      this.lblReceiptNumber.Text = "Fiş No: ";
      // 
      // panelBottom
      // 
      this.panelBottom.BackColor = System.Drawing.Color.WhiteSmoke;
      this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panelBottom.Controls.Add(this.btnExportExcel);
      this.panelBottom.Controls.Add(this.btnPrint);
      this.panelBottom.Controls.Add(this.btnClose);
      this.panelBottom.Controls.Add(this.lblNetAmount);
      this.panelBottom.Controls.Add(this.lblVatAmount);
      this.panelBottom.Controls.Add(this.lblTotalAmount);
      this.panelBottom.Controls.Add(this.lblItemCount);
      this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panelBottom.Location = new System.Drawing.Point(0, 550);
      this.panelBottom.Name = "panelBottom";
      this.panelBottom.Padding = new System.Windows.Forms.Padding(10);
      this.panelBottom.Size = new System.Drawing.Size(1200, 80);
      this.panelBottom.TabIndex = 1;
      // 
      // btnExportExcel
      // 
      this.btnExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(93)))));
      this.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnExportExcel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.btnExportExcel.ForeColor = System.Drawing.Color.White;
      this.btnExportExcel.Location = new System.Drawing.Point(956, 13);
      this.btnExportExcel.Name = "btnExportExcel";
      this.btnExportExcel.Size = new System.Drawing.Size(110, 52);
      this.btnExportExcel.TabIndex = 6;
      this.btnExportExcel.Text = "Excel Export";
      this.btnExportExcel.UseVisualStyleBackColor = false;
      this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
      // 
      // btnPrint
      // 
      this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
      this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.btnPrint.ForeColor = System.Drawing.Color.White;
      this.btnPrint.Location = new System.Drawing.Point(840, 13);
      this.btnPrint.Name = "btnPrint";
      this.btnPrint.Size = new System.Drawing.Size(110, 52);
      this.btnPrint.TabIndex = 5;
      this.btnPrint.Text = "Yazdır";
      this.btnPrint.UseVisualStyleBackColor = false;
      this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
      this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.btnClose.ForeColor = System.Drawing.Color.White;
      this.btnClose.Location = new System.Drawing.Point(1072, 13);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(110, 52);
      this.btnClose.TabIndex = 4;
      this.btnClose.Text = "Kapat";
      this.btnClose.UseVisualStyleBackColor = false;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // lblNetAmount
      // 
      this.lblNetAmount.AutoSize = true;
      this.lblNetAmount.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblNetAmount.ForeColor = System.Drawing.Color.DarkGreen;
      this.lblNetAmount.Location = new System.Drawing.Point(13, 45);
      this.lblNetAmount.Name = "lblNetAmount";
      this.lblNetAmount.Size = new System.Drawing.Size(128, 19);
      this.lblNetAmount.TabIndex = 3;
      this.lblNetAmount.Text = "Net Tutar: 0.00 TL";
      // 
      // lblVatAmount
      // 
      this.lblVatAmount.AutoSize = true;
      this.lblVatAmount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblVatAmount.Location = new System.Drawing.Point(300, 13);
      this.lblVatAmount.Name = "lblVatAmount";
      this.lblVatAmount.Size = new System.Drawing.Size(122, 17);
      this.lblVatAmount.TabIndex = 2;
      this.lblVatAmount.Text = "KDV Toplamı: 0.00 TL";
      // 
      // lblTotalAmount
      // 
      this.lblTotalAmount.AutoSize = true;
      this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblTotalAmount.ForeColor = System.Drawing.Color.DarkBlue;
      this.lblTotalAmount.Location = new System.Drawing.Point(13, 13);
      this.lblTotalAmount.Name = "lblTotalAmount";
      this.lblTotalAmount.Size = new System.Drawing.Size(154, 19);
      this.lblTotalAmount.TabIndex = 1;
      this.lblTotalAmount.Text = "Toplam Tutar: 0.00 TL";
      // 
      // lblItemCount
      // 
      this.lblItemCount.AutoSize = true;
      this.lblItemCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblItemCount.Location = new System.Drawing.Point(300, 45);
      this.lblItemCount.Name = "lblItemCount";
      this.lblItemCount.Size = new System.Drawing.Size(98, 17);
      this.lblItemCount.TabIndex = 0;
      this.lblItemCount.Text = "Kalem Sayısı: 0";
      // 
      // gridControl1
      // 
      this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gridControl1.Location = new System.Drawing.Point(0, 100);
      this.gridControl1.MainView = this.gridView1;
      this.gridControl1.Name = "gridControl1";
      this.gridControl1.Size = new System.Drawing.Size(1200, 450);
      this.gridControl1.TabIndex = 2;
      this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
      // 
      // gridView1
      // 
      this.gridView1.GridControl = this.gridControl1;
      this.gridView1.Name = "gridView1";
      this.gridView1.OptionsBehavior.Editable = false;
      this.gridView1.OptionsBehavior.ReadOnly = true;
      this.gridView1.OptionsView.ShowGroupPanel = false;
      // 
      // FrmSaleReceipt
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1200, 630);
      this.Controls.Add(this.gridControl1);
      this.Controls.Add(this.panelBottom);
      this.Controls.Add(this.panelTop);
      this.Name = "FrmSaleReceipt";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Satış Fişi";
      this.Load += new System.EventHandler(this.FrmSaleReceipt_Load);
      this.panelTop.ResumeLayout(false);
      this.panelTop.PerformLayout();
      this.panelBottom.ResumeLayout(false);
      this.panelBottom.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panelTop;
    private System.Windows.Forms.Label lblReceiptNumber;
    private System.Windows.Forms.Label lblTransDate;
    private System.Windows.Forms.Label lblStore;
    private System.Windows.Forms.Label lblPos;
    private System.Windows.Forms.Panel panelBottom;
    private System.Windows.Forms.Label lblItemCount;
    private System.Windows.Forms.Label lblTotalAmount;
    private System.Windows.Forms.Label lblVatAmount;
    private System.Windows.Forms.Label lblNetAmount;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnPrint;
    private System.Windows.Forms.Button btnExportExcel;
    private DevExpress.XtraGrid.GridControl gridControl1;
    private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
  }
}
