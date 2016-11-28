namespace RestClient
{
    /// <summary>
    /// The Query configuration as required by the RestQuery component.
    /// </summary>
    public interface IRestQueryConfig
    {
        /// <summary>
        /// Gets or sets the base address for the REST api for the environment that this configuration is connected to.
        /// </summary>
        /// <remarks>
        /// When the url is like: <code>https://host/x/y/organizations/orgno</code> and organizations is the name of the controller,
        /// then the base address must be <code>https://host/x/y/</code> including the ending '/'.
        /// The BaseAddress may be changed, in which case AltinnRestClient will reconnect to new host on next call.
        /// </remarks>
        string BaseAddress { get; set; }

        /// <summary>
        /// Gets or sets the ApiKey to be used by the client. 
        /// </summary>
        string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the thumbprint of the certificate required to authenticate as service owner.
        /// </summary>
        string ThumbPrint { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the client should ignore SSL errors.
        /// </summary>
        bool IgnoreSslErrors { get; set; }

        /// <summary>
        /// Gets or sets the timeout for a request in seconds.
        /// </summary>
        int Timeout { get; set; }
    }
}
