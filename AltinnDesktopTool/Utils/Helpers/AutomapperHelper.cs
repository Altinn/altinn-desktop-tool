using AltinnDesktopTool.ViewModel.MapperProfiles;

using AutoMapper;

namespace AltinnDesktopTool.Utils.Helpers
***REMOVED***
***REMOVED***
    /// Helper class for AutoMapper
***REMOVED***
    public class AutoMapperHelper
    ***REMOVED***
    ***REMOVED***
        /// Create the mapper
    ***REMOVED***
        /// <returns>The IMapper object</returns>
        public static IMapper RunCreateMaps()
        ***REMOVED***
            // Add profiles here
            Mapper.Initialize(cfg =>
            ***REMOVED***
                cfg.AddProfile<SearchMapperProfile>();
    ***REMOVED***);

            return Mapper.Configuration.CreateMapper();
***REMOVED***
***REMOVED***
***REMOVED***
