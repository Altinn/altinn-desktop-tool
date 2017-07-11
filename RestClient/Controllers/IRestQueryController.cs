namespace RestClient.Controllers
{
    /// <summary>
    /// Interface representing the controller. Inherits the IRestQuery and adds the ControllerContext.
    /// </summary>
    public interface IRestQueryController : IRestQuery
    {
        /// <summary>
        /// Gets or sets the controller context.
        /// </summary>
        ControllerContext Context { get; set; }
    }
}
