***REMOVED***
***REMOVED***
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
***REMOVED***

namespace RestClient.Deserialize
***REMOVED***
***REMOVED***
    /// Converter class for HAL+Json format
***REMOVED***
    public class HalJsonConverter : JsonConverter
    ***REMOVED***
    ***REMOVED***
        /// Checks if the given <paramref name="objectType"/>
        /// is HalJsonResource
    ***REMOVED***
        /// <param name="objectType">Object type</param>
        /// <returns>True if its an organization else false</returns>
        public static bool IsHalJsonResource(Type objectType)
        ***REMOVED***
            return typeof(HalJsonResource).IsAssignableFrom(objectType);
***REMOVED***

    ***REMOVED***
        /// Writes the given <paramref name="value"/> as JSON
    ***REMOVED***
        /// <param name="writer">The JSON writer</param>
        /// <param name="value">Value to be written</param>
        /// <param name="serializer">The JSON serializer</param>
        /// <exception cref="NotImplementedException">Not implemented</exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        ***REMOVED***
            throw new NotImplementedException();
***REMOVED***

    ***REMOVED***
        /// Reads HalJsonResource from JSON
    ***REMOVED***
        /// <param name="reader">The JSON reader</param>
        /// <param name="objectType">The type (child of HalJsonResource)</param>
        /// <param name="existingValue">The existing value</param>
        /// <param name="serializer">The JSON serializer</param>
        /// <returns>The read object</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        ***REMOVED***
            JToken obj = JToken.ReadFrom(reader);
            object ret = JsonConvert.DeserializeObject(obj.ToString(), objectType, new JsonConverter[] ***REMOVED*** ***REMOVED***);

            //TODO:: deserialize _embedded

            // Deserialize _links
            if (obj["_links"] == null || !obj["_links"].HasValues)
            ***REMOVED***
                return ret;
    ***REMOVED***

            using (IEnumerator<KeyValuePair<string, JToken>> enumeratorEmbedded = ((JObject)obj["_links"]).GetEnumerator())
            ***REMOVED***
                while (enumeratorEmbedded.MoveNext())
                ***REMOVED***
                    string rel = enumeratorEmbedded.Current.Key;

                    foreach (PropertyInfo property in objectType.GetProperties())
                    ***REMOVED***
                        bool attribute = property.Name.Equals(rel, StringComparison.InvariantCultureIgnoreCase);

                        if (attribute)
                        ***REMOVED***
                            property.SetValue(ret, obj["_links"][rel]["href"].ToString());
                ***REMOVED***
            ***REMOVED***
        ***REMOVED***
    ***REMOVED***

            return ret;
***REMOVED***

    ***REMOVED***
        /// Checks if the given <paramref name="objectType"/>
        /// can be converted to HalJsonResource
    ***REMOVED***
        /// <param name="objectType">Type to be checked</param>
        /// <returns>Can be converted to HalJsonResource</returns>
        public override bool CanConvert(Type objectType)
        ***REMOVED***
            return IsHalJsonResource(objectType);
***REMOVED***
***REMOVED***
***REMOVED***
