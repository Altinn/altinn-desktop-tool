using AltinnDesktopTool.Model;
using AltinnDesktopTool.Utils.Helpers;
using AltinnDesktopTool.Utils.PubSub;
using AltinnDesktopTool.ViewModel;
using AutoMapper;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestClient;
using RestClient.DTO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace AltinnDesktopToolTest.ViewModel
{
    /// <summary>
    /// Test class for unit tests of the <see cref="SearchRolesAndRightsInformationViewModel"/> class.
    /// </summary>
    [TestClass]
    public class SearchRolesAndRightsInformationViewModelTest
    {
        private static IMapper mapper;

        private ObservableCollection<RoleModel> searchResult;

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
        public void RoleSearchResultReceivedEventHandler(object sender, PubSubEventArgs<ObservableCollection<RoleModel>> args)
        {
            this.searchResult = args.Item;
        }

        #endregion

        /// <summary>
        /// Scenario: 
        ///   Instantiate a new instance of SearchRolesAndRightsInformationViewModel.
        /// Expected Result: 
        ///   A new instance of SearchRolesAndRightsInformationViewModel is created.
        /// Success Criteria: 
        ///   The Model and SearchCommand properties are being populated and the logger is being called.
        /// </summary>
        [TestMethod]
        [TestCategory("ViewModel")]
        public void SearchRolesAndRightsInformationViewModelTest_Instantiation()
        {
            // Arrange
            Mock<ILog> logger = new Mock<ILog>();

            Mock<IRestQuery> query = new Mock<IRestQuery>();

            // Act
            SearchRolesAndRightsInformationViewModel target = new SearchRolesAndRightsInformationViewModel(logger.Object, mapper, query.Object);

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
        public void SearchRolesAndRightsInformationViewModelTest_SendsEventWhenSearchResultIsReceived()
        {
            // Arrange
            PubSub<ObservableCollection<RoleModel>>.RegisterEvent(EventNames.RoleSearchResultReceivedEvent, this.RoleSearchResultReceivedEventHandler);

            SearchRolesAndRightsInformationModel search = new SearchRolesAndRightsInformationModel
            {
                SubjectSearchText = "16024400143",
                ReporteeSearchText = "910028146"
            };

            SearchRolesAndRightsInformationViewModel target = GetViewModel();

            // Act
            target.SearchCommand.Execute(search);

            // Wait for tasks to complete.
            Thread.Sleep(1000);

            // Asserts
            Assert.IsNotNull(this.searchResult);
        }

        /// <summary>
        /// Scenario: 
        ///   Perform a search after roles with specific subject and reportee.
        /// Expected Result: 
        ///   Search result is updated with a new list of roles.
        /// Success Criteria: 
        ///   The rest query is performed with the parameters subject and reportee and the search result is updated with new data.
        /// </summary>
        [TestMethod]
        [TestCategory("ViewModel")]
        public void SearchOrganizationInformationViewModelTest_EMailSearch_SearchResultIsUpdated()
        {
            // Arrange
            PubSub<ObservableCollection<RoleModel>>.RegisterEvent(EventNames.RoleSearchResultReceivedEvent, this.RoleSearchResultReceivedEventHandler);

            Mock<ILog> logger = new Mock<ILog>();

            IList<Role> roles = new List<Role>();
            roles.Add(new Role());

            Mock<IRestQuery> query = new Mock<IRestQuery>();
            var ting = new List<KeyValuePair<string, string>>()
            {

            };
            query.Setup(s => s.Get<Role>(It.Is<List<KeyValuePair<string, string>>>(l => l.Count == 2))).Returns(roles);


            SearchRolesAndRightsInformationModel search = new SearchRolesAndRightsInformationModel
            {
                SubjectSearchText = "16024400143",
                ReporteeSearchText = "910028146"
            };

            SearchRolesAndRightsInformationViewModel target = new SearchRolesAndRightsInformationViewModel(logger.Object, mapper, query.Object);

            // Act
            target.SearchCommand.Execute(search);

            // Wait for tasks to complete.
            Thread.Sleep(1000);

            // Assert
            query.VerifyAll();

            Assert.IsNotNull(this.searchResult);
            Assert.IsNotNull(target.SearchCommand);
            Assert.IsNotNull(target.Model);
        }

        #region Private Methods

        private static SearchRolesAndRightsInformationViewModel GetViewModel()
        {
            Mock<ILog> logger = new Mock<ILog>();

            List<Role> roles = new List<Role>();
            roles.Add(new Role());

            Mock<IRestQuery> query = new Mock<IRestQuery>();
            query.Setup(s => s.Get<Role>(It.Is<List<KeyValuePair<string, string>>>(l => l.Count == 2))).Returns(roles);

            SearchRolesAndRightsInformationViewModel target = new SearchRolesAndRightsInformationViewModel(logger.Object, mapper, query.Object);

            return target;
        }

        #endregion
    }
}
