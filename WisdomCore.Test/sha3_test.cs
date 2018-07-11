using WisdomCore.HashLib;
using Xunit;

namespace WisdomCore.Test
{
    public class sha3_test
    {
        [Fact]
        public void StringTest()
        {
            IHash hash = HashFactory.Crypto.SHA3.CreateKeccak();
            HashResult hashResult = hash.ComputeString("This is a message!");
            Assert.True(hashResult.GetBytes().Length == 64);
        }
    }
}