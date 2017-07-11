using System;

namespace RestClient.DTO
{
    /// <summary>
    /// Data transfer object representing an organization from the service owner API.
    /// </summary>
    [PluralName("Organizations")]
    public class Organization : HalJsonResource
    {
        /// <summary>
        /// Gets or sets the name of the organization.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the organization number.
        /// </summary>
        public string OrganizationNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of organization.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the time for the last update to the organization profile.
        /// </summary>
        public DateTime? LastChanged { get; set; }

        /// <summary>
        /// Gets or sets the time for the last profile confirmation.
        /// </summary>
        public DateTime? LastConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the list of official contacts associated with the organization.
        /// </summary>
        public string OfficialContacts { get; set; }

        /// <summary>
        /// Gets or sets the list of personal contacts associated with the organization.
        /// </summary>
        public string PersonalContacts { get; set; }
    }
}
