using System.Linq;
using Newtonsoft.Json.Linq;
using RMC.Core.Binary;
using RMC.Core.Util;

namespace RMC.Core
{
    public interface ISerializedType
    {
        void ToBytes(IBytesSink sink);
        JToken ToJson();
    }

    public static class StExtensions
    {
        public static string ToHex(this ISerializedType st)
        {
            BytesList list = new BytesList();
            st.ToBytes(list);
            return list.BytesHex();
        }
        public static string ToDebuggedHex(this ISerializedType st)
        {
            BytesList list = new BytesList();
            st.ToBytes(list);
            return list.RawList().Aggregate("", (a, b) => a + ',' + B16.Encode(b));
        }
    }
}