***REMOVED***

namespace RestClient.DTO
***REMOVED***
***REMOVED***
    /// Data transfer object representing a personal contact from the service owner API.
***REMOVED***
    [PluralName("PersonalContacts")]
    public class PersonalContact : HalJsonResource
    ***REMOVED***
    ***REMOVED***
        /// Gets or sets a unique id on the personal contact in Altinn.
    ***REMOVED***
        public string PersonalContactId ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the name of the contact.
    ***REMOVED***
        public string Name ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the social security number of the contact.
    ***REMOVED***
        public string SocialSecurityNumber ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the mobile number of the personal contact.
    ***REMOVED***
        public string MobileNumber ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the date for the last time the mobile number was added or changed.
    ***REMOVED***
        public DateTime? MobileNumberChanged ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the email address of the personal contact.
    ***REMOVED***
        public string EmailAddress ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the date for the last time the email address was added or changed.
    ***REMOVED***
        public DateTime? EmailAddressChanged ***REMOVED*** get; set; ***REMOVED***
***REMOVED***
***REMOVED***
