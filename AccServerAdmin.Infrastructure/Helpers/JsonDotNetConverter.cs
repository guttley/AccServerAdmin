using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace AccServerAdmin.Infrastructure.Helpers
{
    [ExcludeFromCodeCoverage]
    public class JsonDotNetConverter : IJsonConverter
    {
        /// <inheritdoc/>
        public T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <inheritdoc/>
        public string SerializeObject<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}
