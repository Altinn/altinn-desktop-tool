using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using RestClient.DTO;

namespace RestClient.Test
{
    /// <summary>
    /// Test class for unit tests of the RestQueryStub class.
    /// </summary>
    [TestClass]
    public class RestQueryStubTest
    {
        /// <summary>
        /// Scenario: 
        ///   Attempt to retrieve a list of organization where a contact point have a specific email address.
        /// Expected Result: 
        ///   A list of organizations.
        /// Success Criteria: 
        ///   The list of organizations has exactly 4 entries.
        /// </summary>
        [TestMethod]
        public void GetTest_FilterByEmail_ListOfOrganizations()
        {
            // Arrange
            IRestQuery query = new RestQueryStub();

            // Act
            IList<Organization> list = query.Get<Organization>(new KeyValuePair<string, string>("email", "pål@gmail.com"));

            // Assert
            Assert.IsTrue(list.Count == 4, "Organization stub failed");
        }

        /// <summary>
        /// Scenario: 
        ///   Attempt to retrieve a specific organization based on the organization number.
        /// Expected Result: 
        ///   An organization is returned.
        /// Success Criteria: 
        ///   The organization has the expected organization number.
        /// </summary>
        [TestMethod]
        public void GetTest_SpecificOrganization_Organization()
        {
            // Arrange
            IRestQuery query = new RestQueryStub();

            // Act
            Organization org = query.Get<Organization>("070238225");

            // Assert
            Assert.IsTrue(org != null && org.OrganizationNumber == "070238225", "GetOrganization Stub by Id fails");
        }

        /// <summary>
        /// Scenario: 
        ///   Attempt to retrieve the list of official contacts for a specific organization.
        /// Expected Result: 
        ///   A list of official contacts is returned.
        /// Success Criteria: 
        ///   The list of official contacts has exactly 3 entries.
        /// </summary>
        [TestMethod]
        public void GetByLinkTest_OfficialContact_OfficialContactList()
        {
            // Arrange
            IRestQuery query = new RestQueryStub();

            // Act
            IList<OfficialContact> list = query.GetByLink<OfficialContact>("https://tt02.altinn.basefarm.net/api/serviceowner/organizations/070238225/officialcontacts");

            // Assert
            Assert.IsTrue(list.Count == 3, "Official Contact stub failed");
        }

        /// <summary>
        /// Scenario: 
        ///   Attempt to retrieve a list of personal contacts for a specific organization.
        /// Expected Result: 
        ///   A list of personal contacts is returned.
        /// Success Criteria: 
        ///   The list of personal contacts has exactly 3 entries.
        /// </summary>
        [TestMethod]
        public void GetByLinkTest_PersonalContact_PersonalContactList()
        {
            // Arrange
            IRestQuery query = new RestQueryStub();

            // Act
            IList<PersonalContact> list = query.GetByLink<PersonalContact>("https://tt02.altinn.basefarm.net/api/serviceowner/organizations/070238225/personalcontacts");

            // Assert
            Assert.IsTrue(list.Count == 3, "Personal Contact stub failed");
        }
    }
}
