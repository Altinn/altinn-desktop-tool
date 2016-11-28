using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RestClient.DTO;
using RestClient.Resources;

namespace RestClient.Deserialize
{
    /// <summary>
    /// Class for deserializing HAL+JSON format
    /// </summary>
    public class Deserializer
    {
        private static readonly string ErrorOnDeserialization = "Error while deserializing Json data";

        /// <summary>
        /// Deserializes a list of Typed objects from HAL+JSON format.
        /// Type T should have HalJsonResource as base class
        /// </summary>
        /// <typeparam name="T">type T</typeparam>
        /// <param name="json">Input string to deserialize</param>
        /// <returns>Typed list of T</returns>
        public static List<T> DeserializeHalJsonResourceList<T>(string json) where T : HalJsonResource
        {
            List<T> resources;

            try
            {
                OuterJson outerResource = JsonConvert.DeserializeObject<OuterJson>(json);
                JObject innerObjectJson = outerResource._embedded;

                PluralNameAttribute attribute = (PluralNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(PluralNameAttribute));

                JToken resource = innerObjectJson[attribute.PluralName.ToLower()];
                resources = JsonConvert.DeserializeObject<List<T>>(resource.ToString(), new HalJsonConverter());
            }
            catch (Exception e)
            {
                throw new RestClientException(ErrorOnDeserialization, RestClientErrorCodes.RestClientDeserialiationError, e);                
            }

            return resources;
        }

        /// <summary>
        /// Deserializes a Typed object from HAL+JSON format
        /// Type T should have HalJsonResource as base class
        /// </summary>
        /// <typeparam name="T">type T</typeparam>
        /// <param name="json">Input string in json format</param>
        /// <returns>Instance of type T</returns>
        public static T DeserializeHalJsonResource<T>(string json) where T : HalJsonResource
        {
            T resource;

            try
            { 
                resource = JsonConvert.DeserializeObject<T>(json, new HalJsonConverter());
            }
            catch (Exception e)
            {
                throw new RestClientException(ErrorOnDeserialization, RestClientErrorCodes.RestClientDeserialiationError, e);                
            }

            return resource;
        }
    }
}
