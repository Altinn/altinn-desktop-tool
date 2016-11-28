using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using RestClient.Deserialize;
using RestClient.DTO;

namespace RestClient.Test
{
    /// <summary>
    /// Test class for unit tests of the <see cref="Deserializer"/> class.
    /// </summary>
    [TestClass]
    public class DeserializerTest
    {
        private const string Orgdata =
@"
{
	""Name"": ""KIRKENES OG AUSTBØ"",
	""OrganizationNumber"": ""910021451"",
	""Type"": ""AS"",
	""LastChanged"": ""2016-10-11T08:36:59.43"",
	""LastConfirmed"": ""2016-08-05T15:38:06.403"",
	""_links"": {
		""self"": {
			""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/910021451""
		},
		""personalcontacts"": {
			""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/910021451/personalcontacts""
		},
		""officialcontacts"": {
			""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/910021451/officialcontacts""
		}
	}
}
";

        private const string Organizations =
@"{
	""_links"": {
		""self"": {
			""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations""
		}
	},
	""_embedded"": {
		""organizations"": [{
			""Name"": ""Ikke i Altinn register"",
			""OrganizationNumber"": ""00210    "",
			""Type"": null,
			""LastChanged"": null,
			""LastConfirmed"": null,
			""_links"": {
				""self"": {
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/00210    ""

                },
				""personalcontacts"": {
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/00210    /personalcontacts""
				},
				""officialcontacts"": {
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/00210    /officialcontacts""
				}
			}
		},
		{
			""Name"": ""SKD TEST DLS 004"",
			""OrganizationNumber"": ""007641060"",
			""Type"": ""KS"",
			""LastChanged"": ""2012-03-08T00:00:00"",
			""LastConfirmed"": null,
			""_links"": {
				""self"": {
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/007641060""
				},
				""personalcontacts"": {
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/007641060/personalcontacts""
				},
				""officialcontacts"": {
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/007641060/officialcontacts""
				}
			}
		},
		{
			""Name"": ""SKD TEST DLS 005"",
			""OrganizationNumber"": ""007978863"",
			""Type"": ""KTRF"",
			""LastChanged"": ""2012-03-08T00:00:00"",
			""LastConfirmed"": null,
			""_links"": {
				""self"": {
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/007978863""
				},
				""personalcontacts"": {
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/007978863/personalcontacts""
				},
				""officialcontacts"": {
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/007978863/officialcontacts""
				}
			}
		}
    ]
    }
    }
";

        private const string PersonalContacts =
@"
{
	""_links"": {
		""self"": {
			""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/910021451/personalcontacts""
		}
	},
	""_embedded"": {
		""personalcontacts"": [{
			""PersonalContactId"": ""r50022994"",
			""Name"": ""ROLF BJØRN               "",
			""SocialSecurityNumber"": ""06117701547"",
			""MobileNumber"": ""47419641"",
			""MobileNumberChanged"": ""2016-10-11T08:15:33.987"",
			""EMailAddress"": ""erlend.oksvoll@brreg.no"",
			""EMailAddressChanged"": ""2016-10-11T08:15:33.987"",
			""_links"": {
				""roles"": {
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/910021451/personalcontacts/r50022994/roles""

                }
			}
		},
		{
			""PersonalContactId"": ""r50041943"",
			""Name"": ""DRAGE TARALD"",
			""SocialSecurityNumber"": ""11106700992"",
			""MobileNumber"": ""98008410"",
			""MobileNumberChanged"": ""2016-06-22T14:17:11.23"",
			""EMailAddress"": ""aen@brreg.no"",
			""EMailAddressChanged"": ""2016-06-22T14:17:11.23"",
			""_links"": {
				""roles"": {
					""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/910021451/personalcontacts/r50041943/roles""
				}
			}
		}]
	}
}
";

        private const string OfficialContacts =
@"
{
	""_links"": {
		""self"": {
			""href"": ""https://tt02.altinn.basefarm.net/api/serviceowner/organizations/910021451/officialcontacts""
		}
	},
	""_embedded"": {
		""officialcontacts"": [{
			""MobileNumber"": ""12121313"",
			""MobileNumberChanged"": ""2016-10-11T08:15:33.987"",
			""EMailAddress"": ""petter@gmail.com"",
			""EMailAddressChanged"": null
        },
        {
			""MobileNumber"": ""12121414"",
			""MobileNumberChanged"": ""2016-03-21T02:30:00"",
			""EMailAddress"": ""pål@gmail.com"",
			""EMailAddressChanged"": ""2016-03-21T02:32:25""
        }]
	}
}
";

        /// <summary>
        /// Scenario: 
        ///   Deserialize a <code>hal+json</code> formatted string into a list of organizations.
        /// Expected Result: 
        ///   A populated list of organizations.
        /// Success Criteria: 
        ///   The list of organizations has the correct number of entries.
        /// </summary>
        [TestMethod]
        public void DeserializeHalJsonResourceListTest_DeserializeOrganizationList()
        {
            // Arrange
            // Act
            List<Organization> result = Deserializer.DeserializeHalJsonResourceList<Organization>(Organizations);

            // Assert
            Assert.AreEqual(3, result.Count);
        }

        /// <summary>
        /// Scenario: 
        ///   Deserialize a <code>hal+json</code> formatted string into an organization.
        /// Expected Result: 
        ///   A new instance of the Organization class populated with data from the JSON.
        /// Success Criteria: 
        ///   The result is not null.
        /// </summary>
        [TestMethod]
        public void DeserializeHalJsonResourceTest_DeserializeOrganization()
        {
            // Arrange
            // Act
            Organization result = Deserializer.DeserializeHalJsonResource<Organization>(Orgdata);

            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Scenario: 
        ///   Deserialize a <code>hal+json</code> formatted string into a list of <see cref="PersonalContact"/> instances.
        /// Expected Result: 
        ///   A populated list of personal contacts.
        /// Success Criteria: 
        ///   The list of personal contacts has the correct number of entries.
        /// </summary>
        [TestMethod]
        public void DeserializeHalJsonResourceListTest_DeserializePersonalContactList()
        {
            // Arrange
            // Act
            List<PersonalContact> result = Deserializer.DeserializeHalJsonResourceList<PersonalContact>(PersonalContacts);

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        /// <summary>
        /// Scenario: 
        ///   Deserialize a <code>hal+json</code> formatted string into a list of <see cref="OfficialContact"/> instances.
        /// Expected Result: 
        ///   A populated list of official contacts.
        /// Success Criteria: 
        ///   The list of official contacts has the correct number of entries.
        /// </summary>
        [TestMethod]
        public void DeserializeHalJsonResourceListTest_DeserializeOfficialContactList()
        {
            // Arrange
            // Act
            List<OfficialContact> result = Deserializer.DeserializeHalJsonResourceList<OfficialContact>(OfficialContacts);

            // Assert
            Assert.AreEqual(2, result.Count);
        }
    }
}
