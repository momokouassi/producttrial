using ProductTrial.Services.Interfaces;
using System.Security.Cryptography;

namespace ProductTrial.Services.Services
{
    public class PasswordEncriptionService : IPasswordEncriptionService
    {
        public PasswordEncriptionService()
        {
        }

        public string Encrypt(string password, HashAlgorithmName hashAlgo)
        {
            try
            {
                byte[] salt;
                byte[] buffer2;

                if (password == null)
                {
                    throw new ArgumentNullException(nameof(password));
                }

                using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8, hashAlgo))
                {
                    salt = bytes.Salt;
                    buffer2 = bytes.GetBytes(0x20);
                }

                byte[] dst = new byte[0x31];
                Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
                Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
                return Convert.ToBase64String(dst);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool VerifyHashedPassword(string hashedPassword, string povidedPassword)
        {
            try
            {
                byte[] buffer4;
                if (hashedPassword == null)
                {
                    return false;
                }

                if (povidedPassword == null)
                {
                    throw new ArgumentNullException(nameof(povidedPassword));
                }

                byte[] src = Convert.FromBase64String(hashedPassword);
                if ((src.Length != 0x31) || (src[0] != 0))
                {
                    return false;
                }

                byte[] dst = new byte[0x10];
                Buffer.BlockCopy(src, 1, dst, 0, 0x10);
                byte[] buffer3 = new byte[0x20];
                Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);

                using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(povidedPassword, dst, 0x3e8, HashAlgorithmName.SHA256))
                {
                    buffer4 = bytes.GetBytes(0x20);
                }
                return ByteArraysEqual(buffer3, buffer4);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }
    }
}