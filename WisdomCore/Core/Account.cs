using System;
using System.Linq;
using WisdomCore.Crypto;
using WisdomCore.HashLib;

namespace WisdomCore.Core
{
    public class Account
    {
        public static readonly int PublicKeySizeInBytes = 32;
        public static readonly int AccountAddressLen = 34;
        public static readonly char TestNetHeader = 'S';
        public static string Create(byte[] publicKey, NetType netType)
        {
            if (publicKey.Length != PublicKeySizeInBytes)
                throw new ArgumentException(string.Format("Public key size must be {0}", PublicKeySizeInBytes), "publicKey.Length");

            byte[] s1 = HashFactory.Crypto.SHA3.CreateKeccak().ComputeBytes(publicKey).GetBytes();
            byte[] s2 = RIPEMD160.Create().ComputeHash(s1);
            byte[] s3 = new byte[1].Concat(s2).ToArray();
            if(netType == NetType.Test_Net) 
            {
                int asciiCode = new System.Text.ASCIIEncoding().GetBytes(TestNetHeader.ToString())[0];
                s3 = new byte[1] { (byte)asciiCode}.Concat(s2).ToArray();
            }
            byte[] v = HashFactory.Crypto.SHA3.CreateKeccak().ComputeBytes(s3).GetBytes().Take(4).ToArray();
            byte[] s4 = s3.Concat(v).ToArray();
            string addr = Base58.Encode(s4);

            return addr;
        }

        public static bool Verify(string address, out NetType netType)
        {
            bool bOk = true;
            netType = NetType.Public_Net;

            if(address.Length != AccountAddressLen) return false;
            foreach(char c in address)
            {
                if(!Base58.Alphabet.Contains(c)) return false;
            }
            byte[] s4 = Base58.Decode(address);
            byte[] v = s4.Reverse().Take(4).Reverse().ToArray();
            byte[] s3 = s4.Reverse().Skip(4).Reverse().ToArray();
            
            byte[] v1 = HashFactory.Crypto.SHA3.CreateKeccak().ComputeBytes(s3).GetBytes().Take(4).ToArray();

            if(v1[0] != v[0] ||
               v1[1] != v[1] ||
               v1[2] != v[2] ||
               v1[3] != v[3]) 
               return false;

            if(s3[0] == TestNetHeader) netType = NetType.Test_Net;
            else if(s3[0] == 0) netType = NetType.Public_Net;
            else return false;

            return bOk;
        }


    }
}