using System;
using System.Collections.Generic;
using System.Reflection;

using RestClient.DTO;

namespace RestClient
{
    /// <summary>
    /// The RestQueryStub class is an implementation of the IRestQuery interface. The purpose of this class is to act as a stand in
    /// for the actual RestQuery during development of a program using this library.
    /// </summary>
    public class RestQueryStub : IRestQuery
    {
        private static readonly PropertyInfo PropOrgName = typeof(Organization).GetProperty("Name");
        private static readonly PropertyInfo PropOrgLastChanged = typeof(Organization).GetProperty("LastChanged");
        private static readonly PropertyInfo PropOrgType = typeof(Organization).GetProperty("Type");
        private static readonly PropertyInfo PropOrgOfficialContact = typeof(Organization).GetProperty("OfficialContacts");
        private static readonly PropertyInfo PropOrgPersonalContact = typeof(Organization).GetProperty("PersonalContacts");
        private static readonly PropertyInfo PropOrgOrganizationNumber = typeof(Organization).GetProperty("OrganizationNumber");

        private static readonly PropertyInfo PropOffContEmail = typeof(OfficialContact).GetProperty("EmailAddress");
        private static readonly PropertyInfo PropOffContEmailChanged = typeof(OfficialContact).GetProperty("EmailAddressChanged");
        private static readonly PropertyInfo PropOffContPhone = typeof(OfficialContact).GetProperty("MobileNumber");
        private static readonly PropertyInfo PropOffContPhoneChanged = typeof(OfficialContact).GetProperty("MobileNumberChanged");

        private static readonly PropertyInfo PropPersContEmail = typeof(PersonalContact).GetProperty("EmailAddress");
        private static readonly PropertyInfo PropPersContEmailChanged = typeof(PersonalContact).GetProperty("EmailAddressChanged");
        private static readonly PropertyInfo PropPersContPhone = typeof(PersonalContact).GetProperty("MobileNumber");
        private static readonly PropertyInfo PropPersContPhoneChanged = typeof(PersonalContact).GetProperty("MobileNumberChanged");
        private static readonly PropertyInfo PropPersContPersonalContactId = typeof(PersonalContact).GetProperty("PersonalContactId");
        private static readonly PropertyInfo PropPersContName = typeof(PersonalContact).GetProperty("Name");
        private static readonly PropertyInfo PropPersContSocialSecurityNumber = typeof(PersonalContact).GetProperty("SocialSecurityNumber");

        /// <summary>
        /// Fetches a object by a given link (url).
        /// This is useful where a link is returned in a previous call.
        /// </summary>
        /// <typeparam name="T">The type of object to be retrieved.</typeparam>
        /// <param name="id">The id of the object to retrieve</param>
        /// <returns>An object, possibly null if none found</returns>
        public T Get<T>(string id) where T : HalJsonResource
        {
            T org = Activator.CreateInstance<T>();

            switch (id)
            {
                case "070238225":
                    CreateOrg1(org);
                    break;
                case "010007690":
                    CreateOrg2(org);
                    break;
                case "010007763":
                    CreateOrg3(org);
                    break;
                case "010007828":
                    this.CreateOrg4(org);
                    break;
            }

            return org;
        }

        /// <summary>
        /// Search for a list of objects by filtering on a given name value pair.
        /// The possible values name value pairs depends on the controller being called.
        /// The controller is identified by the type T.
        /// </summary>
        /// <typeparam name="T">The type of objects to be retrieved. This also determines the controller to call.</typeparam>
        /// <param name="filter">The name value pair filter</param>
        /// <returns>A list of objects, empty or null if none found</returns>
        public IList<T> Get<T>(KeyValuePair<string, string> filter) where T : HalJsonResource
        {
            T org1 = Activator.CreateInstance<T>();
            T org2 = Activator.CreateInstance<T>();
            T org3 = Activator.CreateInstance<T>();
            T org4 = Activator.CreateInstance<T>();

            CreateOrg1(org1);
            CreateOrg2(org2);
            CreateOrg3(org3);
            this.CreateOrg4(org4);

            return new List<T>()
            {
                org1, org2, org3, org4
            };
        }

        /// <summary>
        /// Fetches a list of objects from the given URL location.
        /// </summary>
        /// <typeparam name="T">The type of data object (DTO) which must be a subclass of <see cref="HalJsonResource"/> to be returned.</typeparam>
        /// <param name="url">The url to send to Altinn.</param>
        /// <returns>The found object or null if not found.</returns>
        /// <remarks>
        /// Controller is identified by the controller having [RestQueryController(SupportedType=T)] defined with a matching T type.
        /// </remarks>
        public IList<T> GetByLink<T>(string url) where T : HalJsonResource
        {
            T contact1 = Activator.CreateInstance<T>();
            T contact2 = Activator.CreateInstance<T>();
            T contact3 = Activator.CreateInstance<T>();

            List<T> list = new List<T>()
            {
                contact1, contact2, contact3
            };

            if (url.Contains("official"))
            {
                this.CreateOffContact1(contact1);
                this.CreateOffContact2(contact2);
                this.CreateOffContact3(contact3);
            }
            else
            {
                this.CreatePersContact1(contact1);
                this.CreatePersContact2(contact2);
                this.CreatePersContact3(contact3);
            }

            return list;
        }

