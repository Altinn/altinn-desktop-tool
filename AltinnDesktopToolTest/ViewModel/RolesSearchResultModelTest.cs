using AltinnDesktopTool.Model;
using AltinnDesktopTool.Utils.Helpers;
using AltinnDesktopTool.ViewModel;
using AutoMapper;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestClient;
using System;
using System.Windows.Forms;

namespace AltinnDesktopToolTest.ViewModel
{
    /// <summary>
    /// Test class for unit tests of the <see cref="RoleSearchResultViewModel"/> class.
    /// </summary>
    [TestClass]
    public class RolesSearchResultModelTest
    {
        private static IMapper mapper;

        /// <summary>
        /// Gets or sets the test context for the current test.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Initialize the test class. This ensures that <see cref="AutoMapper"/> has been properly configured for all test methods and
        /// that logic performs actual mapping instead of having the mapping mocked.
        /// </summary>
        /// <param name="context">The current <see cref="TestContext"/> for the test class.</param>
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            mapper = AutoMapperHelper.RunCreateMaps();
        }

        /// <summary>
        /// Scenario: 
        ///   Runs the CopyToClipboardExcelFormatHandler of the RolesSearchResultViewModel object.
        /// Expected Result: 
        ///   The text on the clipboard has the correct excel format.
        /// Success Criteria: 
        ///   The text on the clipboard matches the hardcoded "expectedResult" string.
        /// </summary>
        [TestMethod]
        [TestCategory("ViewModel")]
        public void RolesSearchResultModelTest_CopyToExcel_TextInExcelFormat()
        {
            // Arrange
            Mock<ILog> logger = new Mock<ILog>();

            Mock<IRestQuery> query = new Mock<IRestQuery>();

            string expectedResult = "6\tAccounting employee\tAltinn\tAccess to accounting related forms and services" + Environment.NewLine +
                "4\tAccess manager\tAltinn\tAdministration of access" + Environment.NewLine;

            RolesSearchResultViewModel rolesSearchResultViewModel = new RolesSearchResultViewModel(logger.Object, mapper, query.Object);

            RoleModel roleModel1 = new RoleModel
            {
                RoleDefinitionId = "6",
                RoleDescription = "Access to accounting related forms and services",
                RoleName = "Accounting employee",
                RoleType = "Altinn",
                IsSelected = true
            };

            RoleModel roleModel2 = new RoleModel
            {
                RoleDefinitionId = "4",
                RoleDescription = "Administration of access",
                RoleName = "Access manager",
                RoleType = "Altinn",
                IsSelected = true
            };

            rolesSearchResultViewModel.Model.ResultCollection.Add(roleModel1);
            rolesSearchResultViewModel.Model.ResultCollection.Add(roleModel2);

            // Act
            rolesSearchResultViewModel.CopyRolesToClipboardExcelFormatHandler();

            // Assert
            Assert.AreEqual(expectedResult, Clipboard.GetText());
        }

    }
}
