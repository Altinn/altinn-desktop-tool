***REMOVED***

namespace RestClient.DTO
***REMOVED***
***REMOVED***
    /// Data transfer object representing an official contact from the service owner API.
***REMOVED***
    [PluralName("OfficialContacts")]
    public class OfficialContact : HalJsonResource
    ***REMOVED***
    ***REMOVED***
        /// Gets or sets the mobile number of the official contact.
    ***REMOVED***
        public string MobileNumber ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the date for the last time the mobile number was added or changed.
    ***REMOVED***
        public DateTime? MobileNumberChanged ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the email address of the official contact.
    ***REMOVED***
        public string EmailAddress ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the date for the last time the email address was added or changed.
    ***REMOVED***
        public DateTime? EmailAddressChanged ***REMOVED*** get; set; ***REMOVED***
***REMOVED***
***REMOVED***
