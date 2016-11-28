***REMOVED***
using System.Collections.ObjectModel;
using System.Threading;

using AltinnDesktopTool.Model;
using AltinnDesktopTool.Utils.Helpers;
using AltinnDesktopTool.Utils.PubSub;
using AltinnDesktopTool.View;
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
    /// Test class for unit tests of the <see cref="SearchOrganizationInformationViewModel"/> class.
***REMOVED***
***REMOVED***
    public class SearchOrganizationInformationViewModelTest
    ***REMOVED***
        private static IMapper mapper;

        private ObservableCollection<OrganizationModel> searchResult;

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
        /// Perform clean up after every unit test by removing the test result.
    ***REMOVED***
        [TestCleanup]
        public void TestCleanUp()
        ***REMOVED***
            this.searchResult = null;
***REMOVED***

        #region Event handlers 

    ***REMOVED***
        /// Event handler used to pick up the result after a search.
    ***REMOVED***
        /// <param name="sender">The event sender.</param>
        /// <param name="args">The event arguments with the new search result.</param>
        public void SearchResultReceivedEventHandler(object sender, PubSubEventArgs<ObservableCollection<OrganizationModel>> args)
        ***REMOVED***
            this.searchResult = args.Item;
***REMOVED***

        #endregion

    ***REMOVED***
***REMOVED***
        ///   Instantiate a new instance of SearchOrganizationInformationViewModel.
***REMOVED***
        ///   A new instance of SearchOrganizationInformationViewModel is created.
***REMOVED***
        ///   The Model and SearchCommand properties are being populated and the logger is being called.
    ***REMOVED***
***REMOVED***
        [TestCategory("ViewModel")]
        public void SearchOrganizationInformationViewModelTest_Instantiation()
        ***REMOVED***
***REMOVED***
            Mock<ILog> logger = new Mock<ILog>();

            Mock<IRestQuery> query = new Mock<IRestQuery>();

***REMOVED***
            SearchOrganizationInformationViewModel target = new SearchOrganizationInformationViewModel(logger.Object, mapper, query.Object);

***REMOVED***
            Assert.IsNotNull(target.Model);
            Assert.IsNotNull(target.SearchCommand);
***REMOVED***

    ***REMOVED***
***REMOVED***
        ///   Search result is retrieved
***REMOVED***
        ///   An event is published.
***REMOVED***
        ///   Event is retrieved in this test
    ***REMOVED***
***REMOVED***
        [TestCategory("ViewModel")]
        public void SearchOrganizationInformationViewModelTest_SendsEventWhenSearchResultIsReceived()
        ***REMOVED***
***REMOVED***
            PubSub<ObservableCollection<OrganizationModel>>.RegisterEvent(EventNames.SearchResultReceivedEvent, this.SearchResultReceivedEventHandler);

            SearchOrganizationInformationModel search = new SearchOrganizationInformationModel
            ***REMOVED***
                SearchType = SearchType.OrganizationNumber,
                SearchText = "910021451"
    ***REMOVED***

            SearchOrganizationInformationViewModel target = GetViewModel();

***REMOVED***
            target.SearchCommand.Execute(search);
            
            // Wait for tasks to complete.
            Thread.Sleep(1000);

***REMOVED***s
            Assert.IsNotNull(this.searchResult);
***REMOVED***

    ***REMOVED***
***REMOVED***
        ///   Perform a search after a contact with specific email address.
***REMOVED***
        ///   Search result is updated with a new list of organizations.
***REMOVED***
        ///   The rest query is performed with the parameter email and the search result is updated with new data.
    ***REMOVED***
***REMOVED***
        [TestCategory("ViewModel")]
        public void SearchOrganizationInformationViewModelTest_EMailSearch_SearchResultIsUpdated()
        ***REMOVED***
***REMOVED***
            PubSub<ObservableCollection<OrganizationModel>>.RegisterEvent(EventNames.SearchResultReceivedEvent, this.SearchResultReceivedEventHandler);

            Mock<ILog> logger = new Mock<ILog>();

            IList<Organization> orgs = new List<Organization>();
            orgs.Add(new Organization());

            Mock<IRestQuery> query = new Mock<IRestQuery>();
            query.Setup(s => s.Get<Organization>(It.Is<KeyValuePair<string, string>>(pair => pair.Key == SearchType.EMail.ToString()))).Returns(orgs);

            SearchOrganizationInformationModel search = new SearchOrganizationInformationModel
            ***REMOVED***
                SearchType = SearchType.Smart,
                SearchText = "ola.normann@post.no"
    ***REMOVED***

            SearchOrganizationInformationViewModel target = new SearchOrganizationInformationViewModel(logger.Object, mapper, query.Object);

***REMOVED***
            target.SearchCommand.Execute(search);

            // Wait for tasks to complete.
            Thread.Sleep(1000);

***REMOVED***
            query.VerifyAll();

            Assert.IsNotNull(this.searchResult);
            Assert.IsNotNull(target.SearchCommand);
            Assert.IsNotNull(target.Model);

            Assert.IsTrue(search.LabelText.Contains(Resources.EMail) && search.LabelText.Contains(search.SearchText));
***REMOVED***

    ***REMOVED***
***REMOVED***
        ///   Perform a search after a contact with specific phone number
***REMOVED***
        ///   Search result is updated with a new list of organizations.
***REMOVED***
        ///   The rest query is performed with the parameter phone number and the search result is updated with new data.
    ***REMOVED***
***REMOVED***
        [TestCategory("ViewModel")]
        public void SearchOrganizationInformationViewModelTest_PhoneNumberSearch_SearchResultIsUpdated()
        ***REMOVED***
***REMOVED***
            PubSub<ObservableCollection<OrganizationModel>>.RegisterEvent(EventNames.SearchResultReceivedEvent, this.SearchResultReceivedEventHandler);

            Mock<ILog> logger = new Mock<ILog>();

            IList<Organization> orgs = new List<Organization>();
            orgs.Add(new Organization());

            Mock<IRestQuery> query = new Mock<IRestQuery>();
            query.Setup(s => s.Get<Organization>(It.Is<KeyValuePair<string, string>>(pair => pair.Key == SearchType.PhoneNumber.ToString()))).Returns(orgs);

            SearchOrganizationInformationModel search = new SearchOrganizationInformationModel
            ***REMOVED***
                SearchType = SearchType.Smart,
                SearchText = "47419641"
    ***REMOVED***

            SearchOrganizationInformationViewModel target = new SearchOrganizationInformationViewModel(logger.Object, mapper, query.Object);

***REMOVED***
            target.SearchCommand.Execute(search);

            // Wait for tasks to complete.
            Thread.Sleep(1000);

***REMOVED***
            query.VerifyAll();

            Assert.IsNotNull(this.searchResult);
            Assert.IsNotNull(target.SearchCommand);
            Assert.IsNotNull(target.Model);

            Assert.IsTrue(search.LabelText.Contains(Resources.PhoneNumber) && search.LabelText.Contains(search.SearchText));
***REMOVED***

        #region Private Methods

        private static SearchOrganizationInformationViewModel GetViewModel()
        ***REMOVED***
            Mock<ILog> logger = new Mock<ILog>();

            Organization org = new Organization();

            Mock<IRestQuery> query = new Mock<IRestQuery>();
            query.Setup(s => s.Get<Organization>(It.IsAny<string>())).Returns(org);

            SearchOrganizationInformationViewModel target = new SearchOrganizationInformationViewModel(logger.Object, mapper, query.Object);

            return target;
***REMOVED***

        #endregion
***REMOVED***
***REMOVED***
