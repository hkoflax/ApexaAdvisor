using AdvisorManager.Application.Mappings.Resolvers;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Domain;
using AutoMapper;

namespace AdvisorManager.Application.Mappings
{
    public class AdvisorProfile : Profile
    {
        public AdvisorProfile()
        {
            CreateMap<Advisor, AdvisorDto>()
            .ForMember(dest => dest.SIN, opt => opt.ConvertUsing(new FieldMaskResolver(true), src => src.SIN))
            .ForMember(dest => dest.PhoneNumber, opt => opt.ConvertUsing(new FieldMaskResolver(), src => src.PhoneNumber));

            CreateMap<AdvisorDto, Advisor>();

        }
    }
}
