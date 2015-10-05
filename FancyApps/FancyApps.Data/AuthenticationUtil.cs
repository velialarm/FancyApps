namespace FancyApps.Data
{
    using System;
    using System.Linq;
    using System.Text;

    public static class AuthenticationUtil
    {
        ////private const string encryptionKey = "8eac8f47232814c6d6bcf238a29df2103b5583d1f8303713344101924a07a6e7";

        ////TODO rework logic for strong secutiry....
        
        public static string Encrypt(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            
            string token = Convert.ToBase64String(passwordBytes.ToArray());
            var key = Guid.NewGuid().ToString();
            return token + key;
        }
        
        public static bool isValidPassword(string token, string password)
        {
            var t = token.Substring(0, token.Length - 36);

            var data = Convert.FromBase64String(t);
            var pass = Encoding.UTF8.GetString(data);
            if (password != pass)
            {
                return false;
            }
            return true;
        }
    }
}
