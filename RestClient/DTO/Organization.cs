***REMOVED***

namespace RestClient.DTO
***REMOVED***
***REMOVED***
    /// Data transfer object representing an organization from the service owner API.
***REMOVED***
    [PluralName("Organizations")]
    public class Organization : HalJsonResource
    ***REMOVED***
    ***REMOVED***
        /// Gets or sets the name of the organization.
    ***REMOVED***
        public string Name ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the organization number.
    ***REMOVED***
        public string OrganizationNumber ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the type of organization.
    ***REMOVED***
        public string Type ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the time for the last update to the organization profile.
    ***REMOVED***
        public DateTime? LastChanged ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the time for the last profile confirmation.
    ***REMOVED***
        public DateTime? LastConfirmed ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the list of official contacts associated with the organization.
    ***REMOVED***
        public string OfficialContacts ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the list of personal contacts associated with the organization.
    ***REMOVED***
        public string PersonalContacts ***REMOVED*** get; set; ***REMOVED***
***REMOVED***
***REMOVED***
