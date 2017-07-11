using System;

namespace RestClient.DTO
{
    /// <summary>
    /// Data transfer object representing an official contact from the service owner API.
    /// </summary>
    [PluralName("OfficialContacts")]
    public class OfficialContact : HalJsonResource
    {
        /// <summary>
        /// Gets or sets the mobile number of the official contact.
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the date for the last time the mobile number was added or changed.
        /// </summary>
        public DateTime? MobileNumberChanged { get; set; }

        /// <summary>
        /// Gets or sets the email address of the official contact.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the date for the last time the email address was added or changed.
        /// </summary>
        public DateTime? EmailAddressChanged { get; set; }
    }
}
