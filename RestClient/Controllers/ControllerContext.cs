using log4net;

namespace RestClient.Controllers
{
    /// <summary>
    /// The required data for the controller.
    /// </summary>
    public class ControllerContext
    {
        /// <summary>
        /// Gets or sets the log4net Log object, may be null.
        /// </summary>
        public ILog Log { get; set; }

        /// <summary>
        /// Gets or sets the Http wrapper for REST calls to Altinn server.
        /// </summary>
        public AltinnRestClient RestClient { get; set; }

        /// <summary>
        /// Gets or sets the base address for this controller including the controller name.
        /// </summary>
        public string ControllerBaseAddress { get; set; }
    }
}
