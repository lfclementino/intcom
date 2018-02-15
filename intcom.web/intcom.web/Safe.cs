using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace intcom.web
{
    public static class Safe
    {
        static byte[] entropy = System.Text.Encoding.Unicode.GetBytes("Ajjsjhfdauhsdfuyashgdyuahdd");

        public static string EncryptString(string input)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(input);
            byte[] encryptedBytes = ProtectedData.Protect(clearBytes, entropy, System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedBytes);
        }

        

        public static string DecryptString(string input)
        {
            if (!String.IsNullOrEmpty(input))
            {
                byte[] plaintext = ProtectedData.Unprotect(Convert.FromBase64String(input), entropy, DataProtectionScope.CurrentUser);

                return Encoding.ASCII.GetString(plaintext);
            }
            else
            {
                return input;
            }
        }
    }
}