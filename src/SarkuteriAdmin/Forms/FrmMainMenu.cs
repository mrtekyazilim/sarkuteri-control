using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SarkuteriAdmin.Classes;
using Microsoft.Win32;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using Timer = System.Windows.Forms.Timer;

namespace SarkuteriAdmin.Forms
{
    public partial class FrmMainMenu : Form
    {
        private const string REGISTRY_KEY = @"Software\DinamikYazilim\SarkuteriAdmin\GridLayout";
        private const string LAYOUT_VALUE = "FrmMainMenu_GridView1";
        private Timer balloonTimer;

        public FrmMainMenu()
        {
            InitializeComponent();
        }

        private void FrmMainMenu_Load(object sender, EventArgs e)
        {
            try
            {
                // Tarihleri ayarla - bugünden 7 gün önce ile bugün arası
                dtpStartDate.Value = DateTime.Today.AddDays(-7);
                dtpEndDate.Value = DateTime.Today;

                // GridView layout'unu registry'den yükle
                LoadGridLayout();

                // Status listesini yükle
                LoadStatusList();

                // Mağaza listesini yükle
                LoadStores();

                gridView1.OptionsBehavior.Editable = true;
                gridView1.OptionsBehavior.ReadOnly = true;

                // CheckBox durumunu ayarla
                chkAutoApproveActive.Checked = Helper.AutoApproveActive;

                // Timer'ı başlat (sadece aktif ise)
                timerAutoApprove.Interval = Helper.AutoApproveInterval * 60 * 1000; // Dakikayı milisaniyeye çevir

                if (Helper.AutoApproveActive)
                {
                    timerAutoApprove.Start();
                    WriteToTextBox($"Timer başlatıldı. Interval: {Helper.AutoApproveInterval} dakika");
                    Helper.WriteLog($"FrmMainMenu yüklendi. Timer interval: {Helper.AutoApproveInterval} dakika");

                    // İlk çalışma zamanını göster
                    DateTime firstRun = DateTime.Now.AddMinutes(Helper.AutoApproveInterval);
                    WriteToTextBox($"İlk otomatik onaylama: {firstRun:yyyy-MM-dd HH:mm:ss} ({Helper.AutoApproveInterval} dakika sonra)");
                    WriteToTextBox("".PadRight(80, '-')); // Ayırıcı çizgi
                }
                else
                {
                    WriteToTextBox("Otomatik onaylama devre dışı. Aktif hale getirmek için ayarlardan etkinleştirin.");
                    WriteToTextBox("".PadRight(80, '-')); // Ayırıcı çizgi
                    Helper.WriteLog("FrmMainMenu yüklendi. Otomatik onaylama devre dışı.");
                }

                // Form açılınca otomatik listeleme yap
                LoadData();

                // StatusBar'ı ilk kez güncelle
                UpdateStatusBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Form yüklenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Helper.WriteErrorLog("FrmMainMenu_Load hatası", ex);
            }
        }

