***REMOVED***
using System.Collections.ObjectModel;
using System.Windows.Forms;

using AltinnDesktopTool.Model;
using AltinnDesktopTool.Utils.Helpers;
using AltinnDesktopTool.ViewModel;

using AutoMapper;

using log4net;

***REMOVED***

using Moq;

***REMOVED***
***REMOVED***

namespace AltinnDesktopToolTest.ViewModel
***REMOVED***
***REMOVED***
    /// Test class for unit tests of the <see cref="SearchResultViewModel"/> class.
***REMOVED***
***REMOVED***
    public class SearchResultViewModelTest
    ***REMOVED***
        private static IMapper mapper;

    ***REMOVED***
        /// Gets or sets the test context for the current test.
    ***REMOVED***
        public TestContext TestContext ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Initialize the test class. This ensures that <see cref="AutoMapper"/> has been properly configured for all test methods and
        /// that logic performs actual mapping instead of having the mapping mocked.
    ***REMOVED***
        /// <param name="context">The current <see cref="TestContext"/> for the test class.</param>
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        ***REMOVED***
            mapper = AutoMapperHelper.RunCreateMaps();
***REMOVED***

    ***REMOVED***
***REMOVED***
        ///   Runs the CopyToClipboardExcelFormatHandler of the searchResultViewModel object.
***REMOVED***
        ///   The text on the clipboard has the correct excel format.
***REMOVED***
        ///   The text on the clipboard matches the hardcoded "expectedResult" string.
    ***REMOVED***
***REMOVED***
        [TestCategory("ViewModel")]
        public void SearchResultViewModelTest_CopyToExcel_TextInExcelFormat()
        ***REMOVED***
***REMOVED***
            Mock<ILog> logger = new Mock<ILog>();

            Mock<IRestQuery> query = new Mock<IRestQuery>();

            string expectedResult = "LER OG HEREFOSS AS\t910570919\taen@brreg.no\t" + Environment.NewLine +
                "LER OG HEREFOSS AS\t910570919\taina.engen70@gmail.com\t98008410" + Environment.NewLine +
                "LER OG HEREFOSS AS\t910570919\taina.engen@brreg.no\t98008410" + Environment.NewLine + Environment.NewLine;

            SearchResultViewModel searchResultViewModel = new SearchResultViewModel(logger.Object, mapper, query.Object);

            OrganizationModel organizationModel = new OrganizationModel
            ***REMOVED***
                Name = "LER OG HEREFOSS",
                Type = "AS",
                IsSelected = true,
                OrganizationNumber = "910570919",
                OfficialContacts = "https://www.altinn.no/api/serviceowner/organizations/910570919/officialcontacts"
    ***REMOVED***
            searchResultViewModel.Model.ResultCollection.Add(organizationModel);

            ObservableCollection<OfficialContact> officialContacts = new ObservableCollection<OfficialContact>
            ***REMOVED***
                new OfficialContact
                ***REMOVED***
                    EmailAddress = "aen@brreg.no"
        ***REMOVED***,
                new OfficialContact()
                ***REMOVED***
                    MobileNumber = "98008410",
                    EmailAddress = "aina.engen70@gmail.com"
        ***REMOVED***,
                new OfficialContact()
                ***REMOVED***
                    MobileNumber = "98008410",
                    EmailAddress = "aina.engen@brreg.no"
        ***REMOVED***
    ***REMOVED***

            query.Setup(s => s.GetByLink<OfficialContact>(It.Is<string>(url => url == organizationModel.OfficialContacts))).Returns(officialContacts);

***REMOVED***
            searchResultViewModel.CopyToClipboardExcelFormatHandler();

***REMOVED***
            Assert.AreEqual(expectedResult, Clipboard.GetText());
***REMOVED***
***REMOVED***
***REMOVED***
