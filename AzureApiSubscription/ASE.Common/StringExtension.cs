
namespace ASE.Common
{
    using System;
    using System.Text;
    using System.Security.Cryptography;
    using System.Net.Mail;

    public static class StringExtension 
    {
        public static string GenerateHashWithSalt(this string enteredPassword)
        {
            string enteredSalt = "@%!";
            string sHashWithSalt = enteredPassword + enteredSalt;
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(sHashWithSalt);
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            return Convert.ToBase64String(hash);
        }

        public static bool IsValidMail(this string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
