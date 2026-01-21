namespace SarkuteriAdmin.Forms
{
    partial class FrmMainMenu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainMenu));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dosyaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listeleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otomatikOnayıÇalıştırToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.çıkışToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ayarlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseAyarlarıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genelAyarlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yardımToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hakkındaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelTop = new System.Windows.Forms.Panel();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.btnList = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.cmbStore = new System.Windows.Forms.ComboBox();
            this.lblStore = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkAutoApproveActive = new System.Windows.Forms.CheckBox();
            this.btnAutoApprove = new System.Windows.Forms.Button();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedAt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdatedAt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStoreNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colScaleIP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSerialNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colScaleNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboGridStatus = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colScaleDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBarcode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBarcodeFull = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNetWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnitPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPLUNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFkTransactionSaleId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFkTransactionHeaderId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFkStockCardId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFkStoreId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFkPosId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStoreNumStore = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStoreName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPosNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPosName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTSQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTSUnitPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTSTotalPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransactionDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReceiptNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.timerAutoApprove = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.gösterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.çıkışToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.colTeraziName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.menuStrip1.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGridStatus)).BeginInit();
            this.contextMenuStripTray.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dosyaToolStripMenuItem,
            this.ayarlarToolStripMenuItem,
            this.yardımToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dosyaToolStripMenuItem
            // 
            this.dosyaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listeleToolStripMenuItem,
            this.otomatikOnayıÇalıştırToolStripMenuItem,
            this.toolStripSeparator1,
            this.çıkışToolStripMenuItem1});
            this.dosyaToolStripMenuItem.Name = "dosyaToolStripMenuItem";
            this.dosyaToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.dosyaToolStripMenuItem.Text = "Dosya";
            // 
            // listeleToolStripMenuItem
            // 
            this.listeleToolStripMenuItem.Name = "listeleToolStripMenuItem";
            this.listeleToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.listeleToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.listeleToolStripMenuItem.Text = "Listele";
            this.listeleToolStripMenuItem.Click += new System.EventHandler(this.listeleToolStripMenuItem_Click);
            // 
            // otomatikOnayıÇalıştırToolStripMenuItem
            // 
            this.otomatikOnayıÇalıştırToolStripMenuItem.Name = "otomatikOnayıÇalıştırToolStripMenuItem";
            this.otomatikOnayıÇalıştırToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.otomatikOnayıÇalıştırToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.otomatikOnayıÇalıştırToolStripMenuItem.Text = "Otomatik Onayı Çalıştır";
            this.otomatikOnayıÇalıştırToolStripMenuItem.Click += new System.EventHandler(this.otomatikOnayıÇalıştırToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(213, 6);
            // 
            // çıkışToolStripMenuItem1
            // 
            this.çıkışToolStripMenuItem1.Name = "çıkışToolStripMenuItem1";
            this.çıkışToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.çıkışToolStripMenuItem1.Size = new System.Drawing.Size(216, 22);
            this.çıkışToolStripMenuItem1.Text = "Çıkış";
            this.çıkışToolStripMenuItem1.Click += new System.EventHandler(this.çıkışToolStripMenuItem1_Click);
            // 
            // ayarlarToolStripMenuItem
            // 
            this.ayarlarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseAyarlarıToolStripMenuItem,
            this.genelAyarlarToolStripMenuItem});
            this.ayarlarToolStripMenuItem.Name = "ayarlarToolStripMenuItem";
            this.ayarlarToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.ayarlarToolStripMenuItem.Text = "Ayarlar";
            // 
            // databaseAyarlarıToolStripMenuItem
            // 
            this.databaseAyarlarıToolStripMenuItem.Name = "databaseAyarlarıToolStripMenuItem";
            this.databaseAyarlarıToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.databaseAyarlarıToolStripMenuItem.Text = "Database Ayarları";
            this.databaseAyarlarıToolStripMenuItem.Click += new System.EventHandler(this.btnDbSettings_Click);
            // 
            // genelAyarlarToolStripMenuItem
            // 
            this.genelAyarlarToolStripMenuItem.Name = "genelAyarlarToolStripMenuItem";
            this.genelAyarlarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.genelAyarlarToolStripMenuItem.Text = "Genel Ayarlar";
            this.genelAyarlarToolStripMenuItem.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // yardımToolStripMenuItem
            // 
            this.yardımToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hakkındaToolStripMenuItem});
            this.yardımToolStripMenuItem.Name = "yardımToolStripMenuItem";
            this.yardımToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.yardımToolStripMenuItem.Text = "Yardım";
            // 
            // hakkındaToolStripMenuItem
            // 
            this.hakkındaToolStripMenuItem.Name = "hakkındaToolStripMenuItem";
            this.hakkındaToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.hakkındaToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.hakkındaToolStripMenuItem.Text = "Hakkında";
            this.hakkındaToolStripMenuItem.Click += new System.EventHandler(this.hakkındaToolStripMenuItem_Click);
            // 
            // panelTop
            // 
            this.panelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTop.Controls.Add(this.cboStatus);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.txtBarcode);
            this.panelTop.Controls.Add(this.lblBarcode);
            this.panelTop.Controls.Add(this.btnList);
            this.panelTop.Controls.Add(this.dtpEndDate);
            this.panelTop.Controls.Add(this.lblEndDate);
            this.panelTop.Controls.Add(this.dtpStartDate);
            this.panelTop.Controls.Add(this.lblStartDate);
            this.panelTop.Controls.Add(this.cmbStore);
            this.panelTop.Controls.Add(this.lblStore);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 24);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1184, 80);
            this.panelTop.TabIndex = 0;
            // 
            // cboStatus
            // 
            this.cboStatus.Location = new System.Drawing.Point(304, 42);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name3", "Name3"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name4", "Name4", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.cboStatus.Properties.DisplayMember = "Name";
            this.cboStatus.Properties.NullText = "";
            this.cboStatus.Properties.ValueMember = "Id";
            this.cboStatus.Size = new System.Drawing.Size(197, 20);
            this.cboStatus.TabIndex = 13;
            this.cboStatus.EditValueChanged += new System.EventHandler(this.cboStatus_EditValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(239, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Durum";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(586, 12);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(129, 20);
            this.txtBarcode.TabIndex = 9;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Location = new System.Drawing.Point(536, 15);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(44, 13);
            this.lblBarcode.TabIndex = 8;
            this.lblBarcode.Text = "Barkod:";
            // 
            // btnList
            // 
            this.btnList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnList.Location = new System.Drawing.Point(925, 12);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(100, 55);
            this.btnList.TabIndex = 6;
            this.btnList.Text = "Listele (F5)";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(94, 38);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(120, 20);
            this.dtpEndDate.TabIndex = 5;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(24, 41);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(58, 13);
            this.lblEndDate.TabIndex = 4;
            this.lblEndDate.Text = "Bitiş Tarihi:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(94, 11);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(120, 20);
            this.dtpStartDate.TabIndex = 3;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(9, 14);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(85, 13);
            this.lblStartDate.TabIndex = 2;
            this.lblStartDate.Text = "Başlangıç Tarihi:";
            // 
            // cmbStore
            // 
            this.cmbStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStore.FormattingEnabled = true;
            this.cmbStore.Location = new System.Drawing.Point(304, 12);
            this.cmbStore.Name = "cmbStore";
            this.cmbStore.Size = new System.Drawing.Size(200, 21);
            this.cmbStore.TabIndex = 1;
            // 
            // lblStore
            // 
            this.lblStore.AutoSize = true;
            this.lblStore.Location = new System.Drawing.Point(239, 15);
            this.lblStore.Name = "lblStore";
            this.lblStore.Size = new System.Drawing.Size(48, 13);
            this.lblStore.TabIndex = 0;
            this.lblStore.Text = "Mağaza:";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.textBox1.Location = new System.Drawing.Point(208, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(976, 71);
            this.textBox1.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkAutoApproveActive);
            this.panel1.Controls.Add(this.btnAutoApprove);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 71);
            this.panel1.TabIndex = 16;
            // 
            // chkAutoApproveActive
            // 
            this.chkAutoApproveActive.Location = new System.Drawing.Point(12, 47);
            this.chkAutoApproveActive.Name = "chkAutoApproveActive";
            this.chkAutoApproveActive.Size = new System.Drawing.Size(154, 22);
            this.chkAutoApproveActive.TabIndex = 15;
            this.chkAutoApproveActive.Text = "Otomatik Onaylama Aktif";
            this.chkAutoApproveActive.UseVisualStyleBackColor = true;
            this.chkAutoApproveActive.CheckedChanged += new System.EventHandler(this.chkAutoApproveActive_CheckedChanged);
            // 
            // btnAutoApprove
            // 
            this.btnAutoApprove.Location = new System.Drawing.Point(7, 8);
            this.btnAutoApprove.Name = "btnAutoApprove";
            this.btnAutoApprove.Size = new System.Drawing.Size(174, 33);
            this.btnAutoApprove.TabIndex = 14;
            this.btnAutoApprove.Text = "⚡ Otomatik Onay Calistir (F6)";
            this.btnAutoApprove.UseVisualStyleBackColor = true;
            this.btnAutoApprove.Click += new System.EventHandler(this.btnAutoApprove_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 104);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cboGridStatus});
            this.gridControl1.Size = new System.Drawing.Size(1184, 403);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colCreatedAt,
            this.colUpdatedAt,
            this.colStoreNum,
            this.colScaleIP,
            this.colTeraziName,
            this.colSerialNumber,
            this.colScaleNumber,
            this.colStatus,
            this.colScaleDate,
            this.colBarcode,
            this.colBarcodeFull,
            this.colNetWeight,
            this.colUnitPrice,
            this.colTotalPrice,
            this.colProductCode,
            this.colPLUNumber,
            this.colFkTransactionSaleId,
            this.colFkTransactionHeaderId,
            this.colFkStockCardId,
            this.colFkStoreId,
            this.colFkPosId,
            this.colStoreNumStore,
            this.colStoreName,
            this.colPosNum,
            this.colPosName,
            this.colTSQuantity,
            this.colTSUnitPrice,
            this.colTSTotalPrice,
            this.colTransactionDate,
            this.colReceiptNumber,
            this.colStockCode,
            this.colStockDescription});
            this.gridView1.CustomizationFormBounds = new System.Drawing.Rectangle(1486, 466, 264, 272);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsLayout.LayoutVersion = "1.2";
            this.gridView1.OptionsLayout.StoreAllOptions = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupedColumns = true;
            this.gridView1.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView1_PopupMenuShowing);
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Width = 60;
            // 
            // colCreatedAt
            // 
            this.colCreatedAt.Caption = "Oluşturma Tarihi";
            this.colCreatedAt.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.colCreatedAt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreatedAt.FieldName = "CreatedAt";
            this.colCreatedAt.Name = "colCreatedAt";
            this.colCreatedAt.OptionsColumn.AllowEdit = false;
            this.colCreatedAt.Width = 130;
            // 
            // colUpdatedAt
            // 
            this.colUpdatedAt.Caption = "Güncelleme Tarihi";
            this.colUpdatedAt.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.colUpdatedAt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colUpdatedAt.FieldName = "UpdatedAt";
            this.colUpdatedAt.Name = "colUpdatedAt";
            this.colUpdatedAt.OptionsColumn.AllowEdit = false;
            this.colUpdatedAt.Width = 130;
            // 
            // colStoreNum
            // 
            this.colStoreNum.Caption = "Mağaza No";
            this.colStoreNum.FieldName = "StoreNum";
            this.colStoreNum.Name = "colStoreNum";
            this.colStoreNum.OptionsColumn.AllowEdit = false;
            this.colStoreNum.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "StoreNum", "{0}")});
            this.colStoreNum.Visible = true;
            this.colStoreNum.VisibleIndex = 0;
            this.colStoreNum.Width = 80;
            // 
            // colScaleIP
            // 
            this.colScaleIP.Caption = "Terazi IP";
            this.colScaleIP.FieldName = "ScaleIP";
            this.colScaleIP.Name = "colScaleIP";
            this.colScaleIP.OptionsColumn.AllowEdit = false;
            this.colScaleIP.Visible = true;
            this.colScaleIP.VisibleIndex = 1;
            this.colScaleIP.Width = 100;
            // 
            // colSerialNumber
            // 
            this.colSerialNumber.Caption = "Seri No";
            this.colSerialNumber.FieldName = "SerialNumber";
            this.colSerialNumber.Name = "colSerialNumber";
            this.colSerialNumber.OptionsColumn.AllowEdit = false;
            this.colSerialNumber.Visible = true;
            this.colSerialNumber.VisibleIndex = 3;
            this.colSerialNumber.Width = 100;
            // 
            // colScaleNumber
            // 
            this.colScaleNumber.Caption = "Terazi No";
            this.colScaleNumber.FieldName = "ScaleNumber";
            this.colScaleNumber.Name = "colScaleNumber";
            this.colScaleNumber.OptionsColumn.AllowEdit = false;
            this.colScaleNumber.Visible = true;
            this.colScaleNumber.VisibleIndex = 5;
            this.colScaleNumber.Width = 80;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "Durum";
            this.colStatus.ColumnEdit = this.cboGridStatus;
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 4;
            this.colStatus.Width = 128;
            // 
            // cboGridStatus
            // 
            this.cboGridStatus.AutoHeight = false;
            this.cboGridStatus.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.cboGridStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboGridStatus.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name1"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Name2", 20, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.cboGridStatus.DisplayMember = "Name";
            this.cboGridStatus.Name = "cboGridStatus";
            this.cboGridStatus.NullText = "";
            this.cboGridStatus.ShowFooter = false;
            this.cboGridStatus.ShowHeader = false;
            this.cboGridStatus.ValueMember = "Id";
            // 
            // colScaleDate
            // 
            this.colScaleDate.Caption = "Terazi Tarihi";
            this.colScaleDate.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.colScaleDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colScaleDate.FieldName = "ScaleDate";
            this.colScaleDate.Name = "colScaleDate";
            this.colScaleDate.OptionsColumn.AllowEdit = false;
            this.colScaleDate.Visible = true;
            this.colScaleDate.VisibleIndex = 6;
            this.colScaleDate.Width = 130;
            // 
            // colBarcode
            // 
            this.colBarcode.Caption = "Barkod";
            this.colBarcode.FieldName = "Barcode";
            this.colBarcode.Name = "colBarcode";
            this.colBarcode.Visible = true;
            this.colBarcode.VisibleIndex = 7;
            this.colBarcode.Width = 120;
            // 
            // colBarcodeFull
            // 
            this.colBarcodeFull.Caption = "Tam Barkod";
            this.colBarcodeFull.FieldName = "BarcodeFull";
            this.colBarcodeFull.Name = "colBarcodeFull";
            this.colBarcodeFull.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "BarcodeFull", "{0}")});
            this.colBarcodeFull.Visible = true;
            this.colBarcodeFull.VisibleIndex = 8;
            this.colBarcodeFull.Width = 150;
            // 
            // colNetWeight
            // 
            this.colNetWeight.Caption = "Net Ağırlık";
            this.colNetWeight.DisplayFormat.FormatString = "n3";
            this.colNetWeight.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNetWeight.FieldName = "NetWeight";
            this.colNetWeight.Name = "colNetWeight";
            this.colNetWeight.OptionsColumn.AllowEdit = false;
            this.colNetWeight.Visible = true;
            this.colNetWeight.VisibleIndex = 9;
            this.colNetWeight.Width = 90;
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.Caption = "Birim Fiyat";
            this.colUnitPrice.DisplayFormat.FormatString = "n2";
            this.colUnitPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colUnitPrice.FieldName = "UnitPrice";
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.OptionsColumn.AllowEdit = false;
            this.colUnitPrice.Visible = true;
            this.colUnitPrice.VisibleIndex = 10;
            this.colUnitPrice.Width = 90;
            // 
            // colTotalPrice
            // 
            this.colTotalPrice.Caption = "Toplam Fiyat";
            this.colTotalPrice.DisplayFormat.FormatString = "n2";
            this.colTotalPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalPrice.FieldName = "TotalPrice";
            this.colTotalPrice.Name = "colTotalPrice";
            this.colTotalPrice.OptionsColumn.AllowEdit = false;
            this.colTotalPrice.Visible = true;
            this.colTotalPrice.VisibleIndex = 11;
            this.colTotalPrice.Width = 90;
            // 
            // colProductCode
            // 
            this.colProductCode.Caption = "Ürün Kodu";
            this.colProductCode.FieldName = "ProductCode";
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.OptionsColumn.AllowEdit = false;
            this.colProductCode.Visible = true;
            this.colProductCode.VisibleIndex = 12;
            this.colProductCode.Width = 100;
            // 
            // colPLUNumber
            // 
            this.colPLUNumber.Caption = "PLU No";
            this.colPLUNumber.FieldName = "PLUNumber";
            this.colPLUNumber.Name = "colPLUNumber";
            this.colPLUNumber.Visible = true;
            this.colPLUNumber.VisibleIndex = 13;
            this.colPLUNumber.Width = 80;
            // 
            // colFkTransactionSaleId
            // 
            this.colFkTransactionSaleId.Caption = "Satış ID";
            this.colFkTransactionSaleId.FieldName = "Fk_TransactionSaleId";
            this.colFkTransactionSaleId.Name = "colFkTransactionSaleId";
            this.colFkTransactionSaleId.OptionsColumn.AllowEdit = false;
            this.colFkTransactionSaleId.Width = 80;
            // 
            // colFkTransactionHeaderId
            // 
            this.colFkTransactionHeaderId.Caption = "Header ID";
            this.colFkTransactionHeaderId.FieldName = "Fk_TransactionHeaderId";
            this.colFkTransactionHeaderId.Name = "colFkTransactionHeaderId";
            this.colFkTransactionHeaderId.OptionsColumn.AllowEdit = false;
            this.colFkTransactionHeaderId.Width = 80;
            // 
            // colFkStockCardId
            // 
            this.colFkStockCardId.Caption = "Stok Kart ID";
            this.colFkStockCardId.FieldName = "Fk_StockCardId";
            this.colFkStockCardId.Name = "colFkStockCardId";
            this.colFkStockCardId.Width = 80;
            // 
            // colFkStoreId
            // 
            this.colFkStoreId.Caption = "Mağaza ID";
            this.colFkStoreId.FieldName = "Fk_StoreId";
            this.colFkStoreId.Name = "colFkStoreId";
            this.colFkStoreId.OptionsColumn.AllowEdit = false;
            this.colFkStoreId.Width = 80;
            // 
            // colFkPosId
            // 
            this.colFkPosId.Caption = "POS ID";
            this.colFkPosId.FieldName = "Fk_PosId";
            this.colFkPosId.Name = "colFkPosId";
            this.colFkPosId.OptionsColumn.AllowEdit = false;
            this.colFkPosId.Width = 80;
            // 
            // colStoreNumStore
            // 
            this.colStoreNumStore.Caption = "Mağaza No (STORE)";
            this.colStoreNumStore.FieldName = "StoreNum_Store";
            this.colStoreNumStore.Name = "colStoreNumStore";
            this.colStoreNumStore.OptionsColumn.AllowEdit = false;
            this.colStoreNumStore.Visible = true;
            this.colStoreNumStore.VisibleIndex = 14;
            this.colStoreNumStore.Width = 100;
            // 
            // colStoreName
            // 
            this.colStoreName.Caption = "Mağaza Adı";
            this.colStoreName.FieldName = "StoreName";
            this.colStoreName.Name = "colStoreName";
            this.colStoreName.OptionsColumn.AllowEdit = false;
            this.colStoreName.Visible = true;
            this.colStoreName.VisibleIndex = 15;
            this.colStoreName.Width = 150;
            // 
            // colPosNum
            // 
            this.colPosNum.Caption = "POS No";
            this.colPosNum.FieldName = "PosNum";
            this.colPosNum.Name = "colPosNum";
            this.colPosNum.OptionsColumn.AllowEdit = false;
            this.colPosNum.Visible = true;
            this.colPosNum.VisibleIndex = 16;
            this.colPosNum.Width = 80;
            // 
            // colPosName
            // 
            this.colPosName.Caption = "POS Adı";
            this.colPosName.FieldName = "PosName";
            this.colPosName.Name = "colPosName";
            this.colPosName.OptionsColumn.AllowEdit = false;
            this.colPosName.Visible = true;
            this.colPosName.VisibleIndex = 17;
            this.colPosName.Width = 150;
            // 
            // colTSQuantity
            // 
            this.colTSQuantity.Caption = "Miktar";
            this.colTSQuantity.DisplayFormat.FormatString = "n3";
            this.colTSQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTSQuantity.FieldName = "TSQuantity";
            this.colTSQuantity.Name = "colTSQuantity";
            this.colTSQuantity.OptionsColumn.AllowEdit = false;
            this.colTSQuantity.Visible = true;
            this.colTSQuantity.VisibleIndex = 18;
            this.colTSQuantity.Width = 80;
            // 
            // colTSUnitPrice
            // 
            this.colTSUnitPrice.Caption = "Satış Fiyatı";
            this.colTSUnitPrice.DisplayFormat.FormatString = "n2";
            this.colTSUnitPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTSUnitPrice.FieldName = "TSUnitPrice";
            this.colTSUnitPrice.Name = "colTSUnitPrice";
            this.colTSUnitPrice.OptionsColumn.AllowEdit = false;
            this.colTSUnitPrice.Visible = true;
            this.colTSUnitPrice.VisibleIndex = 19;
            this.colTSUnitPrice.Width = 90;
            // 
            // colTSTotalPrice
            // 
            this.colTSTotalPrice.Caption = "Satış Tutarı";
            this.colTSTotalPrice.DisplayFormat.FormatString = "n2";
            this.colTSTotalPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTSTotalPrice.FieldName = "TSTotalPrice";
            this.colTSTotalPrice.Name = "colTSTotalPrice";
            this.colTSTotalPrice.OptionsColumn.AllowEdit = false;
            this.colTSTotalPrice.Visible = true;
            this.colTSTotalPrice.VisibleIndex = 20;
            this.colTSTotalPrice.Width = 90;
            // 
            // colTransactionDate
            // 
            this.colTransactionDate.Caption = "İşlem Tarihi";
            this.colTransactionDate.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.colTransactionDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTransactionDate.FieldName = "TransactionDate";
            this.colTransactionDate.Name = "colTransactionDate";
            this.colTransactionDate.OptionsColumn.AllowEdit = false;
            this.colTransactionDate.Visible = true;
            this.colTransactionDate.VisibleIndex = 21;
            this.colTransactionDate.Width = 130;
            // 
            // colReceiptNumber
            // 
            this.colReceiptNumber.Caption = "Fiş No";
            this.colReceiptNumber.FieldName = "ReceiptNumber";
            this.colReceiptNumber.Name = "colReceiptNumber";
            this.colReceiptNumber.Visible = true;
            this.colReceiptNumber.VisibleIndex = 22;
            this.colReceiptNumber.Width = 120;
            // 
            // colStockCode
            // 
            this.colStockCode.Caption = "Stok Kodu";
            this.colStockCode.FieldName = "StockCode";
            this.colStockCode.Name = "colStockCode";
            this.colStockCode.Visible = true;
            this.colStockCode.VisibleIndex = 23;
            this.colStockCode.Width = 100;
            // 
            // colStockDescription
            // 
            this.colStockDescription.Caption = "Stok Açıklaması";
            this.colStockDescription.FieldName = "StockDescription";
            this.colStockDescription.Name = "colStockDescription";
            this.colStockDescription.OptionsColumn.AllowEdit = false;
            this.colStockDescription.Visible = true;
            this.colStockDescription.VisibleIndex = 24;
            this.colStockDescription.Width = 200;
            // 
            // timerAutoApprove
            // 
            this.timerAutoApprove.Interval = 300000;
            this.timerAutoApprove.Tick += new System.EventHandler(this.timerAutoApprove_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStripTray;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "SarkuteriAdmin";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStripTray
            // 
            this.contextMenuStripTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gösterToolStripMenuItem,
            this.çıkışToolStripMenuItem});
            this.contextMenuStripTray.Name = "contextMenuStripTray";
            this.contextMenuStripTray.Size = new System.Drawing.Size(109, 48);
            // 
            // gösterToolStripMenuItem
            // 
            this.gösterToolStripMenuItem.Name = "gösterToolStripMenuItem";
            this.gösterToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.gösterToolStripMenuItem.Text = "Göster";
            this.gösterToolStripMenuItem.Click += new System.EventHandler(this.gösterToolStripMenuItem_Click);
            // 
            // çıkışToolStripMenuItem
            // 
            this.çıkışToolStripMenuItem.Name = "çıkışToolStripMenuItem";
            this.çıkışToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.çıkışToolStripMenuItem.Text = "Çıkış";
            this.çıkışToolStripMenuItem.Click += new System.EventHandler(this.çıkışToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 578);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 507);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1184, 71);
            this.panel2.TabIndex = 5;
            // 
            // colTeraziName
            // 
            this.colTeraziName.Caption = "Terazi Isim";
            this.colTeraziName.FieldName = "ScaleName";
            this.colTeraziName.Name = "colTeraziName";
            this.colTeraziName.Visible = true;
            this.colTeraziName.VisibleIndex = 2;
            this.colTeraziName.Width = 102;
            // 
            // FrmMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 600);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dinamik Şarküteri Admin - DINAMIK_BIZERBA Kayıtları";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMainMenu_FormClosing);
            this.Load += new System.EventHandler(this.FrmMainMenu_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMainMenu_KeyDown);
            this.Resize += new System.EventHandler(this.FrmMainMenu_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGridStatus)).EndInit();
            this.contextMenuStripTray.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblStore;
        private System.Windows.Forms.ComboBox cmbStore;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.Button btnDbSettings;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblBarcode;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedAt;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdatedAt;
        private DevExpress.XtraGrid.Columns.GridColumn colStoreNum;
        private DevExpress.XtraGrid.Columns.GridColumn colScaleIP;
        private DevExpress.XtraGrid.Columns.GridColumn colSerialNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colScaleNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colScaleDate;
        private DevExpress.XtraGrid.Columns.GridColumn colBarcode;
        private DevExpress.XtraGrid.Columns.GridColumn colBarcodeFull;
        private DevExpress.XtraGrid.Columns.GridColumn colNetWeight;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCode;
        private DevExpress.XtraGrid.Columns.GridColumn colPLUNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colFkTransactionSaleId;
        private DevExpress.XtraGrid.Columns.GridColumn colFkTransactionHeaderId;
        private DevExpress.XtraGrid.Columns.GridColumn colFkStockCardId;
        private DevExpress.XtraGrid.Columns.GridColumn colFkStoreId;
        private DevExpress.XtraGrid.Columns.GridColumn colFkPosId;
        private DevExpress.XtraGrid.Columns.GridColumn colStoreNumStore;
        private DevExpress.XtraGrid.Columns.GridColumn colStoreName;
        private DevExpress.XtraGrid.Columns.GridColumn colPosNum;
        private DevExpress.XtraGrid.Columns.GridColumn colPosName;
        private DevExpress.XtraGrid.Columns.GridColumn colTSQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colTSUnitPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colTSTotalPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colTransactionDate;
        private DevExpress.XtraGrid.Columns.GridColumn colReceiptNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colStockCode;
        private DevExpress.XtraGrid.Columns.GridColumn colStockDescription;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboGridStatus;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private System.Windows.Forms.Button btnAutoApprove;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timerAutoApprove;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkAutoApproveActive;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTray;
        private System.Windows.Forms.ToolStripMenuItem gösterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem çıkışToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dosyaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listeleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otomatikOnayıÇalıştırToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem çıkışToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ayarlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseAyarlarıToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genelAyarlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yardımToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hakkındaToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.Columns.GridColumn colTeraziName;
    }
}

