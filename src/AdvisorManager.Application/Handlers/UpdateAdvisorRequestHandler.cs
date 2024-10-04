using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Extensions;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Advisor.Commands;
using AdvisorManager.Domain;
using AutoMapper;
using MediatR;

namespace AdvisorManager.Application.Handlers
{
    /// <summary>
    /// Handles the update of an existing advisor request.
    /// </summary>
    /// <param name="advisorRepository">The repository for managing advisor data, a <see cref="IAdvisorRepository"/>.</param>
    /// <param name="mapper">The mapper used to map request details to advisor entities, A <see cref="IMapper"/>.</param>
    public class UpdateAdvisorRequestHandler(IAdvisorRepository advisorRepository, IMapper mapper)
        : IRequestHandler<UpdateAdvisorRequest, Response<UpdateAdvisorRequest, AdvisorDto>>
    {
        private readonly IAdvisorRepository _advisorRepository = advisorRepository ?? throw new ArgumentNullException(nameof(advisorRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        /// <inheritdoc />
        public async Task<Response<UpdateAdvisorRequest, AdvisorDto>> Handle(UpdateAdvisorRequest request, CancellationToken cancellationToken)
        {
            try
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
            catch (Exception ex)
            {
                return request.Failed<UpdateAdvisorRequest, AdvisorDto>(ex);
            }
        }
    }
}
