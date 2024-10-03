using AdvisorManagement.Api.Models;
using AdvisorManager.Application.Models.Advisor;
using AutoMapper;

namespace AdvisorManagement.Api.Mappings
{
    public class AdvisorProfile : Profile
    {
        public AdvisorProfile()
        {
            CreateMap<AdvisorDto, AdvisorModel>().ReverseMap();
            CreateMap<CreateAdvisorModel, AdvisorDto>();
            CreateMap<UpdateAdvisorModel, AdvisorDto>();
        }
    }
}
