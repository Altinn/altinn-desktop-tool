using System.Linq;
using AltinnDesktopTool.Configuration;
***REMOVED***

namespace AltinnDesktopTool.Utils.Helpers
***REMOVED***
***REMOVED***
    /// Helper class for the Proxy Configuration
***REMOVED***
    public class ProxyConfigHelper
    ***REMOVED***
    ***REMOVED***
        /// Gets the RestQueryConfig object from the configuration manager. Returns production configuration by default.
    ***REMOVED***
        /// <returns>The IRestQueryConfig object</returns>
        public static IRestQueryConfig GetConfig()
        ***REMOVED***
            return EnvironmentConfigurationManager.EnvironmentConfigurations.FirstOrDefault(c => c.Name == "PROD");
***REMOVED***

    ***REMOVED***
        /// Gets the RestQueryConfig object by name from the configuration manager.
    ***REMOVED***
        /// <param name="environmentName">Name of the environment</param>
        /// <returns>RestQueryConfig object which matches the environmentName</returns>
        public static IRestQueryConfig GetConfig(string environmentName)
        ***REMOVED***
            return EnvironmentConfigurationManager.EnvironmentConfigurations.FirstOrDefault(c => c.Name == environmentName);
***REMOVED***
***REMOVED***
***REMOVED***
