using System.Collections.Generic;

using AltinnDesktopTool.Configuration;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AltinnDesktopToolTest.Configuration
{
    /// <summary>
    /// Test class for unit tests of the <see cref="EnvironmentConfigurationManager"/> class.
    /// </summary>
    [TestClass]
    public class EnvironmentConfigurationTest
    {
        /// <summary>
        /// Scenario: 
        ///   Access environment configurations.
        /// Expected Result: 
        ///   Configuration settings are loaded from the file and returned.
        /// Success Criteria: 
        ///   The configuration object is not null and contains at least one environment.
        /// </summary>
        [TestMethod]
        public void EnvironmentConfigurationsTest_LoadTest()
        {
            // Arrange
            // Act
            List<EnvironmentConfiguration> configs = EnvironmentConfigurationManager.EnvironmentConfigurations;

            // Assert
            Assert.IsNotNull(configs);
            Assert.IsTrue(configs.Count > 0);
        }
    }
}
