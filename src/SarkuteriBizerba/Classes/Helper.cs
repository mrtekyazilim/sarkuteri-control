using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace SarkuteriBizerba.Classes
{
  public static class Helper
  {
    public static SqlConnection conn;
    private static string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.ini");

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
      // primary version of call gets single value given all parameters
      Int32 n = default(Int32);
      string sData = null;
      sData = Space(1024);
      // allocate some room 
      n = GetPrivateProfileString(SectionName, KeyName, DefaultValue, sData, sData.Length, INIPath);
      // return whatever it gave us
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

    // delete single line from section
    public static void INIDelete(string INIPath, string SectionName, string KeyName)
    {
      WritePrivateProfileString(SectionName, KeyName, null, INIPath);
    }

    public static void INIDelete(string INIPath, string SectionName)
    {
      // delete section from INI file
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
      }
      catch (Exception ex)
      {
        throw new Exception($"Config dosyası yüklenirken hata: {ex.Message}");
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

[SETTINGS]
DataFetchIntervalMinutes=5
StoreNum=0
AutoSync=false
AdminPassword=

[BIZERBA]
IP1=
IP2=
IP3=
IP4=
IP5=
IP6=
IP7=
IP8=
IP9=
IP10=
Name1=
Name2=
Name3=
Name4=
Name5=
Name6=
Name7=
Name8=
Name9=
Name10=";

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
        throw new Exception($"Config değeri kaydedilirken hata: {ex.Message}");
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

    public static int SyncIntervalMinutes
    {
      get { return int.Parse(GetConfigValue("SETTINGS", "SyncIntervalMinutes", "5")); }
      set { SetConfigValue("SETTINGS", "SyncIntervalMinutes", value.ToString()); }
    }

    public static bool AutoSync
    {
      get { return bool.Parse(GetConfigValue("SETTINGS", "AutoSync", "false")); }
      set { SetConfigValue("SETTINGS", "AutoSync", value.ToString()); }
    }

    public static int StoreNum
    {
      get
      {
        string value = GetConfigValue("SETTINGS", "StoreNum", "0");
        return int.TryParse(value, out int result) ? result : 0;
      }
      set { SetConfigValue("SETTINGS", "StoreNum", value.ToString()); }
    }

    public static string AdminPassword
    {
      get { return GetConfigValue("SETTINGS", "AdminPassword", ""); }
      set { SetConfigValue("SETTINGS", "AdminPassword", value); }
    }

    // Bizerba Terazi IP Adresleri
    public static string BizerbaIP1
    {
      get { return GetConfigValue("BIZERBA", "IP1", "192.168.1.101"); }
      set { SetConfigValue("BIZERBA", "IP1", value); }
    }

    public static string BizerbaIP2
    {
      get { return GetConfigValue("BIZERBA", "IP2", "192.168.1.102"); }
      set { SetConfigValue("BIZERBA", "IP2", value); }
    }

    public static string BizerbaIP3
    {
      get { return GetConfigValue("BIZERBA", "IP3", "192.168.1.103"); }
      set { SetConfigValue("BIZERBA", "IP3", value); }
    }

    public static string BizerbaIP4
    {
      get { return GetConfigValue("BIZERBA", "IP4", "192.168.1.104"); }
      set { SetConfigValue("BIZERBA", "IP4", value); }
    }

    public static string BizerbaIP5
    {
      get { return GetConfigValue("BIZERBA", "IP5", "192.168.1.105"); }
      set { SetConfigValue("BIZERBA", "IP5", value); }
    }

    public static string BizerbaIP6
    {
      get { return GetConfigValue("BIZERBA", "IP6", "192.168.1.106"); }
      set { SetConfigValue("BIZERBA", "IP6", value); }
    }

    public static string BizerbaIP7
    {
      get { return GetConfigValue("BIZERBA", "IP7", "192.168.1.107"); }
      set { SetConfigValue("BIZERBA", "IP7", value); }
    }

    public static string BizerbaIP8
    {
      get { return GetConfigValue("BIZERBA", "IP8", "192.168.1.108"); }
      set { SetConfigValue("BIZERBA", "IP8", value); }
    }

    public static string BizerbaIP9
    {
      get { return GetConfigValue("BIZERBA", "IP9", "192.168.1.109"); }
      set { SetConfigValue("BIZERBA", "IP9", value); }
    }

    public static string BizerbaIP10
    {
      get { return GetConfigValue("BIZERBA", "IP10", "192.168.1.110"); }
      set { SetConfigValue("BIZERBA", "IP10", value); }
    }

    // Bizerba Terazi İsimleri
    public static string BizerbaName1
    {
      get { return GetConfigValue("BIZERBA", "Name1", ""); }
      set { SetConfigValue("BIZERBA", "Name1", value); }
    }

    public static string BizerbaName2
    {
      get { return GetConfigValue("BIZERBA", "Name2", ""); }
      set { SetConfigValue("BIZERBA", "Name2", value); }
    }

    public static string BizerbaName3
    {
      get { return GetConfigValue("BIZERBA", "Name3", ""); }
      set { SetConfigValue("BIZERBA", "Name3", value); }
    }

    public static string BizerbaName4
    {
      get { return GetConfigValue("BIZERBA", "Name4", ""); }
      set { SetConfigValue("BIZERBA", "Name4", value); }
    }

    public static string BizerbaName5
    {
      get { return GetConfigValue("BIZERBA", "Name5", ""); }
      set { SetConfigValue("BIZERBA", "Name5", value); }
    }

    public static string BizerbaName6
    {
      get { return GetConfigValue("BIZERBA", "Name6", ""); }
      set { SetConfigValue("BIZERBA", "Name6", value); }
    }

    public static string BizerbaName7
    {
      get { return GetConfigValue("BIZERBA", "Name7", ""); }
      set { SetConfigValue("BIZERBA", "Name7", value); }
    }

    public static string BizerbaName8
    {
      get { return GetConfigValue("BIZERBA", "Name8", ""); }
      set { SetConfigValue("BIZERBA", "Name8", value); }
    }

    public static string BizerbaName9
    {
      get { return GetConfigValue("BIZERBA", "Name9", ""); }
      set { SetConfigValue("BIZERBA", "Name9", value); }
    }

    public static string BizerbaName10
    {
      get { return GetConfigValue("BIZERBA", "Name10", ""); }
      set { SetConfigValue("BIZERBA", "Name10", value); }
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
          conn.Close();
      }
      catch
      {
        // Ignore close errors
      }
    }

    public static bool PingTest(string ipAddress)
    {
      try
      {
        // IP adresi boş veya null ise false döner
        if (string.IsNullOrWhiteSpace(ipAddress))
        {
          return false;
        }

        // Ping nesnesi oluştur
        using (Ping ping = new Ping())
        {
          // Hızlı test için düşük timeout (1000ms = 1 saniye)
          PingReply reply = ping.Send(ipAddress, 1000);

          // Başarılı ise true döner
          return reply.Status == IPStatus.Success;
        }
      }
      catch
      {
        // Herhangi bir hata durumunda false döner
        return false;
      }
    }

    public static bool IsScaleReachable(string ipAddress)
    {
      try
      {
        // IP adresi boş veya null ise false döner
        if (string.IsNullOrWhiteSpace(ipAddress))
        {
          return false;
        }

        // Önce ping test et
        if (!PingTest(ipAddress))
        {
          return false;
        }

        // Ping başarılı ise true döner
        // İsteğe bağlı: Burada TCP port kontrolü de eklenebilir
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static string GetDecimalPointer()
    {
      try
      {
        // Sistemin decimal ayırıcısını al
        string decimalSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        // String olarak döndür
        return decimalSeparator;
      }
      catch
      {
        // Hata durumunda varsayılan olarak nokta döndür
        return ".";
      }
    }
  }
}