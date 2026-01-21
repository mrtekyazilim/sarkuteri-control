using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;

namespace SarkuteriAdmin.Classes
{
    public static class Helper
    {
        public static SqlConnection conn;
        private static string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.ini");

        // Etiket Zamanı Ayarları
        public static int LabelTimeT1 { get; private set; } = -300; // -300 saniye (5 dakika önce)
        public static int LabelTimeT2 { get; private set; } = 3600;  // 3600 saniye (1 saat sonra)

        // Ağırlık Toleransı Ayarı (kg cinsinden, varsayılan ±2 gram = 0.002 kg)
        public static decimal WeightTolerance { get; private set; } = 0.002m;

        // Otomatik Onaylama Timer Ayarı (dakika cinsinden)
        public static int AutoApproveInterval { get; private set; } = 5; // 5 dakika
        public static bool AutoApproveActive { get; private set; } = true; // Otomatik onaylama aktif mi?

        // Ayarlar Şifresi (varsayılan: 1234)
        private static string settingsPassword = "1234";

        static Helper()
        {
            LoadConfig();
            InitializeConnection();
        }

        [DllImport("kernel32", EntryPoint = "WritePrivateProfileStringW", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        private static extern Int32 WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        private static extern Int32 GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault, string lpReturnedString, Int32 nSize, string lpFileName);

        public static string INIRead(string INIPath, string SectionName, string KeyName, string DefaultValue = "")
        {
            string functionReturnValue = null;
            Int32 n = default(Int32);
            string sData = null;
            sData = Space(1024);
            n = GetPrivateProfileString(SectionName, KeyName, DefaultValue, sData, sData.Length, INIPath);
            if (n > 0)
            {
                functionReturnValue = sData.Substring(0, n);
            }
            else
            {
                functionReturnValue = "";
            }
            return functionReturnValue;
        }

        public static string Space(int n)
        {
            string s = "";
            for (int i = 0; i < n; i++)
            {
                s += " ";
            }
            return s;
        }

        public static void INIWrite(string INIPath, string SectionName, string KeyName, string TheValue)
        {
            WritePrivateProfileString(SectionName, KeyName, TheValue, INIPath);
        }

        public static void INIDelete(string INIPath, string SectionName, string KeyName)
        {
            WritePrivateProfileString(SectionName, KeyName, null, INIPath);
        }

        public static void INIDelete(string INIPath, string SectionName)
        {
            WritePrivateProfileString(SectionName, null, null, INIPath);
        }

        private static void LoadConfig()
        {
            try
            {
                if (!File.Exists(configPath))
                {
                    CreateDefaultConfig();
                }

                // Etiket Zamanı ayarlarını yükle
                LoadLabelTimeSettings();

                // Otomatik Onaylama ayarlarını yükle
                LoadAutoApproveSettings();

                // Ağırlık Toleransı ayarlarını yükle
                LoadWeightToleranceSettings();

                // Ayarlar Şifresini yükle
                LoadSettingsPassword();
            }
            catch (Exception ex)
            {
                throw new Exception($"Config.ini dosyası yüklenirken hata: {ex.Message}");
            }
        }

        private static void LoadLabelTimeSettings()
        {
            try
            {
                string t1Str = INIRead(configPath, "LabelTime", "T1", "-300");
                string t2Str = INIRead(configPath, "LabelTime", "T2", "3600");

                if (int.TryParse(t1Str, out int t1))
                {
                    LabelTimeT1 = t1;
                }

                if (int.TryParse(t2Str, out int t2))
                {
                    LabelTimeT2 = t2;
                }
            }
            catch
            {
                // Hata durumunda varsayılan değerleri kullan
                LabelTimeT1 = -300;
                LabelTimeT2 = 3600;
            }
        }

        public static void SaveLabelTimeSettings(int t1, int t2)
        {
            try
            {
                INIWrite(configPath, "LabelTime", "T1", t1.ToString());
                INIWrite(configPath, "LabelTime", "T2", t2.ToString());

                LabelTimeT1 = t1;
                LabelTimeT2 = t2;
            }
            catch (Exception ex)
            {
                throw new Exception($"Etiket zamanı ayarları kaydedilirken hata: {ex.Message}");
            }
        }

        private static void LoadWeightToleranceSettings()
        {
            try
            {
                string toleranceStr = INIRead(configPath, "WeightTolerance", "ToleranceKg", "0.002");

                if (decimal.TryParse(toleranceStr.Replace(",", "."), System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out decimal tolerance))
                {
                    WeightTolerance = tolerance;
                }
            }
            catch
            {
                // Hata durumunda varsayılan değeri kullan (±2 gram)
                WeightTolerance = 0.002m;
            }
        }

        public static void SaveWeightToleranceSettings(decimal toleranceKg)
        {
            try
            {
                INIWrite(configPath, "WeightTolerance", "ToleranceKg",
                    toleranceKg.ToString(System.Globalization.CultureInfo.InvariantCulture));
                WeightTolerance = toleranceKg;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ağırlık toleransı ayarları kaydedilirken hata: {ex.Message}");
            }
        }

        private static void LoadAutoApproveSettings()
        {
            try
            {
                string intervalStr = INIRead(configPath, "AutoApprove", "IntervalMinutes", "5");
                string activeStr = INIRead(configPath, "AutoApprove", "Active", "true");

                if (int.TryParse(intervalStr, out int interval))
                {
                    AutoApproveInterval = interval;
                }

                if (bool.TryParse(activeStr, out bool active))
                {
                    AutoApproveActive = active;
                }
            }
            catch
            {
                // Hata durumunda varsayılan değerleri kullan
                AutoApproveInterval = 5;
                AutoApproveActive = true;
            }
        }

        public static void SaveAutoApproveSettings(int intervalMinutes, bool active)
        {
            try
            {
                INIWrite(configPath, "AutoApprove", "IntervalMinutes", intervalMinutes.ToString());
                INIWrite(configPath, "AutoApprove", "Active", active.ToString());
                AutoApproveInterval = intervalMinutes;
                AutoApproveActive = active;
            }
            catch (Exception ex)
            {
                throw new Exception($"Otomatik onaylama ayarları kaydedilirken hata: {ex.Message}");
            }
        }

        private static void LoadSettingsPassword()
        {
            try
            {
                string password = INIRead(configPath, "Settings", "Password", "1234");
                settingsPassword = password;
            }
            catch
            {
                settingsPassword = "1234";
            }
        }

        public static bool ValidateSettingsPassword(string inputPassword)
        {
            return settingsPassword == inputPassword;
        }

        public static void SaveSettingsPassword(string newPassword)
        {
            try
            {
                INIWrite(configPath, "Settings", "Password", newPassword);
                settingsPassword = newPassword;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ayarlar şifresi kaydedilirken hata: {ex.Message}");
            }
        }

        // Log fonksiyonları
        public static void WriteLog(string message, string logType = "log")
        {
            try
            {
                string logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                if (!Directory.Exists(logFolder))
                {
                    Directory.CreateDirectory(logFolder);
                }

                string logFileName = $"{logType}_{DateTime.Now:yyyyMMdd}.txt";
                string logFilePath = Path.Combine(logFolder, logFileName);

                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
            catch
            {
                // Log yazarken hata olursa sessizce geç
            }
        }

        public static void WriteErrorLog(string message, Exception ex = null)
        {
            try
            {
                string logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                if (!Directory.Exists(logFolder))
                {
                    Directory.CreateDirectory(logFolder);
                }

                string logFileName = $"err_{DateTime.Now:yyyyMMdd}.txt";
                string logFilePath = Path.Combine(logFolder, logFileName);

                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR: {message}";
                if (ex != null)
                {
                    logEntry += Environment.NewLine + $"  Exception: {ex.Message}";
                    logEntry += Environment.NewLine + $"  StackTrace: {ex.StackTrace}";
                }

                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
            catch
            {
                // Log yazarken hata olursa sessizce geç
            }
        }

        private static void CreateDefaultConfig()
        {
            string defaultConfig = @"[SQL]
Server=(local)
Database=Genius3
NtAuth=true
SqlUser=sa
SqlPass=

[LabelTime]
T1=-300
T2=3600

[WeightTolerance]
ToleranceKg=0.002

[AutoApprove]
IntervalMinutes=5
Active=true

[Settings]
Password=1234";

            File.WriteAllText(configPath, defaultConfig);
        }

        public static string GetConfigValue(string section, string key, string defaultValue = "")
        {
            try
            {
                if (!File.Exists(configPath))
                    return defaultValue;

                string value = INIRead(configPath, section, key, defaultValue);
                return value;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static void SetConfigValue(string section, string key, string value)
        {
            try
            {
                if (!File.Exists(configPath))
                    CreateDefaultConfig();

                INIWrite(configPath, section, key, value);
            }
            catch (Exception ex)
            {
                throw new Exception($"Config.ini değeri kaydedilirken hata: {ex.Message}");
            }
        }

        public static void InitializeConnection()
        {
            try
            {
                string server = GetConfigValue("SQL", "Server", "localhost");
                string database = GetConfigValue("SQL", "Database", "Genius3");
                bool ntAuth = bool.Parse(GetConfigValue("SQL", "NtAuth", "true"));
                string sqlUser = GetConfigValue("SQL", "SqlUser", "");
                string sqlPass = GetConfigValue("SQL", "SqlPass", "");

                string connectionString;
                if (ntAuth)
                {
                    connectionString = $"Server={server};Database={database};Integrated Security=true;Connection Timeout=30;";
                }
                else
                {
                    connectionString = $"Server={server};Database={database};User Id={sqlUser};Password={sqlPass};Connection Timeout=30;";
                }

                conn = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw new Exception($"SQL bağlantısı başlatılırken hata: {ex.Message}");
            }
        }

        public static bool TestConnection()
        {
            try
            {
                if (conn == null)
                    InitializeConnection();

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                bool result = conn.State == ConnectionState.Open;

                if (conn.State == ConnectionState.Open)
                    conn.Close();

                return result;
            }
            catch
            {
                return false;
            }
        }

        public static bool OpenConnection()
        {
            try
            {
                if (conn == null)
                    InitializeConnection();

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                return conn.State == ConnectionState.Open;
            }
            catch
            {
                return false;
            }
        }

        public static void CloseConnection()
        {
            try
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch
            {
                // Hata durumunda sessizce geç
            }
        }

        public static void ReloadConnection()
        {
            try
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Dispose();
                }

                InitializeConnection();
            }
            catch (Exception ex)
            {
                throw new Exception($"Bağlantı yeniden yüklenirken hata: {ex.Message}");
            }
        }

        // Config properties
        public static string Server
        {
            get { return GetConfigValue("SQL", "Server", "localhost"); }
            set { SetConfigValue("SQL", "Server", value); }
        }

        public static string Database
        {
            get { return GetConfigValue("SQL", "Database", "Genius3"); }
            set { SetConfigValue("SQL", "Database", value); }
        }

        public static bool NtAuth
        {
            get { return bool.Parse(GetConfigValue("SQL", "NtAuth", "true")); }
            set { SetConfigValue("SQL", "NtAuth", value.ToString()); }
        }

        public static string SqlUser
        {
            get { return GetConfigValue("SQL", "SqlUser", ""); }
            set { SetConfigValue("SQL", "SqlUser", value); }
        }

        public static string SqlPass
        {
            get { return GetConfigValue("SQL", "SqlPass", ""); }
            set { SetConfigValue("SQL", "SqlPass", value); }
        }
    }
}
