using AutoMapper;

namespace AltinnDesktopTool.ViewModel.MapperProfiles
{
    /// <summary>
    /// SearchMapperProfile class for configuring profile maps
    /// </summary>
    public class SearchMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchMapperProfile"/> class, configures AutoMapper
        /// </summary>
        public SearchMapperProfile()
        {
            this.CreateMap<RestClient.DTO.Organization, Model.OrganizationModel>();
            this.CreateMap<RestClient.DTO.OfficialContact, Model.OfficialContactModel>();
            this.CreateMap<RestClient.DTO.PersonalContact, Model.PersonalContactModel>();
        }
    }
}
