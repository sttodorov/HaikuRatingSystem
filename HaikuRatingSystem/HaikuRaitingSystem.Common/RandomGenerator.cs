namespace HaikuRaitingSystem.Common
{
    using System;
    using System.Text;
    public static class RandomGenerator
    {
        private static Random random;

        static RandomGenerator()
        {
            random = new Random();
        }

        public static int GetRanodomNumber(int min = 0, int max = 100)
        {
            return random.Next(min, max);
        }

        public static char GetRanodmChar()
        {
            char generatedChar = (char)(GetRanodomNumber(65, 91));
            if (GetRanodomNumber() % 2 == 0)
            {
                generatedChar = Char.ToLower(generatedChar);
            }
            return generatedChar;
        }

        public static string GetRandomString(int length)
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(GetRanodmChar());
            }
            return stringBuilder.ToString();
        }

        public static string GetRandomStringWithRandomLength(int minLength = 10, int maxLength = 15)
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < GetRanodomNumber(minLength, maxLength); i++)
            {
                stringBuilder.Append(GetRanodmChar());
            }
            return stringBuilder.ToString();
        }
    }
}
