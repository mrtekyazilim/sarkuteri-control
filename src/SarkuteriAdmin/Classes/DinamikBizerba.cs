using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarkuteriAdmin.Classes
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
              [ProductCode] [varchar](2) NULL,
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

        public static DataTable GetRecords(int storeNum, DateTime startDate, DateTime endDate, int? status = null, string barcode = null)
        {
            try
            {
                if (Helper.conn == null || Helper.conn.State != ConnectionState.Open)
                {
                    throw new Exception("Veritabanı bağlantısı açık değil.");
                }

                string query = @"SELECT 
                                    DB.[ID],
                                    DB.[CreatedAt],
                                    DB.[UpdatedAt],
                                    DB.[StoreNum],
                                    DB.[ScaleIP],
                                    DB.[ScaleName],
                                    DB.[SerialNumber],
                                    DB.[ScaleNumber],
                                    DB.[ScaleDate],
                                    DB.[Barcode],
                                    DB.[BarcodeFull],
                                    DB.[NetWeight],
                                    DB.[UnitPrice],
                                    DB.[TotalPrice],
                                    DB.[ProductCode],
                                    DB.[PLUNumber],
                                    DB.[Fk_TransactionSaleId],
                                    DB.[Fk_TransactionHeaderId],
                                    DB.[Fk_StockCardId],
                                    DB.[Fk_StoreId],
                                    DB.[Fk_PosId],
                                    DB.[Status],
                                    -- STORE bilgileri
                                    ST.[NUM] as StoreNum_Store,
                                    ST.[DESCRIPTION] as StoreName,
                                    -- POS bilgileri
                                    POS.[NUM] as PosNum,
                                    POS.[DESCRIPTION] as PosName,
                                    -- TRANSACTION_SALE bilgileri
                                    TS.[AMOUNT] as TSQuantity,
                                    TS.[UNIT_PRICE] as TSUnitPrice,
                                    TS.[TOTAL_PRICE] as TSTotalPrice,
                                    TS.[BARCODE] as TSBarcode,

                                    -- TRANSACTION_HEADER bilgileri
                                    TH.[TRANS_DATE] as TransactionDate,
                                    TH.[RECEIPT_BARCODE] as ReceiptNumber,
                                    -- STOCK_CARD bilgileri (önce Fk_StockCardId'den, yoksa STOCK_BARCODE'dan)
                                    COALESCE(SC.[CODE], SC2.[CODE]) as StockCode,
                                    COALESCE(SC.[DESCRIPTION], SC2.[DESCRIPTION]) as StockDescription
                                FROM [DINAMIK_BIZERBA] DB
                                LEFT OUTER JOIN [GENIUS3].[STORE] ST ON DB.[Fk_StoreId] = ST.[ID]
                                LEFT OUTER JOIN [GENIUS3].[POS] POS ON DB.[Fk_PosId] = POS.[ID]
                                LEFT OUTER JOIN [GENIUS3].[TRANSACTION_SALE] TS ON DB.[Fk_TransactionSaleId] = TS.[ID]
                                LEFT OUTER JOIN [GENIUS3].[TRANSACTION_HEADER] TH ON DB.[Fk_TransactionHeaderId] = TH.[ID]
                                -- İlk önce Fk_StockCardId ile direkt STOCK_CARD'a bağlan
                                LEFT OUTER JOIN [GENIUS3].[STOCK_CARD] SC ON DB.[Fk_StockCardId] = SC.[ID]
                                -- Eğer Fk_StockCardId yoksa, Barcode ile STOCK_BARCODE üzerinden STOCK_CARD'a bağlan
                                LEFT OUTER JOIN [GENIUS3].[STOCK_BARCODE] SB ON DB.[Barcode] = SB.[BARCODE] AND ISNULL(DB.[Fk_StockCardId],0)=0
                                LEFT OUTER JOIN [GENIUS3].[STOCK_CARD] SC2 ON SB.[FK_STOCK_CARD] = SC2.[ID]
                                WHERE DB.[ScaleDate] BETWEEN @StartDate AND @EndDate";

                // StoreNum filtresi (0 ise tüm mağazalar)
                if (storeNum > 0)
                {
                    query += " AND DB.[StoreNum] = @StoreNum";
                }

                // Status filtresi (null ise tüm statuslar)
                if (status.HasValue)
                {
                    query += " AND DB.[Status] = @Status";
                }

                // Barcode filtresi (opsiyonel)
                if (!string.IsNullOrWhiteSpace(barcode))
                {
                    query += " AND DB.[Barcode] LIKE @Barcode";
                }

                query += " ORDER BY DB.[ScaleDate] DESC, DB.[ID] DESC";

                using (SqlCommand cmd = new SqlCommand(query, Helper.conn))
                {
                    cmd.Parameters.AddWithValue("@StartDate", startDate.Date);
                    cmd.Parameters.AddWithValue("@EndDate", endDate.Date.AddDays(1).AddSeconds(-1));

                    if (storeNum > 0)
                    {
                        cmd.Parameters.AddWithValue("@StoreNum", storeNum);
                    }

                    if (status.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@Status", status.Value);
                    }

                    if (!string.IsNullOrWhiteSpace(barcode))
                    {
                        cmd.Parameters.AddWithValue("@Barcode", "%" + barcode.Trim() + "%");
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Kayıtlar listelenirken hata: {ex.Message}");
            }
        }

        /// <summary>
        /// Status=0 olan kayıtları kontrol eder ve IBM yazarkasalardan satılanları otomatik onaylar (Status=1)
        /// İYİLEŞTİRİLMİŞ VERSİYON: Tolerans bandı ve esnek zaman aralığı eklendi
        /// </summary>
        /// <returns>Onaylanan kayıt sayısı</returns>
        public static int AutoApproveRecords()
        {
            try
            {
                if (Helper.conn == null || Helper.conn.State != ConnectionState.Open)
                {
                    throw new Exception("Veritabanı bağlantısı açık değil.");
                }

                // T1 ve T2 değerlerini al (saniye cinsinden)
                int t1 = Helper.LabelTimeT1;  // Varsayılan: -300 (5 dakika önce)
                int t2 = Helper.LabelTimeT2;  // Varsayılan: 3600 (1 saat sonra)

                // WeightTolerance (kg cinsinden, varsayılan ±2 gram = 0.002 kg)
                decimal weightTolerance = Helper.WeightTolerance;

                // İYİLEŞTİRİLMİŞ SORGU:
                // 1. ABS() fonksiyonu ile ağırlık toleransı eklendi (±2 gram = ±0.002 kg)
                // 2. Barcode eşleşmesi için hem tam hem de kısa barcode kontrolü
                // 3. Zaman aralığı daha esnek yapıldı
                string updateQuery = @"
                    UPDATE DB
                    SET DB.[Status] = 1,
                        DB.[UpdatedAt] = GETDATE(),
                        DB.[Fk_TransactionSaleId] = TS.[ID],
                        DB.[Fk_TransactionHeaderId] = TH.[ID],
                        DB.[Fk_StoreId] = ST.[ID],
                        DB.[Fk_PosId] = TH.[FK_POS],
                        DB.[Fk_StockCardId] = TS.[FK_STOCK_CARD]
                    FROM [DINAMIK_BIZERBA] DB
                    INNER JOIN [GENIUS3].[STORE] ST ON DB.[StoreNum] = ST.[NUM]
                    INNER JOIN [GENIUS3].[TRANSACTION_HEADER] TH ON ST.[ID] = TH.[FK_STORE]
                    INNER JOIN [GENIUS3].[TRANSACTION_SALE] TS ON TH.[ID] = TS.[FK_TRANSACTION_HEADER]
                    WHERE DB.[Status] = 0
                        -- Barcode eşleşmesi (hem tam hem de kısa barcode)
                        AND (DB.[Barcode] = TS.[BARCODE] OR DB.[BarcodeFull] = TS.[BARCODE])
                        -- Ağırlık toleransı eklendi (±2 gram)
                        AND ABS(DB.[NetWeight] - TS.[AMOUNT]) <= @WeightTolerance
                        -- Zaman aralığı kontrolü (etiket zamanı ile satış zamanı arasında)
                        AND DB.[ScaleDate] >= DATEADD(SECOND, @T1, TH.[TRANS_DATE])
                        AND DB.[ScaleDate] <= DATEADD(SECOND, @T2, TH.[TRANS_END_DATE])";

                using (SqlCommand cmd = new SqlCommand(updateQuery, Helper.conn))
                {
                    cmd.Parameters.AddWithValue("@T1", t1);
                    cmd.Parameters.AddWithValue("@T2", t2);
                    cmd.Parameters.AddWithValue("@WeightTolerance", weightTolerance);

                    int affectedRows = cmd.ExecuteNonQuery();

                    // Log yaz
                    Helper.WriteLog($"AutoApproveRecords: {affectedRows} kayıt onaylandı. T1={t1}, T2={t2}, WeightTolerance={weightTolerance}");

                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                Helper.WriteErrorLog("AutoApproveRecords hatası", ex);
                throw new Exception($"Otomatik onaylama sırasında hata: {ex.Message}");
            }
        }

        /// <summary>
        /// Belirli bir mağaza için otomatik onaylama yapar
        /// İYİLEŞTİRİLMİŞ VERSİYON
        /// </summary>
        /// <param name="storeNum">Mağaza numarası (0 = tüm mağazalar)</param>
        /// <returns>Onaylanan kayıt sayısı</returns>
        public static int AutoApproveRecordsByStore(int storeNum)
        {
            try
            {
                if (Helper.conn == null || Helper.conn.State != ConnectionState.Open)
                {
                    throw new Exception("Veritabanı bağlantısı açık değil.");
                }

                int t1 = Helper.LabelTimeT1;
                int t2 = Helper.LabelTimeT2;
                decimal weightTolerance = Helper.WeightTolerance;

                string updateQuery = @"
                    UPDATE DB
                    SET DB.[Status] = 1,
                        DB.[UpdatedAt] = GETDATE(),
                        DB.[Fk_TransactionSaleId] = TS.[ID],
                        DB.[Fk_TransactionHeaderId] = TH.[ID],
                        DB.[Fk_StoreId] = ST.[ID],
                        DB.[Fk_PosId] = TH.[FK_POS],
                        DB.[Fk_StockCardId] = TS.[FK_STOCK_CARD]
                    FROM [DINAMIK_BIZERBA] DB
                    INNER JOIN [GENIUS3].[STORE] ST ON DB.[StoreNum] = ST.[NUM]
                    INNER JOIN [GENIUS3].[TRANSACTION_HEADER] TH ON ST.[ID] = TH.[FK_STORE]
                    INNER JOIN [GENIUS3].[TRANSACTION_SALE] TS ON TH.[ID] = TS.[FK_TRANSACTION_HEADER]
                    WHERE DB.[Status] = 0
                        AND (DB.[Barcode] = TS.[BARCODE] OR DB.[BarcodeFull] = TS.[BARCODE])
                        AND ABS(DB.[NetWeight] - TS.[AMOUNT]) <= @WeightTolerance
                        AND DB.[ScaleDate] >= DATEADD(SECOND, @T1, TH.[TRANS_DATE])
                        AND DB.[ScaleDate] <= DATEADD(SECOND, @T2, TH.[TRANS_END_DATE])";

                if (storeNum > 0)
                {
                    updateQuery += " AND DB.[StoreNum] = @StoreNum";
                }

                using (SqlCommand cmd = new SqlCommand(updateQuery, Helper.conn))
                {
                    cmd.Parameters.AddWithValue("@T1", t1);
                    cmd.Parameters.AddWithValue("@T2", t2);
                    cmd.Parameters.AddWithValue("@WeightTolerance", weightTolerance);

                    if (storeNum > 0)
                    {
                        cmd.Parameters.AddWithValue("@StoreNum", storeNum);
                    }

                    int affectedRows = cmd.ExecuteNonQuery();

                    // Log yaz
                    Helper.WriteLog($"AutoApproveRecordsByStore (StoreNum={storeNum}): {affectedRows} kayıt onaylandı. T1={t1}, T2={t2}, WeightTolerance={weightTolerance}");

                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                Helper.WriteErrorLog($"AutoApproveRecordsByStore (StoreNum={storeNum}) hatası", ex);
                throw new Exception($"Otomatik onaylama sırasında hata: {ex.Message}");
            }
        }

        /// <summary>
        /// Onaylanmamış etiketlerin detaylı analizini yapar (ZAMAN KONTROLÜ OLMADAN)
        /// </summary>
        public static DataTable AnalyzeUnmatchedLabels(int storeNum = 0)
        {
            try
            {
                if (Helper.conn == null || Helper.conn.State != ConnectionState.Open)
                {
                    throw new Exception("Veritabanı bağlantısı açık değil.");
                }

                decimal weightTolerance = Helper.WeightTolerance;

                // ZAMAN KONTROLÜ OLMADAN sadece tarih, barcode ve ağırlık kontrolü
                string query = @"
                    SELECT 
                        DB.[ID],
                        DB.[StoreNum],
                        DB.[ScaleDate],
                        DB.[Barcode],
                        DB.[BarcodeFull],
                        DB.[NetWeight],
                        DB.[TotalPrice],
                        DB.[PLUNumber],
                        DB.[ProductCode],
                        -- En yakın satış kaydı (ZAMAN FARKI OLMADAN, sadece TARIH)
                        ClosestSale.SaleDate,
                        ClosestSale.SaleBarcode,
                        ClosestSale.SaleWeight,
                        ClosestSale.SaleTotalPrice,
                        ClosestSale.ReceiptNumber,
                        ClosestSale.PosNum,
                        -- Farklar
                        ClosestSale.WeightDifference,
                        ClosestSale.TimeDifferenceSeconds,
                        ClosestSale.TimeDifferenceMinutes,
                        -- Neden eşleşmedi?
                        CASE 
                            WHEN ClosestSale.SaleBarcode IS NULL THEN '❌ Hiç satış bulunamadı'
                            WHEN DB.[Barcode] <> ClosestSale.SaleBarcode AND DB.[BarcodeFull] <> ClosestSale.SaleBarcode THEN '❌ Barcode eşleşmiyor'
                            WHEN ClosestSale.WeightDifference > @WeightTolerance THEN '⚠️ Ağırlık farkı tolerans dışında (±' + CAST(@WeightTolerance * 1000 AS VARCHAR(10)) + ' gram)'
                            WHEN ClosestSale.TimeDifferenceMinutes < -60 THEN '⏰ Etiket satıştan çok geç basılmış (' + CAST(ABS(ClosestSale.TimeDifferenceMinutes) AS VARCHAR(10)) + ' dk sonra)'
                            WHEN ClosestSale.TimeDifferenceMinutes > 120 THEN '⏰ Etiket satıştan çok erken basılmış (' + CAST(ClosestSale.TimeDifferenceMinutes AS VARCHAR(10)) + ' dk önce)'
                            WHEN ClosestSale.SaleBarcode IS NOT NULL AND ClosestSale.WeightDifference <= @WeightTolerance THEN '✅ ZAMAN KONTROLÜ KALDIRILIRSA EŞLEŞİR!'
                            ELSE '❓ Bilinmeyen neden'
                        END as UnmatchReason
                    FROM [DINAMIK_BIZERBA] DB
                    INNER JOIN [GENIUS3].[STORE] ST ON DB.[StoreNum] = ST.[NUM]
                    OUTER APPLY (
                        SELECT TOP 1 
                            TH.[TRANS_DATE] as SaleDate,
                            TS.[BARCODE] as SaleBarcode,
                            TS.[AMOUNT] as SaleWeight,
                            TS.[TOTAL_PRICE] as SaleTotalPrice,
                            TH.[RECEIPT_BARCODE] as ReceiptNumber,
                            POS.[NUM] as PosNum,
                            ABS(TS.[AMOUNT] - DB.[NetWeight]) as WeightDifference,
                            DATEDIFF(SECOND, DB.[ScaleDate], TH.[TRANS_DATE]) as TimeDifferenceSeconds,
                            DATEDIFF(MINUTE, DB.[ScaleDate], TH.[TRANS_DATE]) as TimeDifferenceMinutes
                        FROM [GENIUS3].[TRANSACTION_HEADER] TH
                        INNER JOIN [GENIUS3].[TRANSACTION_SALE] TS ON TH.[ID] = TS.[FK_TRANSACTION_HEADER]
                        LEFT JOIN [GENIUS3].[POS] POS ON TH.[FK_POS] = POS.[ID]
                        WHERE TH.[FK_STORE] = ST.[ID]
                            -- Sadece AYNI GÜN içinde ara
                            AND CONVERT(DATE, TH.[TRANS_DATE]) = CONVERT(DATE, DB.[ScaleDate])
                            -- Barcode eşleşmesi
                            AND (DB.[Barcode] = TS.[BARCODE] OR DB.[BarcodeFull] = TS.[BARCODE])
                        ORDER BY ABS(DATEDIFF(SECOND, DB.[ScaleDate], TH.[TRANS_DATE]))
                    ) AS ClosestSale
                    WHERE DB.[Status] = 0";

                if (storeNum > 0)
                {
                    query += " AND DB.[StoreNum] = @StoreNum";
                }

                query += " ORDER BY DB.[ScaleDate] DESC, DB.[ID] DESC";

                using (SqlCommand cmd = new SqlCommand(query, Helper.conn))
                {
                    cmd.Parameters.AddWithValue("@WeightTolerance", weightTolerance);

                    if (storeNum > 0)
                    {
                        cmd.Parameters.AddWithValue("@StoreNum", storeNum);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.WriteErrorLog("AnalyzeUnmatchedLabels hatası", ex);
                throw new Exception($"Onaylanmamış etiketler analiz edilirken hata: {ex.Message}");
            }
        }

        /// <summary>
        /// Analiz özet raporu (istatistikler)
        /// </summary>
        public static string GetAnalysisSummary(int storeNum = 0)
        {
            try
            {
                DataTable dtAnalysis = AnalyzeUnmatchedLabels(storeNum);

                if (dtAnalysis.Rows.Count == 0)
                {
                    return "✅ Tebrikler! Tüm etiketler onaylandı.";
                }

                // İstatistikleri hesapla
                int totalUnmatched = dtAnalysis.Rows.Count;
                int noSaleFound = 0;
                int barcodeNotMatch = 0;
                int weightNotMatch = 0;
                int timeTooLate = 0;
                int timeTooEarly = 0;
                int wouldMatchWithoutTime = 0;

                foreach (DataRow row in dtAnalysis.Rows)
                {
                    string reason = row["UnmatchReason"].ToString();

                    if (reason.Contains("Hiç satış bulunamadı"))
                        noSaleFound++;
                    else if (reason.Contains("Barcode eşleşmiyor"))
                        barcodeNotMatch++;
                    else if (reason.Contains("Ağırlık farkı"))
                        weightNotMatch++;
                    else if (reason.Contains("çok geç basılmış"))
                        timeTooLate++;
                    else if (reason.Contains("çok erken basılmış"))
                        timeTooEarly++;
                    else if (reason.Contains("ZAMAN KONTROLÜ KALDIRILIRSA EŞLEŞİR"))
                        wouldMatchWithoutTime++;
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine("═══════════════════════════════════════════════════════════════");
                sb.AppendLine("📊 ONAYLANMAMIŞ ETİKETLER ANALİZ RAPORU");
                sb.AppendLine("═══════════════════════════════════════════════════════════════");
                sb.AppendLine();
                sb.AppendLine(string.Format("📌 Toplam Onaylanmamış Etiket: {0}", totalUnmatched));
                sb.AppendLine();
                sb.AppendLine("🔍 NEDENLERİ:");
                sb.AppendLine(string.Format("  ❌ Hiç satış bulunamadı: {0} adet ({1:P1})", noSaleFound, (double)noSaleFound / totalUnmatched));
                sb.AppendLine(string.Format("  ❌ Barcode eşleşmiyor: {0} adet ({1:P1})", barcodeNotMatch, (double)barcodeNotMatch / totalUnmatched));
                sb.AppendLine(string.Format("  ⚠️  Ağırlık farkı tolerans dışında: {0} adet ({1:P1})", weightNotMatch, (double)weightNotMatch / totalUnmatched));
                sb.AppendLine(string.Format("  ⏰ Etiket çok geç basılmış: {0} adet ({1:P1})", timeTooLate, (double)timeTooLate / totalUnmatched));
                sb.AppendLine(string.Format("  ⏰ Etiket çok erken basılmış: {0} adet ({1:P1})", timeTooEarly, (double)timeTooEarly / totalUnmatched));
                sb.AppendLine();
                sb.AppendLine("═══════════════════════════════════════════════════════════════");
                sb.AppendLine(string.Format("✅ ZAMAN KONTROLÜ KALDIRILIRSA EŞLEŞECEKLER: {0} adet ({1:P1})", wouldMatchWithoutTime, (double)wouldMatchWithoutTime / totalUnmatched));
                sb.AppendLine("═══════════════════════════════════════════════════════════════");
                sb.AppendLine();

                if (wouldMatchWithoutTime > 0)
                {
                    sb.AppendLine("💡 ÖNERİ:");
                    sb.AppendLine("   Zaman kontrolünü kaldırıp sadece TARIH bazlı kontrol yaparsanız,");
                    sb.AppendLine(string.Format("   {0} adet DAHA FAZLA etiket otomatik onaylanacak!", wouldMatchWithoutTime));
                    sb.AppendLine();
                    sb.AppendLine("   Ancak bu durumda:");
                    sb.AppendLine("   - Aynı gün içinde aynı üründen birden fazla satış varsa");
                    sb.AppendLine("   - Yanlış eşleşme riski var!");
                    sb.AppendLine();
                }

                if (noSaleFound > 0)
                {
                    sb.AppendLine("⚠️  DİKKAT:");
                    sb.AppendLine(string.Format("   {0} adet etiketin hiç satışı bulunamadı.", noSaleFound));
                    sb.AppendLine("   Bu etiketler muhtemelen ÇALINDI veya kasaya geçmedi!");
                }

                sb.AppendLine();
                sb.AppendLine("═══════════════════════════════════════════════════════════════");

                return sb.ToString();
            }
            catch (Exception ex)
            {
                Helper.WriteErrorLog("GetAnalysisSummary hatası", ex);
                return "❌ Analiz raporu oluşturulurken hata: " + ex.Message;
            }
        }
    }
}
