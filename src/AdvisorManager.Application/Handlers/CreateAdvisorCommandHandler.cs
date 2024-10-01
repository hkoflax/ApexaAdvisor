using AdvisorManager.Application.Abstractions;
using AdvisorManager.Application.DTOs;
using AdvisorManager.Application.Requests.Commands;
using AdvisorManager.Domain;
using AutoMapper;
using MediatR;

namespace AdvisorManager.Application.Handlers
{
    public class CreateAdvisorCommandHandler(IAdvisorRepository advisorRepository, IMapper mapper) : IRequestHandler<CreateAdvisorCommand, AdvisorDto>
    {
        private readonly IAdvisorRepository _advisorRepository = advisorRepository ?? throw new ArgumentNullException(nameof(advisorRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<AdvisorDto> Handle(CreateAdvisorCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var existingAdvisor = await _advisorRepository.GetBySINAsync(request.Details.SIN);
            if (existingAdvisor != null)
            {
                throw new Exception("Advisor with this SIN already exists");
            }

            var newAdvisor = _mapper.Map<Advisor>(request.Details);

            var result = await _advisorRepository.AddAsync(newAdvisor);

            return _mapper.Map<AdvisorDto>(result);
        }
    }
}