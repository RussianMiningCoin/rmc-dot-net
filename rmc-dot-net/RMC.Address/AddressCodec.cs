using System;
using System.Linq;

namespace RMC.Address
{
    public class AddressCodec
    {
        public const string Alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

        public static B58.Version AccountId = B58.Version.With(versionByte: 0, expectedLength: 20);
        public static B58.Version NodePublic = B58.Version.With(versionByte: 28, expectedLength: 33);
        public static B58.Version NodePrivate = B58.Version.With(versionByte: 32, expectedLength: 32);
        public static B58.Version AccountPublic = B58.Version.With(versionByte: 35, expectedLength: 33);
        public static B58.Version AccountPrivate = B58.Version.With(versionByte: 34, expectedLength: 32);
        public static B58.Version AccountWif = B58.Version.With(versionByte: 128, expectedLength: 33);
        public static B58.Version KeySeed = B58.Version.With(versionByte: 33, expectedLength: 16);

        // ReSharper restore RedundantArgumentNameForLiteralExpression, RedundantArgumentName
        private static readonly B58 B58;
        static AddressCodec()
        {
            B58 = new B58(Alphabet);
        }

        public static string EncodeData(byte[] bytes, B58.Version version)
        {
            return B58.Encode(bytes, version);
        }

        public static byte[] DecodeData(string data, B58.Version version)
        {
            return B58.Decode(data, version);
        }

        public static string EncodeWif(byte[] bytes)
        {
            var wif = new byte[bytes.Length + 1];
            bytes.CopyTo(wif, 0);
            wif[wif.Length - 1] = 0;
            return B58.Encode(wif, AccountWif);
        }

        public static byte[] DecodeWif(string key)
        {
            var wif = B58.Decode(key, AccountWif);
            return wif.Take(32).ToArray();
        }

        public static bool IsValid(string data, B58.Version version)
        {
            return B58.IsValid(data, version);
        }
    }
}
