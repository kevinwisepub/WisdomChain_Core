using System;

namespace WisdomCore.HashLib
{
    public static class HashFactory
    {
        public static class Crypto
        {
            public static class SHA3
            {
                public static IHash CreateKeccak()
                {
                    return new HashLib.Crypto.SHA3.Keccak512();
                }
            }
        }
    }
}
