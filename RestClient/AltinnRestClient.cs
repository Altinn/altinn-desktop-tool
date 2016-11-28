***REMOVED***
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

using RestClient.Resources;

namespace RestClient
***REMOVED***
***REMOVED***
    /// Generic Altinn Rest Client.
***REMOVED***
    /// <remarks>
    /// Used internally by RestClient and the Controllers to communicate with Altinn REST Server interface.
    /// Will only request <code>hal+json</code> as response format from the Server.
    /// </remarks>
    public class AltinnRestClient : IDisposable
    ***REMOVED***
        #region private declarations

        private const string AcceptedType = "application/hal+json";

        private HttpClient httpClient;

        private string baseAddress;
        private string apikey;
        private string thumbprint;
        private bool ignoreSslErrors;
        private int timeout;

        #endregion

        #region public properties

    ***REMOVED***
        /// Gets or sets the base address of the API being used by this client.
    ***REMOVED***
        /// <remarks>
        /// When the url is like: <code>https://host/x/y/organizations/orgno</code> and organizations is the name of the controller,
        /// then the base address must be <code>https://host/x/y/</code> including the ending '/'.
        /// The BaseAddress may be changed, in which case AltinnRestClient will reconnect to new host on next call.
        /// </remarks>
        public string BaseAddress
        ***REMOVED***
            get
            ***REMOVED***
                return this.baseAddress;
    ***REMOVED***

            set
            ***REMOVED***
                this.baseAddress = value;
                this.InvalidateHttpClient();
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Gets or sets the ApiKey to be used by the client. 
    ***REMOVED***
        /// <remarks>
        /// The ApiKey is a mandatory value to have in the request header when using the Altinn API.
        /// The ApiKey may be changed, in which case AltinnRestClient will reconnect to new host on next call.
        /// </remarks>
        public string ApiKey
        ***REMOVED***
            get
            ***REMOVED***
                return this.apikey;
    ***REMOVED***

            set
            ***REMOVED***
                this.apikey = value;
                this.InvalidateHttpClient();
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Gets or sets the timeout for a request in seconds.
    ***REMOVED***
        /// <remarks>
        ///  Not mandatory. Timeout may be changed, in which case AltinnRestClient will reconnect to new host on next call.
        /// </remarks>
        public int Timeout
        ***REMOVED***
            get
            ***REMOVED***
                return this.timeout;
    ***REMOVED***

            set
            ***REMOVED***
                this.timeout = value;
                this.InvalidateHttpClient();
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Gets or sets the thumbprint of the certificate required to authenticate as service owner.
    ***REMOVED***
        /// <remarks>
        /// The Certificate with this Thumbprint must be installed on the client computer in current user certificate store.
        /// Thumbprint may be changed, in which case AltinnRestClient will reconnect to new host on next call.
        /// </remarks>
        public string Thumbprint
        ***REMOVED***
            get
            ***REMOVED***
                return this.thumbprint;
    ***REMOVED***

            set
            ***REMOVED***
                this.thumbprint = value;
                this.InvalidateHttpClient();
    ***REMOVED***
***REMOVED***

    ***REMOVED***
***REMOVED***
    ***REMOVED***
        /// <remarks>
        /// This is needed in environments where the SSL certificates has expired or there is a problem with the trust chain.
        /// </remarks>
        public bool IgnoreSslErrors
        ***REMOVED***
            get
            ***REMOVED***
                return this.ignoreSslErrors;
    ***REMOVED***

            set
            ***REMOVED***
                this.ignoreSslErrors = value;
                this.InvalidateHttpClient();
    ***REMOVED***
***REMOVED***

        #endregion

        #region public and protected methods

    ***REMOVED***
        /// Performs a Get towards Altinn
    ***REMOVED***
        /// <param name="uriPart">The uriPart, added to base address if defined to form the full uri. If base address is undefined, this must be the full uri</param>
        /// <returns>hal+Json data string or null if not found</returns>
        /// <remarks>
        /// Exception is raised on communication error or error returned from Altinn server.
        /// </remarks>
        public string Get(string uriPart)
        ***REMOVED***
            this.EnsureHttpClient();

            HttpResponseMessage responseMessage = this.httpClient.GetAsync(uriPart, HttpCompletionOption.ResponseContentRead).Result;

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            ***REMOVED***
                throw new RestClientException(responseMessage.ReasonPhrase, GetErrorCode(responseMessage.StatusCode));
    ***REMOVED***

            return IsJsonResult(responseMessage) ? responseMessage.Content.ReadAsStringAsync().Result : null;
