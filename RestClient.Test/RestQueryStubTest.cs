***REMOVED***
***REMOVED***

***REMOVED***

***REMOVED***

namespace RestClient.Test
***REMOVED***
***REMOVED***
    /// Test class for unit tests of the RestQueryStub class.
***REMOVED***
***REMOVED***
    public class RestQueryStubTest
    ***REMOVED***
    ***REMOVED***
***REMOVED***
        ///   Attempt to retrieve a list of organization where a contact point have a specific email address.
***REMOVED***
        ///   A list of organizations.
***REMOVED***
        ///   The list of organizations has exactly 4 entries.
    ***REMOVED***
***REMOVED***
        public void GetTest_FilterByEmail_ListOfOrganizations()
        ***REMOVED***
***REMOVED***
            IRestQuery query = new RestQueryStub();

***REMOVED***
            IList<Organization> list = query.Get<Organization>(new KeyValuePair<string, string>("email", "pål@gmail.com"));

***REMOVED***
            Assert.IsTrue(list.Count == 4, "Organization stub failed");
***REMOVED***

    ***REMOVED***
***REMOVED***
        ///   Attempt to retrieve a specific organization based on the organization number.
***REMOVED***
        ///   An organization is returned.
***REMOVED***
        ///   The organization has the expected organization number.
    ***REMOVED***
***REMOVED***
        public void GetTest_SpecificOrganization_Organization()
        ***REMOVED***
***REMOVED***
            IRestQuery query = new RestQueryStub();

***REMOVED***
            Organization org = query.Get<Organization>("070238225");

***REMOVED***
            Assert.IsTrue(org != null && org.OrganizationNumber == "070238225", "GetOrganization Stub by Id fails");
***REMOVED***

    ***REMOVED***
***REMOVED***
        ///   Attempt to retrieve the list of official contacts for a specific organization.
***REMOVED***
        ///   A list of official contacts is returned.
***REMOVED***
        ///   The list of official contacts has exactly 3 entries.
    ***REMOVED***
***REMOVED***
        public void GetByLinkTest_OfficialContact_OfficialContactList()
        ***REMOVED***
***REMOVED***
            IRestQuery query = new RestQueryStub();

***REMOVED***
            IList<OfficialContact> list = query.GetByLink<OfficialContact>("https://tt02.altinn.basefarm.net/api/serviceowner/organizations/070238225/officialcontacts");

***REMOVED***
            Assert.IsTrue(list.Count == 3, "Official Contact stub failed");
***REMOVED***

    ***REMOVED***
***REMOVED***
        ///   Attempt to retrieve a list of personal contacts for a specific organization.
***REMOVED***
        ///   A list of personal contacts is returned.
***REMOVED***
        ///   The list of personal contacts has exactly 3 entries.
    ***REMOVED***
***REMOVED***
        public void GetByLinkTest_PersonalContact_PersonalContactList()
        ***REMOVED***
***REMOVED***
            IRestQuery query = new RestQueryStub();

***REMOVED***
            IList<PersonalContact> list = query.GetByLink<PersonalContact>("https://tt02.altinn.basefarm.net/api/serviceowner/organizations/070238225/personalcontacts");

***REMOVED***
            Assert.IsTrue(list.Count == 3, "Personal Contact stub failed");
***REMOVED***
***REMOVED***
***REMOVED***
