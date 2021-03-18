using System;

namespace Hole
{
    internal sealed class Crypto
    {
        public static string CryptoXOR(string text, byte key = 42)
        {
            var result = String.Empty;
            foreach (var simbol in text)
            {

                var tmp = (char)(simbol ^ key);
                result += tmp;
                key = (byte)(key ^ 12 + simbol);
            }
            return result;
        }
        public static string DeCryptoXOR(string text, byte key = 42)
        {
            var result = String.Empty;
            foreach (var simbol in text)
            {

                var tmp = (char)(simbol ^ key);
                result += tmp;
                key = (byte)(key ^ 12+tmp);
            }
            return result;
        }
    }
}