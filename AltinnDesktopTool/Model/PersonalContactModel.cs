namespace AltinnDesktopTool.Model
{
    /// <summary>
    /// Model for Personal Contact
    /// </summary>
    public class PersonalContactModel : ModelBase
    {
        /// <summary>
        /// Gets or sets the Contact Name (as mapped from DTO)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Social Security Number (as mapped from DTO)
        /// </summary>
        public string SocialSecurityNumber { get; set; }

        /// <summary>
        /// Gets or sets Mobile Number (as mapped from DTO)
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the Email address (as mapped from DTO)
        /// </summary>
        public string EmailAddress { get; set; }
    }
}
