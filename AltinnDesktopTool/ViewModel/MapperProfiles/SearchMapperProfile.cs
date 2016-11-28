using AutoMapper;

namespace AltinnDesktopTool.ViewModel.MapperProfiles
***REMOVED***
***REMOVED***
    /// SearchMapperProfile class for configuring profile maps
***REMOVED***
    public class SearchMapperProfile : Profile
    ***REMOVED***
    ***REMOVED***
        /// Initializes a new instance of the <see cref="SearchMapperProfile"/> class, configures AutoMapper
    ***REMOVED***
        public SearchMapperProfile()
        ***REMOVED***
            this.CreateMap<RestClient.DTO.Organization, Model.OrganizationModel>();
            this.CreateMap<RestClient.DTO.OfficialContact, Model.OfficialContactModel>();
            this.CreateMap<RestClient.DTO.PersonalContact, Model.PersonalContactModel>();
***REMOVED***
***REMOVED***
***REMOVED***
