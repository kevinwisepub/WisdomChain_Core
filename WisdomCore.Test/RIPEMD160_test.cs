using System.Security.Cryptography;
using WisdomCore.Crypto;
using Xunit;

namespace WisdomCore.Test
{
    public class RIPEMD160_test
    {
        [Fact]
        public void RIPEMD160_Empty()
        {
            HashAlgorithm hash = RIPEMD160.Create();
            byte[] bt=System.Text.Encoding.Default.GetBytes("");
            byte[] bh = hash.ComputeHash(bt);
            byte[] bRight = new byte[]{0x9c, 0x11, 0x85, 0xa5, 0xc5, 0xe9, 0xfc, 0x54, 0x61, 0x28, 0x08, 0x97, 0x7e, 0xe8, 0xf5, 0x48, 0xb2, 0x25, 0x8d, 0x31};
            Assert.Equal(bh, bRight);
        }
    }
}