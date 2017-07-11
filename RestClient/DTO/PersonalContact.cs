using System;

namespace RestClient.DTO
{
    /// <summary>
    /// Data transfer object representing a personal contact from the service owner API.
    /// </summary>
    [PluralName("PersonalContacts")]
    public class PersonalContact : HalJsonResource
    {
        /// <summary>
        /// Gets or sets a unique id on the personal contact in Altinn.
        /// </summary>
        public string PersonalContactId { get; set; }

        /// <summary>
        /// Gets or sets the name of the contact.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the social security number of the contact.
        /// </summary>
        public string SocialSecurityNumber { get; set; }

        /// <summary>
        /// Gets or sets the mobile number of the personal contact.
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the date for the last time the mobile number was added or changed.
        /// </summary>
        public DateTime? MobileNumberChanged { get; set; }

        /// <summary>
        /// Gets or sets the email address of the personal contact.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the date for the last time the email address was added or changed.
        /// </summary>
        public DateTime? EmailAddressChanged { get; set; }
    }
}
