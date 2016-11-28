***REMOVED***
***REMOVED***

using RestClient.Deserialize;
***REMOVED***

namespace RestClient.Controllers
***REMOVED***
***REMOVED***
    /// Generic Controller, currently supporting organizations. Extendible by adding additional attributes as long as the same URL pattern is used.
***REMOVED***
    [RestQueryController(Name = "organizations", SupportedType = typeof(Organization))]
    public class AltinnBaseController : IRestQueryController
    ***REMOVED***
    ***REMOVED***
        /// Gets or sets the <see cref="ControllerContext"/> for this instance of the controller.
    ***REMOVED***
        public ControllerContext Context ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Perform a REST API request based on the pattern: <code>***REMOVED***baseAddress***REMOVED***/***REMOVED***controller***REMOVED***/***REMOVED***id***REMOVED***</code>.
    ***REMOVED***
        /// <typeparam name="T">The type of object the controller should attempt to deserialize the json response into.</typeparam>
        /// <param name="id">The id of the resource to request.</param>
        /// <returns>An instance of the expected type.</returns>
        public T Get<T>(string id) where T : HalJsonResource
        ***REMOVED***
            string url = $"***REMOVED***this.Context.ControllerBaseAddress***REMOVED***/***REMOVED***id***REMOVED***?ForceEIAuthentication";
            string result = this.Context.RestClient.Get(url);
            return result != null ? Deserializer.DeserializeHalJsonResource<T>(result) : null;
***REMOVED***

    ***REMOVED***
        /// Perform a REST API request based on the pattern: <code>***REMOVED***baseAddress***REMOVED***/***REMOVED***controller***REMOVED***?***REMOVED***key***REMOVED***=***REMOVED***value***REMOVED***</code>.
    ***REMOVED***
        /// <typeparam name="T">The type of object the controller should attempt to deserialize the json response into.</typeparam>
        /// <param name="filter">The filter values for the search.</param>
        /// <returns>A list of instances of the expected type.</returns>
        public IList<T> Get<T>(KeyValuePair<string, string> filter) where T : HalJsonResource
        ***REMOVED***
            string url = $"***REMOVED***this.Context.ControllerBaseAddress***REMOVED***?ForceEIAuthentication&***REMOVED***filter.Key***REMOVED***=***REMOVED***filter.Value***REMOVED***";
            string result = this.Context.RestClient.Get(url);
            return result != null ? Deserializer.DeserializeHalJsonResourceList<T>(result) : null;
***REMOVED***

    ***REMOVED***
        /// Perform a REST API request with the specific input url.
    ***REMOVED***
        /// <typeparam name="T">The type of object the controller should attempt to deserialize the json response into.</typeparam>
        /// <param name="url">The specific url to request.</param>
        /// <returns>A list of instances of the expected type.</returns>
        public IList<T> GetByLink<T>(string url) where T : HalJsonResource
        ***REMOVED***
            url += url.IndexOf("?", StringComparison.InvariantCulture) > 0 ? "&" : "?";
            url += "ForceEIAuthentication";
            string result = this.Context.RestClient.Get(url);
            return result != null ? Deserializer.DeserializeHalJsonResourceList<T>(result) : null;
***REMOVED***
***REMOVED***
***REMOVED***
