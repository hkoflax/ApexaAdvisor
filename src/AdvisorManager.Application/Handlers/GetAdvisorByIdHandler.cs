using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Extensions;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Advisor.Queries;
using AutoMapper;
using MediatR;

namespace AdvisorManager.Application.Handlers
{
    public class GetAdvisorByIdHandler(IAdvisorRepository advisorRepository, IMapper mapper) 
        : IRequestHandler<GetAdvisorById, Response<GetAdvisorById, AdvisorDto>>
        
    {
        private readonly IAdvisorRepository _advisorRepository = advisorRepository ?? throw new ArgumentNullException(nameof(advisorRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<GetAdvisorById, AdvisorDto>> Handle(GetAdvisorById request, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(request);

                var advisor = await _advisorRepository.GetByIdAsync(request.AdvisorId);
                if (advisor != null) return request.Completed(_mapper.Map<AdvisorDto>(advisor));

                return request.Completed<GetAdvisorById, AdvisorDto>(null);
            }
            catch (Exception ex)
            {
                return request.Failed<GetAdvisorById, AdvisorDto>(ex);
            }
        }
    }
}