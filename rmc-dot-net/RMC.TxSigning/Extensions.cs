using RMC.Core;
using RMC.Core.Binary;
using RMC.Core.Hashing;
using RMC.Core.Types;
using RMC.Core.Util;

namespace RMC.TxSigning
{
    internal static class Extensions
    {
        internal static byte[] Bytes(this HashPrefix hp)
        {
            return Bits.GetBytes((uint)hp);
        }
    }
}