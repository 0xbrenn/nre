using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace KengenApp
{
    public static class License
    {
        private const string SECRET_KEY = "REPLACE_WITH_YOUR_LONG_SECRET_256BIT_OR_MORE";
        private const int GROUP_SIZE = 5;
        private const int DAYS_BIAS = 20000;
        private const int MAX_DAYS = 79999;

        private static readonly DateTime EPOCH =
            new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);


        private const string B32_ALPHABET = "023456789ABCDEFGHJKMNPQRSTUVWXYZ";

        private static readonly Regex KeyPattern =
            new Regex(@"^(?<d>\d{5})-(?<t>[A-Z0-9]{5})(?:-(?<g>[A-Z0-9]{5})){5}$",
                      RegexOptions.Compiled);


        public static string Generate(string deviceId, int days)
        {
            return GenerateAt(deviceId, days, DateTime.UtcNow);
        }


        public static string GenerateAt(string deviceId, int days, DateTime issueDateUtc)
        {
            if (days <= 0 || days > MAX_DAYS)
                throw new ArgumentOutOfRangeException(nameof(days), "days must be 1..79999");

            issueDateUtc = ToUtc(issueDateUtc);

            string normId = NormalizeDeviceId(deviceId);
            string d5 = (days + DAYS_BIAS).ToString("D5");


            int minutes = MinutesSinceEpoch(issueDateUtc);
            string t5 = EncodeIntBase32Fixed(minutes, 5);


            string sig25 = ComputeSignature25(normId, d5, t5);
            string[] sigGroups = Enumerable.Range(0, 5)
                .Select(i => sig25.Substring(i * GROUP_SIZE, GROUP_SIZE))
                .ToArray();

            return string.Join("-", new[] { d5, t5 }.Concat(sigGroups).ToArray());
        }

 

        private static string ComputeSignature25(string normId, string d5, string t5)
        {
            byte[] key = Encoding.UTF8.GetBytes(SECRET_KEY);
            byte[] data = Encoding.UTF8.GetBytes(normId + "|" + d5 + "|" + t5);
            byte[] mac;
            using (var h = new HMACSHA256(key))
            {
                mac = h.ComputeHash(data);
            }

            string b32 = Base32Encode(mac);
            if (b32.Length < 25)
            {
                using (var sha = SHA256.Create())
                    b32 += Base32Encode(sha.ComputeHash(mac));
            }
            return new string(b32.Take(25).ToArray());
        }

        private static int MinutesSinceEpoch(DateTime utc)
        {
            utc = ToUtc(utc);
            TimeSpan ts = utc - EPOCH;
            long mins = (long)ts.TotalMinutes;
            if (mins < 0) mins = 0;

            long max = 1;
            for (int i = 0; i < 5; i++) max *= 32; // 32^5
            max -= 1;

            if (mins > max) mins = max;
            return (int)mins;
        }

        private static string EncodeIntBase32Fixed(int value, int width)
        {
            char[] buf = new char[width];
            int v = value;
            for (int i = width - 1; i >= 0; i--)
            {
                int idx = v & 31;
                buf[i] = B32_ALPHABET[idx];
                v >>= 5;
            }
            return new string(buf);
        }

        private static bool TryDecodeBase32Fixed(string s, out int value)
        {
            value = 0;
            if (string.IsNullOrEmpty(s)) return false;

            int v = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int idx = B32_ALPHABET.IndexOf(s[i]);
                if (idx < 0) return false;
                v = (v << 5) | idx;
            }
            value = v;
            return true;
        }

        private static string NormalizeDeviceId(string deviceId)
        {
            if (string.IsNullOrWhiteSpace(deviceId)) return string.Empty;
            var sb = new StringBuilder(deviceId.Length);
            string s = deviceId.ToUpperInvariant();
            for (int i = 0; i < s.Length; i++)
            {
                char ch = s[i];
                if ((ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
                    sb.Append(ch);
            }
            return sb.ToString();
        }

        private static string CleanupKey(string key)
        {
            key = key.Trim().ToUpperInvariant();
            key = Regex.Replace(key, @"\s+", "");
            key = key.Replace('I', '1').Replace('L', '1').Replace('O', '0');
            key = Regex.Replace(key, @"[^A-Z0-9]", "-");
            return key;
        }

        private static string Base32Encode(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return string.Empty;

            int outputLen = (int)Math.Ceiling(bytes.Length * 8 / 5.0);
            var sb = new StringBuilder(outputLen);

            int bitBuffer = 0;
            int bitCount = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                bitBuffer = (bitBuffer << 8) | bytes[i];
                bitCount += 8;

                while (bitCount >= 5)
                {
                    int index = (bitBuffer >> (bitCount - 5)) & 0x1F;
                    bitCount -= 5;
                    sb.Append(B32_ALPHABET[index]);
                }
            }

            if (bitCount > 0)
            {
                int index = (bitBuffer << (5 - bitCount)) & 0x1F;
                sb.Append(B32_ALPHABET[index]);
            }

            return sb.ToString();
        }

        private static bool SlowEquals(string a, string b)
        {
            if (a == null || b == null) return false;
            if (a.Length != b.Length) return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }

        private static DateTime ToUtc(DateTime dt)
        {
            if (dt.Kind == DateTimeKind.Unspecified)
                return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
            if (dt.Kind == DateTimeKind.Local)
                return dt.ToUniversalTime();
            return dt;
        }
    }
}
