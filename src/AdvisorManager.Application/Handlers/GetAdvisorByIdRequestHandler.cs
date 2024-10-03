using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Extensions;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Advisor.Queries;
using AutoMapper;
using MediatR;

namespace AdvisorManager.Application.Handlers
{
    /// <summary>
    /// Handles get advisor by Id request.
    /// </summary>
    /// <param name="advisorRepository">The repository for managing advisor data, a <see cref="IAdvisorRepository"/>.</param>
    /// <param name="mapper">The mapper used to map request details to advisor entities, A <see cref="IMapper"/>.</param>
    public class GetAdvisorByIdRequestHandler(IAdvisorRepository advisorRepository, IMapper mapper)
        : IRequestHandler<GetAdvisorByIdRequest, Response<GetAdvisorByIdRequest, AdvisorDto>>

    {
        private readonly IAdvisorRepository _advisorRepository = advisorRepository ?? throw new ArgumentNullException(nameof(advisorRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        /// <inheritdoc />
        public async Task<Response<GetAdvisorByIdRequest, AdvisorDto>> Handle(GetAdvisorByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(request);

                var advisor = await _advisorRepository.GetByIdAsync(request.AdvisorId);
                if (advisor != null) return request.Completed(_mapper.Map<AdvisorDto>(advisor));

                return request.Failed<GetAdvisorByIdRequest, AdvisorDto>();
            }
            catch (Exception ex)
            {
                return request.Failed<GetAdvisorByIdRequest, AdvisorDto>(ex);
            }
        }
    }
}