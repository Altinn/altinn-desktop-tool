***REMOVED***

using AltinnDesktopTool.Configuration;

***REMOVED***

namespace AltinnDesktopToolTest.Configuration
***REMOVED***
***REMOVED***
    /// Test class for unit tests of the <see cref="EnvironmentConfigurationManager"/> class.
***REMOVED***
***REMOVED***
    public class EnvironmentConfigurationTest
    ***REMOVED***
    ***REMOVED***
***REMOVED***
        ///   Access environment configurations.
***REMOVED***
        ///   Configuration settings are loaded from the file and returned.
***REMOVED***
        ///   The configuration object is not null and contains at least one environment.
    ***REMOVED***
***REMOVED***
        public void EnvironmentConfigurationsTest_LoadTest()
        ***REMOVED***
***REMOVED***
***REMOVED***
            List<EnvironmentConfiguration> configs = EnvironmentConfigurationManager.EnvironmentConfigurations;

***REMOVED***
            Assert.IsNotNull(configs);
            Assert.IsTrue(configs.Count > 0);
***REMOVED***
***REMOVED***
***REMOVED***
