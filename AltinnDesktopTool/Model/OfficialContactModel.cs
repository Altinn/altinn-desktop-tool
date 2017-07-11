namespace AltinnDesktopTool.Model
{
    /// <summary>
    /// Model for Official Contact
    /// </summary>
    public class OfficialContactModel : ModelBase
    {
        /// <summary>
        /// Gets or sets the Mobile number as received from source
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the Email Address as received from source
        /// </summary>
        public string EmailAddress { get; set; }
    }
}
