namespace AltinnDesktopTool.Utils.PubSub
{
    /// <summary>
    /// Static class containing the registered events used
    /// </summary>
    public static class EventNames
    {
        /// <summary>
        /// Search event
        /// </summary>
        public static readonly string SearchResultReceivedEvent = "SearchResultReceivedEvent";

        /// <summary>
        /// Environment Changed Event
        /// </summary>
        public static readonly string EnvironmentChangedEvent = "EnvironmentChangedEvent";

        /// <summary>
        /// Search StartedEvent
        /// </summary>
        public static readonly string SearchStartedEvent = "SearchStartedEvent";

        /// <summary>
        /// Organization Selected Changed Event
        /// </summary>
        public static readonly string OrganizationSelectedChangedEvent = "OrganizationSelectedChangedEvent";
        
        /// <summary>
        /// Organization Selected Changed All Event
        /// </summary>
        public static readonly string OrganizationSelectedAllChangedEvent = "OrganizationSelectedAllChangedEvent";
    }
}
