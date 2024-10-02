using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Extensions;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Advisor.Queries;
using AutoMapper;
using MediatR;

namespace AdvisorManager.Application.Handlers
{
    public class GetAdvisorListHandler(IAdvisorRepository advisorRepository, IMapper mapper)
        : IRequestHandler<GetAdvisorList, Response<GetAdvisorList, AdvisorDto[]>>

    {
        private readonly IAdvisorRepository _advisorRepository = advisorRepository ?? throw new ArgumentNullException(nameof(advisorRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<GetAdvisorList, AdvisorDto[]>> Handle(GetAdvisorList request, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(request);

                var list = await _advisorRepository.GetAllAsync();
                var result = _mapper.Map<AdvisorDto[]>(list.ToArray());
                if (list != null) return request.Completed(_mapper.Map<AdvisorDto[]>(list));

                return request.Completed<GetAdvisorList, AdvisorDto[]>(null);
            }
            catch (Exception ex)
            {
                return request.Failed<GetAdvisorList, AdvisorDto[]>(ex);
            }
        }
    }
}
