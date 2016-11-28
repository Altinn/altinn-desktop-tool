using System;

namespace RestClient.Resources
{
    /// <summary>
    /// All possible error codes from the REST client.
    /// </summary>
    public static class RestClientErrorCodes
    {
        /// <summary>
        /// The client has wrong or missing configuration values.
        /// </summary>
        public const string RestClientConfigurationError = "RestClientConfigurationError";

        /// <summary>
        /// The remote API returned something the client was unable to deserialize.
        /// </summary>
        public const string RestClientDeserialiationError = "RestClientDeserialiationError";

        /// <summary>
        /// The remote API returned a http status that the REST client is unable to handle.
        /// </summary>
        public const string RestClientUnableToHandleResponse = "RestClientUnableToHandleResponse";

        /// <summary>
        /// The remote API returned http status code 400 Bad Request.
        /// </summary>
        public const string RemoteApiReturnedStatusBadRequest = "RemoteApiReturnedStatusBadRequest";

        /// <summary>
        /// The remote API returned http status code 401 Unauthorized.
        /// </summary>
        public const string RemoteApiReturnedStatusUnauthorized = "RemoteApiReturnedStatusUnauthorized";

        /// <summary>
        /// The remote API returned http status code 403 Forbidden.
        /// </summary>
        public const string RemoteApiReturnedStatusForbidden = "RemoteApiReturnedStatusForbidden";

        /// <summary>
        /// The remote API returned http status code 404 Not Found.
        /// </summary>
        public const string RemoteApiReturnedStatusNotFound = "RemoteApiReturnedStatusNotFound";

        /// <summary>
        /// The remote API returned http status code 500 Internal Server Error.
        /// </summary>
        public const string RemoteApiReturnedStatusInternalServerError = "RemoteApiReturnedStatusInternalServerError";
    }
}
