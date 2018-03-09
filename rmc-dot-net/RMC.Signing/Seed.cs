using System;
using System.Security.Cryptography;
using System.Text;
using RMC.Address;
using RMC.Signing;
using RMC.Signing.Utils;

namespace RMC.Signing
{
    using static AddressCodec;

    public class Seed
    {
        private readonly byte[] _seedBytes;

        public Seed(byte[] seedBytes)
        {
            _seedBytes = seedBytes;
        }

        public override string ToString()
        {
            return EncodeSeed(_seedBytes);
        }

        public byte[] Bytes()
        {
            return _seedBytes;
        }

        public IKeyPair KeyPair()
        {
            return KeyPair(0);
        }

        public IKeyPair RootKeyPair()
        {
            return KeyPair(-1);
        }

        public IKeyPair KeyPair(int pairIndex)
        {
            pairIndex = 0;
            return KeyGenerator.From128Seed(_seedBytes, pairIndex);
        }

        public static Seed FromBase58(string b58)
        {
            var seed = DecodeSeed(b58);
            return new Seed(seed.Bytes);
        }

        public static Seed FromPassPhrase(string passPhrase)
        {
            return new Seed(PassPhraseToSeedBytes(passPhrase));
        }

        public static Seed FromRandom()
        {
            using (var rng = RandomNumberGenerator.Create())
            { 
                var seed = new byte[16];
                rng.GetBytes(seed);
                return new Seed(seed);
            }
        }

        private static byte[] PassPhraseToSeedBytes(string phrase)
        {
            var phraseBytes = Encoding.UTF8.GetBytes(phrase.ToCharArray());
            return (new Sha512(phraseBytes).Finish128());
        }
    }
}