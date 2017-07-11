using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;

using AltinnDesktopTool.Model;
using AltinnDesktopTool.Utils.Helpers;
using AltinnDesktopTool.ViewModel;

using AutoMapper;

using log4net;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using RestClient;
using RestClient.DTO;

namespace AltinnDesktopToolTest.ViewModel
{
    /// <summary>
    /// Test class for unit tests of the <see cref="SearchResultViewModel"/> class.
    /// </summary>
    [TestClass]
    public class SearchResultViewModelTest
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
        ///   Runs the CopyToClipboardExcelFormatHandler of the searchResultViewModel object.
        /// Expected Result: 
        ///   The text on the clipboard has the correct excel format.
        /// Success Criteria: 
        ///   The text on the clipboard matches the hardcoded "expectedResult" string.
        /// </summary>
        [TestMethod]
        [TestCategory("ViewModel")]
        public void SearchResultViewModelTest_CopyToExcel_TextInExcelFormat()
        {
            // Arrange
            Mock<ILog> logger = new Mock<ILog>();

            Mock<IRestQuery> query = new Mock<IRestQuery>();

            string expectedResult = "LER OG HEREFOSS AS\t910570919\taen@brreg.no\t" + Environment.NewLine +
                "LER OG HEREFOSS AS\t910570919\taina.engen70@gmail.com\t98008410" + Environment.NewLine +
                "LER OG HEREFOSS AS\t910570919\taina.engen@brreg.no\t98008410" + Environment.NewLine + Environment.NewLine;

            SearchResultViewModel searchResultViewModel = new SearchResultViewModel(logger.Object, mapper, query.Object);

            OrganizationModel organizationModel = new OrganizationModel
            {
                Name = "LER OG HEREFOSS",
                Type = "AS",
                IsSelected = true,
                OrganizationNumber = "910570919",
                OfficialContacts = "https://www.altinn.no/api/serviceowner/organizations/910570919/officialcontacts"
            };
            searchResultViewModel.Model.ResultCollection.Add(organizationModel);

            ObservableCollection<OfficialContact> officialContacts = new ObservableCollection<OfficialContact>
            {
                new OfficialContact
                {
                    EmailAddress = "aen@brreg.no"
                },
                new OfficialContact()
                {
                    MobileNumber = "98008410",
                    EmailAddress = "aina.engen70@gmail.com"
                },
                new OfficialContact()
                {
                    MobileNumber = "98008410",
                    EmailAddress = "aina.engen@brreg.no"
                }
            };

            query.Setup(s => s.GetByLink<OfficialContact>(It.Is<string>(url => url == organizationModel.OfficialContacts))).Returns(officialContacts);

            // Act
            searchResultViewModel.CopyToClipboardExcelFormatHandler();

            // Assert
            Assert.AreEqual(expectedResult, Clipboard.GetText());
        }
    }
}
