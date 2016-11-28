using log4net;

namespace RestClient.Controllers
***REMOVED***
***REMOVED***
    /// The required data for the controller.
***REMOVED***
    public class ControllerContext
    ***REMOVED***
    ***REMOVED***
        /// Gets or sets the log4net Log object, may be null.
    ***REMOVED***
        public ILog Log ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the Http wrapper for REST calls to Altinn server.
    ***REMOVED***
        public AltinnRestClient RestClient ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the base address for this controller including the controller name.
    ***REMOVED***
        public string ControllerBaseAddress ***REMOVED*** get; set; ***REMOVED***
***REMOVED***
***REMOVED***
