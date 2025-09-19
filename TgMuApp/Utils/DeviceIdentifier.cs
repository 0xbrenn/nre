using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace TgMuApp.Utils
{
    public class DeviceIdGenerator
    {
        
        private static readonly char[] ExcludedChars = { 'I', 'O', 'U', '0', '1' };
        private static readonly char[] ValidChars =
            "ABCDEFGHJKLMNPQRSTVWXYZ23456789".ToCharArray();

        /// <summary>
        /// 
        /// </summary>
        /// <returns> ABCDE-FGHJK-LMNPQ-RSTVW</returns>
        public string GenerateDeviceId()
        {
            try
            {
                string hardwareInfo = CollectHardwareInfo();
                string hash = ComputeHash(hardwareInfo);

                return FormatDeviceId(hash);
            }
            catch (Exception ex)
            {
                return GenerateFallbackId();
            }
        }

     
        private string CollectHardwareInfo()
        {
            StringBuilder sb = new StringBuilder();

          
            try { sb.Append(GetCpuId()); } catch { }
            try { sb.Append(GetBaseboardId()); } catch { }
            try { sb.Append(GetDiskId()); } catch { }
            try { sb.Append(GetBiosId()); } catch { }
            try { sb.Append(GetMacAddress()); } catch { }

            if (sb.Length == 0)
            {
                sb.Append(Environment.MachineName);
                sb.Append(Environment.OSVersion.VersionString);
            }

            return sb.ToString();
        }

     
        private string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

               
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

       
        private string FormatDeviceId(string hash)
        {
            
            StringBuilder cleanHash = new StringBuilder();
            foreach (char c in hash)
            {
                if (ValidChars.Contains(c))
                {
                    cleanHash.Append(c);
                }
            }

            //
            if (cleanHash.Length < 20)
            {
                cleanHash.Clear();
                foreach (char c in hash)
                {
                    if (char.IsLetterOrDigit(c) && !ExcludedChars.Contains(c))
                    {
                        cleanHash.Append(c);
                        if (cleanHash.Length >= 20) break;
                    }
                }
            }

            // 
            while (cleanHash.Length < 20)
            {
                cleanHash.Append(ValidChars[cleanHash.Length % ValidChars.Length]);
            }

            // 
            return string.Format("{0}-{1}-{2}-{3}",
                cleanHash.ToString().Substring(0, 5),
                cleanHash.ToString().Substring(5, 5),
                cleanHash.ToString().Substring(10, 5),
                cleanHash.ToString().Substring(15, 5));
        }

     
        private string GetCpuId()
        {
            return GetWmiProperty("Win32_Processor", "ProcessorId");
        }

      
        private string GetBaseboardId()
        {
            return GetWmiProperty("Win32_BaseBoard", "SerialNumber");
        }

     
        private string GetDiskId()
        {
            return GetWmiProperty("Win32_DiskDrive", "SerialNumber");
        }

        
        private string GetBiosId()
        {
            return GetWmiProperty("Win32_BIOS", "SerialNumber");
        }

       
        private string GetMacAddress()
        {
            return GetWmiProperty("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled = TRUE");
        }

     
        private string GetWmiProperty(string className, string propertyName, string condition = "")
        {
            try
            {
                string query = $"SELECT {propertyName} FROM {className}";
                if (!string.IsNullOrEmpty(condition))
                {
                    query += $" WHERE {condition}";
                }

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                using (ManagementObjectCollection collection = searcher.Get())
                {
                    foreach (ManagementObject obj in collection)
                    {
                        if (obj[propertyName] != null)
                        {
                            return obj[propertyName].ToString().Trim();
                        }
                    }
                }
            }
            catch
            {
                // 
            }

            return string.Empty;
        }

     
        private string GenerateFallbackId()
        {
            // 
            string uniqueString = Environment.MachineName + Guid.NewGuid().ToString();
            string hash = ComputeHash(uniqueString);
            return FormatDeviceId(hash);
        }
    }
}
