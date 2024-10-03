using AdvisorManager.Application.Mappings.Resolvers;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Domain;
using AutoMapper;

namespace AdvisorManager.Application.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between <see cref="Advisor"/> and <see cref="AdvisorDto"/>.
    /// </summary>
    public class AdvisorProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdvisorProfile"/> class and defines mapping configurations.
        /// </summary>
        public AdvisorProfile()
        {
            CreateMap<Advisor, AdvisorDto>()
                .ForMember(dest => dest.SIN, opt => opt.ConvertUsing(new FieldMaskResolver(true), src => src.SIN))
                .ForMember(dest => dest.PhoneNumber, opt => opt.ConvertUsing(new FieldMaskResolver(), src => src.PhoneNumber));

            CreateMap<AdvisorDto, Advisor>();
        }
    }
}
