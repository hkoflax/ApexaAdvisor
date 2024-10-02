using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Domain;
using AutoMapper;

namespace AdvisorManager.Application.Mappings
{
    public class AdvisorProfile : Profile
    {
        public AdvisorProfile()
        {
            CreateMap<Advisor, AdvisorDto > ().ReverseMap();
        }
    }
}
