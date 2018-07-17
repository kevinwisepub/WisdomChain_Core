using System.Security.Cryptography;
using WisdomCore.Core;
using WisdomCore.Crypto.Chaos.NaCl;
using Xunit;

namespace WisdomCore.Test
{
    public class Account_test
    {
        [Fact]
        public void CreatePublicNetAccount()
        {
            RandomNumberGenerator random = RNGCryptoServiceProvider.Create();
            byte[] bPriKey = new byte[32];
            random.GetBytes(bPriKey);

            byte[] bExpPriKey = Ed25519.ExpandedPrivateKeyFromSeed(bPriKey);
            byte[] bPubKey = Ed25519.PublicKeyFromSeed(bPriKey);

            string addr = Account.Create(bPubKey, NetType.Public_Net);
            NetType netType = NetType.Public_Net;
            bool bOk = Account.Verify(addr, out netType);

            Assert.True(bOk && netType == NetType.Public_Net);
        }

        [Fact]
        public void CreateTestNetAccount()
        {
            RandomNumberGenerator random = RNGCryptoServiceProvider.Create();
            byte[] bPriKey = new byte[32];
            random.GetBytes(bPriKey);

            byte[] bExpPriKey = Ed25519.ExpandedPrivateKeyFromSeed(bPriKey);
            byte[] bPubKey = Ed25519.PublicKeyFromSeed(bPriKey);

            string addr = Account.Create(bPubKey, NetType.Test_Net);
            NetType netType = NetType.Public_Net;
            bool bOk = Account.Verify(addr, out netType);

            Assert.True(bOk && netType == NetType.Test_Net);
        }
    }
}