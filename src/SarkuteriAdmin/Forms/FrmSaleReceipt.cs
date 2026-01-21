using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SarkuteriAdmin.Classes;

namespace SarkuteriAdmin.Forms
{
  public partial class FrmSaleReceipt : Form
  {
    private long _transactionHeaderId;
    private DataTable _dtSaleDetails;

    public FrmSaleReceipt(long transactionHeaderId)
    {
      InitializeComponent();
      _transactionHeaderId = transactionHeaderId;
    }

    private void FrmSaleReceipt_Load(object sender, EventArgs e)
    {
      try
      {
        LoadSaleReceipt();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Satış fişi yüklenirken hata:\n{ex.Message}", "Hata",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        Helper.WriteErrorLog("FrmSaleReceipt_Load hatası", ex);
        this.Close();
      }
    }

    private void LoadSaleReceipt()
    {
      try
      {
        if (Helper.conn == null || Helper.conn.State != ConnectionState.Open)
        {
          throw new Exception("Veritabanı bağlantısı açık değil.");
        }

        // Ana satış bilgilerini getir
        string headerQuery = @"
                    SELECT 
                        TH.[ID],
                        TH.[TRANS_DATE],
                        TH.[TRANS_END_DATE],
                        TH.[RECEIPT_BARCODE],
                        ST.[NUM] as StoreNum,
                        ST.[DESCRIPTION] as StoreName,
                        POS.[NUM] as PosNum,
                        POS.[DESCRIPTION] as PosName
                    FROM [GENIUS3].[TRANSACTION_HEADER] TH
                    LEFT JOIN [GENIUS3].[STORE] ST ON TH.[FK_STORE] = ST.[ID]
                    LEFT JOIN [GENIUS3].[POS] POS ON TH.[FK_POS] = POS.[ID]
                    WHERE TH.[ID] = @TransactionHeaderId";

        DataTable dtHeader = new DataTable();
        using (SqlCommand cmd = new SqlCommand(headerQuery, Helper.conn))
        {
          cmd.Parameters.AddWithValue("@TransactionHeaderId", _transactionHeaderId);
          using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
          {
            adapter.Fill(dtHeader);
          }
        }

        if (dtHeader.Rows.Count == 0)
        {
          throw new Exception("Satış fişi bulunamadı.");
        }

        DataRow headerRow = dtHeader.Rows[0];

        // Başlık bilgilerini göster
        lblReceiptNumber.Text = $"Fiş No: {headerRow["RECEIPT_BARCODE"]}";
        lblTransDate.Text = $"Tarih: {Convert.ToDateTime(headerRow["TRANS_DATE"]):dd.MM.yyyy HH:mm:ss}";
        lblStore.Text = $"Mağaza: {headerRow["StoreNum"]} - {headerRow["StoreName"]}";
        lblPos.Text = $"Kasa: {headerRow["PosNum"]} - {headerRow["PosName"]}";

        // Detay satırlarını getir
        string detailQuery = @"
                    SELECT 
                        TS.[ID],
                        ROW_NUMBER() OVER (ORDER BY TS.[ID]) as LINE_NUM,
                        TS.[BARCODE],
                        TS.[AMOUNT],
                        TS.[UNIT_PRICE],
                        TS.[TOTAL_PRICE],
                        SC.[CODE] as StockCode,
                        SC.[DESCRIPTION] as StockDescription
                    FROM [GENIUS3].[TRANSACTION_SALE] TS
                    LEFT JOIN [GENIUS3].[STOCK_CARD] SC ON TS.[FK_STOCK_CARD] = SC.[ID]
                    WHERE TS.[FK_TRANSACTION_HEADER] = @TransactionHeaderId
                    ORDER BY TS.[ID]";

        _dtSaleDetails = new DataTable();
        using (SqlCommand cmd = new SqlCommand(detailQuery, Helper.conn))
        {
          cmd.Parameters.AddWithValue("@TransactionHeaderId", _transactionHeaderId);
          using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
          {
            adapter.Fill(_dtSaleDetails);
          }
        }

        // Grid'e bağla
        gridControl1.DataSource = _dtSaleDetails;

        // Kolon görünümlerini ayarla
        gridView1.Columns["ID"].Visible = false;
        gridView1.Columns["LINE_NUM"].Caption = "Sıra";
        gridView1.Columns["LINE_NUM"].Width = 50;
        gridView1.Columns["BARCODE"].Caption = "Barkod";
        gridView1.Columns["BARCODE"].Width = 120;
        gridView1.Columns["StockCode"].Caption = "Stok Kodu";
        gridView1.Columns["StockCode"].Width = 100;
        gridView1.Columns["StockDescription"].Caption = "Ürün Adı";
        gridView1.Columns["StockDescription"].Width = 250;
        gridView1.Columns["AMOUNT"].Caption = "Miktar";
        gridView1.Columns["AMOUNT"].Width = 80;
        gridView1.Columns["AMOUNT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        gridView1.Columns["AMOUNT"].DisplayFormat.FormatString = "n3";
        gridView1.Columns["UNIT_PRICE"].Caption = "Birim Fiyat";
        gridView1.Columns["UNIT_PRICE"].Width = 100;
        gridView1.Columns["UNIT_PRICE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        gridView1.Columns["UNIT_PRICE"].DisplayFormat.FormatString = "n2";
        gridView1.Columns["TOTAL_PRICE"].Caption = "Toplam Fiyat";
        gridView1.Columns["TOTAL_PRICE"].Width = 100;
        gridView1.Columns["TOTAL_PRICE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        gridView1.Columns["TOTAL_PRICE"].DisplayFormat.FormatString = "n2";

        // GridView ayarları
        gridView1.OptionsBehavior.Editable = false;
        gridView1.OptionsBehavior.ReadOnly = true;
        gridView1.OptionsView.ShowGroupPanel = false;
        gridView1.OptionsView.ShowFooter = true;

        // Footer'da toplamları göster
        gridView1.Columns["LINE_NUM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
        gridView1.Columns["LINE_NUM"].SummaryItem.DisplayFormat = "Toplam: {0} kalem";

        gridView1.Columns["TOTAL_PRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
        gridView1.Columns["TOTAL_PRICE"].SummaryItem.DisplayFormat = "{0:n2} TL";

        // Özet bilgileri göster
        int itemCount = _dtSaleDetails.Rows.Count;
        decimal totalAmount = 0;

        foreach (DataRow row in _dtSaleDetails.Rows)
        {
          if (row["TOTAL_PRICE"] != DBNull.Value)
            totalAmount += Convert.ToDecimal(row["TOTAL_PRICE"]);
        }

        // KDV'yi %18 olarak varsayalım (gerçek KDV oranı bilinmediği için)
        decimal vatAmount = totalAmount * 0.18m / 1.18m;
        decimal netAmount = totalAmount - vatAmount;

        lblItemCount.Text = $"Kalem Sayısı: {itemCount}";
        lblTotalAmount.Text = $"Toplam Tutar (KDV Dahil): {totalAmount:N2} TL";
        lblVatAmount.Text = $"KDV Tutarı: {vatAmount:N2} TL";
        lblNetAmount.Text = $"Net Tutar: {netAmount:N2} TL";

        this.Text = $"Satış Fişi - {headerRow["RECEIPT_BARCODE"]}";
      }
      catch (Exception ex)
      {
        Helper.WriteErrorLog("LoadSaleReceipt hatası", ex);
        throw;
      }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnPrint_Click(object sender, EventArgs e)
    {
      try
      {
        // Print preview göster
        gridControl1.ShowPrintPreview();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Yazdırma önizleme hatası:\n{ex.Message}", "Hata",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        Helper.WriteErrorLog("btnPrint_Click hatası", ex);
      }
    }

    private void btnExportExcel_Click(object sender, EventArgs e)
    {
      try
      {
        using (SaveFileDialog sfd = new SaveFileDialog())
        {
          sfd.Filter = "Excel Dosyası (*.xlsx)|*.xlsx";
          sfd.FileName = $"SatisFisi_{_transactionHeaderId}_{DateTime.Now:yyyyMMdd_HHmmss}";

          if (sfd.ShowDialog() == DialogResult.OK)
          {
            gridView1.ExportToXlsx(sfd.FileName);
            MessageBox.Show("Excel dosyası oluşturuldu.", "Başarılı",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        MessageBox.Show($"Excel export hatası:\n{ex.Message}", "Hata",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        Helper.WriteErrorLog("btnExportExcel_Click hatası", ex);
      }
    }
  }
}
