***REMOVED***

***REMOVED***

namespace RestClient
***REMOVED***
***REMOVED***
    /// Interface representing the Client Proxy for accessing the service owner Altinn REST server interface.
***REMOVED***
    public interface IRestQuery
    ***REMOVED***
    ***REMOVED***
        /// Fetches a list of objects from the given URL location.
    ***REMOVED***
        /// <typeparam name="T">The type of data object (DTO) which must be a subclass of <see cref="HalJsonResource"/> to be returned.</typeparam>
        /// <param name="url">The url to send to Altinn.</param>
        /// <returns>The found object or null if not found.</returns>
        /// <remarks>
        /// Controller is identified by the controller having [RestQueryController(SupportedType=T)] defined with a matching T type.
        /// </remarks>
        IList<T> GetByLink<T>(string url) where T : HalJsonResource;

    ***REMOVED***
        /// Search for a list of objects by filtering on a given name value pair.
        /// The possible values name value pairs depends on the controller being called.
        /// The controller is identified by the type T.
    ***REMOVED***
        /// <typeparam name="T">The type of objects to be retrieved. This also determines the controller to call.</typeparam>
        /// <param name="filter">The name value pair filter</param>
        /// <returns>A list of objects, empty or null if none found</returns>
        IList<T> Get<T>(KeyValuePair<string, string> filter) where T : HalJsonResource;

    ***REMOVED***
        /// Fetches a object by a given link (url).
        /// This is useful where a link is returned in a previous call.
    ***REMOVED***
        /// <typeparam name="T">The type of object to be retrieved.</typeparam>
        /// <param name="id">The id of the object to retrieve</param>
        /// <returns>An object, possibly null if none found</returns>
        T Get<T>(string id) where T : HalJsonResource;
***REMOVED***
***REMOVED***
