namespace HaikuRaitingSystem.Common
{
    using System.Text;
    using System.Security.Cryptography;

    public static class Encryptor
    {
        public static string GenerateSalt()
        {
            return RandomGenerator.GetRandomStringWithRandomLength();
        }

        public static string GenerateHash(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            SHA256Managed algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(bytes);
            return Encoding.Default.GetString(hash);
        }
    }
}
