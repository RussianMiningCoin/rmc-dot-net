using RMC.Core.Binary;

namespace RMC.Core.Enums
{
    public delegate ISerializedType FromParser(BinaryParser parser, int? hint = null);
}
