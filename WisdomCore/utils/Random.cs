using System.Security.Cryptography;

namespace WisdomCore.utils
{
    public class Random
    {
        public static byte[] Create(int len)
        {
            byte[] bRet = new byte[len];

            RandomNumberGenerator random = RNGCryptoServiceProvider.Create();
            random.GetBytes(bRet);

            return bRet;
        }
    }
}