using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

using AltinnDesktopTool.Model;
using AltinnDesktopTool.Utils.Helpers;
using AltinnDesktopTool.Utils.PubSub;
using AltinnDesktopTool.View;
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
    /// Test class for unit tests of the <see cref="SearchOrganizationInformationViewModel"/> class.
    /// </summary>
    [TestClass]
    public class SearchOrganizationInformationViewModelTest
    {
        private static IMapper mapper;

        private ObservableCollection<OrganizationModel> searchResult;

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
        /// Perform clean up after every unit test by removing the test result.
        /// </summary>
        [TestCleanup]
        public void TestCleanUp()
        {
            this.searchResult = null;
        }

        #region Event handlers 

        /// <summary>
        /// Event handler used to pick up the result after a search.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="args">The event arguments with the new search result.</param>
        public void SearchResultReceivedEventHandler(object sender, PubSubEventArgs<ObservableCollection<OrganizationModel>> args)
        {
            this.searchResult = args.Item;
        }

        #endregion

        /// <summary>
        /// Scenario: 
        ///   Instantiate a new instance of SearchOrganizationInformationViewModel.
        /// Expected Result: 
        ///   A new instance of SearchOrganizationInformationViewModel is created.
        /// Success Criteria: 
        ///   The Model and SearchCommand properties are being populated and the logger is being called.
        /// </summary>
        [TestMethod]
        [TestCategory("ViewModel")]
        public void SearchOrganizationInformationViewModelTest_Instantiation()
        {
            // Arrange
            Mock<ILog> logger = new Mock<ILog>();

            Mock<IRestQuery> query = new Mock<IRestQuery>();

            // Act
            SearchOrganizationInformationViewModel target = new SearchOrganizationInformationViewModel(logger.Object, mapper, query.Object);

            // Assert
            Assert.IsNotNull(target.Model);
            Assert.IsNotNull(target.SearchCommand);
        }

        /// <summary>
        /// Scenario: 
        ///   Search result is retrieved
        /// Expected Result: 
        ///   An event is published.
        /// Success Criteria: 
        ///   Event is retrieved in this test
        /// </summary>
        [TestMethod]
        [TestCategory("ViewModel")]
        public void SearchOrganizationInformationViewModelTest_SendsEventWhenSearchResultIsReceived()
        {
            // Arrange
            PubSub<ObservableCollection<OrganizationModel>>.RegisterEvent(EventNames.SearchResultReceivedEvent, this.SearchResultReceivedEventHandler);

            SearchOrganizationInformationModel search = new SearchOrganizationInformationModel
            {
                SearchType = SearchType.OrganizationNumber,
                SearchText = "910021451"
            };

            SearchOrganizationInformationViewModel target = GetViewModel();

            // Act
            target.SearchCommand.Execute(search);
            
            // Wait for tasks to complete.
            Thread.Sleep(1000);

            // Asserts
            Assert.IsNotNull(this.searchResult);
        }

        /// <summary>
        /// Scenario: 
        ///   Perform a search after a contact with specific email address.
        /// Expected Result: 
        ///   Search result is updated with a new list of organizations.
        /// Success Criteria: 
        ///   The rest query is performed with the parameter email and the search result is updated with new data.
        /// </summary>
        [TestMethod]
        [TestCategory("ViewModel")]
        public void SearchOrganizationInformationViewModelTest_EMailSearch_SearchResultIsUpdated()
        {
            // Arrange
            PubSub<ObservableCollection<OrganizationModel>>.RegisterEvent(EventNames.SearchResultReceivedEvent, this.SearchResultReceivedEventHandler);

            Mock<ILog> logger = new Mock<ILog>();

            IList<Organization> orgs = new List<Organization>();
            orgs.Add(new Organization());

            Mock<IRestQuery> query = new Mock<IRestQuery>();
            query.Setup(s => s.Get<Organization>(It.Is<KeyValuePair<string, string>>(pair => pair.Key == SearchType.EMail.ToString()))).Returns(orgs);

            SearchOrganizationInformationModel search = new SearchOrganizationInformationModel
            {
                SearchType = SearchType.Smart,
                SearchText = "ola.normann@post.no"
            };

            SearchOrganizationInformationViewModel target = new SearchOrganizationInformationViewModel(logger.Object, mapper, query.Object);

            // Act
            target.SearchCommand.Execute(search);

            // Wait for tasks to complete.
            Thread.Sleep(1000);

            // Assert
            query.VerifyAll();

            Assert.IsNotNull(this.searchResult);
            Assert.IsNotNull(target.SearchCommand);
            Assert.IsNotNull(target.Model);

            Assert.IsTrue(search.LabelText.Contains(Resources.EMail) && search.LabelText.Contains(search.SearchText));
        }

        /// <summary>
        /// Scenario: 
        ///   Perform a search after a contact with specific phone number
        /// Expected Result: 
        ///   Search result is updated with a new list of organizations.
        /// Success Criteria: 
        ///   The rest query is performed with the parameter phone number and the search result is updated with new data.
        /// </summary>
        [TestMethod]
        [TestCategory("ViewModel")]
        public void SearchOrganizationInformationViewModelTest_PhoneNumberSearch_SearchResultIsUpdated()
        {
            // Arrange
            PubSub<ObservableCollection<OrganizationModel>>.RegisterEvent(EventNames.SearchResultReceivedEvent, this.SearchResultReceivedEventHandler);

            Mock<ILog> logger = new Mock<ILog>();

            IList<Organization> orgs = new List<Organization>();
            orgs.Add(new Organization());

            Mock<IRestQuery> query = new Mock<IRestQuery>();
            query.Setup(s => s.Get<Organization>(It.Is<KeyValuePair<string, string>>(pair => pair.Key == SearchType.PhoneNumber.ToString()))).Returns(orgs);

            SearchOrganizationInformationModel search = new SearchOrganizationInformationModel
            {
                SearchType = SearchType.Smart,
                SearchText = "47419641"
            };

            SearchOrganizationInformationViewModel target = new SearchOrganizationInformationViewModel(logger.Object, mapper, query.Object);

            // Act
            target.SearchCommand.Execute(search);

            // Wait for tasks to complete.
            Thread.Sleep(1000);

            // Assert
            query.VerifyAll();

            Assert.IsNotNull(this.searchResult);
            Assert.IsNotNull(target.SearchCommand);
            Assert.IsNotNull(target.Model);

            Assert.IsTrue(search.LabelText.Contains(Resources.PhoneNumber) && search.LabelText.Contains(search.SearchText));
        }

        #region Private Methods

        private static SearchOrganizationInformationViewModel GetViewModel()
        {
            Mock<ILog> logger = new Mock<ILog>();

            Organization org = new Organization();

            Mock<IRestQuery> query = new Mock<IRestQuery>();
            query.Setup(s => s.Get<Organization>(It.IsAny<string>())).Returns(org);

            SearchOrganizationInformationViewModel target = new SearchOrganizationInformationViewModel(logger.Object, mapper, query.Object);

            return target;
        }

        #endregion
    }
}
