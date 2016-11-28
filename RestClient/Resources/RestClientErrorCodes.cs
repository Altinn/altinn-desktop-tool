***REMOVED***

namespace RestClient.Resources
***REMOVED***
***REMOVED***
    /// All possible error codes from the REST client.
***REMOVED***
    public static class RestClientErrorCodes
    ***REMOVED***
    ***REMOVED***
        /// The client has wrong or missing configuration values.
    ***REMOVED***
        public const string RestClientConfigurationError = "RestClientConfigurationError";

    ***REMOVED***
        /// The remote API returned something the client was unable to deserialize.
    ***REMOVED***
        public const string RestClientDeserialiationError = "RestClientDeserialiationError";

    ***REMOVED***
        /// The remote API returned a http status that the REST client is unable to handle.
    ***REMOVED***
        public const string RestClientUnableToHandleResponse = "RestClientUnableToHandleResponse";

    ***REMOVED***
        /// The remote API returned http status code 400 Bad Request.
    ***REMOVED***
        public const string RemoteApiReturnedStatusBadRequest = "RemoteApiReturnedStatusBadRequest";

    ***REMOVED***
        /// The remote API returned http status code 401 Unauthorized.
    ***REMOVED***
        public const string RemoteApiReturnedStatusUnauthorized = "RemoteApiReturnedStatusUnauthorized";

    ***REMOVED***
        /// The remote API returned http status code 403 Forbidden.
    ***REMOVED***
        public const string RemoteApiReturnedStatusForbidden = "RemoteApiReturnedStatusForbidden";

    ***REMOVED***
        /// The remote API returned http status code 404 Not Found.
    ***REMOVED***
        public const string RemoteApiReturnedStatusNotFound = "RemoteApiReturnedStatusNotFound";

    ***REMOVED***
        /// The remote API returned http status code 500 Internal Server Error.
    ***REMOVED***
        public const string RemoteApiReturnedStatusInternalServerError = "RemoteApiReturnedStatusInternalServerError";
***REMOVED***
***REMOVED***
