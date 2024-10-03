using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Extensions;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Advisor.Queries;
using AutoMapper;
using MediatR;

namespace AdvisorManager.Application.Handlers
{
    /// <summary>
    /// Handles the get advisor list request.
    /// </summary>
    /// <param name="advisorRepository">The repository for managing advisor data, a <see cref="IAdvisorRepository"/>.</param>
    /// <param name="mapper">The mapper used to map request details to advisor entities, A <see cref="IMapper"/>.</param>
    public class GetAdvisorListRequestHandler(IAdvisorRepository advisorRepository, IMapper mapper)
        : IRequestHandler<GetAdvisorListRequest, Response<GetAdvisorListRequest, AdvisorDto[]>>

    {
        private readonly IAdvisorRepository _advisorRepository = advisorRepository ?? throw new ArgumentNullException(nameof(advisorRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        /// <inheritdoc />
        public async Task<Response<GetAdvisorListRequest, AdvisorDto[]>> Handle(GetAdvisorListRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(request);

                var list = await _advisorRepository.GetAllAsync();
                return request.Completed(_mapper.Map<AdvisorDto[]>(list));
            }
            catch (Exception ex)
            {
                return request.Failed<GetAdvisorListRequest, AdvisorDto[]>(ex);
            }
        }
    }
}