        private static void CreateOrg1(object org)
        {
            PropOrgName.SetValue(org, "SKD TEST DLS 022");
            PropOrgLastChanged.SetValue(org, new DateTime(2012, 03, 08));
            PropOrgType.SetValue(org, "DA");
            PropOrgOfficialContact.SetValue(org, "https://tt02.altinn.basefarm.net/api/serviceowner/organizations/070238225/officialcontacts");
            PropOrgPersonalContact.SetValue(org, "https://tt02.altinn.basefarm.net/api/serviceowner/organizations/070238225/personalcontacts");
            PropOrgOrganizationNumber.SetValue(org, "070238225");
        }

        private static void CreateOrg2(object org)
        {
            PropOrgName.SetValue(org, "SVATSUM OG JAR");
            PropOrgLastChanged.SetValue(org, new DateTime(2010, 01, 01));
            PropOrgType.SetValue(org, "ANS");
            PropOrgOfficialContact.SetValue(org, "https://tt02.altinn.basefarm.net/api/serviceowner/organizations/010007690/officialcontacts");
            PropOrgPersonalContact.SetValue(org, "https://tt02.altinn.basefarm.net/api/serviceowner/organizations/010007690/personalcontacts");
            PropOrgOrganizationNumber.SetValue(org, "010007690");
        }

        private static void CreateOrg3(object org)
        {
            PropOrgName.SetValue(org, "RISDAL OG KVAMSØY");
            PropOrgLastChanged.SetValue(org, new DateTime(2014, 07, 01));
            PropOrgType.SetValue(org, "IS");
            PropOrgOfficialContact.SetValue(org, "https://tt02.altinn.basefarm.net/api/serviceowner/organizations/010007763/officialcontacts");
            PropOrgPersonalContact.SetValue(org, "https://tt02.altinn.basefarm.net/api/serviceowner/organizations/010007763/personalcontacts");
            PropOrgOrganizationNumber.SetValue(org, "010007763");
        }

        private void CreateOrg4(object org)
        {
            PropOrgName.SetValue(org, "SKARTVEIT OG NITTEDAL");
            PropOrgLastChanged.SetValue(org, new DateTime(2015, 12, 20));
            PropOrgType.SetValue(org, "IS");
            PropOrgOfficialContact.SetValue(org, "https://tt02.altinn.basefarm.net/api/serviceowner/organizations/010007828/officialcontacts");
            PropOrgPersonalContact.SetValue(org, "https://tt02.altinn.basefarm.net/api/serviceowner/organizations/010007828/personalcontacts");
            PropOrgOrganizationNumber.SetValue(org, "010007828");
        }

        private void CreateOffContact1(object offcont)
        {
            PropOffContEmail.SetValue(offcont, "petter@gmail.com");
            PropOffContEmailChanged.SetValue(offcont, new DateTime(2009, 06, 06));
            PropOffContPhone.SetValue(offcont, "12121313");
            PropOffContPhoneChanged.SetValue(offcont, new DateTime(2007, 12, 24));
        }

        private void CreateOffContact2(object offcont)
        {
            PropOffContEmail.SetValue(offcont, "pål@gmail.com");
            PropOffContEmailChanged.SetValue(offcont, new DateTime(2014, 01, 18));
            PropOffContPhone.SetValue(offcont, "12121414");
            PropOffContPhoneChanged.SetValue(offcont, new DateTime(2012, 11, 11));
        }

        private void CreateOffContact3(object offcont)
        {
            PropOffContEmail.SetValue(offcont, "espen@gmail.com");
            PropOffContEmailChanged.SetValue(offcont, new DateTime(2016, 10, 04));
            PropOffContPhone.SetValue(offcont, "12121515");
            PropOffContPhoneChanged.SetValue(offcont, new DateTime(2016, 10, 04));
        }

        private void CreatePersContact1(object cont)
        {
            PropPersContEmail.SetValue(cont, "rolf-bjørn@gmail.com");
            PropPersContEmailChanged.SetValue(cont, new DateTime(2009, 06, 06));
            PropPersContPhone.SetValue(cont, "47419641");
            PropPersContPhoneChanged.SetValue(cont, new DateTime(2007, 12, 24));
            PropPersContPersonalContactId.SetValue(cont, "r50022994");
            PropPersContSocialSecurityNumber.SetValue(cont, "06117701547");
            PropPersContName.SetValue(cont, "ROLF BJØRN");
        }

        private void CreatePersContact2(object cont)
        {
            PropPersContEmail.SetValue(cont, "drage@gmail.com");
            PropPersContEmailChanged.SetValue(cont, new DateTime(2015, 02, 14));
            PropPersContPhone.SetValue(cont, "98008410");
            PropPersContPhoneChanged.SetValue(cont, new DateTime(2014, 10, 10));
            PropPersContPersonalContactId.SetValue(cont, "r50041943");
            PropPersContSocialSecurityNumber.SetValue(cont, "11106700992");
            PropPersContName.SetValue(cont, "DRAGE TARALD");
        }

        private void CreatePersContact3(object cont)
        {
            PropPersContEmail.SetValue(cont, "donald-duck@gmail.com");
            PropPersContEmailChanged.SetValue(cont, new DateTime(1966, 01, 01));
            PropPersContPhone.SetValue(cont, "13131313");
            PropPersContPhoneChanged.SetValue(cont, new DateTime(1997, 01, 01));
            PropPersContPersonalContactId.SetValue(cont, "r13042941");
            PropPersContSocialSecurityNumber.SetValue(cont, "06128801558");
            PropPersContName.SetValue(cont, "DONALD DUCK TRUMP");
        }
    }
}
