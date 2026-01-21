using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SarkuteriBizerba.Classes
{
    public static class DinamikBizerba
    {
        // Database table operations
        public static bool EnsureDinamikBizerbaTableExists()
        {
            try
            {
                if (Helper.conn == null)
                    Helper.InitializeConnection();

                // Tabloyu kontrol et
                string checkTableQuery = @"
          IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DINAMIK_BIZERBA' AND xtype='U')
          BEGIN
            CREATE TABLE [dbo].[DINAMIK_BIZERBA](
              [ID] [bigint] IDENTITY(1,1) NOT NULL,
              [CreatedAt] [datetime] NOT NULL DEFAULT GETDATE(),
              [UpdatedAt] [datetime] NOT NULL DEFAULT GETDATE(),
              [StoreNum] [int] NULL,
              [ScaleIP] [nvarchar](30) NULL,
              [ScaleName] [nvarchar](100) NULL,
              [SerialNumber] [int] NULL,
              [ScaleNumber] [nvarchar](30) NULL,
              [ScaleDate] [datetime] NULL,
              [Barcode] [nvarchar](50) NULL,
              [BarcodeFull] [nvarchar](50) NULL,
              [NetWeight] [decimal](18,4) NULL,
              [UnitPrice] [decimal](18,2) NULL,
              [TotalPrice] [decimal](18,2) NULL,
              [ProductCode] [varchar](30) NULL,
              [PLUNumber] [varchar](5) NULL,
              [Fk_TransactionSaleId] [bigint] NULL,
              [Fk_TransactionHeaderId] [bigint] NULL,
              [Fk_StockCardId] [bigint] NULL,
              [Fk_StoreId] [int] NULL,
              [Fk_PosId] [int] NULL,
              [Status] [int] NULL,
            CONSTRAINT [PK_DINAMIK_BIZERBA] PRIMARY KEY CLUSTERED ([ID] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
                  ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY]

            -- İndeksleri oluştur
            CREATE NONCLUSTERED INDEX [IX_DINAMIK_BIZERBA_ScaleDate] ON [dbo].[DINAMIK_BIZERBA] ([ScaleDate] ASC)
            CREATE NONCLUSTERED INDEX [IX_DINAMIK_BIZERBA_Status] ON [dbo].[DINAMIK_BIZERBA] ([Status] ASC)
            CREATE NONCLUSTERED INDEX [IX_DINAMIK_BIZERBA_StoreNum] ON [dbo].[DINAMIK_BIZERBA] ([StoreNum] ASC)
            CREATE NONCLUSTERED INDEX [IX_DINAMIK_BIZERBA_Barcode] ON [dbo].[DINAMIK_BIZERBA] ([Barcode] ASC)
          END
          ELSE
          BEGIN
            -- Tablo varsa ScaleName kolonu var mı kontrol et, yoksa ekle
            IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DINAMIK_BIZERBA]') AND name = 'ScaleName')
            BEGIN
              ALTER TABLE [dbo].[DINAMIK_BIZERBA] ADD [ScaleName] [nvarchar](100) NULL
            END
          END";

                using (SqlCommand cmd = new SqlCommand(checkTableQuery, Helper.conn))
                {
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"DINAMIK_BIZERBA tablosu oluşturulurken hata: {ex.Message}");
            }
        }

        public static bool CheckTableExists(string tableName)
        {
            try
            {
                if (Helper.conn == null)
                    Helper.InitializeConnection();

                string query = "SELECT COUNT(*) FROM sysobjects WHERE name=@tableName AND xtype='U'";

                using (SqlCommand cmd = new SqlCommand(query, Helper.conn))
                {
                    cmd.Parameters.AddWithValue("@tableName", tableName);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Tablo kontrol edilirken hata: {ex.Message}");
            }
        }

        public static bool InsertTransaction(int storeNum, string scaleIP, string scaleName, BizerbaTransaction transaction)
        {
            try
            {
                if (Helper.conn == null || Helper.conn.State == ConnectionState.Closed)
                    Helper.InitializeConnection();
                
                // Önce aynı veriler var mı kontrol et
                if (TransactionExists(storeNum, scaleIP, transaction))
                {
                    return false; // Zaten var, ekleme yapma
                }

                string insertQuery = @"
                    INSERT INTO [dbo].[DINAMIK_BIZERBA] 
                    (
                        [StoreNum], [ScaleIP], [ScaleName], [SerialNumber], [ScaleNumber], [ScaleDate], 
                        [Barcode], [BarcodeFull], [NetWeight], [UnitPrice], [TotalPrice], 
                        [ProductCode], [PLUNumber], [Fk_TransactionSaleId], [Fk_TransactionHeaderId], 
                        [Fk_StockCardId], [Fk_StoreId], [Fk_PosId], [Status]
                    ) 
                    VALUES 
                    (
                        @StoreNum, @ScaleIP, @ScaleName, @SerialNumber, @ScaleNumber, @ScaleDate, 
                        @Barcode, @BarcodeFull, @NetWeight, @UnitPrice, @TotalPrice, 
                        @ProductCode, @PLUNumber, @Fk_TransactionSaleId, @Fk_TransactionHeaderId, 
                        @Fk_StockCardId, @Fk_StoreId, @Fk_PosId, @Status
                    )";

                using (SqlCommand cmd = new SqlCommand(insertQuery, Helper.conn))
                {
                    // Parametreleri ekle
                    cmd.Parameters.AddWithValue("@StoreNum", storeNum);
                    cmd.Parameters.AddWithValue("@ScaleIP", scaleIP ?? "");
                    cmd.Parameters.AddWithValue("@ScaleName", scaleName ?? "");
                    cmd.Parameters.AddWithValue("@SerialNumber", int.TryParse(transaction.SerialNumber, out int serialNum) ? serialNum : 0);
                    cmd.Parameters.AddWithValue("@ScaleNumber", transaction.ScaleNumber ?? "");

                    // Tarih ve saat birleştir
                    DateTime scaleDate = DateTime.Today;
                    if (DateTime.TryParseExact(transaction.Date, "dd-MM-yy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                    {
                        if (TimeSpan.TryParse(transaction.Time, out TimeSpan parsedTime))
                        {
                            scaleDate = parsedDate.Add(parsedTime);
                        }
                        else
                        {
                            scaleDate = parsedDate;
                        }
                    }
                    cmd.Parameters.AddWithValue("@ScaleDate", scaleDate);

                    cmd.Parameters.AddWithValue("@Barcode", transaction.Barcode ?? "");
                    cmd.Parameters.AddWithValue("@BarcodeFull", transaction.BarcodeFull ?? "");
                    cmd.Parameters.AddWithValue("@NetWeight", transaction.Kilogram);

                    // UnitPrice ve TotalPrice için sistemin decimal pointer'ını kullan
                    string unitPriceStr = transaction.UnitPrice?.Replace(".", Helper.GetDecimalPointer()).Replace(",", Helper.GetDecimalPointer()) ?? "0";
                    string totalPriceStr = transaction.TotalPrice?.Replace(".", Helper.GetDecimalPointer()).Replace(",", Helper.GetDecimalPointer()) ?? "0";

                    cmd.Parameters.AddWithValue("@UnitPrice", decimal.TryParse(unitPriceStr, out decimal unitPrice) ? unitPrice : 0);
                    cmd.Parameters.AddWithValue("@TotalPrice", decimal.TryParse(totalPriceStr, out decimal totalPrice) ? totalPrice : 0);
                    cmd.Parameters.AddWithValue("@ProductCode", transaction.ProductCode ?? "");
                    cmd.Parameters.AddWithValue("@PLUNumber", transaction.PLUNumber ?? "");

                    // Fk_* alanları 0 olarak ayarla
                    cmd.Parameters.AddWithValue("@Fk_TransactionSaleId", 0);
                    cmd.Parameters.AddWithValue("@Fk_TransactionHeaderId", 0);
                    cmd.Parameters.AddWithValue("@Fk_StockCardId", 0);
                    cmd.Parameters.AddWithValue("@Fk_StoreId", 0);
                    cmd.Parameters.AddWithValue("@Fk_PosId", 0);

                    // Status = 0
                    cmd.Parameters.AddWithValue("@Status", 0);
                   
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Transaction kaydedilirken hata: {ex.Message}");
            }
        }

        public static int InsertTransactions(int storeNum, string scaleIP, string scaleName, List<BizerbaTransaction> transactions)
        {
            int successCount = 0;

            foreach (var transaction in transactions)
            {
                try
                {
                    if (InsertTransaction(storeNum, scaleIP, scaleName, transaction))
                    {
                        successCount++;
                    }
                }
                catch (Exception ex)
                {
                    // Log hatayı ama devam et
                    System.Diagnostics.Debug.WriteLine($"Transaction insert hatası: {ex.Message}");
                }
            }

            return successCount;
        }

        private static bool TransactionExists(int storeNum, string scaleIP, BizerbaTransaction transaction)
        {
            try
            {
                if (Helper.conn == null)
                    Helper.InitializeConnection();

                // Tarih ve saat birleştir (aynı logic InsertTransaction'daki ile)
                DateTime scaleDate = DateTime.Today;
                if (DateTime.TryParseExact(transaction.Date, "dd-MM-yy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    if (TimeSpan.TryParse(transaction.Time, out TimeSpan parsedTime))
                    {
                        scaleDate = parsedDate.Add(parsedTime);
                    }
                    else
                    {
                        scaleDate = parsedDate;
                    }
                }

                string checkQuery = @"
                    SELECT COUNT(*) FROM [dbo].[DINAMIK_BIZERBA] 
                    WHERE [StoreNum] = @StoreNum 
                      AND [ScaleIP] = @ScaleIP 
                      AND [SerialNumber] = @SerialNumber 
                      AND [ScaleNumber] = @ScaleNumber 
                      AND [ScaleDate] = @ScaleDate 
                      AND [Barcode] = @Barcode 
                      AND [NetWeight] = @NetWeight";

                using (SqlCommand cmd = new SqlCommand(checkQuery, Helper.conn))
                {
                    cmd.Parameters.AddWithValue("@StoreNum", storeNum);
                    cmd.Parameters.AddWithValue("@ScaleIP", scaleIP ?? "");
                    cmd.Parameters.AddWithValue("@SerialNumber", int.TryParse(transaction.SerialNumber, out int serialNum) ? serialNum : 0);
                    cmd.Parameters.AddWithValue("@ScaleNumber", transaction.ScaleNumber ?? "");
                    cmd.Parameters.AddWithValue("@ScaleDate", scaleDate);
                    cmd.Parameters.AddWithValue("@Barcode", transaction.Barcode ?? "");
                    cmd.Parameters.AddWithValue("@NetWeight", transaction.Kilogram);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Transaction kontrol edilirken hata: {ex.Message}");
            }
        }
    }
}
