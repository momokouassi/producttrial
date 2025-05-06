using System.Security.Cryptography;

namespace ProductTrial.Services.Interfaces
{
    public interface IPasswordEncriptionService
    {
        string Encrypt(string motDePasse, HashAlgorithmName algorithmeChiffrage);

        bool VerifyHashedPassword(string hashedPassword, string povidedPassword);
    }
}