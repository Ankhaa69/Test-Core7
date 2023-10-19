using System.Security.Cryptography;

namespace ItemManagment.Helpers
{
    public static class SecurityUtils
    {
        public static RSA LoadPublicKey(string publicKeyPath)
        {
            var publicKeyText = File.ReadAllText(publicKeyPath);
            var rsa = RSA.Create();
            rsa.ImportFromPem(publicKeyText);
            return rsa;
        }
    }
}
