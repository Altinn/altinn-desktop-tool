***REMOVED***

***REMOVED***

using RestClient.Deserialize;
***REMOVED***

namespace RestClient.Test
***REMOVED***
***REMOVED***
    /// Test class for unit tests of the <see cref="Deserializer"/> class.
***REMOVED***
***REMOVED***
    public class DeserializerTest
    ***REMOVED***
        private const string Orgdata =
@"
***REMOVED***
	""Name"": ""KIRKENES OG AUSTBØ"",
	""OrganizationNumber"": ""910021451"",
	""Type"": ""AS"",
	""LastChanged"": ""2016-10-11T08:36:59.43"",
	""LastConfirmed"": ""2016-08-05T15:38:06.403"",
	""_links"": ***REMOVED***
		""self"": ***REMOVED***
			""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/910021451""
		***REMOVED***,
		""personalcontacts"": ***REMOVED***
			""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/910021451/personalcontacts""
		***REMOVED***,
		""officialcontacts"": ***REMOVED***
			""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/910021451/officialcontacts""
		***REMOVED***
	***REMOVED***
***REMOVED***
";

        private const string Organizations =
@"***REMOVED***
	""_links"": ***REMOVED***
		""self"": ***REMOVED***
			""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations""
		***REMOVED***
	***REMOVED***,
	""_embedded"": ***REMOVED***
		""organizations"": [***REMOVED***
			""Name"": ""Ikke i Altinn register"",
			""OrganizationNumber"": ""00210    "",
			""Type"": null,
			""LastChanged"": null,
			""LastConfirmed"": null,
			""_links"": ***REMOVED***
				""self"": ***REMOVED***
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/00210    ""

        ***REMOVED***,
				""personalcontacts"": ***REMOVED***
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/00210    /personalcontacts""
				***REMOVED***,
				""officialcontacts"": ***REMOVED***
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/00210    /officialcontacts""
				***REMOVED***
			***REMOVED***
		***REMOVED***,
		***REMOVED***
			""Name"": ""SKD TEST DLS 004"",
			""OrganizationNumber"": ""007641060"",
			""Type"": ""KS"",
			""LastChanged"": ""2012-03-08T00:00:00"",
			""LastConfirmed"": null,
			""_links"": ***REMOVED***
				""self"": ***REMOVED***
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/007641060""
				***REMOVED***,
				""personalcontacts"": ***REMOVED***
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/007641060/personalcontacts""
				***REMOVED***,
				""officialcontacts"": ***REMOVED***
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/007641060/officialcontacts""
				***REMOVED***
			***REMOVED***
		***REMOVED***,
		***REMOVED***
			""Name"": ""SKD TEST DLS 005"",
			""OrganizationNumber"": ""007978863"",
			""Type"": ""KTRF"",
			""LastChanged"": ""2012-03-08T00:00:00"",
			""LastConfirmed"": null,
			""_links"": ***REMOVED***
				""self"": ***REMOVED***
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/007978863""
				***REMOVED***,
				""personalcontacts"": ***REMOVED***
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/007978863/personalcontacts""
				***REMOVED***,
				""officialcontacts"": ***REMOVED***
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/007978863/officialcontacts""
				***REMOVED***
			***REMOVED***
		***REMOVED***
    ]
***REMOVED***
***REMOVED***
";

        private const string PersonalContacts =
@"
***REMOVED***
	""_links"": ***REMOVED***
		""self"": ***REMOVED***
			""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/910021451/personalcontacts""
		***REMOVED***
	***REMOVED***,
	""_embedded"": ***REMOVED***
		""personalcontacts"": [***REMOVED***
			""PersonalContactId"": ""r50022994"",
			""Name"": ""ROLF BJØRN               "",
			""SocialSecurityNumber"": ""06117701547"",
			""MobileNumber"": ""47419641"",
			""MobileNumberChanged"": ""2016-10-11T08:15:33.987"",
			""EMailAddress"": ""erlend.oksvoll@brreg.no"",
			""EMailAddressChanged"": ""2016-10-11T08:15:33.987"",
			""_links"": ***REMOVED***
				""roles"": ***REMOVED***
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/910021451/personalcontacts/r50022994/roles""

        ***REMOVED***
			***REMOVED***
		***REMOVED***,
		***REMOVED***
			""PersonalContactId"": ""r50041943"",
			""Name"": ""DRAGE TARALD"",
			""SocialSecurityNumber"": ""11106700992"",
			""MobileNumber"": ""98008410"",
			""MobileNumberChanged"": ""2016-06-22T14:17:11.23"",
			""EMailAddress"": ""aen@brreg.no"",
			""EMailAddressChanged"": ""2016-06-22T14:17:11.23"",
			""_links"": ***REMOVED***
				""roles"": ***REMOVED***
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/910021451/personalcontacts/r50041943/roles""
				***REMOVED***
			***REMOVED***
		***REMOVED***]
	***REMOVED***
