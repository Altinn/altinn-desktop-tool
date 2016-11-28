using System;
using System.Collections.Generic;

using RestClient.Deserialize;
using RestClient.DTO;

namespace RestClient.Controllers
{
    /// <summary>
    /// Generic Controller, currently supporting organizations. Extendible by adding additional attributes as long as the same URL pattern is used.
    /// </summary>
    [RestQueryController(Name = "organizations", SupportedType = typeof(Organization))]
    public class AltinnBaseController : IRestQueryController
    {
        /// <summary>
        /// Gets or sets the <see cref="ControllerContext"/> for this instance of the controller.
        /// </summary>
        public ControllerContext Context { get; set; }

        /// <summary>
        /// Perform a REST API request based on the pattern: <code>{baseAddress}/{controller}/{id}</code>.
        /// </summary>
        /// <typeparam name="T">The type of object the controller should attempt to deserialize the json response into.</typeparam>
        /// <param name="id">The id of the resource to request.</param>
        /// <returns>An instance of the expected type.</returns>
        public T Get<T>(string id) where T : HalJsonResource
        {
            string url = $"{this.Context.ControllerBaseAddress}/{id}?ForceEIAuthentication";
            string result = this.Context.RestClient.Get(url);
            return result != null ? Deserializer.DeserializeHalJsonResource<T>(result) : null;
        }

        /// <summary>
        /// Perform a REST API request based on the pattern: <code>{baseAddress}/{controller}?{key}={value}</code>.
        /// </summary>
        /// <typeparam name="T">The type of object the controller should attempt to deserialize the json response into.</typeparam>
        /// <param name="filter">The filter values for the search.</param>
        /// <returns>A list of instances of the expected type.</returns>
        public IList<T> Get<T>(KeyValuePair<string, string> filter) where T : HalJsonResource
        {
            string url = $"{this.Context.ControllerBaseAddress}?ForceEIAuthentication&{filter.Key}={filter.Value}";
            string result = this.Context.RestClient.Get(url);
            return result != null ? Deserializer.DeserializeHalJsonResourceList<T>(result) : null;
        }

        /// <summary>
        /// Perform a REST API request with the specific input url.
        /// </summary>
        /// <typeparam name="T">The type of object the controller should attempt to deserialize the json response into.</typeparam>
        /// <param name="url">The specific url to request.</param>
        /// <returns>A list of instances of the expected type.</returns>
        public IList<T> GetByLink<T>(string url) where T : HalJsonResource
        {
            url += url.IndexOf("?", StringComparison.InvariantCulture) > 0 ? "&" : "?";
            url += "ForceEIAuthentication";
            string result = this.Context.RestClient.Get(url);
            return result != null ? Deserializer.DeserializeHalJsonResourceList<T>(result) : null;
        }
    }
}
