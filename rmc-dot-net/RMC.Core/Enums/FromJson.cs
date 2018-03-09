using Newtonsoft.Json.Linq;

namespace RMC.Core.Enums
{
    public delegate ISerializedType FromJson(JToken token);
}