        private void LoadStores()
        {
            try
            {
                if (Helper.conn == null || Helper.conn.State != ConnectionState.Open)
                {
                    MessageBox.Show("Veritabanı bağlantısı açık değil.", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string query = @"SELECT ID as Fk_Store
                                       ,NUM as StoreNum
                                       ,DESCRIPTION as StoreName, CAST(NUM as VARCHAR(10)) + ' - ' + DESCRIPTION as StoreNumName
                                FROM [GENIUS3].[STORE]
                                ORDER BY NUM";

                using (SqlCommand cmd = new SqlCommand(query, Helper.conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // "Tümü" seçeneği ekle
                        DataRow allRow = dt.NewRow();
                        allRow["Fk_Store"] = 0;
                        allRow["StoreNum"] = 0;
                        allRow["StoreName"] = "-- Tüm Mağazalar --";
                        dt.Rows.InsertAt(allRow, 0);

                        cmbStore.DisplayMember = "StoreNumName";
                        cmbStore.ValueMember = "StoreNum";
                        cmbStore.DataSource = dt;
                        cmbStore.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Mağaza listesi yüklenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatusList()
        {
            try
            {
                DataTable dtStatus = new DataTable();
                dtStatus.Columns.Add("Id", typeof(int));
                dtStatus.Columns.Add("Name", typeof(string));

                // Status değerini ekle
                dtStatus.Rows.Add(0, "Bekliyor");
                dtStatus.Rows.Add(1, "Otomatik Onaylandı");
                dtStatus.Rows.Add(2, "Manuel Onaylandı");
                dtStatus.Rows.Add(3, "Karantina");
                dtStatus.Rows.Add(99, "Arşiv");

                // cboGridStatus (Grid içindeki Status kolonu için)
                cboGridStatus.DataSource = dtStatus.Copy();
                cboGridStatus.DisplayMember = "Name";
                cboGridStatus.ValueMember = "Id";

                // cboStatus (Filtre için panelTop'daki combobox)
                DataTable dtStatusWithAll = dtStatus.Copy();
                DataRow allRow = dtStatusWithAll.NewRow();
                allRow["Id"] = -1;
                allRow["Name"] = "-- Tümü --";
                dtStatusWithAll.Rows.InsertAt(allRow, 0);

                cboStatus.Properties.DataSource = dtStatusWithAll;
                cboStatus.Properties.DisplayMember = "Name";
                cboStatus.Properties.ValueMember = "Id";
                cboStatus.Properties.Columns.Clear();
                cboStatus.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Durum"));
                cboStatus.EditValue = 0; // Varsayılan olarak "Bekliyor" (0) seçili
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Status listesi yüklenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri yüklenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                // Cursor'u bekleme durumuna al
                this.Cursor = Cursors.WaitCursor;
                btnList.Enabled = false;
                btnList.Text = "Yükleniyor ...";

                // Filtre parametrelerini hazırla
                int selectedStoreNum = Convert.ToInt32(cmbStore.SelectedValue);
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date;
                string barcode = string.IsNullOrWhiteSpace(txtBarcode.Text) ? null : txtBarcode.Text.Trim();

                // Status filtresi (-1 ise tümü, null gönder)
                int? statusFilter = null;
                if (cboStatus.EditValue != null)
                {
                    int selectedStatus = Convert.ToInt32(cboStatus.EditValue);
                    if (selectedStatus >= 0)
                    {
                        statusFilter = selectedStatus;
                    }
                }

                // DinamikBizerba.GetRecords metodunu kullan
                DataTable dt = DinamikBizerba.GetRecords(selectedStoreNum, startDate, endDate, statusFilter, barcode);

                // DevExpress GridControl'e veriyi ata
                gridControl1.DataSource = dt;

                // Kolonları en iyi genişliğe ayarla
                gridView1.BestFitColumns();

                // StatusBar'ı güncelle
                UpdateStatusBar();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri yüklenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnList.Enabled = true;
                btnList.Text = "Listele";
            }
        }

        private void UpdateStatusBar()
        {
            try
            {
                // TODO: Designer'da statusStrip1 ve 3 adet ToolStripStatusLabel eklenince bu kodları aktif et!
                /*
                // Panel 1: Bağlantı Durumu
                if (Helper.conn != null && Helper.conn.State == ConnectionState.Open)
                {
                    lblConnectionStatus.Text = "🟢 Bağlı";
                    lblConnectionStatus.ForeColor = Color.Green;
                }
                else
                {
                    lblConnectionStatus.Text = "🔴 Bağlı Değil";
                    lblConnectionStatus.ForeColor = Color.Red;
                }

                // Panel 2: Kayıt Sayısı + Son Güncelleme
                int recordCount = gridView1.RowCount;
                lblRecordStatus.Text = $"📊 {recordCount:N0} kayıt  •  🕒 {DateTime.Now:HH:mm:ss}";
                lblRecordStatus.ForeColor = Color.Black;

                // Panel 3: Motor Durumu + Sonraki Çalışma
                if (Helper.AutoApproveActive && timerAutoApprove.Enabled)
                {
                    DateTime nextRun = DateTime.Now.AddMinutes(Helper.AutoApproveInterval);
                    lblMotorStatus.Text = $"⚙️ Motor: Aktif  •  ⏰ Sonraki: {nextRun:HH:mm}";
                    lblMotorStatus.ForeColor = Color.Green;
                }
                else
                {
                    lblMotorStatus.Text = "⏸️ Motor: Durduruldu";
                    lblMotorStatus.ForeColor = Color.Gray;
                }
                */
            }
            catch (Exception ex)
            {
                Helper.WriteErrorLog("UpdateStatusBar hatası", ex);
            }
        }

        private void btnDbSettings_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frmDb = new FrmDb())
                {
                    if (frmDb.ShowDialog() == DialogResult.OK)
                    {
                        // Bağlantı ayarları değişti, Config.ini'den yeniden yükle ve bağlan
                        try
                        {
                            // Mevcut bağlantıyı kapat
                            Helper.CloseConnection();

                            // Yeni ayarlarla bağlantıyı yeniden başlat
                            Helper.ReloadConnection();

                            // Bağlantıyı aç
                            if (!Helper.OpenConnection())
                            {
                                MessageBox.Show("Yeni ayarlarla bağlantı kurulamadı.", "Uyarı",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Bağlantı başarılı, mağaza listesini yeniden yükle
                            LoadStores();

                            MessageBox.Show("Bağlantı başarıyla güncellendi.", "Başarılı",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception connEx)
                        {
                            MessageBox.Show($"Bağlantı güncellenirken hata: {connEx.Message}", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database ayarları açılırken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter veya Return tuşuna basıldığında listele
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.Handled = true; // Beep sesini engelle
                e.SuppressKeyPress = true;
                btnList_Click(sender, e); // Listele butonunu tetikle
            }
        }

        private void FrmMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Form kapatılmaya çalışıldığında tray'e gönder, çıkış yapma
            e.Cancel = true;
            HideToTray();
        }

        private void ShowForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.BringToFront();
            this.Activate();
        }

        private void HideToTray()
        {
            this.Hide();
            this.ShowInTaskbar = false;

            // Kullanıcıya bilgi mesajı göster
            notifyIcon1.ShowBalloonTip(1500, "SarkuteriAdmin", "Uygulama simge durumunda çalışıyor", ToolTipIcon.Info);

            // 1.5 saniye sonra balloon tip'i kapat
            if (balloonTimer != null)
            {
                balloonTimer.Stop();
                balloonTimer.Dispose();
            }

            balloonTimer = new Timer();
            balloonTimer.Interval = 1500; // 1.5 saniye
            balloonTimer.Tick += (s, e) =>
            {
                notifyIcon1.Visible = false;
                notifyIcon1.Visible = true;
                balloonTimer.Stop();
                balloonTimer.Dispose();
                balloonTimer = null;
            };
            balloonTimer.Start();
        }

        private void SaveGridLayout()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY))
                {
                    if (key != null)
                    {
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                        {
                            gridView1.SaveLayoutToStream(ms);
                            byte[] data = ms.ToArray();
                            key.SetValue(LAYOUT_VALUE, data, RegistryValueKind.Binary);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Grid layout kaydedilemedi: {ex.Message}");
            }
        }

        private void LoadGridLayout()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY))
                {
                    if (key != null)
                    {
                        byte[] data = key.GetValue(LAYOUT_VALUE) as byte[];
                        if (data != null && data.Length > 0)
                        {
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(data))
                            {
                                gridView1.RestoreLayoutFromStream(ms);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Layout yüklenemezse sessizce devam et
                System.Diagnostics.Debug.WriteLine($"Grid layout yüklenemedi: {ex.Message}");
            }
        }

        private void ResetGridLayout()
        {
            try
            {
                // Registry'den layout'u sil
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY, true))
                {
                    if (key != null)
                    {
                        key.DeleteValue(LAYOUT_VALUE, false);
                    }
                }

                // GridView'i varsayılan durumuna getir
                gridView1.ClearColumnErrors();
                gridView1.ActiveFilter.Clear();

                // Tüm kolonları görünür yap ve varsayılan genişliklere getir
                foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridView1.Columns)
                {
                    col.VisibleIndex = col.AbsoluteIndex;

                    // Foreign key kolonları hariç tümünü görünür yap
                    if (col.FieldName.StartsWith("Fk_"))
                    {
                        col.Visible = false;
                    }
                    else
                    {
                        col.Visible = true;
                    }
                }

                gridView1.BestFitColumns();

                MessageBox.Show("Grid görünümü sıfırlandı.", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Grid görünümü sıfırlanırken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnaylaKayit()
        {
            try
            {
                if (gridView1.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Lütfen onaylamak için bir kayıt seçin.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int selectedRowHandle = gridView1.GetSelectedRows()[0];
                DataRow row = gridView1.GetDataRow(selectedRowHandle);

                if (row == null)
                {
                    MessageBox.Show("Seçili kayıt bulunamadı.", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int id = Convert.ToInt32(row["ID"]);

                DialogResult result = MessageBox.Show(
                    $"ID: {id} numaralı kayıt onaylanacak. Emin misiniz?",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Status'u 1 (Onaylı) yap
                    string updateQuery = "UPDATE [DINAMIK_BIZERBA] SET [Status] = 2, [UpdatedAt] = GETDATE() WHERE [ID] = @ID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, Helper.conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        int affected = cmd.ExecuteNonQuery();

                        if (affected > 0)
                        {
                            MessageBox.Show("Kayıt onaylandı.", "Başarılı",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Listeyi yenile
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt onaylanamadı.", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt onaylanırken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KarantinaKayit()
        {
            try
            {
                if (gridView1.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Lütfen karantina için bir kayıt seçin.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int selectedRowHandle = gridView1.GetSelectedRows()[0];
                DataRow row = gridView1.GetDataRow(selectedRowHandle);

                if (row == null)
                {
                    MessageBox.Show("Seçili kayıt bulunamadı.", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int id = Convert.ToInt32(row["ID"]);

                DialogResult result = MessageBox.Show(
                    $"ID: {id} numaralı kayıt karantinaya alınacak. Emin misiniz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string deleteQuery = "UPDATE[DINAMIK_BIZERBA] SET Status=3, [UpdatedAt] = GETDATE() WHERE [ID] = @ID";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, Helper.conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        int affected = cmd.ExecuteNonQuery();

                        if (affected > 0)
                        {
                            MessageBox.Show("Kayıt karantinaya alındı.", "Başarılı",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Listeyi yenile
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt karantinaya alınamadı.", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt karantinaya alınırken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ArsivKayit()
        {
            try
            {
                if (gridView1.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Lütfen arşive göndermek için bir kayıt seçin.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int selectedRowHandle = gridView1.GetSelectedRows()[0];
                DataRow row = gridView1.GetDataRow(selectedRowHandle);

                if (row == null)
                {
                    MessageBox.Show("Seçili kayıt bulunamadı.", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int id = Convert.ToInt32(row["ID"]);

                DialogResult result = MessageBox.Show(
                    $"ID: {id} numaralı kayıt arşive gönderilecek. Emin misiniz?",
                    "Arşiv Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string updateQuery = "UPDATE [DINAMIK_BIZERBA] SET Status=99, [UpdatedAt] = GETDATE() WHERE [ID] = @ID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, Helper.conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        int affected = cmd.ExecuteNonQuery();

                        if (affected > 0)
                        {
                            MessageBox.Show("Kayıt arşive gönderildi.", "Başarılı",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Listeyi yenile
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt arşive gönderilemedi.", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt arşive gönderilirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExcelExport()
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Dosyası (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls";
                    sfd.FilterIndex = 1;
                    sfd.FileName = $"DINAMIK_BIZERBA_{DateTime.Now:yyyyMMdd_HHmmss}";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        gridControl1.ExportToXlsx(sfd.FileName);

                        MessageBox.Show("Excel dosyası oluşturuldu.", "Başarılı",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Dosyayı aç
                        if (MessageBox.Show("Dosyayı açmak ister misiniz?", "Excel",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Excel export hatası: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PdfExport()
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "PDF Dosyası (*.pdf)|*.pdf";
                    sfd.FileName = $"DINAMIK_BIZERBA_{DateTime.Now:yyyyMMdd_HHmmss}";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        gridControl1.ExportToPdf(sfd.FileName);

                        MessageBox.Show("PDF dosyası oluşturuldu.", "Başarılı",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Dosyayı aç
                        if (MessageBox.Show("Dosyayı açmak ister misiniz?", "PDF",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"PDF export hatası: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == GridMenuType.Row)
            {
                // Varsayılan menüyü temizle
                e.Menu.Items.Clear();

                // Seçili satırın Fk_TransactionHeaderId değerini kontrol et
                long transactionHeaderId = 0;
                DataRow selectedRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (selectedRow != null && selectedRow["Fk_TransactionHeaderId"] != DBNull.Value)
                {
                    transactionHeaderId = Convert.ToInt64(selectedRow["Fk_TransactionHeaderId"]);
                }

                // Satış Fişini Göster (en üstte, sadece Fk_TransactionHeaderId > 0 ise aktif)
                DevExpress.Utils.Menu.DXMenuItem menuShowReceipt = new DevExpress.Utils.Menu.DXMenuItem("Satış Fişini Göster", (s, args) => ShowSaleReceipt());
                menuShowReceipt.Enabled = transactionHeaderId > 0;
                e.Menu.Items.Add(menuShowReceipt);

                // Separator
                e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem("-"));

                // Onayla
                DevExpress.Utils.Menu.DXMenuItem menuOnayla = new DevExpress.Utils.Menu.DXMenuItem("Manuel Onayla", (s, args) => OnaylaKayit());
                e.Menu.Items.Add(menuOnayla);

                // Karantina
                DevExpress.Utils.Menu.DXMenuItem menuKarantina = new DevExpress.Utils.Menu.DXMenuItem("Karantina", (s, args) => KarantinaKayit());
                e.Menu.Items.Add(menuKarantina);

                // Arsiv
                DevExpress.Utils.Menu.DXMenuItem menuArsiv = new DevExpress.Utils.Menu.DXMenuItem("Arşive Gönder(Sil)", (s, args) => ArsivKayit());
                e.Menu.Items.Add(menuArsiv);

                // Separator
                e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem("-"));

                // Grid Görünümü Resetle
                DevExpress.Utils.Menu.DXMenuItem menuReset = new DevExpress.Utils.Menu.DXMenuItem("Grid Görünümü Resetle", (s, args) => ResetGridLayout());
                e.Menu.Items.Add(menuReset);

                // Excel Export
                DevExpress.Utils.Menu.DXMenuItem menuExcel = new DevExpress.Utils.Menu.DXMenuItem("Excel Export", (s, args) => ExcelExport());
                e.Menu.Items.Add(menuExcel);

                // PDF Export
                DevExpress.Utils.Menu.DXMenuItem menuPdf = new DevExpress.Utils.Menu.DXMenuItem("PDF Export", (s, args) => PdfExport());
                e.Menu.Items.Add(menuPdf);
            }
        }

        private void FrmMainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnList_Click(sender, e);
            }
        }

        private void FrmMainMenu_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                HideToTray();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void gösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Timer'ı durdur
                timerAutoApprove.Stop();

                // GridView layout'unu registry'ye kaydet
                SaveGridLayout();

                Helper.WriteLog("FrmMainMenu kapatıldı. Timer durduruldu.");
            }
            catch (Exception ex)
            {
                Helper.WriteErrorLog("Çıkış sırasında hata", ex);
            }
            finally
            {
                // BalloonTimer'ı temizle
                if (balloonTimer != null)
                {
                    balloonTimer.Stop();
                    balloonTimer.Dispose();
                }

                // NotifyIcon'u kapat
                notifyIcon1.Visible = false;

                // Gerçekten çık
                Application.Exit();
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            try
            {
                // Şifre kontrolü
                using (var frmPassword = new FrmPasswordPrompt())
                {
                    if (frmPassword.ShowDialog(this) != DialogResult.OK)
                    {
                        return; // Kullanıcı iptal etti
                    }

                    if (!Helper.ValidateSettingsPassword(frmPassword.Password))
                    {
                        MessageBox.Show("Hatalı şifre! Genel Ayarlara erişim reddedildi.", "Hata",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Şifre doğruysa ayarlar formunu aç
                using (var frmSettings = new FrmSettings())
                {
                    if (frmSettings.ShowDialog() == DialogResult.OK)
                    {
                        // Timer interval'ini güncelle
                        timerAutoApprove.Interval = Helper.AutoApproveInterval * 60 * 1000;

                        // CheckBox durumunu güncelle
                        chkAutoApproveActive.Checked = Helper.AutoApproveActive;

                        // Timer durumunu güncelle
                        if (Helper.AutoApproveActive)
                        {
                            if (!timerAutoApprove.Enabled)
                            {
                                timerAutoApprove.Start();
                                WriteToTextBox($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Timer başlatıldı.");
                            }
                        }
                        else
                        {
                            if (timerAutoApprove.Enabled)
                            {
                                timerAutoApprove.Stop();
                                WriteToTextBox($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Timer durduruldu.");
                            }
                        }

                        string message = $"Ayarlar kaydedildi. Timer interval: {Helper.AutoApproveInterval} dakika, Aktif: {(Helper.AutoApproveActive ? "Evet" : "Hayır")}";
                        MessageBox.Show(message, "Bilgi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        WriteToTextBox($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
                        Helper.WriteLog(message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ayarlar formu açılırken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Helper.WriteErrorLog("btnSettings_Click hatası", ex);
            }
        }

        private void btnAutoApprove_Click(object sender, EventArgs e)
        {
            try
            {
                // Manuel tetikleme - timer'ı çalıştır
                timerAutoApprove_Tick(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Otomatik onaylama sırasında hata oluştu:\n{ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Helper.WriteErrorLog("btnAutoApprove_Click hatası", ex);
            }
        }

        private void timerAutoApprove_Tick(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = "";
                string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Otomatik onaylama başladı...";
                WriteToTextBox(logMessage);
                Helper.WriteLog("Otomatik onaylama işlemi başladı");

                // Cursor'u beklet
                Cursor = Cursors.WaitCursor;

                // Tüm mağazalar için otomatik onaylama yap
                int approvedCount = DinamikBizerba.AutoApproveRecords();
                logMessage = $"Tüm mağazalar: {approvedCount} adet kayıt otomatik onaylandı.";

                WriteToTextBox(logMessage);
                Helper.WriteLog(logMessage);

                // Listeyi yenile (sadece Status=0 görüntüleniyor ise)
                if (cboStatus.EditValue != null && Convert.ToInt32(cboStatus.EditValue) == 0)
                {
                    LoadData();
                }

                logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Otomatik onaylama tamamlandı.";
                WriteToTextBox(logMessage);
                Helper.WriteLog("Otomatik onaylama işlemi tamamlandı");

                // Sonraki çalışma zamanını hesapla ve göster
                DateTime nextRun = DateTime.Now.AddMinutes(Helper.AutoApproveInterval);
                string nextRunMessage = $"Sonraki otomatik onaylama: {nextRun:yyyy-MM-dd HH:mm:ss} ({Helper.AutoApproveInterval} dakika sonra)";
                WriteToTextBox(nextRunMessage);
                WriteToTextBox("".PadRight(80, '-')); // Ayırıcı çizgi

                // StatusBar'ı güncelle
                UpdateStatusBar();
            }
            catch (Exception ex)
            {
                string errorMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] HATA: {ex.Message}";
                WriteToTextBox(errorMessage);
                Helper.WriteErrorLog("timerAutoApprove_Tick hatası", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void WriteToTextBox(string message)
        {
            try
            {
                if (textBox1.InvokeRequired)
                {
                    textBox1.Invoke(new Action(() => WriteToTextBox(message)));
                    return;
                }

                textBox1.AppendText(message + Environment.NewLine);

                // ScrollBar'ı en alta getir
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
            }
            catch
            {
                // TextBox hatası olursa sessizce geç
            }
        }

        private void cboStatus_EditValueChanged(object sender, EventArgs e)
        {
            // Status değiştiğinde otomatik listeleme yap
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri yüklenirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkAutoApproveActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool isActive = chkAutoApproveActive.Checked;

                if (isActive)
                {
                    // Timer'ı başlat
                    timerAutoApprove.Start();

                    string message = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Otomatik onaylama aktif hale getirildi.";
                    WriteToTextBox(message);
                    Helper.WriteLog("Otomatik onaylama aktif hale getirildi");

                    // Sonraki çalışma zamanını göster
                    DateTime nextRun = DateTime.Now.AddMinutes(Helper.AutoApproveInterval);
                    WriteToTextBox($"Sonraki otomatik onaylama: {nextRun:yyyy-MM-dd HH:mm:ss} ({Helper.AutoApproveInterval} dakika sonra)");
                    WriteToTextBox("".PadRight(80, '-'));
                }
                else
                {
                    // Timer'ı durdur
                    timerAutoApprove.Stop();

                    string message = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Otomatik onaylama devre dışı bırakıldı.";
                    WriteToTextBox(message);
                    Helper.WriteLog("Otomatik onaylama devre dışı bırakıldı");
                    WriteToTextBox("".PadRight(80, '-'));
                }

                // Ayarı kaydet
                Helper.SaveAutoApproveSettings(Helper.AutoApproveInterval, isActive);

                // StatusBar'ı güncelle
                UpdateStatusBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Otomatik onaylama durumu değiştirilirken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Helper.WriteErrorLog("chkAutoApproveActive_CheckedChanged hatası", ex);
            }
        }

        #region Menu Event Handlers

        private void ShowSaleReceipt()
        {
            try
            {
                // Seçili satırı al
                DataRow selectedRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (selectedRow == null)
                {
                    MessageBox.Show("Lütfen bir kayıt seçiniz.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Fk_TransactionHeaderId kontrolü
                if (selectedRow["Fk_TransactionHeaderId"] == DBNull.Value)
                {
                    MessageBox.Show("Bu kayıt için satış fişi bulunamadı.\n(Henüz onaylanmamış kayıt)", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                long transactionHeaderId = Convert.ToInt64(selectedRow["Fk_TransactionHeaderId"]);
                if (transactionHeaderId <= 0)
                {
                    MessageBox.Show("Bu kayıt için satış fişi bulunamadı.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Satış fişi formunu aç
                var frmSaleReceipt = new FrmSaleReceipt(transactionHeaderId);
                frmSaleReceipt.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Satış fişi gösterilirken hata:\n{ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Helper.WriteErrorLog("ShowSaleReceipt hatası", ex);
            }
        }

        private void listeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnList_Click(sender, e);
        }

        private void otomatikOnayıÇalıştırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAutoApprove_Click(sender, e);
        }

        private void çıkışToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            çıkışToolStripMenuItem_Click(sender, e);
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frmAbout = new FrmAbout())
                {
                    frmAbout.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hakkında formu açılırken hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Helper.WriteErrorLog("hakkındaToolStripMenuItem_Click hatası", ex);
            }
        }

        #endregion

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
