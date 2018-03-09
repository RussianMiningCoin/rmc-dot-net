using RMC.Core.Binary;
using RMC.Core.Hashing;

namespace RMC.Core.ShaMapTree
{
    public interface IShaMapItem<out T>
    {
        void ToBytes(IBytesSink sink);
        IShaMapItem<T> Copy();
        T Value();
        HashPrefix Prefix();
    }
}
