***REMOVED***
***REMOVED***
using System.Linq;
using System.Xml.Linq;

namespace AltinnDesktopTool.Configuration
***REMOVED***
***REMOVED***
    /// Manages the application environment configuration settings
***REMOVED***
    public class EnvironmentConfigurationManager
    ***REMOVED***
        private const string ConfigPath = "Configuration\\EnvironmentConfigurations.xml";
        private static List<EnvironmentConfiguration> configurationList;

        private static EnvironmentConfiguration activeEnvironmentConfiguration;

    ***REMOVED***
        /// Returns the configuration settings
    ***REMOVED***
        public static List<EnvironmentConfiguration> EnvironmentConfigurations => configurationList ?? (configurationList = LoadEnvironmentConfigurations());

    ***REMOVED***
        /// Gets or sets active environment configuration. Default: prod
    ***REMOVED***
        public static EnvironmentConfiguration ActiveEnvironmentConfiguration
        ***REMOVED***
            get
            ***REMOVED***
                if (activeEnvironmentConfiguration == null)
                ***REMOVED***
                    LoadEnvironmentConfigurations();
        ***REMOVED***

                return activeEnvironmentConfiguration;
    ***REMOVED***

            set
            ***REMOVED***
                activeEnvironmentConfiguration = value;
    ***REMOVED***
***REMOVED***

        private static List<EnvironmentConfiguration> LoadEnvironmentConfigurations()
        ***REMOVED***
            XElement xmlDoc = XElement.Load(ConfigPath);
            IEnumerable<EnvironmentConfiguration> configs = from config in xmlDoc.Descendants("EnvironmentConfiguration")
                                                            select
                                                            new EnvironmentConfiguration
                                                            ***REMOVED***
                                                                Name = config?.Element("name")?.Value,
                                                                ThemeName = config?.Element("themeName")?.Value,
                                                                ApiKey = config?.Element("apiKey")?.Value,
                                                                BaseAddress = config?.Element("baseAddress")?.Value,
                                                                ThumbPrint = config?.Element("thumbprint")?.Value,
                                                                IgnoreSslErrors = ParseBool(config?.Element("ignoreSslErrors")?.Value),
                                                                Timeout = ParseInt(config?.Element("timeout")?.Value)
                                                    ***REMOVED***
            IEnumerable<EnvironmentConfiguration> environmentConfigurations = configs as IList<EnvironmentConfiguration> ?? configs.ToList();
            activeEnvironmentConfiguration = environmentConfigurations.Single(c => c.Name == "PROD");
            return environmentConfigurations.ToList();
***REMOVED***

        private static int ParseInt(string value)
        ***REMOVED***
            int ret;
            return value == null ? 0 : int.TryParse(value, out ret) ? ret : 0;
***REMOVED***

        private static bool ParseBool(string value)
        ***REMOVED***
            bool ret;
            return value != null && (bool.TryParse(value, out ret) && ret);
***REMOVED***
***REMOVED***
***REMOVED***
