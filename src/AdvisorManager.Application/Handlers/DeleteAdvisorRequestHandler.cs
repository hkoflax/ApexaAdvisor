using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Extensions;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Advisor.Commands;
using MediatR;

namespace AdvisorManager.Application.Handlers
{
    public class DeleteAdvisorRequestHandler(IAdvisorRepository advisorRepository)
        : IRequestHandler<DeleteAdvisorRequest, Response<DeleteAdvisorRequest>>
    {
        private readonly IAdvisorRepository _advisorRepository = advisorRepository ?? throw new ArgumentNullException(nameof(advisorRepository));

        public async Task<Response<DeleteAdvisorRequest>> Handle(DeleteAdvisorRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(request);

                var advisor = await _advisorRepository.GetByIdAsync(request.AdvisorId).ConfigureAwait(false);
                if (advisor == null) return request.Failed();

                await _advisorRepository.DeleteAsync(advisor).ConfigureAwait(false);

                return request.Completed();
            }
            catch (Exception ex)
            {
                return request.Failed(ex);
            }
        }
    }
}
