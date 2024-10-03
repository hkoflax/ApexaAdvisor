using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Extensions;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Advisor.Commands;
using AdvisorManager.Domain;
using AutoMapper;
using MediatR;

namespace AdvisorManager.Application.Handlers
{
    public class CreateAdvisorRequestHandler(IAdvisorRepository advisorRepository, IMapper mapper)
        : IRequestHandler<CreateAdvisorRequest, Response<CreateAdvisorRequest, AdvisorDto>>
    {
        private readonly IAdvisorRepository _advisorRepository = advisorRepository ?? throw new ArgumentNullException(nameof(advisorRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        async Task<Response<CreateAdvisorRequest, AdvisorDto>> IRequestHandler<CreateAdvisorRequest, Response<CreateAdvisorRequest, AdvisorDto>>.Handle(CreateAdvisorRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var existingAdvisor = await _advisorRepository.GetBySINAsync(request.Details.SIN);
            if (existingAdvisor != null)
            {
                return request.Faulted<CreateAdvisorRequest, AdvisorDto>(new Exception($"Advisor with this SIN already exists"));
            }

            request.Details.HealthStatus = HealthStatusHelper.GenerateHealthStatus();
            var newAdvisor = _mapper.Map<Advisor>(request.Details);

            var result = await _advisorRepository.AddAsync(newAdvisor);

            return request.Completed(_mapper.Map<AdvisorDto>(result));
        }
    }
}