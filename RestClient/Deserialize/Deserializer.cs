***REMOVED***
***REMOVED***
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

***REMOVED***
using RestClient.Resources;

namespace RestClient.Deserialize
***REMOVED***
***REMOVED***
    /// Class for deserializing HAL+JSON format
***REMOVED***
    public class Deserializer
    ***REMOVED***
        private static readonly string ErrorOnDeserialization = "Error while deserializing Json data";

    ***REMOVED***
        /// Deserializes a list of Typed objects from HAL+JSON format.
        /// Type T should have HalJsonResource as base class
    ***REMOVED***
        /// <typeparam name="T">type T</typeparam>
        /// <param name="json">Input string to deserialize</param>
        /// <returns>Typed list of T</returns>
        public static List<T> DeserializeHalJsonResourceList<T>(string json) where T : HalJsonResource
        ***REMOVED***
            List<T> resources;

            try
            ***REMOVED***
                OuterJson outerResource = JsonConvert.DeserializeObject<OuterJson>(json);
                JObject innerObjectJson = outerResource._embedded;

                PluralNameAttribute attribute = (PluralNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(PluralNameAttribute));

                JToken resource = innerObjectJson[attribute.PluralName.ToLower()];
                resources = JsonConvert.DeserializeObject<List<T>>(resource.ToString(), new HalJsonConverter());
    ***REMOVED***
            catch (Exception e)
            ***REMOVED***
                throw new RestClientException(ErrorOnDeserialization, RestClientErrorCodes.RestClientDeserialiationError, e);                
    ***REMOVED***

            return resources;
***REMOVED***

    ***REMOVED***
        /// Deserializes a Typed object from HAL+JSON format
        /// Type T should have HalJsonResource as base class
    ***REMOVED***
        /// <typeparam name="T">type T</typeparam>
        /// <param name="json">Input string in json format</param>
        /// <returns>Instance of type T</returns>
        public static T DeserializeHalJsonResource<T>(string json) where T : HalJsonResource
        ***REMOVED***
            T resource;

            try
            ***REMOVED*** 
                resource = JsonConvert.DeserializeObject<T>(json, new HalJsonConverter());
    ***REMOVED***
            catch (Exception e)
            ***REMOVED***
                throw new RestClientException(ErrorOnDeserialization, RestClientErrorCodes.RestClientDeserialiationError, e);                
    ***REMOVED***

            return resource;
***REMOVED***
***REMOVED***
***REMOVED***
