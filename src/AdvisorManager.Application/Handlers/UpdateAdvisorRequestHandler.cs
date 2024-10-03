using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Extensions;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Advisor.Commands;
using AdvisorManager.Application.Requests.Advisor.Queries;
using AdvisorManager.Domain;
using AutoMapper;
using MediatR;

namespace AdvisorManager.Application.Handlers
{
    public class UpdateAdvisorRequestHandler(IAdvisorRepository advisorRepository, IMapper mapper)
        : IRequestHandler<UpdateAdvisorRequest, Response<UpdateAdvisorRequest, AdvisorDto>>
    {
        private readonly IAdvisorRepository _advisorRepository = advisorRepository ?? throw new ArgumentNullException(nameof(advisorRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<UpdateAdvisorRequest, AdvisorDto>> Handle(UpdateAdvisorRequest request, CancellationToken cancellationToken)
        {
            var thisAdvisor = await _advisorRepository.GetByIdAsync(request.Details.Id);

            if (thisAdvisor == null)
                return request.Failed<UpdateAdvisorRequest, AdvisorDto>();

            var existingAdvisor = await _advisorRepository.GetBySINAsync(request.Details.SIN);

            if (existingAdvisor != null)
            {
                if (existingAdvisor.Id != thisAdvisor.Id)
                    return request.Faulted<UpdateAdvisorRequest, AdvisorDto>(new Exception($"Advisor with this SIN already exists"));
            }

            request.Details.HealthStatus = thisAdvisor.HealthStatus;
            var updatedAdvisor = _mapper.Map<Advisor>(request.Details);

            var result = await _advisorRepository.UpdateAsync(updatedAdvisor);

            return request.Completed(_mapper.Map<AdvisorDto>(result));
        }
    }
}