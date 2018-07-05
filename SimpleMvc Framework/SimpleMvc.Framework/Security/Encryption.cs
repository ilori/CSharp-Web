namespace SimpleMvc.Framework.Security
{
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public static class Encryption
    {
        public static string Encrypt(string toEncrypt)
        {
            SHA256Managed crypt = new SHA256Managed();

            string hash = string.Empty;

            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(toEncrypt));

            return crypto.Aggregate(hash, (current, @byte) => current + @byte.ToString("x2"));
        }
    }
}