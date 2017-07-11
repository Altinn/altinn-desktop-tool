using System.Linq;
using AltinnDesktopTool.Configuration;
using RestClient;

namespace AltinnDesktopTool.Utils.Helpers
{
    /// <summary>
    /// Helper class for the Proxy Configuration
    /// </summary>
    public class ProxyConfigHelper
    {
        /// <summary>
        /// Gets the RestQueryConfig object from the configuration manager. Returns production configuration by default.
        /// </summary>
        /// <returns>The IRestQueryConfig object</returns>
        public static IRestQueryConfig GetConfig()
        {
            return EnvironmentConfigurationManager.EnvironmentConfigurations.FirstOrDefault(c => c.Name == "PROD");
        }

        /// <summary>
        /// Gets the RestQueryConfig object by name from the configuration manager.
        /// </summary>
        /// <param name="environmentName">Name of the environment</param>
        /// <returns>RestQueryConfig object which matches the environmentName</returns>
        public static IRestQueryConfig GetConfig(string environmentName)
        {
            return EnvironmentConfigurationManager.EnvironmentConfigurations.FirstOrDefault(c => c.Name == environmentName);
        }
    }
}
