using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestClient.Deserialize
***REMOVED***
***REMOVED***
    /// Deserialization wrapper for the outer JSON object which comes with the HAL format
***REMOVED***
    public class OuterJson
    ***REMOVED***
    ***REMOVED***
        ///  Gets or sets the _links container
    ***REMOVED***
        [JsonProperty(PropertyName = "_links")]
        // ReSharper disable once InconsistentNaming
        public JObject _links ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        ///  Gets or sets the _embedded container
    ***REMOVED***
        [JsonProperty(PropertyName = "_embedded")]
        // ReSharper disable once InconsistentNaming
        public JObject _embedded ***REMOVED*** get; set; ***REMOVED***
***REMOVED***
***REMOVED***