***REMOVED***
";

        private const string OfficialContacts =
@"
***REMOVED***
	""_links"": ***REMOVED***
		""self"": ***REMOVED***
			""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/910021451/officialcontacts""
		***REMOVED***
	***REMOVED***,
	""_embedded"": ***REMOVED***
		""officialcontacts"": [***REMOVED***
			""MobileNumber"": ""12121313"",
			""MobileNumberChanged"": ""2016-10-11T08:15:33.987"",
			""EMailAddress"": ""petter@gmail.com"",
			""EMailAddressChanged"": null
***REMOVED***,
        ***REMOVED***
			""MobileNumber"": ""12121414"",
			""MobileNumberChanged"": ""2016-03-21T02:30:00"",
			""EMailAddress"": ""pål@gmail.com"",
			""EMailAddressChanged"": ""2016-03-21T02:32:25""
***REMOVED***]
	***REMOVED***
***REMOVED***
";

    ***REMOVED***
***REMOVED***
        ///   Deserialize a <code>hal+json</code> formatted string into a list of organizations.
***REMOVED***
        ///   A populated list of organizations.
***REMOVED***
        ///   The list of organizations has the correct number of entries.
    ***REMOVED***
***REMOVED***
        public void DeserializeHalJsonResourceListTest_DeserializeOrganizationList()
        ***REMOVED***
***REMOVED***
***REMOVED***
            List<Organization> result = Deserializer.DeserializeHalJsonResourceList<Organization>(Organizations);

***REMOVED***
            Assert.AreEqual(3, result.Count);
***REMOVED***

    ***REMOVED***
***REMOVED***
        ///   Deserialize a <code>hal+json</code> formatted string into an organization.
***REMOVED***
        ///   A new instance of the Organization class populated with data from the JSON.
***REMOVED***
        ///   The result is not null.
    ***REMOVED***
***REMOVED***
        public void DeserializeHalJsonResourceTest_DeserializeOrganization()
        ***REMOVED***
***REMOVED***
***REMOVED***
            Organization result = Deserializer.DeserializeHalJsonResource<Organization>(Orgdata);

***REMOVED***
            Assert.IsNotNull(result);
***REMOVED***

    ***REMOVED***
***REMOVED***
        ///   Deserialize a <code>hal+json</code> formatted string into a list of <see cref="PersonalContact"/> instances.
***REMOVED***
        ///   A populated list of personal contacts.
***REMOVED***
        ///   The list of personal contacts has the correct number of entries.
    ***REMOVED***
***REMOVED***
        public void DeserializeHalJsonResourceListTest_DeserializePersonalContactList()
        ***REMOVED***
***REMOVED***
***REMOVED***
            List<PersonalContact> result = Deserializer.DeserializeHalJsonResourceList<PersonalContact>(PersonalContacts);

***REMOVED***
            Assert.AreEqual(2, result.Count);
***REMOVED***

    ***REMOVED***
***REMOVED***
        ///   Deserialize a <code>hal+json</code> formatted string into a list of <see cref="OfficialContact"/> instances.
***REMOVED***
        ///   A populated list of official contacts.
***REMOVED***
        ///   The list of official contacts has the correct number of entries.
    ***REMOVED***
***REMOVED***
        public void DeserializeHalJsonResourceListTest_DeserializeOfficialContactList()
        ***REMOVED***
***REMOVED***
***REMOVED***
            List<OfficialContact> result = Deserializer.DeserializeHalJsonResourceList<OfficialContact>(OfficialContacts);

***REMOVED***
            Assert.AreEqual(2, result.Count);
***REMOVED***
***REMOVED***
***REMOVED***
