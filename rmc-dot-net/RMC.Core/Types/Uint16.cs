using Newtonsoft.Json.Linq;
using RMC.Core.Binary;
using RMC.Core.Util;

namespace RMC.Core.Types
{
    public class Uint16 : Uint<ushort>
    {
        public Uint16(ushort value) : base(value)
        {
        }
        public static Uint16 FromJson(JToken token)
        {
            return (ushort) token;
        }
        public static implicit operator Uint16(ushort v)
        {
            return new Uint16(v);
        }

        public override byte[] ToBytes()
        {
            return Bits.GetBytes(Value);
        }

        public static Uint16 FromParser(BinaryParser parser, int? hint=null)
        {
            return Bits.ToUInt16(parser.Read(2), 0);
        }
    }
}