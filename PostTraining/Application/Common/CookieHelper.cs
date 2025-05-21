using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace PostTraining.Application.Common
{
    public class CookieHelper
    {
        public static String Encrypt(String text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] protectedData = MachineKey.Protect(data, "cookie-protection");
            return Convert.ToBase64String(protectedData);
        }

        public static String Decrypt(string encryptedText)
        {
            try
            {
                byte[] protectedData = Convert.FromBase64String(encryptedText);
                byte[] data = MachineKey.Unprotect(protectedData, "cookie-protection");
                return Encoding.UTF8.GetString(data);
            }
            catch
            {
                return null;
            }
        }
    }
}