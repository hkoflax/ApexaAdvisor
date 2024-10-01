using AdvisorManager.Application.Abstractions;
using AdvisorManager.Application.DTOs;
using AdvisorManager.Application.Requests.Queries;
using AutoMapper;
using MediatR;

namespace AdvisorManager.Application.Handlers
{
    public class GetAdvisorByIdQueryHandler(IAdvisorRepository advisorRepository, IMapper mapper) : IRequestHandler<GetAdvisorByIdQuery, AdvisorDto>
    {
        private readonly IAdvisorRepository _advisorRepository = advisorRepository ?? throw new ArgumentNullException(nameof(advisorRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<AdvisorDto> Handle(GetAdvisorByIdQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var advisor = await _advisorRepository.GetByIdAsync(request.AdvisorId);
            return advisor == null
                ? throw new Exception("Advisor not found")
                : _mapper.Map<AdvisorDto>(advisor);
        }
    }
}