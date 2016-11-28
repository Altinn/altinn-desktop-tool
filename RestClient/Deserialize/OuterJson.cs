using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestClient.Deserialize
{
    /// <summary>
    /// Deserialization wrapper for the outer JSON object which comes with the HAL format
    /// </summary>
    public class OuterJson
    {
        /// <summary>
        ///  Gets or sets the _links container
        /// </summary>
        [JsonProperty(PropertyName = "_links")]
        // ReSharper disable once InconsistentNaming
        public JObject _links { get; set; }

        /// <summary>
        ///  Gets or sets the _embedded container
        /// </summary>
        [JsonProperty(PropertyName = "_embedded")]
        // ReSharper disable once InconsistentNaming
        public JObject _embedded { get; set; }
    }
}
