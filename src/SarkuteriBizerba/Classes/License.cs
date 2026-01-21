using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace SarkuteriBizerba.Classes
{
  public static class License
  {
    private const string ENCRYPTION_KEY = "DinamikOtomasyon2025!BarkodLtdSti#SecureKey32Char";
    private const string LICENSE_FILE = "License.key";
    private const int TRIAL_DAYS = 30;

    /// <summary>
    /// Bilgisayarın benzersiz Hardware ID'sini oluşturur
    /// </summary>
    public static string GetHardwareID()
    {
      try
      {
        string cpuId = GetCPUID();
        string motherboardId = GetMotherboardID();
        string macAddress = GetMACAddress();

        string combined = $"{cpuId}|{motherboardId}|{macAddress}";
        return GenerateHash(combined);
      }
      catch (Exception ex)
      {
        throw new Exception($"Hardware ID alınamadı: {ex.Message}");
      }
    }

    /// <summary>
    /// CPU ID'sini alır
    /// </summary>
    private static string GetCPUID()
    {
      try
      {
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor");
        foreach (ManagementObject obj in searcher.Get())
        {
          return obj["ProcessorId"]?.ToString() ?? "UNKNOWN";
        }
        return "UNKNOWN";
      }
      catch
      {
        return "UNKNOWN";
      }
    }

    /// <summary>
    /// Motherboard Serial Number'ı alır
    /// </summary>
    private static string GetMotherboardID()
    {
      try
      {
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard");
        foreach (ManagementObject obj in searcher.Get())
        {
          return obj["SerialNumber"]?.ToString() ?? "UNKNOWN";
        }
        return "UNKNOWN";
      }
      catch
      {
        return "UNKNOWN";
      }
    }

    /// <summary>
    /// İlk fiziksel MAC adresini alır
    /// </summary>
    private static string GetMACAddress()
    {
      try
      {
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT MACAddress FROM Win32_NetworkAdapter WHERE PhysicalAdapter=True AND MACAddress IS NOT NULL");
        foreach (ManagementObject obj in searcher.Get())
        {
          string mac = obj["MACAddress"]?.ToString();
          if (!string.IsNullOrEmpty(mac))
            return mac;
        }
        return "UNKNOWN";
      }
      catch
      {
        return "UNKNOWN";
      }
    }

    /// <summary>
    /// String'den SHA256 hash oluşturur
    /// </summary>
    private static string GenerateHash(string input)
    {
      using (SHA256 sha256 = SHA256.Create())
      {
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder builder = new StringBuilder();
        foreach (byte b in bytes)
        {
          builder.Append(b.ToString("X2"));
        }
        return builder.ToString();
      }
    }

    /// <summary>
    /// Hardware ID için lisans anahtarı oluşturur (LisansManager uygulamasında kullanılır)
    /// </summary>
    public static string GenerateLicenseKey(string hardwareID, string customerName, DateTime expiryDate, string features)
    {
      try
      {
        string data = $"{hardwareID}|{customerName}|{expiryDate:yyyy-MM-dd}|{features}";
        return EncryptString(data);
      }
      catch (Exception ex)
      {
        throw new Exception($"Lisans anahtarı oluşturulamadı: {ex.Message}");
      }
    }

    /// <summary>
    /// Lisans dosyasını oluşturur ve kaydeder
    /// </summary>
    public static void SaveLicenseFile(string filePath, string customerName, string hardwareID, DateTime expiryDate, string features, string licenseKey)
    {
      try
      {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Customer={customerName}");
        sb.AppendLine($"HardwareID={hardwareID}");
        sb.AppendLine($"ExpiryDate={expiryDate:yyyy-MM-dd}");
        sb.AppendLine($"Features={features}");
        sb.AppendLine($"LicenseKey={licenseKey}");

        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
      }
      catch (Exception ex)
      {
        throw new Exception($"Lisans dosyası kaydedilemedi: {ex.Message}");
      }
    }

    /// <summary>
    /// Lisansı doğrular (Müşteri uygulamalarında kullanılır)
    /// </summary>
    public static LicenseInfo ValidateLicense(string appName)
    {
      try
      {
        string licenseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LICENSE_FILE);

        // Lisans dosyası yoksa trial kontrolü yap
        if (!File.Exists(licenseFilePath))
        {
          return CheckTrialPeriod(appName);
        }

        // Lisans dosyasını oku
        var licenseData = ParseLicenseFile(licenseFilePath);

        // Hardware ID kontrolü
        string currentHardwareID = GetHardwareID();
        if (licenseData.HardwareID != currentHardwareID)
        {
          return new LicenseInfo
          {
            IsValid = false,
            Message = "Lisans bu bilgisayar için geçerli değil!\nFarklı bir donanımda çalıştırılmaya çalışılıyor.",
            LicenseType = LicenseType.Invalid
          };
        }

        // Lisans anahtarını decrypt et ve doğrula
        string decryptedData = DecryptString(licenseData.LicenseKey);
        string[] parts = decryptedData.Split('|');

        if (parts.Length != 4)
        {
          return new LicenseInfo
          {
            IsValid = false,
            Message = "Lisans dosyası bozuk!",
            LicenseType = LicenseType.Invalid
          };
        }

        // Verileri kontrol et
        if (parts[0] != currentHardwareID)
        {
          return new LicenseInfo
          {
            IsValid = false,
            Message = "Lisans anahtarı donanım ile eşleşmiyor!",
            LicenseType = LicenseType.Invalid
          };
        }

        DateTime expiryDate = DateTime.Parse(parts[2]);
        string features = parts[3];

        // Uygulama adının feature listesinde olup olmadığını kontrol et
        if (!features.Contains(appName))
        {
          return new LicenseInfo
          {
            IsValid = false,
            Message = $"Lisans '{appName}' uygulaması için geçerli değil!",
            LicenseType = LicenseType.Invalid
          };
        }

        // Son kullanma tarihi kontrolü
        if (DateTime.Now > expiryDate)
        {
          return new LicenseInfo
          {
            IsValid = false,
            Message = $"Lisans süresi dolmuş!\nSon kullanma tarihi: {expiryDate:dd.MM.yyyy}",
            ExpiryDate = expiryDate,
            LicenseType = LicenseType.Expired
          };
        }

        // Lisans geçerli
        int daysRemaining = (expiryDate - DateTime.Now).Days;
        return new LicenseInfo
        {
          IsValid = true,
          Message = $"Lisans geçerli.\nSon kullanma tarihi: {expiryDate:dd.MM.yyyy}\nKalan gün: {daysRemaining}",
          CustomerName = parts[1],
          ExpiryDate = expiryDate,
          DaysRemaining = daysRemaining,
          LicenseType = LicenseType.Full
        };
      }
      catch (Exception ex)
      {
        return new LicenseInfo
        {
          IsValid = false,
          Message = $"Lisans doğrulama hatası: {ex.Message}",
          LicenseType = LicenseType.Invalid
        };
      }
    }

    /// <summary>
    /// Trial period kontrolü yapar
    /// </summary>
    private static LicenseInfo CheckTrialPeriod(string appName)
    {
      try
      {
        string trialFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $".trial_{appName}");

        DateTime installDate;
        if (!File.Exists(trialFilePath))
        {
          // İlk çalıştırma - trial başlat
          installDate = DateTime.Now;
          string encryptedDate = EncryptString(installDate.ToString("yyyy-MM-dd HH:mm:ss"));
          File.WriteAllText(trialFilePath, encryptedDate);
          File.SetAttributes(trialFilePath, FileAttributes.Hidden | FileAttributes.System);
        }
        else
        {
          // Trial dosyasını oku
          string encryptedDate = File.ReadAllText(trialFilePath);
          string decryptedDate = DecryptString(encryptedDate);
          installDate = DateTime.Parse(decryptedDate);
        }

        int daysUsed = (DateTime.Now - installDate).Days;
        int daysRemaining = TRIAL_DAYS - daysUsed;

        if (daysRemaining <= 0)
        {
          return new LicenseInfo
          {
            IsValid = false,
            Message = $"Deneme süresi doldu! ({TRIAL_DAYS} gün)\nLisans almak için Dinamik Otomasyon ile iletişime geçin.",
            LicenseType = LicenseType.TrialExpired
          };
        }

        return new LicenseInfo
        {
          IsValid = true,
          Message = $"Deneme sürümü kullanılıyor.\nKalan gün: {daysRemaining} / {TRIAL_DAYS}",
          DaysRemaining = daysRemaining,
          LicenseType = LicenseType.Trial
        };
      }
      catch (Exception ex)
      {
        return new LicenseInfo
        {
          IsValid = false,
          Message = $"Trial kontrol hatası: {ex.Message}",
          LicenseType = LicenseType.Invalid
        };
      }
    }

    /// <summary>
    /// Lisans dosyasını parse eder
    /// </summary>
    private static (string CustomerName, string HardwareID, DateTime ExpiryDate, string Features, string LicenseKey) ParseLicenseFile(string filePath)
    {
      var lines = File.ReadAllLines(filePath);
      string customer = "";
      string hardwareID = "";
      DateTime expiryDate = DateTime.MinValue;
      string features = "";
      string licenseKey = "";

      foreach (var line in lines)
      {
        if (line.StartsWith("Customer="))
          customer = line.Substring(9);
        else if (line.StartsWith("HardwareID="))
          hardwareID = line.Substring(11);
        else if (line.StartsWith("ExpiryDate="))
          expiryDate = DateTime.Parse(line.Substring(11));
        else if (line.StartsWith("Features="))
          features = line.Substring(9);
        else if (line.StartsWith("LicenseKey="))
          licenseKey = line.Substring(11);
      }

      return (customer, hardwareID, expiryDate, features, licenseKey);
    }

    /// <summary>
    /// String'i şifreler (AES-256)
    /// </summary>
    private static string EncryptString(string plainText)
    {
      byte[] key = Encoding.UTF8.GetBytes(ENCRYPTION_KEY.Substring(0, 32));
      byte[] iv = Encoding.UTF8.GetBytes(ENCRYPTION_KEY.Substring(16, 16));

      using (Aes aes = Aes.Create())
      {
        aes.Key = key;
        aes.IV = iv;

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using (MemoryStream msEncrypt = new MemoryStream())
        {
          using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
          {
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
              swEncrypt.Write(plainText);
            }
            return Convert.ToBase64String(msEncrypt.ToArray());
          }
        }
      }
    }

    /// <summary>
    /// Şifreli string'i çözer (AES-256)
    /// </summary>
    private static string DecryptString(string cipherText)
    {
      byte[] key = Encoding.UTF8.GetBytes(ENCRYPTION_KEY.Substring(0, 32));
      byte[] iv = Encoding.UTF8.GetBytes(ENCRYPTION_KEY.Substring(16, 16));
      byte[] buffer = Convert.FromBase64String(cipherText);

      using (Aes aes = Aes.Create())
      {
        aes.Key = key;
        aes.IV = iv;

        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using (MemoryStream msDecrypt = new MemoryStream(buffer))
        {
          using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
          {
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            {
              return srDecrypt.ReadToEnd();
            }
          }
        }
      }
    }
  }

  /// <summary>
  /// Lisans bilgilerini tutan sınıf
  /// </summary>
  public class LicenseInfo
  {
    public bool IsValid { get; set; }
    public string Message { get; set; }
    public string CustomerName { get; set; }
    public string TaxNumber { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public int DaysRemaining { get; set; }
    public LicenseType LicenseType { get; set; }
  }

  /// <summary>
  /// Lisans tipi
  /// </summary>
  public enum LicenseType
  {
    Invalid,        // Geçersiz
    Trial,          // Deneme sürümü
    TrialExpired,   // Deneme süresi dolmuş
    Full,           // Tam lisans
    Expired         // Süresi dolmuş lisans
  }
}
