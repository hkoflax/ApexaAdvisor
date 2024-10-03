using AdvisorManagement.Api.Models;
using AdvisorManager.Application.Models.Advisor;
using AutoMapper;

namespace AdvisorManagement.Api.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between application models and API models related to advisors.
    /// </summary>
    public class AdvisorProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdvisorProfile"/> class and sets up the mappings between various advisor-related models.
        /// </summary>
        public AdvisorProfile()
        {
            CreateMap<AdvisorDto, AdvisorModel>().ReverseMap();
            CreateMap<CreateAdvisorModel, AdvisorDto>();
            CreateMap<UpdateAdvisorModel, AdvisorDto>();
        }
    }

}
