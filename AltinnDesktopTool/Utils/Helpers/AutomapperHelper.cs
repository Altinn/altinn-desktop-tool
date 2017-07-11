using AltinnDesktopTool.ViewModel.MapperProfiles;

using AutoMapper;

namespace AltinnDesktopTool.Utils.Helpers
{
    /// <summary>
    /// Helper class for AutoMapper
    /// </summary>
    public class AutoMapperHelper
    {
        /// <summary>
        /// Create the mapper
        /// </summary>
        /// <returns>The IMapper object</returns>
        public static IMapper RunCreateMaps()
        {
            // Add profiles here
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<SearchMapperProfile>();
            });

            return Mapper.Configuration.CreateMapper();
        }
    }
}
