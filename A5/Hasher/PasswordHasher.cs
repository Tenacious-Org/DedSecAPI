using System.Text;

namespace A5.Hasher
{
    
    ///hashed the password using key
    public static class PasswordHasher
    {
        public static string Key = "#V1M1L1K1J1A5@TENACIOUS#";
        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
        
        //decrypting the encrypted password
        public static string DecryptPassword(string base64EncodedeData)
        {
            if (string.IsNullOrEmpty(base64EncodedeData)) return "";
            var base64EncodeBytes = Convert.FromBase64String(base64EncodedeData);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }
    }
}