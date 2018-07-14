using System.Security.Cryptography;
using WisdomCore.Crypto.Chaos.NaCl;
using Xunit;

namespace WisdomCore.Test
{
    public class ed25519_test
    {
        [Fact]
        public void CreateKeyPairTest()
        {
            RandomNumberGenerator random = RNGCryptoServiceProvider.Create();
            byte[] bPriKey = new byte[32];
            random.GetBytes(bPriKey);

            byte[] bExpPriKey = Ed25519.ExpandedPrivateKeyFromSeed(bPriKey);
            byte[] bPubKey = Ed25519.PublicKeyFromSeed(bPriKey);

            Assert.True(bExpPriKey.Length == 64 && bPubKey.Length == 32);
        }

        [Fact]
        public void SignVerifyTest()
        {
            RandomNumberGenerator random = RNGCryptoServiceProvider.Create();
            byte[] bPriKey = new byte[32];
            random.GetBytes(bPriKey);

            byte[] bExpPriKey = Ed25519.ExpandedPrivateKeyFromSeed(bPriKey);
            byte[] bPubKey = Ed25519.PublicKeyFromSeed(bPriKey);

            byte[] msg = new byte[] { 0x1, 0x2, 0x3, 0x4 };

            byte[] sign = Ed25519.Sign(msg, bExpPriKey);
            Assert.True(Ed25519.Verify(sign, msg, bPubKey));
        }

        [Fact]
        public void EcdhTest()
        {
            RandomNumberGenerator random = RNGCryptoServiceProvider.Create();
            byte[] bAlicePriKey = new byte[32];
            byte[] bBobPriKey = new byte[32];
            random.GetBytes(bAlicePriKey);
            random.GetBytes(bBobPriKey);

            byte[] bAliceExpPriKey = Ed25519.ExpandedPrivateKeyFromSeed(bAlicePriKey);
            byte[] bAlicePubKey = Ed25519.PublicKeyFromSeed(bAlicePriKey);

            byte[] bBobExpPriKey = Ed25519.ExpandedPrivateKeyFromSeed(bBobPriKey);
            byte[] bBobPubKey = Ed25519.PublicKeyFromSeed(bBobPriKey);

            byte[] bAliceShare = Ed25519.KeyExchange(bBobPubKey, bAliceExpPriKey);
            byte[] bBobShare = Ed25519.KeyExchange(bAlicePubKey, bBobExpPriKey);

            Assert.Equal(bAliceShare, bBobShare);
        }
    }
}