using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections;

namespace IQtidorly.Api.Extensions
{
    public static class JsonExtensions
    {
        public static JArray ToJArray(this IEnumerable list)
        {
            return JArray.FromObject(list, new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });
        }
    }
}
