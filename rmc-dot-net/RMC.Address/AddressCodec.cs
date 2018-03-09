using System;

namespace RMC.Address
{
    public class AddressCodec
    {
        public const string Alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

        // ReSharper disable RedundantArgumentNameForLiteralExpression, RedundantArgumentName
        public static B58.Version AccountId = B58.Version.With(versionByte: 0, expectedLength: 20);

        public static B58.Version NodePublic = B58.Version.With(versionByte: 28, expectedLength: 33);
        public static B58.Version NodePrivate = B58.Version.With(versionByte: 32, expectedLength: 32);

        public static B58.Version AccountPublic = B58.Version.With(versionByte: 35, expectedLength: 33);
        public static B58.Version AccountPrivate = B58.Version.With(versionByte: 34, expectedLength: 32);
        public static B58.Version AccountWif = B58.Version.With(versionByte: 128, expectedLength: 33);

        public static B58.Version FamilyGenerator = B58.Version.With(versionByte: 41, expectedLength: 33);

        public static B58.Version K256Seed = B58.Version.With(versionByte: 33, expectedLength: 16);

        public static B58.Versions AnySeed = B58.Versions
                                                .With("secp256k1", K256Seed);
        // ReSharper restore RedundantArgumentNameForLiteralExpression, RedundantArgumentName
        private static readonly B58 B58;
        static AddressCodec()
        {
            B58 = new B58(Alphabet);
        }

        public class DecodedSeed
        {
            public readonly byte[] Bytes;

            public DecodedSeed(byte[] payload)
            {
                Bytes = payload;
            }
        }

        public static DecodedSeed DecodeSeed(string seed)
        {
            var decoded = B58.Decode(seed, AnySeed);
            return new DecodedSeed(decoded.Payload);
        }

        public static string EncodeSeed(byte[] bytes)
        {
            return B58.Encode(bytes, "secp256k1", AnySeed);
        }

        public static string EncodeKey(byte[] bytes)
        {
            var wif = new byte[bytes.Length + 1];
            bytes.CopyTo(wif, 0);
            wif[wif.Length - 1] = 0;
            return B58.Encode(wif, AccountWif);
        }

        public static byte[] DecodeK256Seed(string seed)
        {
            return B58.Decode(seed, K256Seed);
        }

        public static string EncodeK256Seed(byte[] bytes)
        {
            return B58.Encode(bytes, K256Seed);
        }

        public static string EncodeAddress(byte[] bytes)
        {
            return B58.Encode(bytes, AccountId);
        }

        public static byte[] DecodeAddress(string address)
        {
            return B58.Decode(address, AccountId);
        }

        public static bool IsValidAddress(string address)
        {
            return B58.IsValid(address, AccountId);
        }

        public static bool IsValidSeed(string seed)
        {
            return B58.IsValid(seed, K256Seed);
        }
    }
}
