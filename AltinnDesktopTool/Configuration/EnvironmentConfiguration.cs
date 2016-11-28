using RestClient;

namespace AltinnDesktopTool.Configuration
{
    /// <summary>
    /// Class for storing environment based configuration settings
    /// </summary>
    public class EnvironmentConfiguration : IUiEnvironmentConfig, IRestQueryConfig    
    {
        /// <summary>
        /// Gets or sets the name of the environment that this configuration is connected to.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the theme name for the environment that this configuration is connected to.
        /// </summary>
        public string ThemeName { get; set; }

        /// <summary>
        /// Gets or sets the api key for the environment that this configuration is connected to.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the base address for the REST api for the environment that this configuration is connected to.
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// Gets or sets the certificate thumbprint for the environment that this configuration is connected to.
        /// </summary>
        public string ThumbPrint { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the client should ignore SSL errors.
        /// </summary>
        public bool IgnoreSslErrors { get; set; }

        /// <summary>
        /// Gets or sets the timeout for the REST api call for the environment that this configuration is connected to.
        /// </summary>
        public int Timeout { get; set; }
    }
}
