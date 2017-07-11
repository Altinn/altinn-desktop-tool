namespace AltinnDesktopTool.Configuration
{
    /// <summary>
    /// Configuration for User Interface
    /// </summary>
    public interface IUiEnvironmentConfig
    {
        /// <summary>
        /// Gets or sets the Name of environment
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the Name of theme selected for this environment
        /// </summary>
        string ThemeName { get; set; }
    }
}