***REMOVED***

    ***REMOVED***
        /// Releases all resources used by the AltinnRestClient.
    ***REMOVED***
        public void Dispose()
        ***REMOVED***
            this.Dispose(true);
            GC.SuppressFinalize(this);
***REMOVED***

    ***REMOVED***
        /// Releases all resources used by the AltinnRestClient.
    ***REMOVED***
        /// <param name="disposing">A value indicating whether the object is being disposed.</param>
        protected virtual void Dispose(bool disposing)
        ***REMOVED***
            if (disposing)
            ***REMOVED***
                this.InvalidateHttpClient();
    ***REMOVED***
***REMOVED***

        #endregion

        #region private implementation

        private static bool IsJsonResult(HttpResponseMessage responseMessage)
        ***REMOVED***
            string conttype = responseMessage.Content.Headers.ContentType.ToString();
            return conttype.StartsWith(AcceptedType, StringComparison.InvariantCultureIgnoreCase);
***REMOVED***

        private static string GetErrorCode(HttpStatusCode statusCode)
        ***REMOVED***
            switch (statusCode)
            ***REMOVED***
                case HttpStatusCode.BadRequest:
                    return RestClientErrorCodes.RemoteApiReturnedStatusBadRequest;
                case HttpStatusCode.Unauthorized:
                    return RestClientErrorCodes.RemoteApiReturnedStatusUnauthorized;
                case HttpStatusCode.Forbidden:
                    return RestClientErrorCodes.RemoteApiReturnedStatusForbidden;
                case HttpStatusCode.NotFound:
                    return RestClientErrorCodes.RemoteApiReturnedStatusNotFound;
                case HttpStatusCode.InternalServerError:
                    return RestClientErrorCodes.RemoteApiReturnedStatusInternalServerError;
                default:
                    return RestClientErrorCodes.RestClientUnableToHandleResponse;
    ***REMOVED***
***REMOVED***

        private void InvalidateHttpClient()
        ***REMOVED***
            if (this.httpClient == null)
            ***REMOVED***
                return;
    ***REMOVED***

            this.httpClient.Dispose();
            this.httpClient = null;
***REMOVED***

        private void EnsureHttpClient()
        ***REMOVED***
            if (this.httpClient != null)
            ***REMOVED***
                return;
    ***REMOVED***

            this.InitHttpClient();
***REMOVED***

        private void InitHttpClient()
        ***REMOVED***
            if (string.IsNullOrEmpty(this.apikey))
            ***REMOVED***
                throw new RestClientException("ApiKey is missing", RestClientErrorCodes.RestClientConfigurationError);
    ***REMOVED***

            if (string.IsNullOrEmpty(this.thumbprint))
            ***REMOVED***
                throw new RestClientException("Certificate Thumbprint is missing", RestClientErrorCodes.RestClientConfigurationError);
    ***REMOVED***

            X509Store store = new X509Store(StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection certificateColl = store.Certificates.Find(X509FindType.FindByThumbprint, this.thumbprint, false);
            if (certificateColl.Count < 0)
            ***REMOVED***
                throw new RestClientException("Certificate not found.", RestClientErrorCodes.RestClientConfigurationError);
    ***REMOVED***

            X509Certificate2 cert = certificateColl[0];
            bool verify = cert.Verify();

            if (!verify)
            ***REMOVED***
                throw new RestClientException("Certificate not valid", RestClientErrorCodes.RestClientConfigurationError);
    ***REMOVED***

            WebRequestHandler httpClientHandler = new WebRequestHandler ***REMOVED*** ClientCertificateOptions = ClientCertificateOption.Manual ***REMOVED***;

            httpClientHandler.ClientCertificates.Add(cert);

            this.httpClient = new HttpClient(httpClientHandler, true);
            this.httpClient.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
            this.httpClient.DefaultRequestHeaders.Add("Accept", AcceptedType);
            this.httpClient.DefaultRequestHeaders.Add("ApiKey", this.apikey);

            if (this.ignoreSslErrors)
            ***REMOVED***
                ServicePointManager.ServerCertificateValidationCallback = delegate ***REMOVED*** return true; ***REMOVED***;
    ***REMOVED***
            else
            ***REMOVED***
                ServicePointManager.ServerCertificateValidationCallback = null;
    ***REMOVED***

            if (this.timeout > 0)
            ***REMOVED***
                this.httpClient.Timeout = new TimeSpan(0, 0, 0, this.timeout);
    ***REMOVED***

            if (!string.IsNullOrEmpty(this.baseAddress))
            ***REMOVED***
                this.httpClient.BaseAddress = new Uri(this.baseAddress);
    ***REMOVED***
***REMOVED***

        #endregion
***REMOVED***
***REMOVED***
