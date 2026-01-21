using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SarkuteriBizerba.Classes
{
    public static class Bizerba
    {
        [DllImport("TSTRANSDLL1.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = false, EntryPoint = "SendPluFile")]
        public static extern IntPtr SendPluFile(string pIPFilePath, string pPLUFilePath);

        [DllImport("TSTRANSDLL1.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = false, EntryPoint = "SendPreKey")]
        public static extern IntPtr SendPreKey(string pIPFilePath, string pPrekeyFilePath);

        [DllImport("TSTRANSDLL1.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = false, EntryPoint = "SendPrintFormat")]
        public static extern IntPtr SendPrintFormat(string pIPFilePath, string pPrintFormatFilePath, int pPrintFormatNumber);

        [DllImport("TSTRANSDLL1.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = false, EntryPoint = "sum1")]
        public extern static int sum11(int a, int b);

        [DllImport("TSTRANSDLL1.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = false, EntryPoint = "SendPluJsonData")]
        public static extern IntPtr SendPluJsonData(string ipAddressRelativeFilePath, string pluRelativeFilePath);

        [DllImport("TSTRANSDLL1.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = false, EntryPoint = "DeletePlu")]
        public static extern IntPtr DeletePlu(string ipAddressRelativeFilePath, int pluNumber);
        // P/Invoke 声明

        [DllImport("TSTRANSDLL1.dll", EntryPoint = "RecycleReport", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr RecycleReport(string ipAddressRelativeFilePath, string startDate, string endDate);

        [DllImport("TSTRANSDLL1.dll", EntryPoint = "GetTransactionInformation", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetTransactionInformation(string ipAddressRelativeFilePath, string type, string startDate, string endDate, string startTime, string endTime, int no);

        public static string lastError = "";
        public static List<BizerbaTransaction> Transactions(string ip, DateTime startDate, DateTime endDate)
        {
            try
            {
                lastError = "";
                DirectoryInfo path = new DirectoryInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);


                string strIpPath = Path.Combine(path.FullName, @"ip.csv");
                File.WriteAllText(strIpPath, ip);

                IntPtr resultPtr = GetTransactionInformation(strIpPath, "label", startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"), "0000", "2359", 0);
                if (resultPtr == IntPtr.Zero)
                {
                    return new List<BizerbaTransaction>();
                }
                string jsonData = Marshal.PtrToStringAnsi(resultPtr);
                File.WriteAllText(Path.Combine(path.FullName, @"jsonData.json"), jsonData);



                jsonData = jsonData.Replace("},]", "}]");
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                if (jsonData.Trim() == "")
                {
                    return new List<BizerbaTransaction>();
                }
                else
                {
                    List<BizerbaTransaction> transactions = serializer.Deserialize<List<BizerbaTransaction>>(jsonData);
                    return transactions;
                }

            }
            catch (Exception ex)
            {
                lastError = "Hata oluştu." + Environment.NewLine + ex.Message;
            }
            return null;
        }
    }
    public class BizerbaTransaction
    {
        public string SerialNumber { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string ScaleNumber { get; set; }
        public string Gross { get; set; }
        public string Net { get; set; }
        public string Tare { get; set; }
        public string Mode { get; set; }
        public string PLUNumber { get; set; }
        public string ProductCode { get; set; }
        public decimal Kilogram
        {
            get
            {
                string s = this.Net.Split(' ')[0].Replace(".", Helper.GetDecimalPointer());
                if (decimal.TryParse(s, out _))
                {
                    return Convert.ToDecimal(s);
                }
                else
                {
                    return 0;
                }
            }
        }
        public string Barcode
        {
            get
            {
                return ProductCode + PLUNumber;
            }
        }
        public string BarcodeFull
        {
            get
            {
                string s = ProductCode + PLUNumber + (this.Kilogram * 1000).ToString("00000");

                // EAN-13 checksum hesapla
                if (s.Length == 12)
                {
                    int checksum = CalculateEAN13Checksum(s);
                    s += checksum.ToString();
                }

                return s;
            }
        }

        private int CalculateEAN13Checksum(string barcode12)
        {
            if (barcode12.Length != 12) return 0;

            int oddSum = 0;  // 1., 3., 5., ... pozisyonlar
            int evenSum = 0; // 2., 4., 6., ... pozisyonlar

            for (int i = 0; i < 12; i++)
            {
                if (int.TryParse(barcode12[i].ToString(), out int digit))
                {
                    if (i % 2 == 0) // Tek pozisyon (0-indexed, yani 1., 3., 5., ...)
                        oddSum += digit;
                    else // Çift pozisyon (0-indexed, yani 2., 4., 6., ...)
                        evenSum += digit;
                }
            }

            int total = oddSum + (evenSum * 3);
            int checksum = (10 - (total % 10)) % 10;

            return checksum;
        }
        public string Pcs { get; set; }
        public string PrintQuantity { get; set; }
        public string UnitPrice { get; set; }
        public string TotalPrice { get; set; }
        public string SalePrice { get; set; }
        public string SaleTotalPrice { get; set; }
        public string PriceUnit { get; set; }
        public int SaleValid { get; set; }
        public string TotalWeight { get; set; }
        public int TotalPCS { get; set; }
    }
}
