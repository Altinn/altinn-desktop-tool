namespace AltinnDesktopTool.Model
{
    /// <summary>
    /// Indicates the type of input given in the search.
    /// </summary>
    public enum SearchType
    {
        /// <summary>
        /// The search input is undefined. Try to find the correct type automatically.
        /// </summary>
        Smart = 0,

        /// <summary>
        /// The search input is a phone number.
        /// </summary>
        PhoneNumber = 1,

        /// <summary>
        /// The search input is an email address.
        /// </summary>
        EMail = 2,

        /// <summary>
        /// The search input is an organization number.
        /// </summary>
        OrganizationNumber = 3
    }
}