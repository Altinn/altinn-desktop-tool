using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

using RestClient.Resources;

namespace RestClient
{
    /// <summary>
    /// Generic Altinn Rest Client.
    /// </summary>
    /// <remarks>
    /// Used internally by RestClient and the Controllers to communicate with Altinn REST Server interface.
    /// Will only request <code>hal+json</code> as response format from the Server.
    /// </remarks>
    public class AltinnRestClient : IDisposable
    {
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

        /// <summary>
        /// Gets or sets the base address of the API being used by this client.
        /// </summary>
        /// <remarks>
        /// When the url is like: <code>https://host/x/y/organizations/orgno</code> and organizations is the name of the controller,
        /// then the base address must be <code>https://host/x/y/</code> including the ending '/'.
        /// The BaseAddress may be changed, in which case AltinnRestClient will reconnect to new host on next call.
        /// </remarks>
        public string BaseAddress
        {
            get
            {
                return this.baseAddress;
            }

            set
            {
                this.baseAddress = value;
                this.InvalidateHttpClient();
            }
        }

        /// <summary>
        /// Gets or sets the ApiKey to be used by the client. 
        /// </summary>
        /// <remarks>
        /// The ApiKey is a mandatory value to have in the request header when using the Altinn API.
        /// The ApiKey may be changed, in which case AltinnRestClient will reconnect to new host on next call.
        /// </remarks>
        public string ApiKey
        {
            get
            {
                return this.apikey;
            }

            set
            {
                this.apikey = value;
                this.InvalidateHttpClient();
            }
        }

        /// <summary>
        /// Gets or sets the timeout for a request in seconds.
        /// </summary>
        /// <remarks>
        ///  Not mandatory. Timeout may be changed, in which case AltinnRestClient will reconnect to new host on next call.
        /// </remarks>
        public int Timeout
        {
            get
            {
                return this.timeout;
            }

            set
            {
                this.timeout = value;
                this.InvalidateHttpClient();
            }
        }

        /// <summary>
        /// Gets or sets the thumbprint of the certificate required to authenticate as service owner.
        /// </summary>
        /// <remarks>
        /// The Certificate with this Thumbprint must be installed on the client computer in current user certificate store.
        /// Thumbprint may be changed, in which case AltinnRestClient will reconnect to new host on next call.
        /// </remarks>
        public string Thumbprint
        {
            get
            {
                return this.thumbprint;
            }

            set
            {
                this.thumbprint = value;
                this.InvalidateHttpClient();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the client should ignore SSL errors.
        /// </summary>
        /// <remarks>
        /// This is needed in environments where the SSL certificates has expired or there is a problem with the trust chain.
        /// </remarks>
        public bool IgnoreSslErrors
        {
            get
            {
                return this.ignoreSslErrors;
            }

            set
            {
                this.ignoreSslErrors = value;
                this.InvalidateHttpClient();
            }
        }

        #endregion

        #region public and protected methods

        /// <summary>
        /// Performs a Get towards Altinn
        /// </summary>
        /// <param name="uriPart">The uriPart, added to base address if defined to form the full uri. If base address is undefined, this must be the full uri</param>
        /// <returns>hal+Json data string or null if not found</returns>
        /// <remarks>
        /// Exception is raised on communication error or error returned from Altinn server.
        /// </remarks>
        public string Get(string uriPart)
        {
            this.EnsureHttpClient();

            HttpResponseMessage responseMessage = this.httpClient.GetAsync(uriPart, HttpCompletionOption.ResponseContentRead).Result;

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new RestClientException(responseMessage.ReasonPhrase, GetErrorCode(responseMessage.StatusCode));
            }

            return IsJsonResult(responseMessage) ? responseMessage.Content.ReadAsStringAsync().Result : null;
        }

        /// <summary>
        /// Releases all resources used by the AltinnRestClient.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by the AltinnRestClient.
        /// </summary>
        /// <param name="disposing">A value indicating whether the object is being disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.InvalidateHttpClient();
            }
        }

        #endregion

        #region private implementation

        private static bool IsJsonResult(HttpResponseMessage responseMessage)
        {
            string conttype = responseMessage.Content.Headers.ContentType.ToString();
            return conttype.StartsWith(AcceptedType, StringComparison.InvariantCultureIgnoreCase);
        }

        private static string GetErrorCode(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
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
            }
        }

        private void InvalidateHttpClient()
        {
            if (this.httpClient == null)
            {
                return;
            }

            this.httpClient.Dispose();
            this.httpClient = null;
        }

        private void EnsureHttpClient()
        {
            if (this.httpClient != null)
            {
                return;
            }

            this.InitHttpClient();
        }

        private void InitHttpClient()
        {
            if (string.IsNullOrEmpty(this.apikey))
            {
                throw new RestClientException("ApiKey is missing", RestClientErrorCodes.RestClientConfigurationError);
            }

            if (string.IsNullOrEmpty(this.thumbprint))
            {
                throw new RestClientException("Certificate Thumbprint is missing", RestClientErrorCodes.RestClientConfigurationError);
            }

            X509Store store = new X509Store(StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection certificateColl = store.Certificates.Find(X509FindType.FindByThumbprint, this.thumbprint, false);
            if (certificateColl.Count < 0)
            {
                throw new RestClientException("Certificate not found.", RestClientErrorCodes.RestClientConfigurationError);
            }

            X509Certificate2 cert = certificateColl[0];
            bool verify = cert.Verify();

            if (!verify)
            {
                throw new RestClientException("Certificate not valid", RestClientErrorCodes.RestClientConfigurationError);
            }

            WebRequestHandler httpClientHandler = new WebRequestHandler { ClientCertificateOptions = ClientCertificateOption.Manual };

            httpClientHandler.ClientCertificates.Add(cert);

            this.httpClient = new HttpClient(httpClientHandler, true);
            this.httpClient.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
            this.httpClient.DefaultRequestHeaders.Add("Accept", AcceptedType);
            this.httpClient.DefaultRequestHeaders.Add("ApiKey", this.apikey);

            if (this.ignoreSslErrors)
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            }
            else
            {
                ServicePointManager.ServerCertificateValidationCallback = null;
            }

            if (this.timeout > 0)
            {
                this.httpClient.Timeout = new TimeSpan(0, 0, 0, this.timeout);
            }

            if (!string.IsNullOrEmpty(this.baseAddress))
            {
                this.httpClient.BaseAddress = new Uri(this.baseAddress);
            }
        }

        #endregion
    }
}
