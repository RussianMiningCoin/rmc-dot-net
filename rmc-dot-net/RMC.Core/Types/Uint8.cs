using Newtonsoft.Json.Linq;
using RMC.Core.Binary;

namespace RMC.Core.Types
{
    public class Uint8 : Uint<byte>
    {
        public Uint8(byte value) : base(value)
        {
        }

        public override byte[] ToBytes()
        {
            return new [] {Value};
        }

        public static Uint8 FromJson(JToken token)
        {
            return (byte) token;
        }
        public static implicit operator Uint8(byte v)
        {
            return new Uint8(v);
        }

        public static Uint8 FromParser(BinaryParser parser, int? hint=null)
        {
            return parser.ReadOne();
        }
    }
}