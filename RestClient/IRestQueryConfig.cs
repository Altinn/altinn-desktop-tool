namespace RestClient
***REMOVED***
***REMOVED***
    /// The Query configuration as required by the RestQuery component.
***REMOVED***
    public interface IRestQueryConfig
    ***REMOVED***
    ***REMOVED***
***REMOVED***
    ***REMOVED***
        /// <remarks>
        /// When the url is like: <code>https://host/x/y/organizations/orgno</code> and organizations is the name of the controller,
        /// then the base address must be <code>https://host/x/y/</code> including the ending '/'.
        /// The BaseAddress may be changed, in which case AltinnRestClient will reconnect to new host on next call.
        /// </remarks>
        string BaseAddress ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the ApiKey to be used by the client. 
    ***REMOVED***
        string ApiKey ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the thumbprint of the certificate required to authenticate as service owner.
    ***REMOVED***
        string ThumbPrint ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
***REMOVED***
    ***REMOVED***
        bool IgnoreSslErrors ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the timeout for a request in seconds.
    ***REMOVED***
        int Timeout ***REMOVED*** get; set; ***REMOVED***
***REMOVED***
***REMOVED***
